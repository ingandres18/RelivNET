using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RelivMVC.Data;
using RelivMVC.Models;

namespace RelivMVC.Controllers
{
    public class StatesController : Controller
    {
        private readonly RelivMVCContext _context;
        string Baseurl = "https://localhost:7200/";

        public StatesController(RelivMVCContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<State> StateInfo = new List<State>();
            using (var client = new HttpClient())

            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("/api/State");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    StateInfo = JsonConvert.DeserializeObject<List<State>>(EmpResponse);
                }

                return View(StateInfo);
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            State StateInfo = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"/api/State/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    StateInfo = JsonConvert.DeserializeObject<State>(EmpResponse);
                }
            }
            if (StateInfo == null)
            {
                return NotFound();
            }

            return View(StateInfo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StateId,Description")] State state)
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
                        Description = state.Description
                    };

                    JsonContent content = JsonContent.Create(obj);

                    HttpResponseMessage Res = await client.PostAsync("/api/State", content);
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(state);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            State StateInfo = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"/api/State/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    StateInfo = JsonConvert.DeserializeObject<State>(EmpResponse);
                }
            }
            if (StateInfo == null)
            {
                return NotFound();
            }
            return View(StateInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StateId,Description")] State state)
        {
            if (id != state.StateId)
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
                        StateId = state.StateId,
                        Description = state.Description
                    };

                    JsonContent content = JsonContent.Create(obj);

                    HttpResponseMessage Res = await client.PutAsync("/api/State", content);
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(state);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            State StateInfo = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"/api/State/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    StateInfo = JsonConvert.DeserializeObject<State>(EmpResponse);
                }
            }

            return View(StateInfo);
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

                HttpResponseMessage Res = await client.DeleteAsync($"/api/State/{id}");
                if (Res.IsSuccessStatusCode)
                {

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(id);
        }
        private bool StateExists(int id)
        {
          return (_context.State?.Any(e => e.StateId == id)).GetValueOrDefault();
        }
    }
}
