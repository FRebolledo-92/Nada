using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models.WS;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class AccessController : ApiController
    {
        [HttpGet]
        public Reply HolaMundo()
        {
            Reply oR = new Reply();
            oR.result = 1;
            oR.message = "hola";
            return oR;
        }

        [HttpPost]
        public Reply Login([FromBody]AccessViewModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            try
            {
                using (PruebaEntities db = new PruebaEntities())
                {
                    var lst = db.Usuario.Where(d => d.username == model.username && d.password == model.password);

                    if(lst.Count() > 0)
                    {

                        oR.result = 1;
                        oR.data = Guid.NewGuid().ToString();

                        Usuario oUser = lst.First();
                        oUser.token = (string)oR.data;

                        db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();


                    }else
                    {
                        oR.message = "Credenciales Erroneas";
                    }
                }


            }
            catch (Exception ex)
            {
                oR.result = 0;
                oR.message = " Ocurrió un error!";
            }

            return oR;
        }

    }
}
