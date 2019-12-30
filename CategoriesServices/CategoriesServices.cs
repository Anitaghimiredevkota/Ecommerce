﻿using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ClothBazar.Services
{
    public class CategoriesServices
    {
        public string search;
        #region Singleton
        public static CategoriesServices Instance
        {
            get
            {
                if (instance == null) instance = new CategoriesServices();
                return instance;
            }
        }
        private static CategoriesServices instance { get; set; }
        private CategoriesServices()
        {

        }
        #endregion

        public Category GetCategory(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Categories.Find(ID);

            }
        }

        public int GetCategoriesCount(string serach)
        {
            using (var context = new CBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Where(category => category.Name != null
                    && category.Name.ToLower().Contains(search.ToLower())).Count();
                }
                else
                {
                    return context.Categories.Count();
                }
            }
        }
        public Category GetFeaturedCategories(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Categories.Find(ID);

            }
        }
        public List<Category> GetAllCategories()
        {
            using (var context = new CBContext())
            {
                return context.Categories.ToList();

            }

        }
        public List<Category> GetCategories()
        {
            using (var context = new CBContext())
            {
                return context.Categories.ToList();

            }

        }
        public List<Category> GetCategories(string search, int pageNo)
        {
            int pageSize = 3;
            using (var context = new CBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Where(category => category.Name != null
                    && category.Name.ToLower().Contains(search.ToLower()))
                    .OrderBy(x => x.ID)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }
                else
                {
                    return context.Categories
                     .OrderBy(x => x.ID)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .Include(x => x.Products)
                    .ToList();
                }
            }
        }
        public List<Category> GetFeaturedCategories()
        {
            using (var context = new CBContext())
            {
                return context.Categories.Where(x => x.isFeatured && x.ImageURL != null).ToList();

            }

        }
        public void SaveCategory(Category category)
        {
            using (var context = new CBContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }
        public void UpdateCategory(Category category)
        {
            using (var context = new CBContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteCategory(int ID)
        {
            using (var context = new CBContext())
            {
                var category = context.Categories.Where(x => x.ID == ID).Include(x => x.Products).FirstOrDefault();
                //context.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                context.Products.RemoveRange(category.Products);
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }


    }
}
