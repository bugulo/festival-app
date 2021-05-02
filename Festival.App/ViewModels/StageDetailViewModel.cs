using Festival.App.Messages;
using Festival.App.Services;
using Festival.App.Services.MessageDialog;
using Festival.App.Wrappers;
using Festival.BL.Models;
using Festival.BL.Repositories;
using System;
using System.Windows.Input;
using Festival.App.Commands;

namespace Festival.App.ViewModels
{
    public class StageDetailViewModel : ViewModelBase, IStageDetailViewModel
    {
        private readonly StageRepository _stageRepository;
        private readonly IMediator _mediator;
        private readonly IMessageDialogService _messageDialogService;

        public StageDetailViewModel(
            StageRepository stageRepository,
            IMessageDialogService messageDialogService,
            IMediator mediator)
        {
            _stageRepository = stageRepository;
            _messageDialogService = messageDialogService;
            _mediator = mediator;

            SaveCommand = new RelayCommand(Save, CanSave);
            DeleteCommand = new RelayCommand(Delete);
        }

        public StageWrapper? Model { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }


        public void Load(Guid id)
        {
            Model = _stageRepository.GetById(id) ?? new StageDetailModel();
        }

        public void Save()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            Model = _stageRepository.InsertOrUpdate(Model.Model);
            _mediator.Send(new UpdatedMessage<StageWrapper> { Model = Model });
        }

        private bool CanSave() =>
            Model != null
            && !string.IsNullOrWhiteSpace(Model.Name)
            && !string.IsNullOrWhiteSpace(Model.PhotoURL)
            && !string.IsNullOrWhiteSpace(Model.Description);

        public void Delete()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be deleted");
            }

            if (Model.Id != Guid.Empty)
            {
                var delete = _messageDialogService.Show(
                    $"Delete",
                    $"Do you want to delete {Model?.Name}?.",
                    MessageDialogButtonConfiguration.YesNo,
                    MessageDialogResult.No);

                if (delete == MessageDialogResult.No) return;

                try
                {
                    _stageRepository.Delete(Model.Id);
                }
                catch
                {
                    var _ = _messageDialogService.Show(
                        $"Deleting of {Model?.Name} failed!",
                        "Deleting failed",
                        MessageDialogButtonConfiguration.OK,
                        MessageDialogResult.OK);
                }

                _mediator.Send(new DeletedMessage<StageWrapper>
                {
                    Model = Model
                });
            }
        }

        public override void LoadInDesignMode()
        {
            base.LoadInDesignMode();
            Model = new StageWrapper(new StageDetailModel
            {
                //Name = "Voda",
                //Description = "Popis vody",
                //PhotoURL = "https://www.pngitem.com/pimgs/m/40-406527_cartoon-glass-of-water-png-glass-of-water.png"
            });
        }
    }
}