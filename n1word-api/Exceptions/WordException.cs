namespace n1word_api.Exceptions
{
    public class WordException : Exception
    {
        public WordException(string message) : base(message)
        {
        }

        public WordException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
