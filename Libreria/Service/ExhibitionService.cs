using Libreria.Repository;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Libreria.Models.EntityModel;
    

namespace Libreria.Service
{
    public class ExhibitionService
    {
        private readonly LibreriaRepository _dBRepository;
        DateTime Nowdate = DateTime.Now;
        
        public ExhibitionService()
        {
            _dBRepository = new LibreriaRepository();
            
        }
       public List<ExhibitionVIewModel> GetAll()
        {
            return _dBRepository.GetAll<Exhibition>().Select(x => new ExhibitionVIewModel()
            {
                ExHibitionIntro=x.ExhibitionIntro,
                ExhibitionID=x.ExhibitionID,
                ExName=x.ExName,
                ExhibitionStartTime=x.ExhibitionStartTime,
                ExhibitionEndTime=x.ExhibitionEndTime,
                EditModifyDate=x.EditModifyDate,
                ExhibitionPrice=x.ExhibitionPrice,
                ExPhoto=x.ExPhoto,
                ExCustomerId=x.ExCustomerId,
                MasterUnit=x.MasterUnit
            }).ToList();
        }
        public ExhibitionVIewModel GetExhibitioning()
        {
            var GetEx = GetAll();
            if (GetEx.FirstOrDefault(x => x.ExhibitionEndTime >= Nowdate)!=null)
            {
                return GetEx.FirstOrDefault(x => x.ExhibitionEndTime >= Nowdate);
            }
            else
            {
                return GetEx.FirstOrDefault(x => x.ExhibitionEndTime <= Nowdate);
            }
        }
        public List<ExhibitionVIewModel> OverdueExhibitioning()
        {
            var GetEx = GetAll();
            var result = GetEx.Where(x => x.ExhibitionEndTime < Nowdate)
                        .OrderByDescending(x => x.ExhibitionEndTime)
                        .ToList();


            return result;
        }

        public List<ExhibitionVIewModel> NotYetExhibitioning()
        {
            var GetEx = GetAll();
            
            var result = GetEx.Where(x => x.ExhibitionStartTime > Nowdate)
                        .OrderBy(x => x.ExhibitionStartTime)
                        .ToList();

            return result;
        }
    }
}