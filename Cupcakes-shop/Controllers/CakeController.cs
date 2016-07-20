using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cupcakes_shop.BOL;
using Cupcakes_shop.DAL;

namespace Cupcakes_shop.Controllers
{
    public class CakeController : Controller
    {
        CupcakeRepository cupcakerepository;
        IngredientRepository ingredientrepository;
        CupcakesDBContext DBcontext;

        public CakeController()
        {
            cupcakerepository = new CupcakeRepository();
            ingredientrepository = new IngredientRepository();
            DBcontext = new CupcakesDBContext();
        }

        //Home page
        public ActionResult Index()
        {       
            var model = DBcontext.Cupcakes;
            return View(model);
        }

        public bool IsCupcakePresent(string CupcakeName)
        {
            return cupcakerepository.IsCupcakePresent(CupcakeName);
        }



        // Creates Cupcakes and ingredients

        public ActionResult CreateCupcake()
        {
            return View();
        }

        [HttpPost]
        [HandleError()]
        public ActionResult CreateCupcake(BOL.Cupcake cupcake)
        {
            if (cupcakerepository.IsCupcakePresent(cupcake.CupcakeName))
            {
                ModelState.AddModelError("CupcakeName", "Cupcake already added");
            }

                if (ModelState.IsValid)
            {
                cupcake.Datecreated = DateTime.Now;
                cupcake.DateModified = DateTime.Now; 
                cupcakerepository.Insert(cupcake);
                return RedirectToAction("CreateIngredients");               
            }
            else
            {
                return View("CreateCupcake");
            }
        }

        //Edits Cupcake

        public ActionResult CupcakeEdit(int id)
        {
            Cupcake cupcake = cupcakerepository.GetByID(id);
            return View(cupcake);           
        }


        [HttpPost]
        [HandleError()]
        public ActionResult CupcakeEdit(Cupcake cupcake)
        {
            try
            {
                cupcakerepository.Update(cupcake);
                
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());

            }
            return RedirectToAction("Index");
        }

       

        public ActionResult CupcakeDetails(int id)
        {
            Cupcake cupcake = cupcakerepository.GetByID(id);
            return View(cupcake);
        }


        [HandleError()]
        public ActionResult CupcakeDelete(int id)
        {
             cupcakerepository.Delete(id);
             return RedirectToAction("Index");
        }



//Ingredients
//////////////////////////////////////////////////////

        public ActionResult IngredientDetails(int id)
        {
            List<Ingredient> ingredients = ingredientrepository.IngredientsListByID(id);
            TempData["IngredientstoAdd"] = id;
            return View(ingredients);         
        }

        public ActionResult IngredientsAdd()
        {
            return View();
        }

        [HttpPost]
        [HandleError()]
        public ActionResult IngredientsAdd(Ingredient ingredient)
        {
           int id = (int)TempData["IngredientstoAdd"];
           ingredientrepository.InserttoCupcake(ingredient, (int)TempData["IngredientstoAdd"]);
           return RedirectToAction("IngredientDetails", new { id = (int)TempData["IngredientstoAdd"] });
        }

        //Creates Ingredients after adding cupcakes
        public ActionResult CreateIngredients()
        {
            return View();
        }

        [HttpPost]
        [HandleError()]
        public ActionResult CreateIngredients(Ingredient ingredient)
        {
            ingredientrepository.Insert(ingredient);
            return RedirectToAction("Index");
        }


        //Deletes Ingredients

        [HandleError()]
        public ActionResult IngredientsDelete(int Id)
        {
            ingredientrepository.Delete(Id);
            return RedirectToAction("IngredientDetails", new { id = TempData["IngredientstoAdd"] });
        }


        public ActionResult IngredientsEdit(int Id)
        {
            Ingredient ingredient = ingredientrepository.GetByID(Id);
            return View(ingredient);
        }

        [HttpPost]
        public ActionResult IngredientsEdit(Ingredient ingredient)
        {
            ingredientrepository.Update(ingredient);
            return RedirectToAction("Index");
        }
    }
}