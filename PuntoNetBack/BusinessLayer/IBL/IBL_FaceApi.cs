using DataAccessLayer.Helpers;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBL
{
    public interface IBL_FaceApi
    {
        Task<Person> ReconocimientoFacial(Stream imagen, string PersonGroupId);
        Task CreatePersonGroup(Institucion personGroupId);
        Task DeletePersonGroup(Institucion personGroupId);
        Task<bool> VerificarCaras(Guid idCara1, Guid idPersona);
        Task<bool> AgregarPersona(string email, Stream stream, string PersonGroupId);
        Task<bool> BorrarPersona(string documento, string PersonGroupId);
        Task<bool> ActualizarPersona(string documentoViejo, string documentoNuevo, string PersonGroupId, Stream stream);
        Task ActualizarInstitucion(string nombreViejo, string nombreNuevo);
        Task actualizarDocumentoAzure(string documentoViejo, string documentoNuevo, string personGroupId);
    }
}
