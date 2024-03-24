using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq.Protected;
using Moq;
using VsLocalizedIntellisense.Models.Logger;
using VsLocalizedIntellisense.Models.Service.GitHub;
using System.Text;
using System.IO;

namespace VsLocalizedIntellisense.Test.Models.Service.GitHub
{
    [TestClass]
    public class GitHubServiceTest
    {
        #region function

        [TestMethod]
        [DataRow("", "", "")]
        [DataRow("a", "a", "")]
        [DataRow("a", "", "a")]
        [DataRow("a/b/c", "a", "b", "c")]
        [DataRow("a/b/c", "/a/", "/b/", "/c/")]
        [DataRow("a/b/c", "\\a\\", "\\b\\", "\\c\\")]
        public void JoinPathTest(string expected, string path1, string path2, params string[] pathN)
        {
            var ghs = new GitHubService(new GitHubRepository(string.Empty, string.Empty), new GitHubOptions(), NullLoggerFactory.Instance);
            var actual = ghs.JoinPath(path1, path2, pathN);
            Assert.AreEqual(expected, actual);
        }

        private GitHubService CreateGitHubService(Mock<HttpMessageHandler> mockHttpMessageHandler)
        {
            return new GitHubService(
                new GitHubRepository("OWNER", "NAME"),
                new GitHubOptions() {
                    RequestHeaders = new Dictionary<string, string>() {
                        ["User-Agent"] = "UA",
                        ["Accept"] = "application/vnd.github+json",
                        ["X-GitHub-Api-Version"] = "2022-11-28",
                    }
                },
                new HttpClient(mockHttpMessageHandler.Object),
                NullLoggerFactory.Instance
            );
        }

        [TestMethod]
        public async Task GetContentsAsync_empty_Test()
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
                    Content = new StringContent("[]")
                });

            var test = CreateGitHubService(mockHttpMessageHandler);
            var actual = await test.GetContentsAsync("REVISION", "PATH");
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public async Task GetContentsAsync_data_Test()
        {
            var input = @"[
  {
    ""name"": ""name-1"",
    ""path"": ""PATH/CHILD/1"",
    ""sha"": ""SHA-1"",
    ""size"": 0,
    ""url"": ""http://example.com/url-1"",
    ""html_url"": ""http://example.com/html_url-1"",
    ""git_url"": ""http://example.com/git_url-1"",
    ""download_url"": null,
    ""type"": ""dir"",
    ""_links"": {
      ""self"": ""http://example.com/self-1"",
      ""git"": ""http://example.com/git-1"",
      ""html"": ""http://example.com/html-1""
    }
  },
  {
    ""name"": ""name-2"",
    ""path"": ""PATH/CHILD/2"",
    ""sha"": ""SHA-2"",
    ""size"": 2,
    ""url"": ""http://example.com/url-2"",
    ""html_url"": ""http://example.com/html_url-2"",
    ""git_url"": ""http://example.com/git_url-2"",
    ""download_url"": ""http://example.com/download_url-2"",
    ""type"": ""file"",
    ""_links"": {
      ""self"": ""http://example.com/self-2"",
      ""git"": ""http://example.com/git-2"",
      ""html"": ""http://example.com/html-2""
    }
  }
]";
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
                    Content = new StringContent(input)
                })
            ;

            var test = CreateGitHubService(mockHttpMessageHandler);
            var actual = await test.GetContentsAsync("REVISION", "PATH");
            Assert.AreEqual(2, actual.Count);

            var actual1 = actual[0];
            Assert.AreEqual("name-1", actual1.Name);
            Assert.AreEqual("PATH/CHILD/1", actual1.Path);
            Assert.AreEqual("SHA-1", actual1.Sha);
            Assert.AreEqual(0, actual1.Size);
            Assert.AreEqual("http://example.com/url-1", actual1.Url);
            Assert.AreEqual("http://example.com/html_url-1", actual1.HtmlUrl);
            Assert.AreEqual("http://example.com/git_url-1", actual1.GitUrl);
            Assert.AreEqual(null, actual1.DownloadUrl);
            Assert.AreEqual("dir", actual1.Type);
            Assert.AreEqual("http://example.com/self-1", actual1.Link.Self);
            Assert.AreEqual("http://example.com/git-1", actual1.Link.Git);
            Assert.AreEqual("http://example.com/html-1", actual1.Link.Html);

            var actual2 = actual[1];
            Assert.AreEqual("name-2", actual2.Name);
            Assert.AreEqual("PATH/CHILD/2", actual2.Path);
            Assert.AreEqual("SHA-2", actual2.Sha);
            Assert.AreEqual(2, actual2.Size);
            Assert.AreEqual("http://example.com/url-2", actual2.Url);
            Assert.AreEqual("http://example.com/html_url-2", actual2.HtmlUrl);
            Assert.AreEqual("http://example.com/git_url-2", actual2.GitUrl);
            Assert.AreEqual("http://example.com/download_url-2", actual2.DownloadUrl);
            Assert.AreEqual("file", actual2.Type);
            Assert.AreEqual("http://example.com/self-2", actual2.Link.Self);
            Assert.AreEqual("http://example.com/git-2", actual2.Link.Git);
            Assert.AreEqual("http://example.com/html-2", actual2.Link.Html);
        }

        [TestMethod]
        public async Task GetRawAsyncTest()
        {
            var input = new byte[] { 0, 1, 2, 3, 4, 5 };
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
                    Content = new StreamContent(new MemoryStream(input))
                })
            ;

            var test = CreateGitHubService(mockHttpMessageHandler);
            using var stream = await test.GetRawAsync("REVISION", "PATH");
            var actual = new byte[input.Length];
            await stream.ReadAsync(actual, 0, input.Length);
            CollectionAssert.AreEqual(input, actual);
        }

        #endregion
    }
}
