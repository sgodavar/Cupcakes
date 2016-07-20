using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Cupcakes_shop.BOL
{
    public class CupcakesDBContext :DbContext
    {
        public CupcakesDBContext() : base()
        { }
        public DbSet<Cupcake> Cupcakes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }
}