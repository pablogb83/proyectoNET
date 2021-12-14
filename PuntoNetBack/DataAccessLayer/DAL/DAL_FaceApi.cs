using DataAccessLayer.Helpers;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using Shared.ModeloDeDominio;

namespace DataAccessLayer.DAL
{
    public class DAL_FaceApi
    {

        private static readonly IFaceClient client = new FaceClient(new ApiKeyServiceClientCredentials(SUBSCRIPTION_KEY)) { Endpoint = ENDPOINT };
        const string SUBSCRIPTION_KEY = "15fcfc123c1442f6b77895fd71ebfe1d";
        const string ENDPOINT = "https://gonzalez.cognitiveservices.azure.com";
        const string url = "https://csdx.blob.core.windows.net/resources/Face/Images/";
        static string personGroupId = "mi-grupo-bruno540";
        // Set in GetOrCreatePersonAsync()
        const string recognitionModel03 = RecognitionModel.Recognition04;
        private static Person searchedForPerson;
        private readonly Person emptyPerson = new Person(Guid.Empty, string.Empty);
        // A trained PersonGroup has at least 1 added face for the specifed person
        // and has successfully completed the training process at least once.
        private bool isPersonGroupTrained;
        public bool IsPersonGroupTrained
        {
            get => isPersonGroupTrained;
            set => setPersonGroupTrained();
        }

        private void setPersonGroupTrained()
        {
            throw new NotImplementedException();
        }

        public DAL_FaceApi(IFaceClient faceClient)
        {
            searchedForPerson = emptyPerson;
        }
        public static async Task Verify()
        {
            Debug.WriteLine("========VERIFY========");
            Debug.WriteLine("\n");

            List<string> targetImageFileNames = new List<string> { "Family1-Dad1.jpg", "Family1-Dad2.jpg" };
            string sourceImageFileName1 = "Family1-Dad3.jpg";
            string sourceImageFileName2 = "Family1-Son1.jpg";


            List<Guid> targetFaceIds = new List<Guid>();
            foreach (var imageFileName in targetImageFileNames)
            {
                // Detect faces from target image url.
                List<DetectedFace> detectedFaces = await DetectFaceRecognize(client, $"{url}{imageFileName} ", recognitionModel03);
                targetFaceIds.Add(detectedFaces[0].FaceId.Value);
                Debug.WriteLine($"{detectedFaces.Count} faces detected from image `{imageFileName}`.");
            }

            // Detect faces from source image file 1.
            List<DetectedFace> detectedFaces1 = await DetectFaceRecognize(client, $"{url}{sourceImageFileName1} ", recognitionModel03);
            Debug.WriteLine($"{detectedFaces1.Count} faces detected from image `{sourceImageFileName1}`.");
            Guid sourceFaceId1 = detectedFaces1[0].FaceId.Value;

            // Detect faces from source image file 2.
            List<DetectedFace> detectedFaces2 = await DetectFaceRecognize(client, $"{url}{sourceImageFileName2} ", recognitionModel03);
            Debug.WriteLine($"{detectedFaces2.Count} faces detected from image `{sourceImageFileName2}`.");
            Guid sourceFaceId2 = detectedFaces2[0].FaceId.Value;

            // Verification example for faces of the same person.
            VerifyResult verifyResult1 = await client.Face.VerifyFaceToFaceAsync(sourceFaceId1, targetFaceIds[0]);
            Debug.WriteLine(
                verifyResult1.IsIdentical
                    ? $"Faces from {sourceImageFileName1} & {targetImageFileNames[0]} are of the same (Positive) person, similarity confidence: {verifyResult1.Confidence}."
                    : $"Faces from {sourceImageFileName1} & {targetImageFileNames[0]} are of different (Negative) persons, similarity confidence: {verifyResult1.Confidence}.");

            // Verification example for faces of different persons.
            VerifyResult verifyResult2 = await client.Face.VerifyFaceToFaceAsync(sourceFaceId2, targetFaceIds[0]);
            Debug.WriteLine(
                verifyResult2.IsIdentical
                    ? $"Faces from {sourceImageFileName2} & {targetImageFileNames[0]} are of the same (Negative) person, similarity confidence: {verifyResult2.Confidence}."
                    : $"Faces from {sourceImageFileName2} & {targetImageFileNames[0]} are of different (Positive) persons, similarity confidence: {verifyResult2.Confidence}.");

            Debug.WriteLine("\n");
        }
        /*
		 * END - VERIFY 
		 */

