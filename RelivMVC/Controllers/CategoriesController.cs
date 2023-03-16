using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RelivMVC.Data;
using RelivMVC.Models;
using System.Net.Http.Headers;

namespace RelivMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly RelivMVCContext _context;
        string Baseurl = "https://localhost:7200/";

        public CategoriesController(RelivMVCContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> CatInfo = new List<Category>();
            using (var client = new HttpClient())
            
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("/api/Category");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    CatInfo = JsonConvert.DeserializeObject<List<Category>>(EmpResponse);
                }

                return View(CatInfo);
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category CatInfo = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"/api/Category/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    CatInfo = JsonConvert.DeserializeObject<Category>(EmpResponse);
                }
            }
            if (CatInfo == null)
            {
                return NotFound();
            }

            return View(CatInfo);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var obj = new
                    {
                        Description = category.Description
                    };

                    JsonContent content = JsonContent.Create(obj);

                    HttpResponseMessage Res = await client.PostAsync("/api/Category", content);
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category CatInfo = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"/api/Category/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    CatInfo = JsonConvert.DeserializeObject<Category>(EmpResponse);
                }
            }
            if (CatInfo == null)
            {
                return NotFound();
            }
            return View(CatInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Description")] Category category)
        {
            if (id != category.CategoryId)
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
                        CategoryId = category.CategoryId,
                        Description = category.Description
                    };

                    JsonContent content = JsonContent.Create(obj);

                    HttpResponseMessage Res = await client.PutAsync("/api/Category", content);
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(category);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category CatInfo = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"/api/Category/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    CatInfo = JsonConvert.DeserializeObject<Category>(EmpResponse);
                }
            }

            return View(CatInfo);
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

                HttpResponseMessage Res = await client.DeleteAsync($"/api/Category/{id}");
                if (Res.IsSuccessStatusCode)
                {

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(id);
        }

        private bool CategoryExists(int id)
        {
          return (_context.Category?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
