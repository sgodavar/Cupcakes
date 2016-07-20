using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Cupcakes_shop.BOL
{
    public class Cupcake
    {
        public int CupcakeId { get; set; }
        [Required]
        public string CupcakeName { get; set; }
        [Required]
        public Decimal CupcakePrice { get; set; }
        public DateTime Datecreated { get; set; }
        public DateTime DateModified { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}