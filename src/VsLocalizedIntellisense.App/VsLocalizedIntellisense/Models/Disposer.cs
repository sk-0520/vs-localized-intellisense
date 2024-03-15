using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace VsLocalizedIntellisense.Models
{
    /// <summary>
    /// <see cref="IDisposable.Dispose"/>が行われたかどうかを確認できるようにする。
    /// </summary>
    public interface IDisposed: IDisposable
    {
        #region property

        /// <summary>
        /// <see cref="IDisposable.Dispose"/>されたか。
        /// </summary>
        bool IsDisposed { get; }

        #endregion
    }

    /// <summary>
    /// <see cref="IDisposable.Dispose"/>をサポートする基底クラス。
    /// </summary>
    public abstract class DisposerBase: IDisposed
    {
        ~DisposerBase()
        {
            Dispose(false);
        }

        #region IDisposed

        /// <summary>
        /// <see cref="IDisposable.Dispose"/>されたか。
        /// </summary>
        [IgnoreDataMember, XmlIgnore]
        public bool IsDisposed { get; protected set; }

        /// <summary>
        /// 既に破棄済みの場合は処理を中断する。
        /// </summary>
        /// <param name="_callerMemberName"></param>
        /// <exception cref="ObjectDisposedException">破棄済み。</exception>
        /// <seealso cref="IDisposed"/>
        protected void ThrowIfDisposed([CallerMemberName] string _callerMemberName = "")
        {
            if(IsDisposed) {
                throw new ObjectDisposedException(_callerMemberName);
            }
        }

        /// <summary>
        /// <see cref="IDisposable.Dispose"/>の内部処理。
        /// <para>継承先クラスでは本メソッドを呼び出す必要がある。</para>
        /// </summary>
        /// <param name="disposing">CLRの管理下か。</param>
        protected virtual void Dispose(bool disposing)
        {
            if(IsDisposed) {
                return;
            }

            if(disposing) {
                GC.SuppressFinalize(this);
            }

            IsDisposed = true;
        }


        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
