using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace LibraryManagement.Presentation.RazorPages.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var loginData = new {Username = this.Username, Password = this.Password};
            var json = JsonSerializer.Serialize(loginData);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7007/api/login", content);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                HttpContext.Session.SetString("JWT", token);
                ViewData["Token"] = token;
            }
            else
            {
                var status = response.StatusCode;
                var body = await response.Content.ReadAsStringAsync();
                ErrorMessage = $"Status: {status}, Body: {body}";
            }


            return Page();

        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("JWT");
            return RedirectToPage("/Login");
        }

    }
}
