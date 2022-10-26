using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreMVC.DTOs;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StoreMVC.Controllers
{
    public class CategoriesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage responseMessage = null;
            string endpoint = "https://localhost:44327/admin/api/categories";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization","Bearer " + Request.Cookies["AuthToken"]);
                responseMessage = await client.GetAsync(endpoint);
            }
            if (responseMessage != null && responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await responseMessage.Content.ReadAsStringAsync();
                CategoryListDto categoryListDto = JsonConvert.DeserializeObject<CategoryListDto>(content);
                return View(categoryListDto);
            }

            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public async Task<IActionResult> Edit (int id)
        {
            HttpResponseMessage responseMessage = null;
            string endpoint = "https://localhost:44327/admin/api/categories/"+ id;
            using (HttpClient client = new HttpClient())
            {
                responseMessage = await client.GetAsync(endpoint);
            }
            if(responseMessage != null && responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await responseMessage.Content.ReadAsStringAsync();
                CategoryEditDto categoryEditDto = JsonConvert.DeserializeObject<CategoryEditDto>(content);
                ViewBag.Id = id;
                return View(categoryEditDto);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, CategoryEditDto categoryEditDto)
        {
            if (!ModelState.IsValid)
                return View();

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(categoryEditDto), Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = null;
            string endpoint = "https://localhost:44327/admin/api/categories/"+ id;
            using (HttpClient client = new HttpClient())
            {
                responseMessage = await client.PutAsync(endpoint,requestContent);
            }
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return RedirectToAction("index");
            }
            return NotFound();
        }
    }
}
