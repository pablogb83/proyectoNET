using BusinessLayer.IBL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BL
{
    public class BL_Noticias : IBL_Noticias
    {
        private readonly DataAccessLayer.IDAL.IDAL_Noticias _dal;

        public BL_Noticias(DataAccessLayer.IDAL.IDAL_Noticias dal)
        {
            _dal = dal;
        }

        public void CreateNoticia(Noticias not)
        {
            _dal.CreateNoticia(not);
        }

        public void DeleteNoticia(Noticias not)
        {
            _dal.DeleteNoticia(not);
        }

        public IEnumerable<Noticias> GetAllNoticias()
        {
            return _dal.GetAllNoticias();
        }

        public Noticias GetNoticiaById(int Id)
        {
            return _dal.GetNoticiaById(Id);
        }

        public bool SaveChanges()
        {
            return _dal.SaveChanges();
        }

        public void UpdateNoticia(Noticias not)
        {
            _dal.UpdateNoticia(not);
        }
    }
}
