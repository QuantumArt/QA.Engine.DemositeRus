namespace Demosite.ViewModels.Search
{
    public class SearchCorrection
    {
        public string Text { get; }
        public string Snippet { get; }
        public bool ResultsAreCorrected { get; }

        public SearchCorrection(string text, string snippet, bool resultsAreCorrected)
        {
            Text = text;
            Snippet = snippet;
            ResultsAreCorrected = resultsAreCorrected;
        }
    }
}
