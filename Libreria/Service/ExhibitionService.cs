using Libreria.Repository;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Libreria.Models.EntityModel;
using Libreria.ViewModels;
    

namespace Libreria.Service
{
    public class ExhibitionService
    {
        private readonly LibreriaRepository _dBRepository;
        public ExhibitionService()
        {
            _dBRepository = new LibreriaRepository();
        }
       public List<ExhibitionVIewModel> GetAll()
        {
            return _dBRepository.GetAll<Exhibition>().Select(x => new ExhibitionVIewModel()
            {
                ExhibitionID=x.ExhibitionID,
                ExName=x.ExName,
                ExhibitionStartTime=x.ExhibitionStartTime,
                ExhibitionEndTime=x.ExhibitionEndTime,
                EditModifyDate=x.EditModifyDate,
                ExhibitionPrice=x.ExhibitionPrice,
                ExPhoto=x.ExPhoto,
                ExCustomerId=x.ExCustomerId
            }).ToList();
        }
        //public List<ExhibitionVIewModel> GetExhibitioning()
        //{
        //    DateTime Nowdate = DateTime.Now;
        //    _dBRepository.GetAll<Exhibition>().Select(x => new ExhibitionVIewModel()
        //    {
        //        ExhibitionID = x.ExhibitionID,
        //        ExName = x.ExName,
        //        ExhibitionStartTime = x.ExhibitionStartTime,
        //        ExhibitionEndTime = x.ExhibitionEndTime,
        //        EditModifyDate = x.EditModifyDate,
        //        ExhibitionPrice = x.ExhibitionPrice,
        //        ExPhoto = x.ExPhoto,
        //        ExCustomerId = x.ExCustomerId
        //    });
        //    if(DateTime.Compare(Nowdate, ExhibitionStartTime))
        //}
    }
}