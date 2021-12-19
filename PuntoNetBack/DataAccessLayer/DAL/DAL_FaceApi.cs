using DataAccessLayer.Helpers;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Shared.ModeloDeDominio;
using DataAccessLayer.IDAL;

namespace DataAccessLayer.DAL
{
    public class DAL_FaceApi : IDAL_FaceApi
    {

        private static readonly IFaceClient client = new FaceClient(new ApiKeyServiceClientCredentials(SUBSCRIPTION_KEY)) { Endpoint = ENDPOINT };
        const string SUBSCRIPTION_KEY = "15fcfc123c1442f6b77895fd71ebfe1d";
        const string ENDPOINT = "https://gonzalez.cognitiveservices.azure.com";
        const string url = "https://csdx.blob.core.windows.net/resources/Face/Images/";
        static string personGroupId = "mi-grupo-bruno540";
        const string recognitionModel03 = RecognitionModel.Recognition04;
     

        public DAL_FaceApi()
        {
        }

        public async Task<Person> ReconocimientoFacial(Stream imagen, string PersonGroupId)
        {
            IList<Person> people = await client.PersonGroupPerson.ListAsync(PersonGroupId);
            if (people.Count <= 0)
            {
                throw new AppException("No hay personas registradas facialmente");
            }
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

        public async Task CreatePersonGroup(Institucion personGroupId)
        {
            await client.PersonGroup.CreateAsync(personGroupId.Id, personGroupId.Name, recognitionModel: recognitionModel03);
            await client.PersonGroup.TrainAsync(personGroupId.Id);

        }
        public async Task DeletePersonGroup(Institucion personGroupId)
        {
            await client.PersonGroup.DeleteAsync(personGroupId.Id);
        }

        private async Task<List<DetectedFace>> DetectFaceRecognizeStream(IFaceClient faceClient, Stream imagen, string recognition_model)
        {
            IList<DetectedFace> detectedFaces = await client.Face.DetectWithStreamAsync(imagen, true, false, new FaceAttributeType[] { FaceAttributeType.Age, FaceAttributeType.Gender }, recognitionModel03);
            Debug.WriteLine($"{detectedFaces.Count} face(s) detected from image `{Path.GetFileName(url)}`");
            return detectedFaces.ToList();
        }

        public async Task<bool> VerificarCaras(Guid idCara1, Guid idPersona)
        {
            VerifyResult result = await client.Face.VerifyFaceToPersonAsync(idCara1, idPersona,personGroupId);
            return result.IsIdentical;
        }
        
        public async Task<bool> AgregarPersona(string email, Stream stream, string PersonGroupId)
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
            var cantPersonas = await client.PersonGroupPerson.ListAsync(PersonGroupId);
            if (cantPersonas.Count() > 0)
            {
                Guid sourceFaceId1 = detectedFaces1[0].FaceId.Value;
                List<Guid> cara = new List<Guid>();
                cara.Add(sourceFaceId1);

                var identifyResults = await client.Face.IdentifyAsync(cara, PersonGroupId);
                if (identifyResults.Any() && identifyResults[0].Candidates.Any())
                {
                    throw new AppException("La persona presente en la imagen ya ha sido registrada en el sistema");
                }
            }
            Person p1 = await client.PersonGroupPerson.CreateAsync(PersonGroupId, email);
            if (stream != null)
            {
                PersistedFace persistedFace = await client.PersonGroupPerson.AddFaceFromStreamAsync(PersonGroupId, p1.PersonId, stream);
            }
            await client.PersonGroup.TrainAsync(PersonGroupId);
            return true;
        }

        public async Task<bool> BorrarPersona(string documento, string PersonGroupId)
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

        public async Task<bool> ActualizarPersona(string documentoViejo, string documentoNuevo, string PersonGroupId, Stream stream)
        {
            PersonGroupId = PersonGroupId.ToLower();
            var personas = await client.PersonGroupPerson.ListAsync(PersonGroupId);
            var person = personas.FirstOrDefault(x => x.Name == documentoViejo);
            if (person == null)
            {
                Person p1 = await client.PersonGroupPerson.CreateAsync(PersonGroupId, documentoNuevo);
                await client.PersonGroup.TrainAsync(PersonGroupId);
                personas = await client.PersonGroupPerson.ListAsync(PersonGroupId);
                person = personas.FirstOrDefault(x => x.Name == documentoNuevo);
            }
            if (person != null && await validateFace(stream, PersonGroupId,personas))
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

        public async Task ActualizarInstitucion(string nombreViejo, string nombreNuevo)
        {
            nombreViejo = nombreViejo.ToLower();
            nombreNuevo = nombreNuevo.ToLower();
            await client.PersonGroup.UpdateAsync(nombreViejo, nombreNuevo);
        }

        public async Task<bool> validateFace(Stream imagen, string personGroupId,IList<Person> personas)
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
            var personasConCaras = personas.FirstOrDefault(p => p.PersistedFaceIds.Count > 0);
            try
            {
                if (personasConCaras!=null)
                {
                    Guid sourceFaceId1 = detectedFaces1[0].FaceId.Value;
                    List<Guid> cara = new List<Guid>();
                    cara.Add(sourceFaceId1);
                    var identifyResults = await client.Face.IdentifyAsync(cara, personGroupId);
                    if (identifyResults.Any() && identifyResults[0].Candidates.Any())
                    {
                        throw new AppException("La persona presente en la imagen ya ha sido registrada en el sistema");
                    }
                }
            }
            catch(Exception e)
            {

            }
            return true;
        }

        public async Task actualizarDocumentoAzure(string documentoViejo, string documentoNuevo, string personGroupId)
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
