namespace LibraryManagement.Presentation.RazorPages.Dtos
{
    public class LoanDto
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public BookDto Book { get; set; }

        public int ReaderId { get; set; }
        public ReaderDto Reader { get; set; }

        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
