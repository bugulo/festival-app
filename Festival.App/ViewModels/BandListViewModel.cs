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
    public class BandListViewModel : ViewModelBase, IBandListViewModel
    {
        private readonly BandRepository _bandRepository; //hopefully alright
        private readonly IMediator _mediator;

        public BandListViewModel(BandRepository bandRepository, IMediator mediator)
        {
            _bandRepository = bandRepository;
            _mediator = mediator;

            BandSelectedCommand = new RelayCommand<BandListModel>(BandSelected);
            BandNewCommand = new RelayCommand(BandNew);

            mediator.Register<UpdatedMessage<BandWrapper>>(BandUpdated);
            mediator.Register<DeletedMessage<BandWrapper>>(BandDeleted);
        }

        public ObservableCollection<BandListModel> Bands { get; set; } = new ObservableCollection<BandListModel>();

        public ICommand BandSelectedCommand { get; }
        public ICommand BandNewCommand { get; }

        private void BandNew() => _mediator.Send(new NewMessage<BandWrapper>());

        private void BandSelected(BandListModel band) => _mediator.Send(new SelectedMessage<BandWrapper> { Id = band.Id });

        private void BandUpdated(UpdatedMessage<BandWrapper> _) => Load();

        private void BandDeleted(DeletedMessage<BandWrapper> _) => Load();

        public void Load()
        {
            Bands.Clear();
            var bands = _bandRepository.GetAll();
            Bands.AddRange(bands);
        }
        public override void LoadInDesignMode()
        {
            //Bands.Add(new BandListModel { Name = "Voda", PhotoURL = "https://www.pngitem.com/pimgs/m/40-406527_cartoon-glass-of-water-png-glass-of-water.png" });
        }
    }
}