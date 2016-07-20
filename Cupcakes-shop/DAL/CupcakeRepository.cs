using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cupcakes_shop.BOL;

namespace Cupcakes_shop.DAL
{
    public class CupcakeRepository
    {
        private CupcakesDBContext db = new BOL.CupcakesDBContext();

        public List<Cupcake> GetALL()
        {
            return db.Cupcakes.ToList();
        }

        public Cupcake GetByID(int Id)
        {
            return db.Cupcakes.Find(Id);

        }

        public void Insert(Cupcake cupcake)
        {
            db.Cupcakes.Add(cupcake);
            Save();
        }

        public bool IsCupcakePresent(string CupcakeName)
        {
            return db.Cupcakes.Any(cupcake => cupcake.CupcakeName == CupcakeName);
        }

        public void Delete(int Id)
        {
            Cupcake cupcake = db.Cupcakes.Find(Id);
            List<Ingredient> Ingredients = db.Ingredients.Where(id => id.Cupcake.CupcakeId == Id).ToList();
            foreach(var item in Ingredients)
            {
                db.Ingredients.Remove(item);
            }
            db.Cupcakes.Remove(cupcake);
            Save();
        }

        public void Update(BOL.Cupcake cupcake)
        {
            cupcake.DateModified = DateTime.Now;
            db.Cupcakes.Attach(cupcake);
            var entry = db.Entry(cupcake);
            entry.Property(e => e.CupcakeName).IsModified = true;
            entry.Property(e => e.DateModified).IsModified = true;
            entry.Property(e => e.CupcakePrice).IsModified = true;
            Save();
        }

        public void Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
    }
}