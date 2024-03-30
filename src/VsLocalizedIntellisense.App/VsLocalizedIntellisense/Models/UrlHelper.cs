using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Models
{
    public static class UrlHelper
    {
        #region function

        public static Uri JoinUri(string baseUrl, string path, params string[] paths)
        {
            var h = baseUrl.Trim('/');
            var pathList = new List<string>() {
                path,
            };
            pathList.AddRange(paths);
            var url = h + "/" + string.Join("/", pathList.Select(a => a.Trim('/')));

            return new Uri(url);
        }

        #endregion
    }
}
