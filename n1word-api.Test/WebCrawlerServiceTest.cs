using Moq;
using FluentAssertions;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using n1word_api.Exceptions;
using n1word_api.Interfaces;
using n1word_api.Models;
using n1word_api.Service;


namespace n1word_api.Test
{
    [TestClass]
    public class WebCrawlerServiceTest
    {

        private readonly Mock<IWordRepository> _mockRepository;
        private readonly Mock<IDocService> _mockDocService;
        private readonly Mock<IOptions<WordConfig>> _mockConfig;
        private readonly Mock<ILogger<WebCrawlerService>> _mockLog;

        public WebCrawlerServiceTest()
        {
            _mockRepository = new Mock<IWordRepository>();
            _mockDocService = new Mock<IDocService>();
            _mockConfig = new Mock<IOptions<WordConfig>>();
            _mockLog = new Mock<ILogger<WebCrawlerService>>();
        }

        [TestMethod]
        public void CreateWordsTest_EmptyNode()
        {
            // Arrange
            var service = new WebCrawlerService(_mockConfig.Object, _mockRepository.Object, _mockDocService.Object, _mockLog.Object);
            _mockDocService.Setup(s => s.GetHtmlDocument("")).ReturnsAsync(new HtmlDocument());
            // Act Assert
            var exception = Assert.ThrowsExceptionAsync<WordException>(() => service.ComplieWords(""));
        }

        [TestMethod]
        public async Task CreateWordsTest_HasCorrectData()
        {
            // Arrange
            var service = new WebCrawlerService(_mockConfig.Object, _mockRepository.Object, _mockDocService.Object, _mockLog.Object);
            string testData = "<html><body><div id='article-content-inner'>801&nbsp;&nbsp; &nbsp;けんやく&nbsp;&nbsp; &nbsp;倹約&nbsp;&nbsp; &nbsp;節約、節省</div></body></html>";
            var html = new HtmlDocument();
            html.LoadHtml(testData);
            _mockDocService.Setup(s => s.GetHtmlDocument("")).ReturnsAsync(html);
            var expect = new List<Word>() { new Word() { jp_Word = "けんやく", jp_word_chi = "倹約", jp_Word_kanji = "節約、節省", jp_Word_No = 1 } };
            // Act
            var result = await service.ComplieWords("");

            // Assert
            result.Should().BeEquivalentTo(expect);
        }

    }
}