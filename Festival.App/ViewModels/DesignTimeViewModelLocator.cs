using Festival.App.Factories;
using Festival.App.Services;
using Festival.App.Services.MessageDialog;
using Festival.BL.Repositories;
using Festival.DAL.Factories;

namespace Festival.App.ViewModels
{
    public class DesignTimeViewModelLocator
    {
        //private const string DesignTimeConnectionString = @"inMemory:Festival"; //??

        public BandListViewModel BandListViewModel { get; }
        public StageListViewModel StageListViewModel { get; }
        public SlotListViewModel SlotListViewModel { get; }
        public BandDetailViewModel BandDetailViewModel { get; set; }
        public StageDetailViewModel StageDetailViewModel { get; set; }
        public SlotDetailViewModel SlotDetailViewModel { get; set; }

        public DesignTimeViewModelLocator()
        {
            var bandRepository = new BandRepository(new DesignTimeDbContextFactory());
            var stageRepository = new StageRepository(new DesignTimeDbContextFactory());
            var slotRepository = new SlotRepository(new DesignTimeDbContextFactory());
            var mediator = new Mediator();
            var messageDialogService = new MessageDialogService();

            BandListViewModel = new BandListViewModel(bandRepository, mediator);
            StageListViewModel = new StageListViewModel(stageRepository, mediator);
            SlotListViewModel = new SlotListViewModel(slotRepository, mediator);
            
            BandDetailViewModel = new BandDetailViewModel(bandRepository, messageDialogService, mediator);
            StageDetailViewModel = new StageDetailViewModel(stageRepository, messageDialogService, mediator);
            SlotDetailViewModel = new SlotDetailViewModel(slotRepository, messageDialogService, mediator);
        }
    }
}