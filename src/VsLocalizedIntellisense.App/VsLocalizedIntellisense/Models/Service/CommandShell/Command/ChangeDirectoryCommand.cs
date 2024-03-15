using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Service.CommandShell.Value;

namespace VsLocalizedIntellisense.Models.Service.CommandShell.Command
{
    public class ChangeDirectoryCommand : CommandBase
    {
        public ChangeDirectoryCommand()
            : base(Name)
        { }

        #region property

        public static string Name { get; } = "cd";

        /// <summary>
        /// 現在のドライブだけでなく、ドライブの現在のディレクトリも変更します。
        /// <para>/d</para>
        /// </summary>
        public bool WithDrive { get; set; }

        public Express Path { get; set; }

        #endregion

        #region CommandBase

        public override string GetStatement()
        {
            var sb = new StringBuilder();

            sb.Append(GetStatementCommandName());
            sb.Append(' ');

            if (WithDrive)
            {
                sb.Append("/d ");
            }

            sb.Append(Path.Escape());

            return sb.ToString();
        }

        #endregion
    }
}
