namespace LibraryManagement.Presentation.WebAPI.Dtos
{
    public class CreateLoanDto
    {
        public int ReaderId { get; set; }
        public int BookId { get; set; }
        public DateTime LoanDate { get; set; }
    }
}
