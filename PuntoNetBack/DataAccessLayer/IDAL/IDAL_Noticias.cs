using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.IDAL
{
    public interface IDAL_Noticias
    {
        IEnumerable<Noticias> GetAllNoticias();
        IEnumerable<Noticias> GetAllNoticiasPublicas();
        IEnumerable<Noticias> GetNoticiasByInstitucion(string idinstitucion);
        IEnumerable<Noticias> GetUltimasNoticias();
        Noticias GetNoticiaById(int Id);
        void CreateNoticia(Noticias not);
        void UpdateNoticia(Noticias not);
        void DeleteNoticia(Noticias not);
        bool SaveChanges();
    }
}
