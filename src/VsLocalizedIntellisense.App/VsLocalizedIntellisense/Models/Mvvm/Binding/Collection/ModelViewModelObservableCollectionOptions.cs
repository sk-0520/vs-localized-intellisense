using System;
using System.Collections.Generic;
using System.Threading;

namespace VsLocalizedIntellisense.Models.Mvvm.Binding.Collection
{
    /// <summary>
    /// <see cref="ModelViewModelObservableCollectionManager{TModel, TViewModel}"/>の振る舞い設定。
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TViewModel"></typeparam>
    public class ModelViewModelObservableCollectionOptions<TModel, TViewModel>
        where TViewModel : ViewModelBase
    {
        #region define

        public interface IApplyParameter
        {
            #region proeprty

            ModelViewModelObservableCollectionManager<TModel, TViewModel> Sender { get; }
            ModelViewModelObservableCollectionViewModelApply Apply { get; }

            #endregion
        }

        public abstract class ApplyParameter: IApplyParameter
        {
            #region proeprty

            public ModelViewModelObservableCollectionManager<TModel, TViewModel> Sender { get; set; }
            public ModelViewModelObservableCollectionViewModelApply Apply { get; set; }

            #endregion
        }

        public interface IAddItemParameter: IApplyParameter
        {
            #region proeprty

            IReadOnlyList<TModel> NewModels { get; }
            IReadOnlyList<TViewModel> NewViewModels { get; }

            #endregion
        }
        public class AddItemParameter: ApplyParameter, IAddItemParameter
        {
            #region IAddItemParameter

            public IReadOnlyList<TModel> NewModels { get; set; }
            public IReadOnlyList<TViewModel> NewViewModels { get; set; }

            #endregion
        }

        public interface IInsertItemParameter: IApplyParameter
        {
            #region proeprty

            int InsertIndex { get; }
            IReadOnlyList<TModel> NewModels { get; }

            #endregion
        }
        public class InsertItemParameter: ApplyParameter, IInsertItemParameter
        {
            #region IInsertItemParameter

            public int InsertIndex { get; set; }
            public IReadOnlyList<TModel> NewModels { get; set; }
            public IReadOnlyList<TViewModel> NewViewModels { get; set; }

            #endregion
        }

        public interface IRemoveItemParameter: IApplyParameter
        {
            #region proeprty

            int OldStartingIndex { get; }
            IReadOnlyList<TModel> OldModels { get; }
            IReadOnlyList<TViewModel> OldViewModels { get; }


            #endregion
        }
        public class RemoveItemParameter: ApplyParameter, IRemoveItemParameter
        {
            #region IRemoveItemParameter

            public int OldStartingIndex { get; set; }
            public IReadOnlyList<TModel> OldModels { get; set; }
            public IReadOnlyList<TViewModel> OldViewModels { get; set; }

            #endregion
        }

        public interface IReplaceItemParameter: IApplyParameter
        {
            #region proeprty

            int StartIndex { get; }
            IReadOnlyList<TModel> NewModels { get; }
            IReadOnlyList<TModel> OldModels { get; }
            IReadOnlyList<TViewModel> NewViewModels { get; }
            IReadOnlyList<TViewModel> OldViewModels { get; }

            #endregion
        }
        public class ReplaceItemParameter: ApplyParameter, IReplaceItemParameter
        {
            #region IReplaceItemParameter

            public int StartIndex { get; set; }
            public IReadOnlyList<TModel> NewModels { get; set; }
            public IReadOnlyList<TModel> OldModels { get; set; }
            public IReadOnlyList<TViewModel> NewViewModels { get; set; }
            public IReadOnlyList<TViewModel> OldViewModels { get; set; }

            #endregion
        }

        public interface IMoveItemParameter: IApplyParameter
        {
            #region proeprty

            int NewStartingIndex { get; }
            int OldStartingIndex { get; }

            #endregion
        }
        public class MoveItemParameter: ApplyParameter, IMoveItemParameter
        {
            #region IMoveItemParameter

            public int NewStartingIndex { get; set; }
            public int OldStartingIndex { get; set; }

            #endregion
        }

        public interface IResetItemParameter: IApplyParameter
        {
            #region proeprty

            IReadOnlyList<TViewModel> OldViewModels { get; }

            #endregion
        }
        public class ResetItemParameter: ApplyParameter, IResetItemParameter
        {
            #region IResetItemParameter

            public IReadOnlyList<TViewModel> OldViewModels { get; set; }

            #endregion
        }

        public delegate TViewModel ToViewModelDelegate(TModel model);

        public delegate void AddItemsApplyDelegate(IAddItemParameter parameter);
        public delegate void InsertItemsApplyDelegate(IInsertItemParameter parameter);
        public delegate void RemoveItemsApplyDelegate(IRemoveItemParameter parameter);
        public delegate void ReplaceItemsApplyDelegate(IReplaceItemParameter parameter);
        public delegate void MoveItemsApplyDelegate(IMoveItemParameter parameter);
        public delegate void ResetItemsApplyDelegate(IResetItemParameter parameter);

        #endregion

        #region property

        public ToViewModelDelegate ToViewModel { get; set; }

        public AddItemsApplyDelegate AddItems { get; set; }
        public InsertItemsApplyDelegate InsertItems { get; set; }
        public RemoveItemsApplyDelegate RemoveItems { get; set; }
        public ReplaceItemsApplyDelegate ReplaceItems { get; set; }
        public MoveItemsApplyDelegate MoveItems { get; set; }
        public ResetItemsApplyDelegate ResetItems { get; set; }

        public SynchronizationContext SynchronizationContext { get; set; } = SynchronizationContext.Current;

        /// <summary>
        /// アイテム削除時に対象 ViewModel の <see cref="IDisposable.Dispose"/> を呼び出すか。
        /// </summary>
        /// <remarks>
        /// <para>置き換え時(<c>list[n] = newViewModel</c>)には破棄されない点に注意。</para>
        /// </remarks>
        public bool AutoDisposeViewModel { get; set; } = true;

        #endregion
    }
}
