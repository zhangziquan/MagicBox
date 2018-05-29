using System;
using Windows.System;
using MagicBox.Models;
using MagicBox.ViewModels;
using Windows.Media.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.Storage.Streams;
using Windows.Storage;
using Windows.UI.Popups;
using System.Text;
using SQLitePCL;

namespace MagicBox.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel
        {
            get { return DataContext as MainViewModel; }
        }

        public MainPage()
        {
            InitializeComponent();
            ViewModel.initiate();
        }

        private void ListViewItemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel.SelectedItem = (Model)e.ClickedItem;
            if (this.ActualWidth < 80)
            {
                //Frame.Navigate(typeof(DetailPage));
            }
            else
            {
                photoImageDetail.Source = new BitmapImage(ViewModel.SelectedItem.photoUri);
                moodTextBlockDetail.Text = ViewModel.SelectedItem.mood;
                dateDatePickerDetail.Date = ViewModel.SelectedItem.date;
                diaryTextBoxDetail.Text = ViewModel.SelectedItem.diary;
                feedbackTextBlock.Text = ViewModel.SelectedItem.feedback;
                mediaElement.Source = ViewModel.SelectedItem.songUri;
                musicNameTextBlock.Text = ViewModel.SelectedItem.musicName;
               /// Play.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
               // Pause.Visibility = Windows.UI.Xaml.Visibility.Visible;
                createButton.Content = "Update";
            }
        }

        /* private void PlayClick(object sender, RoutedEventArgs e)
           {
               Play.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
               Pause.Visibility = Windows.UI.Xaml.Visibility.Visible;
               mediaElement.Play();
           }

           private void PauseClick(object sender, RoutedEventArgs e)
           {
               Pause.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
               Play.Visibility = Windows.UI.Xaml.Visibility.Visible;
               mediaElement.Pause();
           }
           */
        private async void SelectClick(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.List;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".mp3");
            picker.FileTypeFilter.Add(".wma");
            var file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                string type = file.FileType;
                IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read);
                if (type == ".mp3" || type == ".wma")
                {
                  //  mediaElement.Visibility = Visibility.Collapsed;
                //    Play.Visibility = Visibility.Collapsed;
                 //   Pause.Visibility = Visibility.Visible;
                    await file.CopyAsync(ApplicationData.Current.LocalFolder, file.Name, NameCollisionOption.ReplaceExisting);
                    Uri uri = new Uri("ms-appdata:///local/" + file.Name);
                    musicNameTextBlock.Text = file.Name;
                    mediaElement.Source = uri;
                    mediaElement.Play();
                }
            }
        }

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            if(diaryTextBoxDetail.Text.Trim() == String.Empty)
            {
                var message = new MessageDialog("日记内容未填写！").ShowAsync();
                return;
            }
            if (mediaElement.Source == null) {
                var message = new MessageDialog("歌曲未选择！").ShowAsync();
                return;
            }

            if(createButton.Content.ToString() == "Update")
            {
                ViewModel.updateItem(ViewModel.SelectedItem.getId(), mediaElement.Source,(photoImageDetail.Source as BitmapImage).UriSource,
                    moodTextBlockDetail.Text, diaryTextBoxDetail.Text, feedbackTextBlock.Text, musicNameTextBlock.Text);
                var message = new MessageDialog("更新成功！").ShowAsync();
                ViewModel.SelectedItem = null;
                Frame.Navigate(typeof(MainPage));
            }
            else if(createButton.Content.ToString() == "Create")
            {
                ViewModel.AddItem(mediaElement.Source, (photoImageDetail.Source as BitmapImage).UriSource,
                    moodTextBlockDetail.Text, diaryTextBoxDetail.Text, feedbackTextBlock.Text, musicNameTextBlock.Text);
                var message = new MessageDialog("创建成功！").ShowAsync();
                Frame.Navigate(typeof(MainPage));
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedItem = null;
            Frame.Navigate(typeof(MainPage));
        }

        private void DeleteClick(object sender, RoutedEventArgs e) {
            var message = new MessageDialog("删除日记").ShowAsync();
            diaryTextBoxDetail.Text =  "diary";
            if (ViewModel.SelectedItem.getId() != null)
            ViewModel.deleteItem(ViewModel.SelectedItem.getId());
   
        }

        private async void PhotoClick(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.List;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");
            var picture = await picker.PickSingleFileAsync();
            if (picture != null)
            {
                string temp = picture.Name;
                await picture.CopyAsync(ApplicationData.Current.LocalFolder, temp, NameCollisionOption.GenerateUniqueName);
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appdata:///local/" + temp));
                photoImageDetail.Source = bitmapImage;
            }
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            StringBuilder strBuilder = new StringBuilder("%%");
            strBuilder.Insert(1, searchTextBox.Text);
            String result = String.Empty;

            var db = App.connection;
            using (var statement = db.Prepare(App.SEARCH))
            {
                statement.Bind(1, strBuilder.ToString());
                statement.Bind(2, strBuilder.ToString());
                statement.Bind(3, strBuilder.ToString());
                
                while (SQLiteResult.ROW == statement.Step())
                {
                    if(statement[0].ToString() == App.userName)
                    {
                        result += "日期: " + statement[1].ToString() + " ";
                        result += "歌名: " + statement[2].ToString() + " ";
                        result += "心情: " + statement[3].ToString() + "\n";
                    }                 
                }
            }
            if(result != "")
            {
                var message = new MessageDialog(result).ShowAsync();
            }
            else
            {
                var message = new MessageDialog("没有查找到相关内容！").ShowAsync();
            }
            
            
        }
    }
}
