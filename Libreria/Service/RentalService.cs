using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Imgur.API.Models;
using Libreria.Models.EntityModel;
using Libreria.Repository;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
        
        public async  Task<OperationResult>  ConfirmBooling(RentalConfirmViewModel model)
        {
            var result = new OperationResult();

            LibreriaDataModel context = new LibreriaDataModel();

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                   string imageUrl = null;
                    if (model.ExPhoto.ContentLength > 0)
                    {
                        model.ExPhoto.InputStream.Position = 0;
                        await model.ExPhoto.InputStream.FlushAsync();
                        var apiClient = new ApiClient("8b8585e4ec973fc");
                        var httpClient = new HttpClient();
                        var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
                        var imageUpload = await imageEndpoint.UploadImageAsync(model.ExPhoto.InputStream);
                        imageUrl = imageUpload.Link;
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
                        ExPhoto = imageUrl,
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

        public IEnumerable<string> GetPickDateRange()
        {
            var exhibitionOrders = _DbRepository.GetAll<ExhibitionOrder>().ToList();
            var convertDateRange = exhibitionOrders.Select(x =>
             {
                 var days = (x.EndDate - x.StartDate).Days + 1;
                 return Enumerable.Range(0, days).Select(index => x.StartDate.AddDays(index).ToString("yyyy/MM/dd"));
             });
            var listDateRange = convertDateRange.SelectMany(s => s);

            return listDateRange.Distinct();
        }
    }
    
}