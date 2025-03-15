namespace teko_projektarbeit_loesungsalgorithmen.Objects
{
    public class Comment
{
        public int Id { get; set; }
        public string Text { get; set; }
        public int AuthorId { get; set; }
        public int InformationId { get; set; }

        public Comment(int id, string text, int authorId, int informationId)
        {
            Id = Id;
            Text = text;
            AuthorId = authorId;
            InformationId = informationId;
        }
    }
}
