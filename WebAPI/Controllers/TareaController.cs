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
    public class TareaController : BaseController
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
                    List<ListTareasViewModel> lst = (from d in db.Tarea
                                                     select new ListTareasViewModel
                                                     {
                                                         nombre = d.Nombre,
                                                         descripcion = d.descripcion
                                                         
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
        public Reply Agregar([FromBody]TareaViewModel modelo)
        {
            Reply oR = new Reply();
            oR.result = 0;

            if (!Verify(modelo.token))
            {
                oR.message = " No autorizado";
                return oR;
            }

            //validaciones
            if(!Validate(modelo))
            {
                oR.message = error;
                return oR;
            }

            try
            {
                using (PruebaEntities db = new PruebaEntities())
                {
                    Tarea oTarea = new Tarea();
                    oTarea.estado = false;
                    oTarea.Nombre = modelo.nombre;
                    oTarea.descripcion = modelo.descripcion;

                    db.Tarea.Add(oTarea);
                    db.SaveChanges();

                    List<ListTareasViewModel> lst = (from d in db.Tarea
                                                     select new ListTareasViewModel
                                                     {
                                                         nombre = d.Nombre,
                                                         descripcion = d.descripcion

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



        [HttpPost]
        public Reply Edit([FromBody]TareaViewModel modelo)
        {
            Reply oR = new Reply();
            oR.result = 0;

            if (!Verify(modelo.token))
            {
                oR.message = " No autorizado";
                return oR;
            }

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
                    Tarea oTarea = db.Tarea.Find(modelo.Id);
                    oTarea.estado = modelo.estado;

                    db.Entry(oTarea).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    List<ListTareasViewModel> lst = (from d in db.Tarea
                                                     select new ListTareasViewModel
                                                     {
                                                         nombre = d.Nombre,
                                                         descripcion = d.descripcion

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



        [HttpDelete]
        public Reply Delete([FromBody]TareaViewModel modelo)
        {
            Reply oR = new Reply();
            oR.result = 0;

            if (!Verify(modelo.token))
            {
                oR.message = " No autorizado";
                return oR;
            }

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
                    Tarea oTarea = db.Tarea.Find(modelo.Id);
                    oTarea.estado = modelo.estado;
                    oTarea.Nombre = modelo.nombre;
                    oTarea.descripcion = modelo.descripcion;

                    db.Entry(oTarea).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();

                    List<ListTareasViewModel> lst = (from d in db.Tarea
                                                     select new ListTareasViewModel
                                                     {
                                                         nombre = d.Nombre,
                                                         descripcion = d.descripcion

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

        private bool Validate( TareaViewModel model)
        {
            if(model.nombre == "")
            {
                error = "el nombre es obligatorio";
                return false;
            }

            if (model.descripcion == "")
            {
                error = "la descripcion es obligatoria";
                return false;
            }

            return true;

        }

        #endregion


    }
}
