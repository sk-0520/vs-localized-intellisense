using System;

namespace VsLocalizedIntellisense.Models.Mvvm.Message
{
    [System.AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class MessengerAttribute : Attribute
    {
        // This is a positional argument
        public MessengerAttribute()
        { }
    }
}
