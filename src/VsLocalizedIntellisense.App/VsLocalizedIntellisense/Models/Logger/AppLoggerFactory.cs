using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Configuration;
using VsLocalizedIntellisense.Models.Element;

namespace VsLocalizedIntellisense.Models.Logger
{
    internal class AppLoggerFactory : DisposerBase, ILoggerFactory
    {
        public AppLoggerFactory(AppConfiguration configuration, ObservableCollection<LogItemElement> logItems = null)
        {
            var multiLogOptions = new MultiLogOptions();

            var level = configuration.GetLogDefaultLevel();

            if (configuration.IsEnableDebugLog())
            {
                var options = new DebugLogOptions()
                {
                    Level = level,
                    Format = configuration.GetLogDebugFormat(),
                };

                multiLogOptions.Options[nameof(DebugLogOptions)] = options;
            }

            if (configuration.IsEnableFileLog())
            {
                var options = new FileLogOptions()
                {
                    Level = level,
                    Format = configuration.GetLogFileFormat(),
                    FilePath = configuration.GetLogFilePath(),
                };

                multiLogOptions.Options[nameof(FileLogOptions)] = options;
            }

            if (logItems != null)
            {
                multiLogOptions.Options[nameof(StockLogOptions)] = new StockLogOptions()
                {
                    Level = LogLevel.Trace,
                    LogItems = logItems,
                    LoggerFactory = this,
                };
            }

            MultiLogOptions = multiLogOptions;
        }

        #region property

        private MultiLogOptions MultiLogOptions { get; }

        #endregion

        #region function

        #endregion

        #region ILoggerFactory

        public ILogger CreateLogger(string categoryName)
        {
            return new MultiLogger(categoryName, MultiLogOptions);
        }

        #endregion
    }
}
