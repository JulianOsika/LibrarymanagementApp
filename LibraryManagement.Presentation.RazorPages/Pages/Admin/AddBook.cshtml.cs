using LibraryManagement.Presentation.RazorPages.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;

namespace LibraryManagement.Presentation.RazorPages.Pages.Admin
{
    public class AddBookModel : PageModel
    {
        [BindProperty]
        public NewBookDto NewBook { get; set; } = new();

        private IHttpClientFactory _httpClientFactory;

        public AddBookModel(IHttpClientFactory httpClientFactory)
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

            var json = JsonSerializer.Serialize(NewBook);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7007/api/book", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Admin/Dashboard");
            }

            return Page();
        }

    }
}
