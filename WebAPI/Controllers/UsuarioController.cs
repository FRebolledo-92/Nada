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
    public class UsuarioController : BaseController
    {

        [HttpPost]
        public Reply Get([FromBody]SecurityViewModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;

            if (!Verify(model.token))
            {
                oR.message = " No autorizado";
                return oR;
            }

            try
            {
                using (PruebaEntities db = new PruebaEntities())
                {
                    List<ListUsuarioViewModel> lst = (from d in db.Usuario
                                                     select new ListUsuarioViewModel
                                                     {
                                                         nombre = d.nombre,
                                                         username = d.username

                                                     }).ToList();

                    oR.data = lst;
                    oR.result = 1;


                }

            }
            catch (Exception ex)
            {
                oR.message = "ocurrio el siguiente error" + ex.ToString();
                throw;
            }


            return oR;
        }

        [HttpPost]
        public Reply Agregar([FromBody]UsuarioViewModel modelo)
        {
            Reply oR = new Reply();
            oR.result = 0;


            //validaciones
            if (!Validate(modelo))
            {
                oR.message = error;
                return oR;
            }

            try
            {
                using (PruebaEntities db = new PruebaEntities())
                {

                    Usuario oUser = new Usuario();
                    oUser.nombre = modelo.nombre;
                    oUser.username = modelo.username;
                    oUser.password = modelo.password;
                   

                    db.Usuario.Add(oUser);
                    db.SaveChanges();

                    List<ListUsuarioViewModel> lst = (from d in db.Usuario
                                                     select new ListUsuarioViewModel
                                                     {
                                                         nombre = d.nombre,
                                                         username = d.username

                                                     }).ToList();

                    oR.result = 1;
                    oR.data = lst;

                }
            }
            catch (Exception ex)
            {
                oR.message = "Ocurrio el siguiente error:" + ex.ToString();
                throw;
            }

            return oR;

        }


        #region Helpers

        private bool Validate(UsuarioViewModel model)
        {
            if (model.nombre == "")
            {
                error = "el nombre es obligatorio";
                return false;
            }

            if (model.username == "")
            {
                error = "el nombre de usuario es obligatorio";
                return false;
            }

            if (model.password == "")
            {
                error = "La contraseña es obligatoria";
                return false;
            }

            return true;

        }

        #endregion
    }
}
