using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LatHtaukBayDin20221120GraphQL.App.Controllers
{
    public class BlogController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BlogController(IHttpClientFactory httpClientFactory) =>
            _httpClientFactory = httpClientFactory;

        [ActionName("Index")]
        public async Task<IActionResult> BlogList()
        {
            List<BlogModel> lst = new List<BlogModel>();
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7136/api/blog/1/100");
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr);
            }
            return View("BlogList", lst);
        }

        [HttpPost]
        [ActionName("Find")]
        public async Task<IActionResult> BlogFind(BlogModel reqModel)
        {
            var model = new BlogModel();
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7136/api/blog/{reqModel.Blog_Id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<BlogModel>(jsonStr);
                HttpContext.Session.SetString("BlogModel", JsonConvert.SerializeObject(model));
            }
            return Json(new { RedirectUrl = "/Blog/Edit", Status = 200, Message = "Success" });
        }

        [HttpGet]
        [ActionName("Edit")]
        public IActionResult BlogEdit()
        {
            string jsonStr = HttpContext.Session.GetString("BlogModel");
            var model = JsonConvert.DeserializeObject<BlogModel>(jsonStr);
            return View("BlogEdit", model);
        }
    }

    public class BlogModel
    {
        public int Blog_Id { get; set; }
        public string Blog_Title { get; set; }
        public string Blog_Author { get; set; }
        public string Blog_Content { get; set; }
    }
}
