using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ARTorch.Classes;
using Windows.ApplicationModel.Store;

namespace ARTorch.Pages
{
    public partial class SoundPick
    {
        private AppSettings AppSettings { get; set; }
        private TorchPacks.Pack CurrentPack { get; set; }

        public SoundPick()
        {
            InitializeComponent();
        }

        //Code for initialization, capture completed, image availability events; also setting the source for the viewfinder.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            AppSettings = new AppSettings();

            CurrentPack = AppSettings.CurrentTorchSetSetting;

            var currentPackButton = (Button)FindName(string.Format("Show{0}", CurrentPack.ToString()));
            if(currentPackButton != null) ResetBorders(currentPackButton);

            SetSource();

            // Initializes SoundEffects class
            SoundEffects.Initialize();
            SoundEffects.SfxSilence.Play();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            AppSettings = null;
        }

        private void ResetBorders(Button activeButton)
        {
            var buttons = ContentPanel.Children.OfType<Button>();
            foreach (var button in buttons)
            {
                button.BorderBrush = (Brush) Application.Current.Resources["PhoneAccentBrush"];
            }
            activeButton.BorderBrush = (Brush)Application.Current.Resources["PhoneBorderBrush"];
        }


        private void ShowDefault_Click(object sender, RoutedEventArgs e)
        {
            ResetBorders((Button)sender);
            CurrentPack = TorchPacks.Pack.Torches;
            SetSource();
        }

        private void ShowGross_Click(object sender, RoutedEventArgs e)
        {
            ResetBorders((Button)sender);
            CurrentPack = TorchPacks.Pack.Gross;
            SetSource();
        }


        private void ShowAnimals_Click(object sender, RoutedEventArgs e)
        {
            ResetBorders((Button)sender);
            CurrentPack = TorchPacks.Pack.Animals;
            SetSource();
        }

        private void ShowSciFi_Click(object sender, RoutedEventArgs e)
        {
            ResetBorders((Button)sender);
            CurrentPack = TorchPacks.Pack.SciFi;
            SetSource();
        }

        private bool IsOwned()
        {
            var isOwned = false;

            var currentPack = TorchPacks.GetPack(CurrentPack);
            if (currentPack.IsOwned || AppSettings.LicensesSetting.Contains(currentPack.ProductId))
            {
                isOwned = true;
            }
            else
            {
                var license = CurrentApp.LicenseInformation.ProductLicenses[currentPack.ProductId];

                if (license.IsActive)
                {
                    // User owns this pack
                    isOwned = true;
                }
            }

            return isOwned;
        }

        private void SetSource()
        {
            var currentPack = TorchPacks.GetPack(CurrentPack);

            if(IsOwned())
            {
                AvailablePacks.Height = 420;
                ApplicationBar.IsVisible = false;
                BuyBlocker.Visibility = Visibility.Collapsed;
            }
            else
            {
                AvailablePacks.Height = 360;
                ApplicationBar.IsVisible = true;
                BuyBlocker.Visibility = Visibility.Visible;
            }

            this.AvailablePacks.ItemsSource = currentPack.Torches;
            AppSettings.CurrentTorchSetSetting = CurrentPack;

        }

        private void SoundSample_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var button = (Rectangle)sender;
            if (button.Tag != null)
            {
                SoundEffects.PlaySound(new Uri(button.Tag.ToString(), UriKind.Relative));
            }
            else
            {
                SoundEffects.PlayError();
            }

        }

        private void SelectSound_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var torchPack = TorchPacks.GetPack(CurrentPack);

            if (IsOwned())
            {
                var torches = torchPack.Torches;
                var selectedTorch = torches.SingleOrDefault(s => s.Title == (string) button.Tag);

                if (selectedTorch != null)
                {
                    AppSettings.SoundOnSetting = selectedTorch.OnSoundLocation;
                    AppSettings.SoundOffSetting = selectedTorch.OffSoundLocation;
                    AppSettings.ImageOnSetting = selectedTorch.OnImageLocation;
                    AppSettings.ImageOffSetting = selectedTorch.OffImageLocation;

                    NavigationService.Navigate(new Uri("/Pages/MainPage.xaml", UriKind.Relative));
                }
            }
            else
            {
                MessageBox.Show("You do not own this torch pack. Hit the 'buy' button to purchase it");
            }
        }

        private void BuyPack_Click(object sender, EventArgs e)
        {
            PurchaseProduct(TorchPacks.GetPack(CurrentPack).ProductId);
        }

        async void PurchaseProduct(string productId)
        {
            try
            {
                // Kick off purchase; don't ask for a receipt when it returns
                await CurrentApp.RequestProductPurchaseAsync(productId, false);

                // Now that purchase is done, give the user the goods they paid for
                DoFulfillment();
            }
            catch (Exception ex)
            {
                // When the user does not complete the purchase (e.g. cancels or navigates back from the Purchase Page), an exception with an HRESULT of E_FAIL is expected.
                //MessageBox.Show("Sorry, an error occured during processing. Please try again later.\n\n:(");
            }
        }

        public void DoFulfillment()
        {
            var productLicenses = CurrentApp.LicenseInformation.ProductLicenses;

            AppSettings.LicensesSetting = string.Format("{0},{1}", AppSettings.LicensesSetting, productLicenses.ToString());

        }

    }
}