using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ARTorch.Classes;
using Microsoft.Devices;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Media;
using Windows.Phone.Media.Capture;

namespace ARTorch.Pages
{
    public partial class MainPage
    {
        private AudioVideoCaptureDevice _captureDevice;
        private bool _flashOn;
        private const CameraSensorLocation SensorLocation = CameraSensorLocation.Back;
        //private const string AppId = "19ccf7cc-0a11-4de3-8e9a-1e6775213025";

        //private readonly SolidColorBrush _themeHighlight = (SolidColorBrush) Application.Current.Resources["PhoneAccentBrush"];
        private ImageBrush _torchOnImage;
        private ImageBrush _torchOffImage;

        private string soundOn;
        private string soundOff;

        private AppSettings _appSettings;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // For DigitalDash
            //AdControl.ApplicationId = "ece8f94a-6219-4c57-9f4b-79b074c0111f";
            //AdControl.AdUnitId = "136376";

            //AdControl.ApplicationId = "c517297d-61fb-45e2-92bc-ad6c2889f5d0";
            //AdControl.AdUnitId = "136627";

            //AdControl.ApplicationId = "test_client";
            //AdControl.AdUnitId = "Image480_80";
            
            //AdControl.Width = 480;
            //AdControl.Height = 80;

        }


        //Code for initialization, capture completed, image availability events; also setting the source for the viewfinder.
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Initializes SoundEffects class
            SoundEffects.Initialize();

            _appSettings = new AppSettings();

            _torchOnImage = new ImageBrush { ImageSource = new BitmapImage(new Uri(_appSettings.ImageOnSetting, UriKind.Relative)) };
            _torchOffImage = new ImageBrush { ImageSource = new BitmapImage(new Uri(_appSettings.ImageOffSetting, UriKind.Relative)) };

            soundOn = _appSettings.SoundOnSetting;
            soundOff = _appSettings.SoundOffSetting;

            TorchButton.Background = _appSettings.TorchInitStateSetting ? _torchOnImage : _torchOffImage;

            SetBatteryIcon();

            InitialiseCaptureDevice();

            // The event is fired when the shutter button receives a full press.
            CameraButtons.ShutterKeyPressed += OnButtonFullPress;

            // Plays a silent wav to init the audio device
            if (_appSettings.EnableSoundSetting) SoundEffects.SfxSilence.Play();
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            CameraButtons.ShutterKeyPressed -= OnButtonFullPress;