        /*
		 * IDENTIFY FACES
		 * To identify faces, you need to create and define a person group.
		 * The Identify operation takes one or several face IDs from DetectedFace or PersistedFace and a PersonGroup and returns 
		 * a list of Person objects that each face might belong to. Returned Person objects are wrapped as Candidate objects, 
		 * which have a prediction confidence value.
		 */
        // <snippet_persongroup_files>
        public static async Task IdentifyInPersonGroup()
        {
            Debug.WriteLine("========IDENTIFY FACES========");
            Debug.WriteLine("\n");

            // Create a dictionary for all your images, grouping similar ones under the same key.
            Dictionary<string, string[]> personDictionary =
                new Dictionary<string, string[]>
                    { { "Family1-Dad", new[] { "Family1-Dad1.jpg", "Family1-Dad2.jpg" } },
                      { "Family1-Mom", new[] { "Family1-Mom1.jpg", "Family1-Mom2.jpg" } },
                      { "Family1-Son", new[] { "Family1-Son1.jpg", "Family1-Son2.jpg" } },
                      { "Family1-Daughter", new[] { "Family1-Daughter1.jpg", "Family1-Daughter2.jpg" } },
                      { "Family2-Lady", new[] { "Family2-Lady1.jpg", "Family2-Lady2.jpg" } },
                      { "Family2-Man", new[] { "Family2-Man1.jpg", "Family2-Man2.jpg" } }
                    };
            // A group photo that includes some of the persons you seek to identify from your dictionary.
            string sourceImageFileName = "identification1.jpg";
            // </snippet_persongroup_files>

            // <snippet_persongroup_create>
            // Create a person group. 
            Debug.WriteLine($"Create a person group ({personGroupId}).");
            await client.PersonGroup.CreateAsync(personGroupId, personGroupId, recognitionModel: recognitionModel03);
            // The similar faces will be grouped into a single person group person.
            foreach (var groupedFace in personDictionary.Keys)
            {
                // Limit TPS
                await Task.Delay(250);
                Person person = await client.PersonGroupPerson.CreateAsync(personGroupId: personGroupId, name: groupedFace);
                Debug.WriteLine($"Create a person group person '{groupedFace}'.");

                // Add face to the person group person.
                foreach (var similarImage in personDictionary[groupedFace])
                {
                    Debug.WriteLine($"Add face to the person group person({groupedFace}) from image `{similarImage}`");
                    PersistedFace face = await client.PersonGroupPerson.AddFaceFromUrlAsync(personGroupId, person.PersonId,
                        $"{url}{similarImage}", similarImage);
                }
            }
            // </snippet_persongroup_create>

            // <snippet_persongroup_train>
            // Start to train the person group.
            Debug.WriteLine("\n");
            Debug.WriteLine($"Train person group {personGroupId}.");
            await client.PersonGroup.TrainAsync(personGroupId);

            // Wait until the training is completed.
            while (true)
            {
                await Task.Delay(1000);
                var trainingStatus = await client.PersonGroup.GetTrainingStatusAsync(personGroupId);
                Debug.WriteLine($"Training status: {trainingStatus.Status}.");
                if (trainingStatus.Status == TrainingStatusType.Succeeded) { break; }
            }
            Debug.WriteLine("\n");

            // </snippet_persongroup_train>
            // <snippet_identify_sources>
            List<Guid> sourceFaceIds = new List<Guid>();
            // Detect faces from source image url.
            List<DetectedFace> detectedFaces = await DetectFaceRecognize(client, $"{url}{sourceImageFileName}", recognitionModel03);

            // Add detected faceId to sourceFaceIds.
            foreach (var detectedFace in detectedFaces) { sourceFaceIds.Add(detectedFace.FaceId.Value); }
            // </snippet_identify_sources>

            // <snippet_identify>
            // Identify the faces in a person group. 
            var identifyResults = await client.Face.IdentifyAsync(sourceFaceIds, personGroupId);

            foreach (var identifyResult in identifyResults)
            {
                Person person = await client.PersonGroupPerson.GetAsync(personGroupId, identifyResult.Candidates[0].PersonId);
                Debug.WriteLine($"Person '{person.Name}' is identified for face in: {sourceImageFileName} - {identifyResult.FaceId}," +
                    $" confidence: {identifyResult.Candidates[0].Confidence}.");
            }
            Debug.WriteLine("\n");
        }
        private static async Task<List<DetectedFace>> DetectFaceRecognize(IFaceClient faceClient, string url, string recognition_model)
        {
            // Detect faces from image URL. Since only recognizing, use the recognition model 1.
            // We use detection model 3 because we are not retrieving attributes.
            IList<DetectedFace> detectedFaces = await faceClient.Face.DetectWithUrlAsync(url, recognitionModel: recognition_model, detectionModel: DetectionModel.Detection03);
            Debug.WriteLine($"{detectedFaces.Count} face(s) detected from image `{Path.GetFileName(url)}`");
            return detectedFaces.ToList();
        }

