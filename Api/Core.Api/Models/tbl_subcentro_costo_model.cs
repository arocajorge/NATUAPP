using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Api.Models
{
    public class tbl_subcentro_costo_model
    {
        public int IdEmpresa { get; set; }
        public string IdCentroCosto { get; set; }
        public string IdSubCentroCosto { get; set; }
        public string nom_subcentro { get; set; }
    }
}