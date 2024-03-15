using System.Runtime.Serialization;

namespace VsLocalizedIntellisense.Models.Service.GitHub
{
    [DataContract]
    public class GitHubContentResponseItem
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "path")]
        public string Path { get; set; }
        [DataMember(Name = "sha")]
        public string Sha { get; set; }
        [DataMember(Name = "size")]
        public long Size { get; set; }
        [DataMember(Name = "url")]
        public string Url { get; set; }
        [DataMember(Name = "html_url")]
        public string HtmlUrl { get; set; }
        [DataMember(Name = "git_url")]
        public string GitUrl { get; set; }
        [DataMember(Name = "download_url")]
        public object DownloadUrl { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "_links")]
        public GitHubResponseLink Link { get; set; }
    }



}
