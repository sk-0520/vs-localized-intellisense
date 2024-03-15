using System;

namespace VsLocalizedIntellisense.Models.Service.CommandShell.Value
{
    public class Variable: ValueBase
    {
        #region variable

        private string _name;

        #endregion

        public Variable(string name, bool isReadOnly = false)
        {
            Name = name;
            IsReadOnly = isReadOnly;
        }

        #region property

        public bool IsReadOnly { get; }

        public string Name
        {
            get => this._name;
            set
            {
                if(IsReadOnly) {
                    throw new InvalidOperationException("readonly");
                }

                if(string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException(nameof(value));
                }
                this._name = value;
            }
        }

        public bool DelayedExpansion { get; set; }

        #endregion


        #region Value

        public override string Expression
        {
            get
            {
                if(DelayedExpansion) {
                    return $"!{Name}!";
                }

                return $"%{Name}%";
            }
        }

        #endregion
    }
}
