using newSoundBoard.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace newSoundBoard
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Sound> main_sound;
        private List<Icon> main_icon;
        private SolidColorBrush gray = new SolidColorBrush(Colors.Gray);
        private SolidColorBrush red = new SolidColorBrush(Colors.Red);
        private SolidColorBrush dimgray = new SolidColorBrush(Colors.DimGray);
        private SolidColorBrush skybule = new SolidColorBrush(Colors.SkyBlue);
        private SolidColorBrush lightblue = new SolidColorBrush(Colors.LightBlue);
        private SolidColorBrush brown = new SolidColorBrush(Colors.Brown);
        private SolidColorBrush deeppink = new SolidColorBrush(Colors.DeepPink);
        private string value;
        private double opacity_value;
        private AcrylicBrush myBrush = new AcrylicBrush();
        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        private ApplicationDataContainer local_opacity = ApplicationData.Current.LocalSettings;
        public MainPage()
        {
            this.InitializeComponent();
            main_sound = new ObservableCollection<Sound>();
            SoundManager.GetSound(main_sound);
            main_icon = SetIcon.GetIcon();
            back_button.Visibility = Visibility.Collapsed;
        }

        private void list_button_Click(object sender, RoutedEventArgs e)
        {
            main_splitview.IsPaneOpen = !main_splitview.IsPaneOpen;
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.GetSound(main_sound);
            back_button.Visibility = Visibility.Collapsed;
            main_listview.SelectedItem = null;
            main_textblock.Text = "All Sounds";
            type_textblock.Text = "";
            media_element.Stop();
            main_gridview.Visibility = Visibility.Visible;
            main_textblock.Visibility = Visibility.Visible;
            combobox_stackPanel.Visibility = Visibility.Collapsed;
            main_listbox.SelectedIndex = -1;
            auto_suggestbox.Visibility = Visibility.Visible;
        }

        private void main_listview_ItemClick(object sender, ItemClickEventArgs e)
        {
            main_listbox.SelectedIndex = -1;
            main_gridview.Visibility = Visibility.Visible;
            main_textblock.Visibility = Visibility.Visible;
            combobox_stackPanel.Visibility = Visibility.Collapsed;
            back_button.Visibility = Visibility.Visible;
            media_element.Stop();
            var value = (Icon)e.ClickedItem;
            SoundManager.GetCategorySound(main_sound, value.Category);
            string value_str = value.Category.ToString();
            main_textblock.Text = value_str;
            type_textblock.Text = "";
        }

        private void main_gridview_ItemClick(object sender, ItemClickEventArgs e)
        {
            var value = (Sound)e.ClickedItem;
            media_element.Source = new Uri(this.BaseUri, value.AudioFile);
            type_textblock.Text = value.Name.ToString();
            auto_suggestbox.Text = "";

        }

        private async void main_gridview_Drop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();

                if (items.Any())
                {
                    var storageFile = items[0] as StorageFile;
                    var contentType = storageFile.ContentType;

                    StorageFolder folder = ApplicationData.Current.LocalFolder;

                    if (contentType == "audio/wav" || contentType == "audio/mpeg")
                    {
                        StorageFile newFile = await storageFile.CopyAsync(folder, storageFile.Name,
                                NameCollisionOption.GenerateUniqueName);

                        media_element.SetSource(await storageFile.OpenAsync(FileAccessMode.Read), contentType);

                        media_element.Play();
                    }
                }
            }
        }

        private void main_gridview_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;

            e.DragUIOverride.Caption = "drop to create a custom sound and tile";
            e.DragUIOverride.IsCaptionVisible = true;
            e.DragUIOverride.IsContentVisible = true;
            e.DragUIOverride.IsGlyphVisible = true;
        }

        private void auto_suggestbox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (!String.IsNullOrEmpty(auto_suggestbox.Text))
            {
                SoundManager.GetSound(main_sound);
                var value = main_sound.Where(p => p.Name.StartsWith(sender.Text.ToUpper())).Select(p => p.Name).ToList();
                auto_suggestbox.ItemsSource = value;
            }
        }

        private void auto_suggestbox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            SoundManager.GetSoundByName(main_sound, sender.Text);
            back_button.Visibility = Visibility.Visible;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            combobox_stackPanel.Visibility = Visibility.Collapsed;
          
            if (Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.IsSupported())
            {
                this.listbox_itemfeedback.Visibility = Visibility.Visible;
            }
            GetLocalBackground();
            GetLocalOpacity();
        }

        private void GetLocalBackground()
        {
            try
            {
                value = localSettings.Values["Background"].ToString();
            }
            catch
            {

                value = "deeppink";
            }
            switch (value)
            {
                case "gray":
                    myBrush.TintColor = Colors.Gray;
                    myBrush.FallbackColor = Colors.Gray;
                    main_combobox.SelectedIndex = 0;
                    break;
                case "red":
                    myBrush.TintColor = Colors.Red;
                    myBrush.FallbackColor = Colors.Red;
                    main_combobox.SelectedIndex = 1;
                    break;
                case "dimgray":
                    myBrush.TintColor = Colors.DimGray;
                    myBrush.FallbackColor = Colors.DimGray;
                    main_combobox.SelectedIndex = 2;
                    break;
                case "skyblue":
                    myBrush.TintColor = Colors.SkyBlue;
                    myBrush.FallbackColor = Colors.SkyBlue;
                    main_combobox.SelectedIndex = 3;
                    break;
                case "lightblue":
                    myBrush.TintColor = Colors.LightBlue;
                    myBrush.FallbackColor = Colors.LightBlue;
                    main_combobox.SelectedIndex = 4;
                    break;
                case "brown":
                    myBrush.TintColor = Colors.Brown;
                    myBrush.FallbackColor = Colors.Brown;
                    main_combobox.SelectedIndex = 5;
                    break;
                case "deeppink":
                    myBrush.TintColor = Colors.DeepPink;
                    main_combobox.SelectedIndex = 6;
                    myBrush.FallbackColor = Colors.DeepPink;

                    break;
                default: break;
            }
        }
        
        private void GetLocalOpacity()
        {
            try
            {
                opacity_value = (double)localSettings.Values["opacity"];
                setting_slider.Value = (double)opacity_value;
            }
            catch
            {
                opacity_value = 0.3;
                setting_slider.Value = 0.3;
            }

            SetAcrylic(opacity_value);
        }
        private void SameBackgroundMethod(SolidColorBrush color)
        {
            main_grid.Background = color;
            back_button.Background = color;
            list_button.Background = color;
            main_listview.Background = color;
            main_listbox.Background = color;
            content_button.Background = color;
        }

        private async void about_button_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog content = new ContentDialog
            {
                Title = "About this application",
                Content = "As a test release of application\nListening to the sound of nature" +
                "\nImages, sound material is derived from the network",
                IsPrimaryButtonEnabled = true,
                PrimaryButtonText = "OK",
            };
            ContentDialogResult result = await content.ShowAsync();
        }

        private void SetAcrylic(double num)
        {
            #region 设置亚克力背景
            //double nums = num / 100;
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.XamlCompositionBrushBase"))
            {
                myBrush.BackgroundSource = AcrylicBackgroundSource.HostBackdrop;
                myBrush.TintOpacity = num;
                main_grid.Background = myBrush;
                back_button.Background = myBrush;
                list_button.Background = myBrush;
                main_listview.Background = myBrush;
                main_listbox.Background = myBrush;
                content_button.Background = myBrush;

            }
            else
            {
                SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(100, 20, 24, 37));
                main_grid.Background = myBrush;
                back_button.Background = myBrush;
                list_button.Background = myBrush;
                main_listview.Background = myBrush;
                main_listbox.Background = myBrush;
                content_button.Background = myBrush;

            }
            #endregion
        }

        private async void main_listbox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            type_textblock.Text = "";
            main_listview.SelectedIndex = -1;
            if (listbox_itemfeedback.IsSelected)
            {
                var launcher = Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.GetDefault();
                await launcher.LaunchAsync();
            }
            if (listbox_itemSetting.IsSelected)
            {
                back_button.Visibility = Visibility.Visible;
                main_textblock.Visibility = Visibility.Collapsed;
                main_gridview.Visibility = Visibility.Collapsed;
                combobox_stackPanel.Visibility = Visibility.Visible;
                auto_suggestbox.Visibility = Visibility.Collapsed;
            }
            media_element.Stop();
        }

        private void main_combobox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

            var value = (ComboBox)sender;
            var color_value = (ComboBoxItem)value.SelectedItem;
            var color_String = color_value.Content.ToString();
            switch (color_String)
            {
                #region 背景色ComboBox
                case "Gray":
                    myBrush.TintColor = Colors.Gray;
                    myBrush.FallbackColor = Colors.Gray;
                    SetAcrylic(opacity_value);
                    type_textblock.Foreground = new SolidColorBrush(Colors.White);
                    localSettings.Values["Background"] = "gray"; break;
                case "Red":
                    myBrush.TintColor = Colors.Red;
                    myBrush.FallbackColor = Colors.Red;
                    SetAcrylic(opacity_value);
                    type_textblock.Foreground = new SolidColorBrush(Colors.White);
                    localSettings.Values["Background"] = "red"; break;
                case "DimGray":
                    myBrush.TintColor = Colors.DimGray;
                    myBrush.FallbackColor = Colors.DimGray;
                    SetAcrylic(opacity_value);
                    type_textblock.Foreground = new SolidColorBrush(Colors.White);
                    localSettings.Values["Background"] = "dimgray"; break;
                case "SkyBlue":
                    myBrush.TintColor = Colors.SkyBlue;
                    myBrush.FallbackColor = Colors.SkyBlue;
                    SetAcrylic(opacity_value);
                    type_textblock.Foreground = new SolidColorBrush(Colors.Black);
                    localSettings.Values["Background"] = "skyblue"; break;
                case "LightBlue":
                    myBrush.TintColor = Colors.LightBlue;
                    myBrush.FallbackColor = Colors.LightBlue;
                    SetAcrylic(opacity_value);
                    type_textblock.Foreground = new SolidColorBrush(Colors.Black);
                    localSettings.Values["Background"] = "lightblue"; break;
                case "Brown":
                    myBrush.TintColor = Colors.Brown;
                    myBrush.FallbackColor = Colors.Brown;
                    SetAcrylic(opacity_value);
                    type_textblock.Foreground = new SolidColorBrush(Colors.White);
                    localSettings.Values["Background"] = "brown"; break;
                case "DeepPink":
                    myBrush.TintColor = Colors.DeepPink;
                    myBrush.FallbackColor = Colors.DeepPink;
                    SetAcrylic(opacity_value);
                    type_textblock.Foreground = new SolidColorBrush(Colors.Black);
                    localSettings.Values["Background"] = "deeppink"; break;
                default: break;
                    #endregion
            }
        }

        private void setting_slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            var value = (Slider)sender;
            var num = (double)value.Value;
            SetAcrylic(num);
            local_opacity.Values["opacity"] = num;
        }
    }
}
