using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class BaseController : ApiController
    {
        public string error = "";
        public bool Verify(string token)
        {
            using (PruebaEntities db = new PruebaEntities())
            {
                if (db.Usuario.Where(d => d.token == token).Count() > 0)
                    return true;
            }
            return false;
        }
    }
}
