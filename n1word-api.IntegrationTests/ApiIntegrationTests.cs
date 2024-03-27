using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.Extensions.Configuration;
using n1word_api.Models;
using System.Diagnostics.Tracing;

namespace n1word_api.IntegrationTests
{
    [TestClass]
    public class ApiIntegrationTests
    {

        private readonly HttpClient _client;

        public ApiIntegrationTests()
        {
            var factory = new WebApplicationFactoryIntegration();
            _client = factory.CreateClient();
        }

        
        [DataTestMethod]
        [DataRow("https://arashiyama888.pixnet.net/blog/post/303739424-%e6%97%a5%e6%9c%ac%e8%aa%9e%e8%83%bd%e5%8a%9b%e6%aa%a2%e5%ae%9a%ef%bc%aa%ef%bc%ac%ef%bc%b0%ef%bc%b4-%e6%97%a5%e6%aa%a2%ef%bc%ae1%e5%96%ae%e5%ad%97%e8%a1%a8-")]
        public async Task Post_Arashiyama_SuccessfullyAddsWordsToDatabase(string url)
        {
            // Arrange
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(url),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await _client.PostAsync("/api/WebCrawler/arashiyama", requestContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("單字已全數加入！", responseString);
        }

        [TestMethod]
        public async Task GetWord_SuccessfullyGetAllWords()
        {
            // Arrange
            // Act
            var response = await _client.GetAsync("/api/WebCrawler");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var words = JsonConvert.DeserializeObject<List<Word>>(responseString);

            Assert.IsNotNull(words,"列表不應為null");

        }

    }

}
