using Microsoft.Extensions.DependencyInjection;
using MVVMUtilities.Abstractions;
using MVVMUtilities.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VideoSnapshoter.Models.Abstractions;
using VideoSnapshoter.Models.Services;
using VideoSnapshoter.ViewModels;
using VideoSnapshoter.Views;

namespace VideoSnapshoter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<IFileDialogService<FileOpenAction>, OpenFileDialogService>((serviceProvider) =>
            {
                return new OpenFileDialogService("Видео", "*.mp4");
            });
            services.AddSingleton<IFileDialogService<FileSaveAction>, SaveFileDialogService>((serviceProvider) =>
            {
                return new SaveFileDialogService("Изображение", "*.png");
            });
            services.AddSingleton<INavigationService, NavigationService>(serviceProvider =>
            {
                var navigationService = new NavigationService(serviceProvider);
                navigationService.ConfigureWindow<MainViewModel, MainWindow>();
                return navigationService;
            });
            services.AddSingleton<ISnapshotService, SnapshotService>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var service = serviceProvider.GetRequiredService<INavigationService>();
            service.Show<MainViewModel>();
        }
    }
}
