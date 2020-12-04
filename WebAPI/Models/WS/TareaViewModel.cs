using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.WS
{
    public class TareaViewModel : SecurityViewModel
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool estado{ get; set; }

        public string estadoTexto { get; set; }




    }
}