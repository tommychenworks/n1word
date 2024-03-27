using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using n1word_api.Exceptions;
using n1word_api.Interfaces;
using n1word_api.Models;
using System.Diagnostics.Tracing;

namespace n1word_api.Service
{

    public class WebCrawlerService : IWebCrawlerService
    {
        private readonly WordConfig _config;
        private readonly IWordRepository _wordRepository;
        private readonly IDocService _docService;
        private readonly ILogger<WebCrawlerService> _logger;

        public WebCrawlerService(IOptions<WordConfig> config, IWordRepository repository, IDocService docService, ILogger<WebCrawlerService> logger)
        {
            _config = config.Value;
            _wordRepository = repository;
            _docService = docService;
            _logger = logger;
        }

        public List<Word> GetWords()
        {
            return _wordRepository.GetWord();
        }

        public async Task<List<Word>> ComplieWords(string url)
        {
            List<Word> words = new List<Word>();
            //api或是html取得內容
            var doc = await _docService.GetHtmlDocument(url);
            // 客製抓取網頁關鍵字所需的東西
            var divNode = doc.DocumentNode.SelectSingleNode("//div[@id='article-content-inner']");
            if (divNode != null)
            {
                try
                {
                    _logger.LogInformation(doc.DocumentNode.InnerHtml);
                    string divHtml = divNode.InnerHtml;
                    string[] parts = divHtml.Split(new string[] { "<br>" }, StringSplitOptions.None);

                    int index = 1;
                    foreach (string part in parts)
                    {
                        var text = HtmlEntity.DeEntitize(part).Trim();
                        text = text.Replace("&nbsp;", "").Replace(" ", "|");
                        var text_sp = text.Split("|");
                        //找出單字對應
                        if (text_sp.Count() > 3)
                        {
                            words.Add(new Word
                            {
                                jp_Word_No = index,
                                jp_Word = text_sp[1].Trim(),
                                jp_word_chi = text_sp[2].Trim(),
                                jp_Word_kanji = text_sp[3].Trim()
                            });
                            index++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }

                return words;
            }
            else
            {
                throw new WordException("無法取得網頁！");
            }
        }
    }
}
