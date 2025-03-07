namespace teko_projektarbeit_loesungsalgorithmen.Objects
{
    public class Comment
{
        public int Id { get; set; }

        public string Text { get; set; }
        public User Author { get; set; }

        public Comment(string text, User author)
        {
            Text = text;
            Author = author;
        }
    }
}