            if (!_appSettings.OperateUnderLockscreenSetting && _captureDevice != null)
            {
                _captureDevice.Dispose();
                _captureDevice = null;
            }
        }

        private async void InitialiseCaptureDevice()
        {
            if (_captureDevice == null)
            {
                _captureDevice = await GetCaptureDevice();
            }

            viewfinderBrush.RelativeTransform = new CompositeTransform { CenterX = 0.5, CenterY = 0.5, Rotation = 90 };
            viewfinderBrush.SetSource(_captureDevice);

            // Turn the LED on when loading up...
            if (_appSettings.TorchInitStateSetting)
            {
                _flashOn = false;
                ToggleFlash();
            }

        }
        private async Task<AudioVideoCaptureDevice> GetCaptureDevice()
        {
            return await AudioVideoCaptureDevice.OpenAsync(SensorLocation, AudioVideoCaptureDevice.GetAvailableCaptureResolutions(SensorLocation).First());
        }

        private void SetBatteryIcon()
        {
            var batteryPercent = Windows.Phone.Devices.Power.Battery.GetDefault().RemainingChargePercent;
            BatteryIcon.Width = batteryPercent * 0.5;
            BatteryPercent.Text = string.Format("{0}%", batteryPercent);
        }

        private void TorchButton_Click(object sender, RoutedEventArgs e)
        {
            if (_captureDevice != null)
            {
                ToggleFlash();
            }
        }

        // Toggle torch with the hardware camera button
        private void OnButtonFullPress(object sender, EventArgs e)
        {
            if (_appSettings.EnableHardwareButtonSetting)
            {
                ToggleFlash();
            }
        }

        /// <summary>
        /// Here's where the magic happens. 
        /// </summary>
        private void ToggleFlash()
        {
            try
            {
                var vibrator = VibrateController.Default;

                var supportedCameraModes = AudioVideoCaptureDevice.GetSupportedPropertyValues(SensorLocation, KnownCameraAudioVideoProperties.VideoTorchMode);
               
                if (supportedCameraModes.ToList().Contains((UInt32)VideoTorchMode.On))
                {
                    if (!_flashOn)
                    {
                        _captureDevice.SetProperty(KnownCameraAudioVideoProperties.VideoTorchMode, VideoTorchMode.On);
                        _captureDevice.SetProperty(KnownCameraAudioVideoProperties.VideoTorchPower, AudioVideoCaptureDevice.GetSupportedPropertyRange(SensorLocation, KnownCameraAudioVideoProperties.VideoTorchPower).Max);

                        if(_appSettings.EnableSoundSetting) SoundEffects.PlaySound(new Uri(soundOn, UriKind.Relative));

                        if (_appSettings.EnableVibrateSetting && vibrator != null) vibrator.Start(new TimeSpan(0, 0, 0, 0, 20));

                        TorchButton.Background = _torchOnImage;

                        _flashOn = true;
                    }
                    else
                    {
                        _captureDevice.SetProperty(KnownCameraAudioVideoProperties.VideoTorchMode, VideoTorchMode.Off);

                        if (_appSettings.EnableSoundSetting) SoundEffects.PlaySound(new Uri(soundOff, UriKind.Relative));

                        if (_appSettings.EnableVibrateSetting && vibrator != null) vibrator.Start(new TimeSpan(0, 0, 0, 0, 10));

                        TorchButton.Background = _torchOffImage;

                        _flashOn = false;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugText.Text = ex.Message;
            }

            // Update battery icon
            SetBatteryIcon();
        }

        private void ContactDev_Click(object sender, RoutedEventArgs e)
        {
            var emailComposeTask = new EmailComposeTask();
            emailComposeTask.To = "soutarm@gmail.com";
            emailComposeTask.Body = "";
            emailComposeTask.Subject = "AR Torch Feedback";
            emailComposeTask.Show();
        }

        private void ToggleCameraButtonTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (viewfinderCanvas.Visibility == Visibility.Visible)
            {
                viewfinderCanvas.Visibility = Visibility.Collapsed;
                ToggleCameraButton.Opacity = 1.0;
            }
            else
            {
                viewfinderCanvas.Visibility = Visibility.Visible;
                ToggleCameraButton.Opacity = 0.5;
            }
        }

        private void SettingsButtonTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            SettingsButton.Opacity = SettingsButton.Opacity == 1.0 ? 0.5 : 1.0;

            SettingsView.Visibility = SettingsView.Visibility == Visibility.Visible
                                          ? Visibility.Collapsed
                                          : Visibility.Visible;
        }

        private void RateApp_Click(object sender, RoutedEventArgs e)
        {
            //Open app in review page
            new MarketplaceReviewTask().Show();
        }

        private void ScreenGrabTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var fileName = String.Format("ARTorch_{0:}.jpg", DateTime.Now.Ticks);
            var bmpCurrentScreenImage = new WriteableBitmap((int)this.ActualWidth, (int)this.ActualHeight);
            bmpCurrentScreenImage.Render(LayoutRoot, new MatrixTransform());
            bmpCurrentScreenImage.Invalidate();
            SaveToMediaLibrary(bmpCurrentScreenImage, fileName, 100);

            var toast = new Coding4Fun.Toolkit.Controls.ToastPrompt();
            toast.Title = "AR Torch";
            toast.Message = "Image saved to 'Saved Pictures'";
            toast.ImageSource = new BitmapImage(new Uri("/Assets/ToastIcon.png", UriKind.RelativeOrAbsolute));
            toast.Show();
        }
 
        public void SaveToMediaLibrary(WriteableBitmap bitmap, string name, int quality)
        {
            using (var stream = new MemoryStream())
            {
                // Save the picture to the Windows Phone media library.
                bitmap.SaveJpeg(stream, bitmap.PixelWidth, bitmap.PixelHeight, 0, quality);
                stream.Seek(0, SeekOrigin.Begin);
                new MediaLibrary().SavePicture(name, stream);
            }
        }

        private void SoundSelectButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/TorchPick.xaml", UriKind.Relative));
        }
    }
}