using System;

namespace Festival.App.ViewModels
{
    public interface IDetailViewModel<TDetail> : IViewModel
    {
        TDetail Model { get; set; }
        void Load(Guid id);
    }
}