        public static async Task<Person> ReconocimientoFacial(Stream imagen,string PersonGroupId)
        {
            //IList<Person> people = await client.PersonGroupPerson.ListAsync(personGroupId);
            PersonGroupId = PersonGroupId.ToLower();
            List<DetectedFace> detectedFaces1 = await DetectFaceRecognizeStream(client, imagen, recognitionModel03);
            if (detectedFaces1.Any())
            {
                if(detectedFaces1.Count > 1)
                {
                    throw new AppException("Por favor ingrese la foto de UN SOLO individuo");
                }
                Guid sourceFaceId1 = detectedFaces1[0].FaceId.Value;
                List<Guid> cara = new List<Guid>();
                cara.Add(sourceFaceId1);
                var identifyResults = await client.Face.IdentifyAsync(cara, PersonGroupId);
                if(identifyResults.Any() && identifyResults[0].Candidates.Any())
                {
                    var faceid = identifyResults[0].Candidates[0].PersonId;
                    var person = await client.PersonGroupPerson.GetAsync(PersonGroupId, faceid);
                    return person;
                }
                else
                {
                    throw new AppException("No se encontro la persona de la foto");
                }
            }
            else
            {
                throw new AppException("No se encontraron caras en la imagen ingresada");
            }
        }

        public static async Task CreatePersonGroup(Institucion personGroupId)
        {
            await client.PersonGroup.CreateAsync(personGroupId.Id, personGroupId.Name, recognitionModel: recognitionModel03);
            await client.PersonGroup.TrainAsync(personGroupId.Id);

        }
        public static async Task DeletePersonGroup(Institucion personGroupId)
        {
            await client.PersonGroup.DeleteAsync(personGroupId.Id);
        }

        private static async Task<List<DetectedFace>> DetectFaceRecognizeStream(IFaceClient faceClient, Stream imagen, string recognition_model)
        {
            IList<DetectedFace> detectedFaces = await client.Face.DetectWithStreamAsync(imagen, true, false, new FaceAttributeType[] { FaceAttributeType.Age, FaceAttributeType.Gender }, recognitionModel03);
            Debug.WriteLine($"{detectedFaces.Count} face(s) detected from image `{Path.GetFileName(url)}`");
            return detectedFaces.ToList();
        }

        public static async Task<bool> VerificarCaras(Guid idCara1, Guid idPersona)
        {
            VerifyResult result = await client.Face.VerifyFaceToPersonAsync(idCara1, idPersona,personGroupId);
            return result.IsIdentical;
        }
        
