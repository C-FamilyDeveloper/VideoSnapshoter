using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using VideoSnapshoter.Core;
using VideoSnapshoter.Models;
using VideoSnapshoter.Models.Exceptions;
using VideoSnapshoter.Models.Logics;
using VideoSnapshoter.Models.Services;

namespace VideoSnapshoter.ViewModels
{
    public class MainViewModel : ObservableObject
    {
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
        
        public RelayCommand<object> ChooseVideoFile { get; init; }
        public RelayCommand<FrameworkElement> TakeSnapshot { get; init; }
        public RelayCommand<object> MediaOpened { get; init; }
        public RelayCommand<object> MediaFailed { get; init; }
        public RelayCommand<object> SnapshotSelectionChanged { get; init; }
        public RelayCommand<object> SnapshotSave { get; init; }

        public MainViewModel() 
        {
            MediaOpened = new RelayCommand<object>((i) => IsVideoLoaded = true);
            MediaFailed = new RelayCommand<object>((i) => IsVideoLoaded = false);
            SnapshotSelectionChanged = new RelayCommand<object>((i) => IsSelectedSnapshot = SelectedSnapshot != null);
            ChooseVideoFile = new RelayCommand<object>(
                async (i) =>
                {
                    var dialogService = new OpenFileDialogService("Видео", "*.mp4");
                    try
                    {
                        VideoUri = new Uri(dialogService.GetFileName());
                        await Task.Delay(1);
                    }
                    catch (FileNotChooseException ex)
                    {
                        MessageBox.Show(ex.Message, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }                   
                });
            TakeSnapshot = new RelayCommand<FrameworkElement>(
                async (FrameworkElement element) =>
                {
                    Snapshots.Add(new Snapshot
                    {
                        DateTimeString = DateTime.Now.ToString("ddMMyyyy HH:mm:ss"),
                        SnapshotOriginal = Snapshoter.MakeSnapshot(element),
                        Image = BitmapResizerService.Resize(Snapshoter.MakeSnapshot(element),
                            newHeight:200, newWidth:300)
                    }); 
                    await Task.Delay(1);
                });
            SnapshotSave = new RelayCommand<object>(
                async (i) =>
                {
                    try
                    {
                        var dialogService = new SaveFileDialogService("Изображение","*.png");
                        ImageFileCreator.Save(SelectedSnapshot, dialogService.GetFileName());
                        await Task.Delay(1);
                    }
                    catch (FileNotChooseException ex)
                    {
                        MessageBox.Show(ex.Message, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);               
                    }
                });
        }
    }
}
