using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;
using VsLocalizedIntellisense.Models.Service.CommandShell.Value;

namespace VsLocalizedIntellisense.Models.Service.CommandShell
{
    public class CommandShellEditor
    {
        #region property

        public CommandShellOptions Options { get; set; } = new CommandShellOptions();

        public List<ActionBase> Actions { get; } = new List<ActionBase>();

        #endregion

        #region function

        public EmptyLine CreateEmptyLine()
        {
            var result = new EmptyLine();
            return result;
        }
        public EmptyLine AddEmptyLine()
        {
            var result = CreateEmptyLine();

            Actions.Add(result);

            return result;
        }

        public EmptyLine[] AddEmptyLines(int length)
        {
            if(length < 0) {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            var result = Enumerable.Repeat(0, length).Select(x => new EmptyLine()).ToArray();

            Actions.AddRange(result);

            return result;
        }

        #region command

        private TCommand CreateCommand<TCommand>()
            where TCommand : CommandBase, new()
        {
            var result = new TCommand() {
                SuppressCommand = Options.SuppressCommand,
                CommandNameIsUpper = Options.CommandNameIsUpper,
                IndentSpace = Options.IndentSpace,
            };

            return result;
        }

        private TCommand AddCommand<TCommand>(TCommand command)
            where TCommand : CommandBase
        {
            Actions.Add(command);
            return command;
        }

        #endregion

        #region create/add-command

        public ChangeCodePageCommand CreateChangeCodePage(Encoding encoding)
        {
            var command = CreateCommand<ChangeCodePageCommand>();
            command.Encoding = encoding;
            return command;
        }
        public ChangeCodePageCommand AddChangeCodePage(Encoding encoding)
            => AddCommand(CreateChangeCodePage(encoding));

        public ChangeDirectoryCommand CreateChangeDirectory(Express path, bool withDrive = true)
        {
            var command = CreateCommand<ChangeDirectoryCommand>();
            command.Path = path;
            command.WithDrive = withDrive;
            return command;
        }
        public ChangeDirectoryCommand AddChangeDirectory(Express path, bool withDrive = true)
            => AddCommand(CreateChangeDirectory(path, withDrive));

        public ChangeSelfDirectoryCommand CreateChangeSelfDirectory()
        {
            var command = CreateCommand<ChangeSelfDirectoryCommand>();
            return command;
        }
        public ChangeSelfDirectoryCommand AddChangeSelfDirectory()
            => AddCommand(CreateChangeSelfDirectory());

        public CopyCommand CreateCopy(Express source, Express destination, PromptMode promptMode = PromptMode.Default)
        {
            var command = CreateCommand<CopyCommand>();
            command.Source = source;
            command.Destination = destination;
            command.PromptMode = promptMode;
            return command;
        }
        public CopyCommand AddCopy(Express source, Express destination, PromptMode promptMode = PromptMode.Default)
            => AddCommand(CreateCopy(source, destination, promptMode));

        public EchoCommand CreateEcho(Express value)
        {
            var command = CreateCommand<EchoCommand>();
            command.Value = value;
            return command;
        }
        public EchoCommand AddEcho(Express value)
            => AddCommand(CreateEcho(value));

        public IfErrorLevelCommand CreateIfErrorLevel(int level, bool isNot = false)
        {
            var command = CreateCommand<IfErrorLevelCommand>();
            command.Level = level;
            command.IsNot = isNot;
            return command;
        }
        public IfErrorLevelCommand AddIfErrorLevel(int level, bool isNot = false)
            => AddCommand(CreateIfErrorLevel(level, isNot));

        public IfExpressCommand CreateIfExpress(Express left, Express right, bool isNot = false)
        {
            var command = CreateCommand<IfExpressCommand>();
            command.Left = left;
            command.Right = right;
            command.IsNot = isNot;
            return command;
        }
        public IfExpressCommand AddIfExpress(Express left, Express right, bool isNot = false)
            => AddCommand(CreateIfExpress(left, right, isNot));

        public IfExistCommand CreateIfExist(Express path, bool isNot = false)
        {
            var command = CreateCommand<IfExistCommand>();
            command.Path = path;
            command.IsNot = isNot;
            return command;
        }
        public IfExistCommand AddIfExist(Express path, bool isNot = false)
            => AddCommand(CreateIfExist(path, isNot));

        public MakeDirectoryCommand CreateMakeDirectory(Express path)
        {
            var command = CreateCommand<MakeDirectoryCommand>();
            command.Path = path;
            return command;
        }
        public MakeDirectoryCommand AddMakeDirectory(Express path)
            => AddCommand(CreateMakeDirectory(path));

        public PauseCommand CreatePause()
        {
            var command = CreateCommand<PauseCommand>();
            return command;
        }
        public PauseCommand AddPause()
            => AddCommand(CreatePause());

        public RemarkCommand CreateRemark(Express comment)
        {
            var command = CreateCommand<RemarkCommand>();
            command.Comment = comment;
            return command;
        }
        public RemarkCommand AddRemark(Express comment)
            => AddCommand(CreateRemark(comment));

        public SetVariableCommand CreateSetVariable(string variableName, Express value)
        {
            var command = CreateCommand<SetVariableCommand>();
            command.VariableName = variableName;
            command.Value = value;
            return command;
        }
        public SetVariableCommand AddSetVariable(string variableName, Express value)
            => AddCommand(CreateSetVariable(variableName, value));

        public SwitchEchoCommand CreateSwitchEcho(bool isOn)
        {
            var command = CreateCommand<SwitchEchoCommand>();
            command.SuppressCommand = true;
            command.On = isOn;
            return command;
        }
        public SwitchEchoCommand AddSwitchEcho(bool isOn)
            => AddCommand(CreateSwitchEcho(isOn));

        #endregion

        public string ToSourceCode()
        {
            var sb = new StringBuilder();

            var indentContext = new IndentContext(Options.IndentSpace, 0);
            foreach(var action in Actions) {
                var statement = action.ToStatement(indentContext);
                sb.Append(statement);
                sb.Append(Options.NewLine);
            }

            return sb.ToString();
        }

        public async Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            using(var writer = new StreamWriter(stream, Options.Encoding, 1024, true) {
                NewLine = Options.NewLine,
            }) {
                var indentContext = new IndentContext(Options.IndentSpace, 0);
                foreach(var action in Actions) {
                    cancellationToken.ThrowIfCancellationRequested();

                    var statement = action.ToStatement(indentContext);
                    await writer.WriteLineAsync(statement);
                }
            }
        }

        #endregion
    }
}
