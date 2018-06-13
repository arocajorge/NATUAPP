using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Api.Models
{
    public class in_UnidadMedida_Equiv_conversion_model
    {
        public string IdUnidadMedida { get; set; }
        public string IdUnidadMedida_equiva { get; set; }
        public double valor_equiv { get; set; }
    }
}