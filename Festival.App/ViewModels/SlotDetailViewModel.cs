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
    public class SlotDetailViewModel : ViewModelBase, ISlotDetailViewModel
    {
        private readonly SlotRepository _slotRepository;
        private readonly IMediator _mediator;
        private readonly IMessageDialogService _messageDialogService;

        public BandListViewModel BandList   { get; set; }
        public StageListViewModel StageList { get; set; }

        public SlotDetailViewModel(
            SlotRepository slotRepository,
            IBandListViewModel bandListViewModel,
            IStageListViewModel stageListViewModel,
            IMessageDialogService messageDialogService,
            IMediator mediator)
        {
            _slotRepository = slotRepository;
            _messageDialogService = messageDialogService;
            _mediator = mediator;

            StageList = (StageListViewModel) stageListViewModel;
            BandList = (BandListViewModel) bandListViewModel;

            SaveCommand = new RelayCommand(Save, CanSave);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
        }

        private SlotWrapper _model;
        public SlotWrapper Model { 
            get => _model; 
            set {
                _model = value;
                OnPropertyChanged();
            }
        }

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

            var result = _slotRepository.InsertOrUpdate(Model.Model);
            if(result == null)
            {
                _messageDialogService.Show(
                    $"Error",
                    $"This slot collides with other slot",
                    MessageDialogButtonConfiguration.OK,
                    MessageDialogResult.OK);
            }
            else
            {
                Model = result;
                _mediator.Send(new UpdatedMessage<SlotWrapper> { Model = Model });
            }
        }

        private bool CanSave() =>
            Model != null
            && Model.StartAt < Model.FinishAt
            && Model.StageId != Guid.Empty
            && Model.BandId != Guid.Empty;

        private bool CanDelete() =>
            Model != null && Model.Id != Guid.Empty;

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