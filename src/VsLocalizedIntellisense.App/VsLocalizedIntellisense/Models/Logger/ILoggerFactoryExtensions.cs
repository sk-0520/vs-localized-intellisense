using System;

namespace VsLocalizedIntellisense.Models.Logger
{
    public static class ILoggerFactoryExtensions
    {
        #region function

        public static ILogger CreateLogger(this ILoggerFactory factory, Type type)
        {
            return factory.CreateLogger(type.FullName);
        }

        public static ILogger CreateLogger<T>(this ILoggerFactory factory)
        {
            return CreateLogger(factory, typeof(T));
        }

        #endregion
    }
}
