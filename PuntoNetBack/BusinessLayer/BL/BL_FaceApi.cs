using BusinessLayer.IBL;
using DataAccessLayer.IDAL;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BL
{
    public class BL_FaceApi : IBL_FaceApi
    {

        private readonly IDAL_FaceApi _dal;

        public BL_FaceApi(IDAL_FaceApi dal)
        {
            _dal = dal;
        }


        public async Task actualizarDocumentoAzure(string documentoViejo, string documentoNuevo, string personGroupId)
        {
            await _dal.actualizarDocumentoAzure(documentoViejo, documentoNuevo, personGroupId);
        }

        public async Task ActualizarInstitucion(string nombreViejo, string nombreNuevo)
        {
            await _dal.ActualizarInstitucion(nombreViejo, nombreNuevo);
        }

        public async Task<bool> ActualizarPersona(string documentoViejo, string documentoNuevo, string PersonGroupId, Stream stream)
        {
            return await _dal.ActualizarPersona(documentoViejo,documentoNuevo,PersonGroupId,stream);

        }

        public async Task<bool> AgregarPersona(string email, Stream stream, string PersonGroupId)
        {
            return await _dal.AgregarPersona(email, stream, PersonGroupId);

        }

        public async Task<bool> BorrarPersona(string documento, string PersonGroupId)
        {
            return await _dal.BorrarPersona(documento, PersonGroupId);
        }

        public async Task CreatePersonGroup(Institucion personGroupId)
        {
            await _dal.CreatePersonGroup(personGroupId);
        }

        public async Task DeletePersonGroup(Institucion personGroupId)
        {
            await _dal.DeletePersonGroup(personGroupId);
        }

        public async Task<Person> ReconocimientoFacial(Stream imagen, string PersonGroupId)
        {
            return await _dal.ReconocimientoFacial(imagen, PersonGroupId);
        }

        public async Task<bool> VerificarCaras(Guid idCara1, Guid idPersona)
        {
            return await _dal.VerificarCaras(idCara1, idPersona);
        }
    }
}
