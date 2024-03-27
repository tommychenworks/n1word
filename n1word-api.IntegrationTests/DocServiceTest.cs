using Microsoft.Extensions.Logging;
using Moq;
using n1word_api.Service;

namespace n1word_api.Tests
{
    [TestClass]
    public class DocServiceTest
    {
        private readonly Mock<ILogger<DocService>> _mockLog;
        public DocServiceTest()
        {
            _mockLog = new Mock<ILogger<DocService>>();
        }

        [TestMethod]
        public void GetHtmlDocument_ErrorUrl()
        {
            // Arrange
            var service = new DocService(_mockLog.Object);
            // Act
            // Assert
            var exception = Assert.ThrowsExceptionAsync<InvalidOperationException>(() => service.GetHtmlDocument(""));

        }


    }
}
