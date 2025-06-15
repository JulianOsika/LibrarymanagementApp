using LibraryManagement.Presentation.RazorPages.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace LibraryManagement.Presentation.RazorPages.Pages.Admin
{
    public class EditReaderModel : PageModel
    {
        [BindProperty]
        public ReaderDto Reader { get; set; } = new();

        private IHttpClientFactory _httpClientFactory;

        public EditReaderModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("JWT");

            if (string.IsNullOrEmpty(token))
                return RedirectToPage("/Login");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"https://localhost:7007/api/reader/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToPage("/Admin/Readers");

            var json = await response.Content.ReadAsStringAsync();
            Reader = JsonSerializer.Deserialize<ReaderDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("JWT");

            if (string.IsNullOrEmpty(token))
                return RedirectToPage("/Login");

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(Reader);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"https://localhost:7007/api/reader/{Reader.Id}", content);

            Console.WriteLine($"Status: {response.StatusCode}");
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Body: {body}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Admin/Readers");
            }

            return Page();
        }
    }
}
