using HtmlAgilityPack;

namespace n1word_api.Interfaces
{
    public interface IDocService
    {
        Task<HtmlDocument> GetHtmlDocument(string url);

    }
}
