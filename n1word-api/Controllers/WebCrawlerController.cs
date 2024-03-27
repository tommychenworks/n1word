using Microsoft.AspNetCore.Mvc;
using n1word_api.Interfaces;
using n1word_api.Models;
using System.Net;

namespace n1word_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebCrawlerController : ControllerBase
    {
        private readonly IWebCrawlerService _webCrawlerService;
        private readonly IWordRepository _wordRepository;
        public WebCrawlerController(IWebCrawlerService webCrawlerService,IWordRepository wordRepository) {
            _webCrawlerService = webCrawlerService;
            _wordRepository = wordRepository;
        }

        [HttpGet]
        [Route("")]
        public List<Word> GetWords() 
        {
            return _webCrawlerService.GetWords();
        }

        [HttpPost]
        [Route("arashiyama")]
        public async Task<ActionResult> arashiyama([FromBody]string url) 
        {
            var words = await _webCrawlerService.ComplieWords(url);
            _wordRepository.CreatWord(words);
            return Ok("單字已全數加入！");
        }
    }
}
