using System.Runtime.Serialization;

namespace VsLocalizedIntellisense.Models.Service.GitHub
{
    [DataContract]
    public class GitHubResponseLink
    {
        [DataMember(Name = "self")]
        public string Self { get; set; }
        [DataMember(Name = "git")]
        public string Git { get; set; }
        [DataMember(Name = "html")]
        public string Html { get; set; }
    }
}
