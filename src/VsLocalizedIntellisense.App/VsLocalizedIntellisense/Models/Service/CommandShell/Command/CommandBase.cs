using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Models.Service.CommandShell.Command
{
    public abstract class CommandBase : ActionBase
    {
        #region variable

        private readonly string _commandName;

        #endregion

        public CommandBase(string name)
        {
            this._commandName = name;
        }

        #region property

        /// <summary>
        /// コマンド表示を抑制するか。
        /// </summary>
        public bool SuppressCommand { get; set; }
        /// <summary>
        /// コマンド名を大文字にするか。
        /// </summary>
        public bool CommandNameIsUpper { get; set; }

        #endregion

        #region function

        protected internal string GetStatementCommandName()
        {
            var commandName = CommandNameIsUpper
                ? this._commandName.ToUpperInvariant()
                : this._commandName
            ;

            return SuppressCommand
                ? "@" + commandName
                : commandName
            ;
        }

        #endregion

        #region function


        #endregion

        #region ActionBase

        public override string CommandName
        {
            get => this._commandName;
            set => throw new NotSupportedException(nameof(CommandName));
        }

        #endregion
    }
}
