using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ApiMonedas.Models
{
    public class Moneda
    {
        public int Id { get; set; }

        [StringLength(20)]
        public String Divisa { get; set; }

        public double Precio { get; set; }
    }
}
