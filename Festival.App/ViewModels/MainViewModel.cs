using Festival.App.Factories;
using Festival.App.Messages;
using Festival.App.Services;
using Festival.App.Wrappers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Festival.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IFactory<IBandDetailViewModel> _bandDetailViewModelFactory;
        private readonly IFactory<IStageDetailViewModel> _stageDetailViewModelFactory;
        private readonly IFactory<ISlotDetailViewModel> _slotDetailViewModelFactory;
        //Program DetailViewModel?

        public MainViewModel(
            IBandListViewModel bandListViewModel,
            IStageListViewModel stageListViewModel,
            ISlotListViewModel slotListViewModel,
            //Program ListViewModel probably not?

            IMediator mediator,
            IFactory<IBandDetailViewModel> bandDetailViewModelFactory,
            IFactory<IStageDetailViewModel> stageDetailViewModelFactory,
            IFactory<ISlotDetailViewModel> slotDetailViewModelFactory
            //Program DetailViewModel Factory
            )
        {
            _bandDetailViewModelFactory = bandDetailViewModelFactory;
            _stageDetailViewModelFactory = stageDetailViewModelFactory;
            _slotDetailViewModelFactory = slotDetailViewModelFactory;
            //Program DetailViewModel Factory

            BandListViewModel = bandListViewModel;
            BandDetailViewModel = _bandDetailViewModelFactory.Create();

            StageListViewModel = stageListViewModel;
            StageDetailViewModel = _stageDetailViewModelFactory.Create();

            SlotListViewModel = slotListViewModel;
            SlotDetailViewModel = _slotDetailViewModelFactory.Create();

            //Program ListViewModel? and DetailViewModel.Create();

            mediator.Register<NewMessage<BandWrapper>>(OnBandNewMessage);
            mediator.Register<NewMessage<StageWrapper>>(OnStageNewMessage);
            mediator.Register<NewMessage<SlotWrapper>>(OnSlotNewMessage);

            mediator.Register<SelectedMessage<BandWrapper>>(OnBandSelected);
            mediator.Register<SelectedMessage<StageWrapper>>(OnStageSelected);
            mediator.Register<SelectedMessage<SlotWrapper>>(OnSlotSelected);

            mediator.Register<DeletedMessage<BandWrapper>>(OnBandDeleted);
            mediator.Register<DeletedMessage<StageWrapper>>(OnStageDeleted);
            mediator.Register<DeletedMessage<SlotWrapper>>(OnSlotDeleted);

            //Register Program Wrapper probably not
        }

        public IBandListViewModel BandListViewModel { get; }
        public IBandDetailViewModel BandDetailViewModel { get; }

        public IStageListViewModel StageListViewModel { get; }
        public IStageDetailViewModel StageDetailViewModel { get; }

        public ISlotListViewModel SlotListViewModel { get; }
        public ISlotDetailViewModel SlotDetailViewModel { get; }

        //Program ViewModel(s) init?

        public ObservableCollection<IBandDetailViewModel> BandDetailViewModels { get; } =
            new ObservableCollection<IBandDetailViewModel>();

        public ObservableCollection<IStageDetailViewModel> StageDetailViewModels { get; } = 
            new ObservableCollection<IStageDetailViewModel>();

        public ObservableCollection<ISlotDetailViewModel> SlotDetailViewModels { get; } =
            new ObservableCollection<ISlotDetailViewModel>();

        public IBandDetailViewModel SelectedBandDetailViewModel { get; set; }
        public IStageDetailViewModel SelectedStageDetailViewModel { get; set; }
        public ISlotDetailViewModel SelectedSlotDetailViewModel { get; set; }

        private void OnBandNewMessage(NewMessage<BandWrapper> _)
        {
            SelectBand(Guid.Empty);
        }
        private void OnStageNewMessage(NewMessage<StageWrapper> _)
        {
            SelectStage(Guid.Empty);
        }
        private void OnSlotNewMessage(NewMessage<SlotWrapper> _)
        {
            SelectSlot(Guid.Empty);
        }

        private void OnBandSelected(SelectedMessage<BandWrapper> message)
        {
            SelectBand(message.Id);
        }
        private void OnStageSelected(SelectedMessage<StageWrapper> message)
        {
            SelectStage(message.Id);
        }
        private void OnSlotSelected(SelectedMessage<SlotWrapper> message)
        {
            SelectSlot(message.Id);
        }

        private void OnBandDeleted(DeletedMessage<BandWrapper> message)
        {
            var band = BandDetailViewModels.SingleOrDefault(i => i.Model.Id == message.Id);
            if (band != null)
            {
                BandDetailViewModels.Remove(band);
            }
        }
        private void OnStageDeleted(DeletedMessage<StageWrapper> message)
        {
            var stage = StageDetailViewModels.SingleOrDefault(i => i.Model.Id == message.Id);
            if (stage != null)
            {
                StageDetailViewModels.Remove(stage);
            }
        }
        private void OnSlotDeleted(DeletedMessage<SlotWrapper> message)
        {
            var slot = SlotDetailViewModels.SingleOrDefault(i => i.Model.Id == message.Id);
            if (slot != null)
            {
                SlotDetailViewModels.Remove(slot);
            }
        }

        private void SelectBand(Guid id)
        {
            var bandDetailViewModel =
                BandDetailViewModels.SingleOrDefault(vm => vm.Model.Id == id);
            if (bandDetailViewModel == null)
            {
                bandDetailViewModel = _bandDetailViewModelFactory.Create();
                BandDetailViewModels.Add(bandDetailViewModel);
                bandDetailViewModel.Load(id);
            }

            SelectedBandDetailViewModel = bandDetailViewModel;
        }
        private void SelectStage(Guid id)
        {
            var stageDetailViewModel =
                StageDetailViewModels.SingleOrDefault(vm => vm.Model.Id == id);
            if (stageDetailViewModel == null)
            {
                stageDetailViewModel = _stageDetailViewModelFactory.Create();
                StageDetailViewModels.Add(stageDetailViewModel);
                stageDetailViewModel.Load(id);
            }

            SelectedStageDetailViewModel = stageDetailViewModel;
        }
        private void SelectSlot(Guid id)
        {
            var slotDetailViewModel =
                SlotDetailViewModels.SingleOrDefault(vm => vm.Model.Id == id);
            if (slotDetailViewModel == null)
            {
                slotDetailViewModel = _slotDetailViewModelFactory.Create();
                SlotDetailViewModels.Add(slotDetailViewModel);
                slotDetailViewModel.Load(id);
            }

            SelectedSlotDetailViewModel = slotDetailViewModel;
        }
    }
}
