﻿using GeekShooping.Web.Models;
using GeekShooping.Web.Services.IServices;
using GeekShooping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShooping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [Authorize]
        public async Task<IActionResult> ProductIndex()
        {
            string token = await HttpContext.GetTokenAsync("access_token");
            var products = await _productService.FindAllProducts(token);
            return View(products);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                string token = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.CreateProduct(model,token);
                if (response != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ProductUpdate(int id)
        {
            string token = await HttpContext.GetTokenAsync("access_token");
            var model = await _productService.FindProductById(id,token);
            if (model != null)
            {
                return View(model);
            }
            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                string token = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.UpdateProduct(model,token);
                if (response != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> ProductDelete(int id)
        {
            string token = await HttpContext.GetTokenAsync("access_token");
            var model = await _productService.FindProductById(id,token);
            if (model != null)
            {
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductModel model)
        {
            string token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.DeleteProductById(model.Id,token);
            if (response)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
            return View(model);
        }
    }
}
