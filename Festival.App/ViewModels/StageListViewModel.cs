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
    public class StageListViewModel : ViewModelBase, IStageListViewModel
    {
        private readonly StageRepository _stageRepository; //hopefully alright
        private readonly IMediator _mediator;

        public StageListViewModel(StageRepository stageRepository, IMediator mediator)
        {
            _stageRepository = stageRepository;
            _mediator = mediator;

            StageSelectedCommand = new RelayCommand<StageListModel>(StageSelected);
            StageNewCommand = new RelayCommand(StageNew);

            mediator.Register<UpdatedMessage<StageWrapper>>(StageUpdated);
            mediator.Register<DeletedMessage<StageWrapper>>(StageDeleted);
        }

        public ObservableCollection<StageListModel> Stages { get; set; } = new ObservableCollection<StageListModel>();

        public ICommand StageSelectedCommand { get; }
        public ICommand StageNewCommand { get; }

        private void StageNew() => _mediator.Send(new NewMessage<StageWrapper>());

        private void StageSelected(StageListModel stage) => _mediator.Send(new SelectedMessage<StageWrapper> { Id = stage.Id });

        private void StageUpdated(UpdatedMessage<StageWrapper> _) => Load();

        private void StageDeleted(DeletedMessage<StageWrapper> _) => Load();

        public void Load()
        {
            Stages.Clear();
            var bands = _stageRepository.GetAll();
            Stages.AddRange(bands);
        }
        public override void LoadInDesignMode()
        {
            //Stages.Add(new BandListModel { Name = "Voda", PhotoURL = "https://www.pngitem.com/pimgs/m/40-406527_cartoon-glass-of-water-png-glass-of-water.png" });
        }
    }
}