        public static async Task<bool> AgregarPersona(string email, Stream stream, string PersonGroupId)
        {
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            ms.Position = 0;
            List<DetectedFace> detectedFaces1 = await DetectFaceRecognizeStream(client, ms, recognitionModel03);
            if (!detectedFaces1.Any())
            {
                throw new AppException("La imagen ingresada debe contener al menos una cara");
            }
            if (detectedFaces1.Count > 1)
            {
                throw new AppException("Por favor ingrese la foto de UN SOLO individuo");
            }
            stream.Position = 0;
            Guid sourceFaceId1 = detectedFaces1[0].FaceId.Value;
            List<Guid> cara = new List<Guid>();
            cara.Add(sourceFaceId1);
            var status = await client.PersonGroup.GetTrainingStatusAsync(PersonGroupId);
            if(status.Status == TrainingStatusType.Failed || status.Status==TrainingStatusType.Nonstarted)
            {
                await client.PersonGroup.TrainAsync(PersonGroupId);
            }
            var identifyResults = await client.Face.IdentifyAsync(cara, PersonGroupId);
            if (identifyResults.Any() && identifyResults[0].Candidates.Any())
            {
                throw new AppException("La persona presente en la imagen ya ha sido registrada en el sistema");
            }
            Person p1 = await client.PersonGroupPerson.CreateAsync(PersonGroupId, email);
            try
            {
                PersistedFace persistedFace = await client.PersonGroupPerson.AddFaceFromStreamAsync(PersonGroupId, p1.PersonId, stream);
            }
            catch(Exception e)
            {
                Debug.WriteLine("LLA");
            }
            await client.PersonGroup.TrainAsync(PersonGroupId);
            return true;
        }

        public static async Task<bool> BorrarPersona(string documento, string PersonGroupId)
        {
            PersonGroupId = PersonGroupId.ToLower();
            var personas = await client.PersonGroupPerson.ListAsync(PersonGroupId);
            var person = personas.First(x => x.Name == documento);
            if (person != null)
            {
                await client.PersonGroupPerson.DeleteAsync(PersonGroupId, person.PersonId);
                await client.PersonGroup.TrainAsync(PersonGroupId);
                return true;
            }
            return false;
        }

        public static async Task<bool> ActualizarPersona(string documentoViejo, string documentoNuevo, string PersonGroupId, Stream stream)
        {
            PersonGroupId = PersonGroupId.ToLower();
            var personas = await client.PersonGroupPerson.ListAsync(PersonGroupId);
            var person = personas.First(x => x.Name == documentoViejo);
            if (person != null && await validateFace(stream, PersonGroupId))
            {
                stream.Position = 0;
                var carasPersona = person.PersistedFaceIds;
                if (carasPersona.Any())
                {
                    await client.PersonGroupPerson.DeleteFaceAsync(PersonGroupId, person.PersonId, carasPersona.FirstOrDefault());
                }
                await client.PersonGroupPerson.AddFaceFromStreamAsync(PersonGroupId, person.PersonId, stream);
                await client.PersonGroup.TrainAsync(PersonGroupId);
                return true;
            }
            return false;
        }

        public static async Task ActualizarInstitucion(string nombreViejo, string nombreNuevo)
        {
            nombreViejo = nombreViejo.ToLower();
            nombreNuevo = nombreNuevo.ToLower();
            await client.PersonGroup.UpdateAsync(nombreViejo, nombreNuevo);
        }

        public static async Task<bool> validateFace(Stream imagen, string personGroupId)
        {
            personGroupId = personGroupId.ToLower();
            MemoryStream ms = new MemoryStream();
            imagen.CopyTo(ms);
            ms.Position = 0;
            List<DetectedFace> detectedFaces1 = await DetectFaceRecognizeStream(client, ms, recognitionModel03);
            if (!detectedFaces1.Any())
            {
                throw new AppException("La imagen ingresada debe contener al menos una cara");
            }
            if (detectedFaces1.Count > 1)
            {
                throw new AppException("Por favor ingrese la foto de UN SOLO individuo");
            }
            Guid sourceFaceId1 = detectedFaces1[0].FaceId.Value;
            List<Guid> cara = new List<Guid>();
            cara.Add(sourceFaceId1);
            var identifyResults = await client.Face.IdentifyAsync(cara, personGroupId);
            return true;
        }

        public static async Task actualizarDocumentoAzure(string documentoViejo, string documentoNuevo, string personGroupId)
        {
            personGroupId = personGroupId.ToLower();
            var personas = await client.PersonGroupPerson.ListAsync(personGroupId);
            var person = personas.First(x => x.Name == documentoViejo);
            if (person != null)
            {
                await client.PersonGroupPerson.UpdateAsync(personGroupId, person.PersonId, documentoNuevo);
                await client.PersonGroup.TrainAsync(personGroupId);
            }
        }
    }
}
