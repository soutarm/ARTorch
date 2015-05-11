using System;
using System.IO.IsolatedStorage;
using System.Diagnostics;
using System.Collections.Generic;


namespace ARTorch.Classes
{
    public class AppSettings
    {
        // Our settings
        private readonly IsolatedStorageSettings _settings;

        // The key names of our settings
        private const string TorchInitStateSettingKeyName = "TorchInitStateSetting";
        private const string EnableSoundSettingKeyName = "EnableSoundSetting";
        private const string EnableVibrateSettingKeyName = "EnableVibrateSetting";
        private const string EnableHardwareButtonSettingKeyName = "EnableHardwareButtonSetting";
        private const string OperateUnderLockscreenSettingKeyName = "OperateUnderLockscreenSetting";
        private const string SoundOnSettingKeyName = "SoundOnSetting";
        private const string SoundOffSettingKeyName = "SoundOffSetting";
        private const string ImageOnSettingKeyName = "ImageOnSetting";
        private const string ImageOffSettingKeyName = "ImageOffSetting";
        private const string CurrentTorchSetSettingKeyName = "CurrentTorchSetSetting";
        private const string LicensesSettingKeyName = "LicensesSetting";

        // The default value of our settings
        private const bool TorchInitStateSettingDefault = true;
        private const bool EnableSoundSettingDefault = true;
        private const bool EnableVibrateSettingDefault = true;
        private const bool EnableHardwareButtonSettingDefault = true;
        private const bool OperateUnderLockscreenSettingDefault = false;
        public const string SoundOnSettingDefault = "assets/sounds/torches/on.wav";
        public const string SoundOffSettingDefault = "assets/sounds/torches/off.wav";
        public const string ImageOnSettingDefault = "/assets/images/torches/on.png";
        public const string ImageOffSettingDefault = "/assets/images/torches/off.png";
        public const TorchPacks.Pack CurrentTorchSetSettingDefault = TorchPacks.Pack.Torches;
        public const string LicensesSettingDefault = "";

        /// <summary>
        /// Constructor that gets the application settings.
        /// </summary>
        public AppSettings()
        {
            // Get the settings for this application.
            _settings = IsolatedStorageSettings.ApplicationSettings;
        }

        /// <summary>
        /// Update a setting value for our application. If the setting does not
        /// exist, then add the setting.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddOrUpdateValue(string key, Object value)
        {
            bool valueChanged = false;

            // If the key exists
            if (_settings.Contains(key))
            {
                // If the value has changed
                if (_settings[key] != value)
                {
                    // Store the new value
                    _settings[key] = value;
                    valueChanged = true;
                }
            }
                // Otherwise create the key.
            else
            {
                _settings.Add(key, value);
                valueChanged = true;
            }
            return valueChanged;
        }

        /// <summary>
        /// Get the current value of the setting, or if it is not found, set the 
        /// setting to the default setting.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetValueOrDefault<T>(string key, T defaultValue)
        {
            T value;

            // If the key exists, retrieve the value.
            if (_settings.Contains(key))
            {
                value = (T) _settings[key];
            }
                // Otherwise, use the default value.
            else
            {
                value = defaultValue;
            }
            return value;
        }

        /// <summary>
        /// Save the settings.
        /// </summary>
        public void Save()
        {
            _settings.Save();
        }


        /// <summary>
        /// Property to get and set the TorchInitStateSetting
        /// </summary>
        public bool TorchInitStateSetting
        {
            get { return GetValueOrDefault<bool>(TorchInitStateSettingKeyName, TorchInitStateSettingDefault); }
            set { if (AddOrUpdateValue(TorchInitStateSettingKeyName, value)) Save(); }
        }

        /// <summary>
        /// Property to get and set the EnableSoundSetting
        /// </summary>
        public bool EnableSoundSetting
        {
            get { return GetValueOrDefault<bool>(EnableSoundSettingKeyName, EnableSoundSettingDefault); }
            set { if (AddOrUpdateValue(EnableSoundSettingKeyName, value)) Save(); }
        }

        /// <summary>
        /// Property to get and set the EnableVibrateSetting
        /// </summary>
        public bool EnableVibrateSetting
        {
            get { return GetValueOrDefault<bool>(EnableVibrateSettingKeyName, EnableVibrateSettingDefault); }
            set { if (AddOrUpdateValue(EnableVibrateSettingKeyName, value)) Save(); }
        }

        /// <summary>
        /// Property to get and set the EnableHardwareButtonSetting
        /// </summary>
        public bool EnableHardwareButtonSetting
        {
            get { return GetValueOrDefault<bool>(EnableHardwareButtonSettingKeyName, EnableHardwareButtonSettingDefault); }
            set { if (AddOrUpdateValue(EnableHardwareButtonSettingKeyName, value)) Save(); }
        }

        /// <summary>
        /// Property to get and set the OperateUnderLockscreenSetting
        /// </summary>
        public bool OperateUnderLockscreenSetting
        {
            get { return GetValueOrDefault<bool>(OperateUnderLockscreenSettingKeyName, OperateUnderLockscreenSettingDefault); }
            set { if (AddOrUpdateValue(OperateUnderLockscreenSettingKeyName, value)) Save(); }
        }

        /// <summary>
        /// Property to get and set the SoundPackSetting
        /// </summary>
        public string SoundOnSetting
        {
            get { return GetValueOrDefault(SoundOnSettingKeyName, SoundOnSettingDefault); }
            set { if (AddOrUpdateValue(SoundOnSettingKeyName, value)) Save(); }
        }

        /// <summary>
        /// Property to get and set the SoundOffSetting
        /// </summary>
        public string SoundOffSetting
        {
            get { return GetValueOrDefault(SoundOffSettingKeyName, SoundOffSettingDefault); }
            set { if (AddOrUpdateValue(SoundOffSettingKeyName, value)) Save(); }
        }

        /// <summary>
        /// Property to get and set the SoundPackSetting
        /// </summary>
        public string ImageOnSetting
        {
            get { return GetValueOrDefault(ImageOnSettingKeyName, ImageOnSettingDefault); }
            set { if (AddOrUpdateValue(ImageOnSettingKeyName, value)) Save(); }
        }

        /// <summary>
        /// Property to get and set the ImageOffSetting
        /// </summary>
        public string ImageOffSetting
        {
            get { return GetValueOrDefault(ImageOffSettingKeyName, ImageOffSettingDefault); }
            set { if (AddOrUpdateValue(ImageOffSettingKeyName, value)) Save(); }
        }

        /// <summary>
        /// Property to get and set the CurrentTorchSetSetting
        /// </summary>
        public TorchPacks.Pack CurrentTorchSetSetting
        {
            get { return GetValueOrDefault(CurrentTorchSetSettingKeyName, CurrentTorchSetSettingDefault); }
            set { if (AddOrUpdateValue(CurrentTorchSetSettingKeyName, value)) Save(); }
        }

        /// <summary>
        /// Property to get and set the current user's product licenses
        /// </summary>
        public string LicensesSetting
        {
            get { return GetValueOrDefault(LicensesSettingKeyName, LicensesSettingDefault); }
            set { if (AddOrUpdateValue(LicensesSettingKeyName, value)) Save(); }
        }
    }
}
