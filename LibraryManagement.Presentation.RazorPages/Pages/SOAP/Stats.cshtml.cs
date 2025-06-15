using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoapReference;


namespace LibraryManagement.Presentation.RazorPages.Pages.SOAP
{
    public class StatsModel : PageModel
    {
        public LibraryStats Stats { get; set; }

        public async Task OnGetAsync()
        {
            var client = new LibraryStatsServiceClient(); // lub inna nazwa klasy proxy
            Stats = await client.GetLibraryStatsAsync();
        }
    }
}
