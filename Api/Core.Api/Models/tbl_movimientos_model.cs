using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Api.Models
{
    public class tbl_movimientos_model
    {
        public decimal IdSincronizacion { get; set; }
        public string IdUsuarioSCI { get; set; }
        public int IdFecha { get; set; }
        public System.DateTime Fecha { get; set; }
        public List<tbl_movimientos_det_model> lst_det { get; set; }
    }
}