using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cupcakes_shop.BOL;

namespace Cupcakes_shop.DAL
{
    public class IngredientRepository
    {
        private BOL.CupcakesDBContext db = new BOL.CupcakesDBContext();

        public List<BOL.Ingredient> GetALL()
        {
            return db.Ingredients.ToList();
        }

        public Ingredient GetByID(int Id)
        {
            return db.Ingredients.Find(Id);

        }

        public List<BOL.Ingredient> IngredientsListByID(int Id)
        {

            return db.Ingredients.Where(p => p.Cupcake.CupcakeId == Id).ToList();

        }

        public void Insert(BOL.Ingredient ingredients)
        {
            String[] ingredientsArray = ingredients.IngredientName.Split(',');
            Cupcake lastcake = db.Cupcakes.OrderByDescending(id => id.CupcakeId).First();
            ingredients.Cupcake = lastcake;
            foreach (string item in ingredientsArray)
            {
                Ingredient ingredient = new Ingredient();
                ingredient.IngredientName = item;
                ingredient.Cupcake = lastcake;
                db.Ingredients.Add(ingredient);
            }

            Save();
        }
        public void InserttoCupcake(BOL.Ingredient ingredients, int Id)
        {
            String[] ingredientsArray = ingredients.IngredientName.Split(',');
            Cupcake cupcake = db.Cupcakes.Single(id => id.CupcakeId == Id);
            ingredients.Cupcake = cupcake;
            foreach (string item in ingredientsArray)
            {
                Ingredient ingredient = new Ingredient();
               ingredient.IngredientName = item;
               ingredient.Cupcake = cupcake;
               db.Ingredients.Add(ingredient);
            }

            Save();
        }

        public void Update(Ingredient ingredient)
        {
            
            db.Ingredients.Attach(ingredient);
            var entry = db.Entry(ingredient);
            entry.Property(e => e.IngredientName).IsModified = true;
            Save();
        }
        public void Delete(int Id)
        {
            BOL.Ingredient Ingredient = db.Ingredients.Find(Id);
            db.Ingredients.Remove(Ingredient);
            Save();
        }
        public void Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
    }
}