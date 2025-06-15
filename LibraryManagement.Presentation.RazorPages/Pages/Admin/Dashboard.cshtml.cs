using LibraryManagement.Presentation.RazorPages.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using LibraryManagement.Presentation.RazorPages.Dtos;
using System.Text.Json;
using System.Text;

namespace LibraryManagement.Presentation.RazorPages.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        public List<BookDto> Books { get; set; } = new();

        private IHttpClientFactory _httpClientFactory;

        [BindProperty]
        public NewBookDto NewBook { get; set; } = new();


        public DashboardModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("JWT");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Login");
            }

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("https://localhost:7007/api/book");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Books = JsonSerializer.Deserialize<List<BookDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<BookDto>();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("JWT");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Login");
            }

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"https://localhost:7007/api/book/{id}");

            return RedirectToPage();
        }

    }
}
