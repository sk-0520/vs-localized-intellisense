using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Models
{
    public static class IOHelper
    {
        #region function

        public static bool DeleteDirectory(string path)
        {
            if(Directory.Exists(path))
            {
                Directory.Delete(path, true);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 指定したディレクトリっぽいパスから物理ディレクトリを取得する。
        /// <para>環境変数の展開を行う。</para>
        /// </summary>
        /// <param name="path">ディレクトリパス。</param>
        /// <returns>ディレクトリ。存在しない場合は<see langword="null" /></returns>
        public static DirectoryInfo GetPhysicalDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            var dir = Environment.ExpandEnvironmentVariables(path);
            if (!Directory.Exists(dir))
            {
                return null;
            }

            return new DirectoryInfo(dir);
        }

        #endregion
    }
}
