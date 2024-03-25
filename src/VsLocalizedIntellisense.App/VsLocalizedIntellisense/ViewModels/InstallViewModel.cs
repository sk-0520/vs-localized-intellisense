using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VsLocalizedIntellisense.Models.Configuration;
using VsLocalizedIntellisense.Models.Element;
using VsLocalizedIntellisense.Models.Logger;
using VsLocalizedIntellisense.Models.Mvvm.Binding;
using VsLocalizedIntellisense.Models.Mvvm.Command;
using VsLocalizedIntellisense.Models.Mvvm.Message;
using VsLocalizedIntellisense.Models.Service.CommandShell;

namespace VsLocalizedIntellisense.ViewModels
{
    public class InstallViewModel: ContextContentViewModelBase<MainElement>
    {
        #region variable

        private bool _isExecuting = false;
        private string _installCommand = string.Empty;

        private DelegateCommand _backCommand;
        private AsyncDelegateCommand _executeCommand;

        #endregion

        public InstallViewModel(MainElement model, IReadOnlyDictionary<DirectoryElement, IList<FileInfo>> installItems, Action<ContextMode> changedCallback, ISendableMessenger messenger, AppConfiguration configuration, ILoggerFactory loggerFactory)
            : base(model, changedCallback, messenger, configuration, loggerFactory)
        {
            InstallItems = installItems;

            GeneratedCommandShellEditor = Model.GenerateShellCommand(InstallItems);
            InstallCommand = GeneratedCommandShellEditor.ToSourceCode();

            AddCommandHook(
                BackCommand,
                new[] {
                    nameof(IsExecuting),
                }
            );
            AddCommandHook(
                ExecuteCommand,
                new[] {
                    nameof(IsExecuting),
                }
            );
        }

        #region property

        private IReadOnlyDictionary<DirectoryElement, IList<FileInfo>> InstallItems { get; }
        private CommandShellEditor GeneratedCommandShellEditor { get; set; }

        public string InstallCommand
        {
            get => this._installCommand;
            set => SetVariable(ref this._installCommand, value);
        }

        public bool IsExecuting
        {
            get => this._isExecuting;
            set => SetVariable(ref this._isExecuting, value);
        }

        #endregion

        #region command

        public ICommand BackCommand => this._backCommand ??= new DelegateCommand(
           _ => {
               ChangeMode(ContextMode.Download);
           },
            _ => !IsExecuting
       );

        public ICommand ExecuteCommand => this._executeCommand ??= new AsyncDelegateCommand(
            async _ => {
                IsExecuting = true;
                try {
                    await Model.ExecuteCommandShellAsync(GeneratedCommandShellEditor);
                } finally {
                    IsExecuting = false;
                }
            },
            _ => !IsExecuting
        );

        #endregion

        #region function

        #endregion
    }
}
