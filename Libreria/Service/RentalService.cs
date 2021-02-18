using Libreria.Models.EntityModel;
using Libreria.Repository;
using System;
using System.Collections.Generic;
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
        //public List<Exhibition> GetAll()
        //{
        //    return _DbRepository.GetAll<Exhibition>().Select((x) => new Exhibition()
        //    {
        //        ExCustomerId = x.ExCustomerId
        //    }).ToList();
        //}

    }
    
}