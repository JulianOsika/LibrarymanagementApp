using LibraryManagement.Presentation.RazorPages.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace LibraryManagement.Presentation.RazorPages.Pages.Admin
{
    public class AddLoanModel : PageModel
    {
        [BindProperty]
        public NewLoanDto NewLoan { get; set; } = new();

        public List<SelectListItem> BookOptions { get; set; } = new();
        public List<SelectListItem> ReaderOptions { get; set; } = new();

        private readonly IHttpClientFactory _httpClientFactory;

        public AddLoanModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("JWT");

            if (string.IsNullOrEmpty(token))
                return RedirectToPage("/Login");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var booksResponse = await client.GetAsync("https://localhost:7007/api/book/available");
            var readersResponse = await client.GetAsync("https://localhost:7007/api/reader");

            if (booksResponse.IsSuccessStatusCode)
            {
                var json = await booksResponse.Content.ReadAsStringAsync();
                var books = JsonSerializer.Deserialize<List<BookDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();

                BookOptions = books.Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Title
                }).ToList();
            }

            if (readersResponse.IsSuccessStatusCode)
            {
                var json = await readersResponse.Content.ReadAsStringAsync();
                var readers = JsonSerializer.Deserialize<List<ReaderDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();

                ReaderOptions = readers.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = $"{r.Name} {r.Surname}"
                }).ToList();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("JWT");

            if (string.IsNullOrEmpty(token))
                return RedirectToPage("/Login");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(NewLoan);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7007/api/loan", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Admin/Loans");
            }

            return Page();
        }


    }
}
