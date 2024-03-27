using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using n1word_api.Interfaces;
using n1word_api.Models;
using Newtonsoft.Json;

namespace n1word_api.Service
{

    public class DocService : IDocService
    {
        private readonly ILogger<DocService> _logger;
        public DocService(ILogger<DocService> logger)
        {
            _logger = logger;
        }

        public async Task<HtmlDocument> GetHtmlDocument(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // 發送GET請求
                    HttpResponseMessage response = await client.GetAsync(url);
                    // 確保HTTP響應狀態碼為200 OK
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var doc = new HtmlDocument();
                    doc.LoadHtml(responseBody);
                    return doc;
                }
            }
            catch (InvalidOperationException ex) 
            {
                _logger.LogError(ex, "錯誤的網址！");
                throw new InvalidOperationException("錯誤的網址！",ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "呼叫請求失敗。");
                throw;
            }


        }
    }
}
