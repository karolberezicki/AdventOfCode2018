namespace Day12
{
    public class Note
    {
        public Note(string pattern, char result)
        {
            Pattern = pattern;
            Result = result;
        }

        public string Pattern { get; set; }
        public char Result { get; set; }
    }
}
