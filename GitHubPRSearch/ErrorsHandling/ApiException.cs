namespace GitHubPRSearch.Infrastracture
{
    public class ApiException : Exception
    {
        public ApiException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}
