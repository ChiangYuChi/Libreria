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
            var result = (from e in _dBRepository.GetAll<Exhibition>()
                          join o in _dBRepository.GetAll<ExhibitionOrder>()
                          on e.ExCustomerId equals o.ExCustomerId
                          select new ExhibitionVIewModel()
                          {
                              ExHibitionIntro = e.ExhibitionIntro,
                              ExhibitionID = e.ExhibitionID,
                              ExName = e.ExName,
                              ExhibitionStartTime = e.ExhibitionStartTime,
                              ExhibitionEndTime = e.ExhibitionEndTime,
                              EditModifyDate = e.EditModifyDate,
                              ExhibitionPrice = e.ExhibitionPrice,
                              ExPhoto = e.ExPhoto,
                              ExCustomerId = e.ExCustomerId,
                              MasterUnit = e.MasterUnit,
                              CustomerVerify = o.customerVerify,
                              IsCanceled = o.isCanceled
                          }).ToList();

            return result;
        }
        /// <summary>
        /// 取得 依照目前日期所判定正在舉辦之展覽，以展覽結束時間來做判斷，若目前沒有則會視即將舉辦展覽，若也沒有即將舉辦的展覽會是過往展覽第一筆。
        /// </summary>
        /// <returns>
        /// 回傳單筆展覽資料
        /// </returns>
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
        /// <summary>
        /// 取得過往展覽之展覽資料，依新至舊排列
        /// </summary>
        /// <returns>
        /// 回傳為集合
        /// </returns>
        public List<ExhibitionVIewModel> OverdueExhibitioning()
        {
            var GetEx = GetAll();
            var result = GetEx.Where(x => x.ExhibitionEndTime < Nowdate)
                        .OrderByDescending(x => x.ExhibitionEndTime)
                        .ToList();


            return result;
        }
        /// <summary>
        /// 取得即將舉辦之展覽資料
        /// </summary>
        /// <returns>
        /// 回傳為集合
        /// </returns>
        public List<ExhibitionVIewModel> NotYetExhibitioning()
        {
            var GetEx = GetAll();
            
            var result = GetEx.Where(x => x.ExhibitionStartTime > Nowdate && x.CustomerVerify == true && x.IsCanceled == false)
                        .OrderBy(x => x.ExhibitionStartTime)
                        .ToList();

            return result;
        }
    }
}