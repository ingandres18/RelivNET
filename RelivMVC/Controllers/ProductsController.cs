﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RelivMVC.Data;
using RelivMVC.Models;
using System.Net.Http.Headers;

namespace RelivMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly RelivMVCContext _context;
        string Baseurl = "https://localhost:7200/";
        public ProductsController(RelivMVCContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            List<Product> ProdInfo = new List<Product>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("Product");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    ProdInfo = JsonConvert.DeserializeObject<List<Product>>(EmpResponse);
                }

                return View(ProdInfo);
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product ProdInfo = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"Product/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    ProdInfo = JsonConvert.DeserializeObject<Product>(EmpResponse);
                }
            }
            if (ProdInfo == null)
            {
                return NotFound();
            }

            return View(ProdInfo);
        }
        public async Task<IActionResult> Create()
        {
            List<Category> CatInfo = new List<Category>();
            List<State> StateInfo = new List<State>();

            var prod = new Product();
            using (var client = new HttpClient())

            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Ress = await client.GetAsync("/api/Category");
                if (Ress.IsSuccessStatusCode)
                {
                    var EmpResponse = Ress.Content.ReadAsStringAsync().Result;
                    CatInfo = JsonConvert.DeserializeObject<List<Category>>(EmpResponse);
                }
                HttpResponseMessage Resss = await client.GetAsync("/api/State");
                if (Ress.IsSuccessStatusCode)
                {
                    var EmpResponse = Resss.Content.ReadAsStringAsync().Result;
                    StateInfo = JsonConvert.DeserializeObject<List<State>>(EmpResponse);
                }

                List<SelectListItem> cats = new List<SelectListItem>();
                List<SelectListItem> states = new List<SelectListItem>();

                CatInfo.ForEach(s => {
                    cats.Add(new SelectListItem
                    {
                        Text = s.Description,
                        Value = s.CategoryId.ToString()
                    });
                });
             
                prod.Category = cats;

                StateInfo.ForEach(s => {
                    states.Add(new SelectListItem
                    {
                        Text = s.Description,
                        Value = s.StateId.ToString()
                    });
                });

                prod.State = states;
            }
                return View(prod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Price,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {

                List<Category> CatInfo = new List<Category>();
                using (var client = new HttpClient())

                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                 
               
                    var obj = new
                    {

                        Name = product.Name,
                        Price = product.Price,
                        Stock = product.Stock,
                        CategoryId = 10,
                        StateId = 4
                        //CategoryId = product.Category.CategoryId,
                        //StateId = product.State.StateId
                    };

                    JsonContent content = JsonContent.Create(obj);

                    HttpResponseMessage Res = await client.PostAsync("Product",content);
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(product);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product ProdInfo = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"Product/{id}");

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    ProdInfo = JsonConvert.DeserializeObject<Product>(EmpResponse);
                }
            }
            if (ProdInfo == null)
            {
                return NotFound();
            }
            return View(ProdInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Price,Stock")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var obj = new
                    {
                        ProductId = product.ProductId,
                        Name = product.Name,
                        Price = product.Price,
                        Stock = product.Stock,
                        CategoryId = 4,
                        StateId = 4
                        //CategoryId = product.Category.CategoryId,
                        //StateId = product.State.StateId
                    };

                    JsonContent content = JsonContent.Create(obj);

                    HttpResponseMessage Res = await client.PutAsync("Product", content);
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            return View(product);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }
            Product ProdInfo = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"Product/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    ProdInfo = JsonConvert.DeserializeObject<Product>(EmpResponse);
                }
            }

            return View(ProdInfo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.DeleteAsync($"Product/{id}");
                if (Res.IsSuccessStatusCode)
                {

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(id);
        }

        private bool ProductExists(int id)
        {
            return (_context.Product?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
