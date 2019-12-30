
using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            //var categories = categoryService.GetCategories();
            return View();
        }
        public ActionResult CategoryTable(string search, int? pageNo)
        {

            CategorySearchViewModel model = new CategorySearchViewModel();

            model.SearchTerm = search;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            var totalRecords = CategoriesServices.Instance.GetCategoriesCount(search);

            model.Categories = CategoriesServices.Instance.GetCategories(search, pageNo.Value);

            if (model.Categories != null)
            {
                model.Pager = new Pager(totalRecords, pageNo, 3);
                //model.SearchTerm = model.Categories.Where(x=>x.Name !=null && x.Name.ToLower()
                //.Contains(search.ToLower())).ToList(); 
                return PartialView("_CategoryTable", model);
            }
            else
            {
                return HttpNotFound();
            }

        }
        [HttpGet]
        public ActionResult Create()
        {
            NewCategoryViewModel model = new NewCategoryViewModel();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newCategory = new Category();
                newCategory.Name = model.Name;
                newCategory.Description = model.Description;
                newCategory.ImageURL = model.ImageURL;
                newCategory.isFeatured = model.isFeatured;

                CategoriesServices.Instance.SaveCategory(newCategory);
                return RedirectToAction("CategoryTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }

        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();

            var category = CategoriesServices.Instance.GetCategory(ID);

            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ImageURL = category.ImageURL;
            model.isFeatured = category.isFeatured;

            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            var existingCategory = CategoriesServices.Instance.GetCategory(model.ID);
            existingCategory.Name = model.Name;
            existingCategory.Description = model.Description;
            existingCategory.ImageURL = model.ImageURL;
            existingCategory.isFeatured = model.isFeatured;

            CategoriesServices.Instance.UpdateCategory(existingCategory);
            return RedirectToAction("CategoryTable");
        }
        //[HttpGet]
        //public ActionResult Delete(int ID)
        //{
        //    var category = categoryService.GetFeaturedCategories(ID);
        //    return View(category);
        //}
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            CategoriesServices.Instance.DeleteCategory(ID);
            return RedirectToAction("CategoryTable");
        }
    }
}