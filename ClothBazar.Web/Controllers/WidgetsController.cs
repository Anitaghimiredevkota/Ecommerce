﻿using ClothBazar.Services;
using ClothBazar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class WidgetsController : Controller
    {
        // GET: Widgets
        public ActionResult Products(bool isLatestProducts, int? CategoryID)
        {
            ProductWidgetViewModels model = new ProductWidgetViewModels();
            model.IsLatestProducts = isLatestProducts;

            if (isLatestProducts)
            {
                model.Products = ProductsServices.Instance.GetLatestProducts(4);
            }
            else if (CategoryID.HasValue && CategoryID.Value > 0)
            {
                model.Products = ProductsServices.Instance.GetProductsByCategory(CategoryID.Value, 4);
            }
            else
            {
                model.Products = ProductsServices.Instance.GetProducts(1, 8);

            }
            return PartialView(model);
        }
    }
}