using Libreria.Models.EntityModel;
using Libreria.Repository;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Libreria.Controllers
{
   
    public class MemberApiController : ApiController
    {
        private readonly LibreriaRepository _libreriaRepository;
        public MemberApiController()
        {
            _libreriaRepository = new LibreriaRepository();
        }

        //private readonly DbContext _dbContext;
        //public MemberApiController()
        //{
        //    _dbContext = new LibreriaDataModel();
        //}
        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    if (_libreriaRepository != null)
            //    {
            //        _libreriaRepository.Dispose();
            //    }
            //}
            base.Dispose(disposing);
        }
        //hot DbContext enter apicontroller
        public IHttpActionResult getMember(string MemberName)
        {
           
            var result = _libreriaRepository.GetAll<member>().Where(x => x.memberName == MemberName)
                .FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        public IHttpActionResult getMember(int Memberid)
        {
            var result = _libreriaRepository.GetAll<member>().Where(x => x.memberId == Memberid)
                            .FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        internal class InternalLibreriaDataModel : LibreriaDataModel
        {
            public virtual DbSet<member> MembersforWebApi { get; set; }
        }

    }
}
