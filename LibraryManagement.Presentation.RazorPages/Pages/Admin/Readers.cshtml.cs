using LibraryManagement.Presentation.RazorPages.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace LibraryManagement.Presentation.RazorPages.Pages.Admin
{
    public class ReadersModel : PageModel
    {
        public List<ReaderDto> Readers { get; set; } = new();

        private IHttpClientFactory _httpClientFactory;

        public ReadersModel(IHttpClientFactory httpClientFactory)
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

            var response = await client.GetAsync("https://localhost:7007/api/reader");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Readers = JsonSerializer.Deserialize<List<ReaderDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<ReaderDto>();
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

            var response = await client.DeleteAsync($"https://localhost:7007/api/reader/{id}");

            return RedirectToPage();
        }
    }
}
