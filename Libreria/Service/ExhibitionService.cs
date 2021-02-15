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
        public readonly LibreriaRepository _dBRepository;
        public ExhibitionService()
        {
            _dBRepository = new LibreriaRepository();
        }
        //List<ExhibitionVIewModel> GetAll()
        //{
        //    //return _dBRepository.GetAll<Exhibition>().Select(x => new ExhibitionVIewModel()
        //    //{

        //    //}).ToList();
        //}
    }
}