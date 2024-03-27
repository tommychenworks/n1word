using n1word_api.Models;

namespace n1word_api.Interfaces
{
    public interface IWebCrawlerService
    {
        Task<List<Word>> ComplieWords(string url);
        List<Word> GetWords();
    }
}
