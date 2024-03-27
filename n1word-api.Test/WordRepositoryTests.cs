using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using n1word_api.Models;
using n1word_api.Repositories;

namespace n1word_api.Tests
{
    [TestClass]
    public class WordRepositoryIntegrationTests
    {
        private readonly Mock<ILogger<WordRepository>> _logger;
        private readonly string _filepath;
        private readonly WordRepository _repository;

        public WordRepositoryIntegrationTests()
        {
            _filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppData\\word.json");
            _logger = new Mock<ILogger<WordRepository>>();
            
            var mockConfig = new Mock<IOptions<WordConfig>>();
            mockConfig.Setup(m => m.Value).Returns(new WordConfig { jsonWordDB = _filepath });
            _repository = new WordRepository(mockConfig.Object, _logger.Object);
        }

        [TestInitialize]
        public void Initialize() {
            File.WriteAllText(_filepath, @"[{""jp_Word_No"":1,""jp_Word"":""けんりょく"",""jp_Word_kanji"":""權力"",""jp_word_chi"":""権力""}]");
        }

        [TestMethod]
        public void GetWord_CorrentJson() 
        {

            var words = _repository.GetWord();
            List<Word> word_test = new List<Word>() { new Word() { jp_Word = "けんりょく", jp_Word_kanji = "權力", jp_word_chi = "権力", jp_Word_No = 1 } } ;

            Assert.IsNotNull(words);
            Assert.IsTrue(words.Count > 0);
            word_test.Should().BeEquivalentTo(words);

        }


        [TestCleanup]
        public void Cleanup()
        {
            File.WriteAllText(_filepath, string.Empty);
        }
    }
}
