using LibraryManagement.Presentation.RazorPages.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;

namespace LibraryManagement.Presentation.RazorPages.Pages.Admin
{
    public class AddReaderModel : PageModel
    {
        [BindProperty]
        public NewReaderDto NewReader { get; set; } = new();

        private IHttpClientFactory _httpClientFactory;

        public AddReaderModel (IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("JWT");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Login");
            }

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(NewReader);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7007/api/reader", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Admin/Readers");
            }

            return Page();
        }
    }
}
