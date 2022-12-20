namespace Demosite.ViewModels.Search
{
    public class SearchCorrection
    {
        public string Text { get; }
        public string Snippet { get; }
        public bool ResultsAreCorrected { get; }
        public string OriginalQuery { get; }

        public SearchCorrection(string text, string snippet, string originalQuery, bool resultsAreCorrected)
        {
            Text = text;
            Snippet = snippet;
            OriginalQuery = originalQuery;
            ResultsAreCorrected = resultsAreCorrected;
        }
    }
}
