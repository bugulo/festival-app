using Festival.App.Commands;
using Festival.App.Messages;
using Festival.App.Services;
using Festival.App.Services.MessageDialog;
using Festival.App.Wrappers;
using Festival.BL.Models;
using Festival.BL.Repositories;
using System;
using System.Windows.Input;

namespace Festival.App.ViewModels
{
    public class BandDetailViewModel : ViewModelBase, IBandDetailViewModel
    {
        private readonly BandRepository _bandRepository;
        private readonly IMediator _mediator;
        private readonly IMessageDialogService _messageDialogService;

        public BandDetailViewModel(
            BandRepository bandRepository,
            IMessageDialogService messageDialogService,
            IMediator mediator)
        {
            _bandRepository = bandRepository;
            _messageDialogService = messageDialogService;
            _mediator = mediator;

            SaveCommand = new RelayCommand(Save, CanSave);
            DeleteCommand = new RelayCommand(Delete);
        }

        public BandWrapper Model { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }


        public void Load(Guid id)
        {
            Model = _bandRepository.GetById(id) ?? new BandDetailModel();
        }

        public void Save()
        {
            Model = _bandRepository.InsertOrUpdate(Model.Model);
            _mediator.Send(new UpdatedMessage<BandWrapper> { Model = Model });
        }

        private bool CanSave() =>
            Model != null
            && !string.IsNullOrWhiteSpace(Model.Name)
            && !string.IsNullOrWhiteSpace(Model.Genre)
            && !string.IsNullOrWhiteSpace(Model.PhotoURL)
            //Festival(.Common?) Enums Country
            && !string.IsNullOrWhiteSpace(Model.Description);
            //Program Description probably not

        public void Delete()
        {
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
                    _bandRepository.Delete(Model.Id);
                }
                catch
                {
                    var _ = _messageDialogService.Show(
                        $"Deleting of {Model?.Name} failed!",
                        "Deleting failed",
                        MessageDialogButtonConfiguration.OK,
                        MessageDialogResult.OK);
                }

                _mediator.Send(new DeletedMessage<BandWrapper>
                {
                    Model = Model
                });
            }
        }
        public override void LoadInDesignMode()
        {
            base.LoadInDesignMode();
            Model = new BandWrapper(new BandDetailModel
            {
                //Name = "Voda",
                //Description = "Popis vody",
                //PhotoURL = "https://www.pngitem.com/pimgs/m/40-406527_cartoon-glass-of-water-png-glass-of-water.png"
            });
        }
    }
}