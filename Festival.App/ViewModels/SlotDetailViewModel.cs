using Festival.App.Messages;
using Festival.App.Services;
using Festival.App.Services.MessageDialog;
using Festival.App.Wrappers;
using Festival.BL.Models;
using Festival.BL.Repositories;
using System;
using System.Windows.Input;
using Festival.App.Commands;
using System.Globalization;

namespace Festival.App.ViewModels
{
    public class SlotDetailViewModel : ViewModelBase, ISlotDetailViewModel
    {
        private readonly SlotRepository _slotRepository;
        private readonly IMediator _mediator;
        private readonly IMessageDialogService _messageDialogService;

        public SlotDetailViewModel(
            SlotRepository slotRepository,
            IMessageDialogService messageDialogService,
            IMediator mediator)
        {
            _slotRepository = slotRepository;
            _messageDialogService = messageDialogService;
            _mediator = mediator;

            SaveCommand = new RelayCommand(Save, CanSave);
            DeleteCommand = new RelayCommand(Delete);
        }

        public SlotWrapper? Model { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }


        public void Load(Guid id)
        {
            Model = _slotRepository.GetById(id) ?? new SlotDetailModel();
        }

        public void Save()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            Model = _slotRepository.InsertOrUpdate(Model.Model);
            _mediator.Send(new UpdatedMessage<SlotWrapper> { Model = Model });
        }

        private bool CanSave() =>
            Model != null
            && !DateTime.TryParseExact(Model.StartAt.ToString(), "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime start)
            && !DateTime.TryParseExact(Model.FinishAt.ToString(), "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime finish);
        //test this
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
                    $"Do you want to delete time slot starting at {Model?.StartAt}?.",
                    MessageDialogButtonConfiguration.YesNo,
                    MessageDialogResult.No);

                if (delete == MessageDialogResult.No) return;

                try
                {
                    _slotRepository.Delete(Model.Id);
                }
                catch
                {
                    var _ = _messageDialogService.Show(
                        $"Deleting of time slot {Model?.StartAt} failed!",
                        "Deleting failed",
                        MessageDialogButtonConfiguration.OK,
                        MessageDialogResult.OK);
                }

                _mediator.Send(new DeletedMessage<SlotWrapper>
                {
                    Model = Model
                });
            }
        }
        public override void LoadInDesignMode()
        {
            base.LoadInDesignMode();
            Model = new SlotWrapper(new SlotDetailModel
            {
                //Name = "Voda",
                //Description = "Popis vody",
                //PhotoURL = "https://www.pngitem.com/pimgs/m/40-406527_cartoon-glass-of-water-png-glass-of-water.png"
            });
        }
    }
}