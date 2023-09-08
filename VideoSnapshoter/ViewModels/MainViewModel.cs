using MVVMUtilities.Abstractions;
using MVVMUtilities.Core;
using MVVMUtilities.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using VideoSnapshoter.Models;
using VideoSnapshoter.Models.Abstractions;
using VideoSnapshoter.Models.Extensions;
using VideoSnapshoter.Models.Services;

namespace VideoSnapshoter.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region DI
        private readonly IFileDialogService<FileOpenAction> fileOpenService;
        private readonly IFileDialogService<FileSaveAction> fileSaveService;
        private readonly IDialogService dialogService;
        private readonly ISnapshotService snapshotService;
        #endregion
        #region DataBindings
        private Uri uri;
        public Uri VideoUri
        {
            get => uri;
            set
            {
                if (uri!=value)
                {
                    uri = value;
                    OnPropertyChanged();
                }
            }
        }       
        private ObservableCollection<Snapshot> snapshots = new();
        public ObservableCollection<Snapshot> Snapshots
        {    
            get => snapshots;
            set
            {
                if (snapshots != value)
                {
                    snapshots = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool isLoaded;
        public bool IsVideoLoaded
        {
            get => isLoaded;
            set
            {
                if (isLoaded != value)
                {
                    isLoaded = value;
                    OnPropertyChanged();
                }
            }
        }
        private Snapshot? selected = null;
        public Snapshot? SelectedSnapshot
        {
            get => selected;
            set
            {
                if (selected != value) 
                {
                    selected = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool isselectedsnapshot;
        public bool IsSelectedSnapshot
        {
            get => isselectedsnapshot;
            set 
            {
                if (isselectedsnapshot != value)
                {
                    isselectedsnapshot = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion
        #region Commands
        public RelayCommand ChooseVideoFile { get; init; }
        public RelayCommand<FrameworkElement> TakeSnapshot { get; init; }
        public RelayCommand MediaOpened { get; init; }
        public RelayCommand MediaFailed { get; init; }
        public RelayCommand SnapshotSelectionChanged { get; init; }
        public RelayCommand SnapshotSave { get; init; }
        #endregion

        public MainViewModel(IFileDialogService<FileOpenAction> fileOpenService, IFileDialogService<FileSaveAction> fileSaveService,
            IDialogService dialogService, ISnapshotService snapshotService) 
        {
            this.fileOpenService = fileOpenService;
            this.fileSaveService = fileSaveService;
            this.dialogService = dialogService;
            this.snapshotService = snapshotService;
            MediaOpened = new RelayCommand(()=> IsVideoLoaded = true);
            MediaFailed = new RelayCommand(()=> IsVideoLoaded = false);
            SnapshotSelectionChanged = new RelayCommand(() => IsSelectedSnapshot = SelectedSnapshot != null);
            ChooseVideoFile = new RelayCommand(
                async () =>
                {
                    try
                    {
                        await Dispatcher.CurrentDispatcher.InvokeAsync(()=>VideoUri = new Uri(fileOpenService.GetFileName()));
                    }
                    catch (FileNotChooseException ex)
                    {
                        dialogService.ShowErrorMessage(ex.Message, "Ошибка");
                    }                   
                });
            TakeSnapshot = new RelayCommand<FrameworkElement>(
                async (FrameworkElement element) =>
                {
                    await Dispatcher.CurrentDispatcher.InvokeAsync(() =>
                    {
                        var snapshot = snapshotService.MakeSnapshot(element);
                        Snapshots.Add(new Snapshot
                        {
                            DateTimeString = DateTime.Now.ToString("ddMMyyyy HH:mm:ss"),
                            SnapshotOriginal = snapshot,
                            Image = BitmapExtensions.Resize(snapshot, newHeight: 200, newWidth: 300)
                        });
                    });
                });
            SnapshotSave = new RelayCommand(
                async () =>
                {
                    try
                    {
                        await Dispatcher.CurrentDispatcher.InvokeAsync(()=>
                            SelectedSnapshot.SnapshotOriginal.Save(fileSaveService.GetFileName()));
                    }
                    catch (FileNotChooseException ex)
                    {
                        dialogService.ShowErrorMessage(ex.Message, "Ошибка");
                    }
                });
        }
    }
}
