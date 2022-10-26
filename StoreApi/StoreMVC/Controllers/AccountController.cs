using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreMVC.DTOs;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StoreMVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(loginDto),Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = null;
            string endpoint = "https://localhost:44327/admin/api/accounts/login";
            using (HttpClient client = new HttpClient())
            {
                responseMessage = await client.PostAsync(endpoint, requestContent);
            }
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();

                LoginResponseDto responseLoginDto = JsonConvert.DeserializeObject<LoginResponseDto>(content);
                Response.Cookies.Append("AuthToken",responseLoginDto.Token);
                return RedirectToAction("index", "categories");
            }
            return NotFound();
        }
    }
}
