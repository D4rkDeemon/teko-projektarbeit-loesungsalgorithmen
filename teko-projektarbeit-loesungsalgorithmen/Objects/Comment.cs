namespace teko_projektarbeit_loesungsalgorithmen.Objects
{
    public class Comment
{
        public int Id { get; set; }
        public string Text { get; set; }
        public int AuthorId { get; set; }

        public Comment(int id, string text, int authorId)
        {
            Id = Id;
            Text = text;
            AuthorId = authorId;
        }
    }
}
