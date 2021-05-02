using Festival.App.Commands;
using Festival.App.Extensions;
using Festival.App.Messages;
using Festival.App.Services;
using Festival.App.Wrappers;
using Festival.BL.Models;
using Festival.BL.Repositories;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Festival.App.ViewModels
{
    public class SlotListViewModel : ViewModelBase, ISlotListViewModel
    {
        private readonly SlotRepository _slotRepository; //hopefully alright
        private readonly IMediator _mediator;

        public SlotListViewModel(SlotRepository slotRepository, IMediator mediator)
        {
            _slotRepository = slotRepository;
            _mediator = mediator;

            SlotSelectedCommand = new RelayCommand<SlotListModel>(SlotSelected);
            SlotNewCommand = new RelayCommand(SlotNew);

            mediator.Register<UpdatedMessage<SlotWrapper>>(SlotUpdated);
            mediator.Register<DeletedMessage<SlotWrapper>>(SlotDeleted);
        }

        public ObservableCollection<SlotListModel> Slots { get; set; } = new ObservableCollection<SlotListModel>();

        public ICommand SlotSelectedCommand { get; }
        public ICommand SlotNewCommand { get; }

        private void SlotNew() => _mediator.Send(new NewMessage<SlotWrapper>());

        private void SlotSelected(SlotListModel slot) => _mediator.Send(new SelectedMessage<SlotWrapper> { Id = slot.Id });

        private void SlotUpdated(UpdatedMessage<SlotWrapper> _) => Load();

        private void SlotDeleted(DeletedMessage<SlotWrapper> _) => Load();

        public void Load()
        {
            Slots.Clear();
            var slots = _slotRepository.GetAll();
            Slots.AddRange(slots);
        }
        public override void LoadInDesignMode()
        {
            //Slots.Add(new BandListModel { Name = "Voda", PhotoURL = "https://www.pngitem.com/pimgs/m/40-406527_cartoon-glass-of-water-png-glass-of-water.png" });
        }
    }
}
