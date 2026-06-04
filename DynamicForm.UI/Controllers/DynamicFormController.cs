using DynamicForm.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DynamicForm.UI.Controllers
{
    public class DynamicFormController(IHttpClientFactory _httpClientFactory,IConfiguration _configuration) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = await GetTokenAsync();

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiSettings:BaseUrl"]!);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("/api/FormConfig/claim-details");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Unable to load form configuration.";
                return View(new FormConfigViewModel());
            }

            var json = await response.Content.ReadAsStringAsync();

            var formConfig = JsonSerializer.Deserialize<FormConfigViewModel>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return View(formConfig);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(IFormCollection form)
        {
            var token = await GetTokenAsync();

            var data = new Dictionary<string, string?>();

            foreach (var key in form.Keys)
            {
                if (key != "FormKey" && key != "FormVersion")
                {
                    data[key] = form[key];
                }
            }

            var request = new
            {
                formKey = form["FormKey"].ToString(),
                formVersion = int.Parse(form["FormVersion"].ToString()),
                data
            };

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiSettings:BaseUrl"]!);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/api/FormSubmit", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Form submitted successfully.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = await response.Content.ReadAsStringAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> GetTokenAsync()
        {
            var existingToken = HttpContext.Session.GetString("JwtToken");

            if (!string.IsNullOrWhiteSpace(existingToken))
                return existingToken;

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiSettings:BaseUrl"]!);

            var loginRequest = new
            {
                userName = "admin",
                password = "Admin@123"
            };

            var content = new StringContent(
                JsonSerializer.Serialize(loginRequest),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/api/Auth/login", content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(json);
            var token = document.RootElement.GetProperty("token").GetString();

            HttpContext.Session.SetString("JwtToken", token!);

            return token!;
        }
    }
}

