using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace Cupcakes_shop.BOL
{
    public class CupcakeDBSeeder : DropCreateDatabaseAlways<CupcakesDBContext>
    {
        protected override void Seed(CupcakesDBContext context)
        {
            Ingredient ingredient1 = new Ingredient()
            {
                IngredientName = "sugar",
                Cupcake = new Cupcake()
                {
                    CupcakeName = "Rich Red Velvet",
                    CupcakePrice = 12.11M,
                    Datecreated = DateTime.Now,
                    DateModified = DateTime.Now

                }
            };

            Ingredient ingredient2 = new Ingredient()
            {
                IngredientName = "milk",
                Cupcake = new Cupcake()
                {
                    CupcakeName = "Strawberry Lemonade",
                    CupcakePrice = 19.11M,
                    Datecreated = DateTime.Now,
                    DateModified = DateTime.Now

                }
            };

            context.Ingredients.Add(ingredient1);
            context.Ingredients.Add(ingredient2);
            base.Seed(context);
        }
    }
}
