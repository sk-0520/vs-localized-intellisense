using System;
using System.IO;

namespace VsLocalizedIntellisense.Models.Logger
{
    public sealed class FileLogger: LoggerBase<FileLogOptions>, IDisposable
    {
        #region variable

        private bool _disposedValue;

        #endregion  

        public FileLogger(string category, FileLogOptions options)
            : base(category, options)
        {
            if(File.Exists(Options.FilePath)) {
                Stream = new FileStream(Options.FilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            } else {
                var parentDir = Path.GetDirectoryName(Options.FilePath);
                // スペシャルファイル対策
                if(!string.IsNullOrEmpty(parentDir)) {
                    Directory.CreateDirectory(parentDir);
                    Stream = new FileStream(Options.FilePath, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite);
                } else {
                    Stream = FileStream.Null;
                }
            }

            Writer = new StreamWriter(Stream);
        }

        ~FileLogger()
        {
            Dispose(false);
        }

        #region proeprty

        private Stream Stream { get; }
        private TextWriter Writer { get; }

        #endregion

        #region LoggerBase

        public override void OutputLog(in LogItem logItem)
        {
            var log = Logging.Format(Category, logItem, Options);
            Stream.Seek(0, SeekOrigin.End);
            Writer.WriteLine(log);
            Writer.Flush();
        }

        #endregion

        #region IDisposable

        private void Dispose(bool disposing)
        {
            if(!this._disposedValue) {
                if(disposing) {
                    Writer.Dispose();
                }

                this._disposedValue = true;
            }
        }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
