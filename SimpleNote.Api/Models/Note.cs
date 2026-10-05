namespace SimpleNote.Api.Models
{
    public class Note
    {
        public int Id { get; set; }
        public required string Title { get; set; } 
        public string Content { get; set; } = string.Empty;
        public string Category { get; set; } = "Uncategorized";
        public DateTime CreationDate { get; set; }
    }
}
