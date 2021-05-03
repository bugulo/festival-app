using System;
using System.Windows;

using Festival.App.Extensions;
using Festival.App.Services;
using Festival.App.Services.MessageDialog;
using Festival.App.ViewModels;
using Festival.App.Views;

using Festival.BL.Repositories;

using Festival.DAL.Factories;
using Festival.DAL.Interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Festival.App
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices((context, services) => { ConfigureServices(context.Configuration, services); })
                .Build();
        }

        private static void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
        {
            builder.AddJsonFile(@"settings.json", false, true);
        }

        private static void ConfigureServices(IConfiguration configuration,
            IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();

            services.AddSingleton<BandRepository>();
            services.AddSingleton<SlotRepository>();
            services.AddSingleton<StageRepository>();

            services.AddSingleton<IMessageDialogService, MessageDialogService>();
            services.AddSingleton<IMediator, Mediator>();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<IBandListViewModel, BandListViewModel>();
            services.AddFactory<IBandDetailViewModel, BandDetailViewModel>();
            services.AddSingleton<IStageListViewModel, StageListViewModel>();
            services.AddFactory<IStageDetailViewModel, StageDetailViewModel>();
            services.AddFactory<ISlotDetailViewModel, SlotDetailViewModel>();
            services.AddSingleton<ISlotListViewModel, SlotListViewModel>();

            services.AddSingleton<IDbContextFactory>(provider => new SqlServerDbContextFactory(configuration.GetConnectionString("DefaultConnection")));
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}
