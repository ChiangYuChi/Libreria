﻿using Libreria.Models.EntityModel;
using Libreria.Repository;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Libreria.Service
{
    public class RentalService
    {
        private readonly LibreriaRepository _DbRepository;

        public RentalService()
        {
            _DbRepository = new LibreriaRepository();
        }
        
        public OperationResult ConfirmBooling(RentalConfirmViewModel model)
        {
            var result = new OperationResult();

            LibreriaDataModel context = new LibreriaDataModel();

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    string fileName = null;
                    if (model.ExPhoto.ContentLength > 0)
                    {
                        fileName = Path.GetFileName(model.ExPhoto.FileName);
                        //var path = Path.Combine(Server.MapPath("~/FileUploads"), fileName);
                        //file.SaveAs(path);
                    }

                    ExhibitionCustomer exhibitionCustomer = new ExhibitionCustomer()
                    {
                        ExCustomerName = model.ExCustomerName,
                        ExCustomerPhone = model.ExCustomerPhone,
                        ExCustomerEmail = model.ExCustomerEmail
                    };
                    _DbRepository.Create(exhibitionCustomer);
                    ExhibitionOrder exhibitionOrder = new ExhibitionOrder()
                    {
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        Price = ((model.EndDate - model.StartDate).Days + 1) * 2500,
                        ExCustomerId = exhibitionCustomer.ExCustomerId
                    };
                    _DbRepository.Create(exhibitionOrder);
                    Exhibition entity = new Exhibition()
                    {
                        ExhibitionStartTime = model.ExhibitionStartTime,
                        ExhibitionEndTime = model.ExhibitionEndTime,
                        ExhibitionIntro = model.ExhibitionIntro,
                        MasterUnit = model.MasterUnit,
                        ExhibitionPrice = Convert.ToDecimal(model.ExhibitionPrice),
                        EditModifyDate = DateTime.Now,
                        ExCustomerId = exhibitionCustomer.ExCustomerId,
                        ExPhoto = fileName,
                        ExName = model.ExName
                    };
                    _DbRepository.Create(entity);
                    result.IsSuccessful = true;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    result.IsSuccessful = false;
                    result.exception = ex;
                    transaction.Rollback();
                }
            }

            return result;
        }
        public List<RentalConfirmViewModel> GetRentalDate()
        {
            return _DbRepository.GetAll<ExhibitionOrder>().Select(x => new RentalConfirmViewModel()
            {
                StartDate = x.StartDate,
                EndDate = x.EndDate
            }).ToList();
        }

    }
    
}