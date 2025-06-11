namespace LibraryManagement.Presentation.WebAPI.Dtos
{
    public class UpdateLoanDto
    {
        public int ReaderId { get; set; }
        public int BookId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
