using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq.Protected;
using Moq;
using VsLocalizedIntellisense.Models.Service.Application;
using Castle.Core.Logging;
using VsLocalizedIntellisense.Models.Logger;
using VsLocalizedIntellisense.Models.Configuration;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Net.Http.Headers;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using Xunit;

namespace VsLocalizedIntellisense.Test.Models.Service.Application
{
    public class AppIntellisensePageServiceTest
    {
        #region property

        private System.Configuration.Configuration BaseConfiguration { get; set; }

        #endregion

        #region function

        private AppConfiguration CreateConfiguration(IReadOnlyDictionary<string, string> parameters, AppConfigurationInitializeParameters initializeParameters)
        {
            if(BaseConfiguration == null) {
                var path = Path.Combine(Test.GetProjectDirectory().FullName, "Models", "Service", "Application", "AppIntellisensePageServiceTest.config");
                var map = new ExeConfigurationFileMap {
                    ExeConfigFilename = path,
                };
                var config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                BaseConfiguration = config;
            }
            BaseConfiguration.AppSettings.Settings.Clear();

            BaseConfiguration.AppSettings.Settings.Add("page-base-url", "https://example.com");
            BaseConfiguration.AppSettings.Settings.Add("http-user-agent", "UA");

            foreach(var pair in parameters) {
                BaseConfiguration.AppSettings.Settings.Add(new KeyValueConfigurationElement(pair.Key, pair.Value));
            }

            return new AppConfiguration(BaseConfiguration, initializeParameters);
        }

        private AppIntellisensePageService CreateAppIntellisensePageService(Mock<HttpMessageHandler> mockHttpMessageHandler)
            => CreateAppIntellisensePageService(mockHttpMessageHandler, new Dictionary<string, string>());

        private AppIntellisensePageService CreateAppIntellisensePageService(Mock<HttpMessageHandler> mockHttpMessageHandler, IReadOnlyDictionary<string, string> configuration)
        {
            var config = CreateConfiguration(configuration, new AppConfigurationInitializeParameters(DateTime.UtcNow));

            return new AppIntellisensePageService(
                new HttpClient(mockHttpMessageHandler.Object),
                config,
                NullLoggerFactory.Instance
            );
        }

        [Fact]
        public async Task GetContentsAsync_member_null_Test()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    nameof(HttpClient.SendAsync),
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{}")
                })
            ;

            var test = CreateAppIntellisensePageService(mockHttpMessageHandler);
            var actual = await test.GetDataListAsync("PATH");
            Assert.Equal(0, actual.Files.Length);
            Assert.Equal(0, actual.Directories.Length);
        }


        [Fact]
        public async Task GetContentsAsync_file_only_Test()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    nameof(HttpClient.SendAsync),
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(@"{
                        ""file"": [
                            ""1""
                        ],
                    }")
                })
            ;

            var test = CreateAppIntellisensePageService(mockHttpMessageHandler);
            var actual = await test.GetDataListAsync("PATH");
            Assert.Equal(new string[] { "1" }, actual.Files);
            Assert.Equal(0, actual.Directories.Length);
        }

        [Fact]
        public async Task GetContentsAsync_directory_only_Test()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    nameof(HttpClient.SendAsync),
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(@"{
                        ""directory"": [
                            ""A""
                        ],
                    }")
                })
            ;

            var test = CreateAppIntellisensePageService(mockHttpMessageHandler);
            var actual = await test.GetDataListAsync("PATH");
            Assert.Equal(0, actual.Files.Length);
            Assert.Equal(new string[] { "A" }, actual.Directories);
        }

        [Fact]
        public async Task GetDataStreamAsyncTest()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    nameof(HttpClient.SendAsync),
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ByteArrayContent(new byte[] { 1, 2, 3 })
                })
            ;

            var test = CreateAppIntellisensePageService(mockHttpMessageHandler);
            using var stream = await test.GetDataStreamAsync("PATH");
            using var reader = new MemoryStream();
            stream.CopyTo(reader);
            reader.Position = 0;
            var actual = reader.ToArray();

            Assert.Equal(new byte[] { 1, 2, 3 }, actual);
        }

        #endregion
    }
}
