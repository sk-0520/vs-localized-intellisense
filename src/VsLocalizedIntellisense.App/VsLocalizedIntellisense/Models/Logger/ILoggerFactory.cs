using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Models.Logger
{
    public interface ILoggerFactory : IDisposable
    {
        #region function

        ILogger CreateLogger(string categoryName);

        #endregion
    }
}
