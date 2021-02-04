using Libreria.Models.EntityModel;
using Libreria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.Service
{
    public class SalesManService
    {
        private readonly DBRepository _dBRepository;

        public SalesManService()
        {
            _dBRepository = new DBRepository();
        }

        public List<SalesMan> GetAll()
        {
            return _dBRepository.GetAll<SalesMan>().ToList();
        }
    }
}