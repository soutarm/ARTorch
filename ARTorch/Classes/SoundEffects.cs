/****************************************************************
 * Audio manager for Windows Phone 8. Currently juse handles SFX
 * Dave Voyles 2/16/2013
 ***************************************************************/

using System;
using System.Windows;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace ARTorch.Classes
{
    public static class SoundEffects
    {
        private static bool _initialized = false;
        private static SoundEffect _sfxSilence;


        /****************************************************************
        * Must be called before using static methods
        ****************************************************************/
        public static void Initialize()
        {
            if (_initialized)
                return;

            // Adds an Update delegate, to simulate the XNA update method.
            FrameworkDispatcher.Update();
            _initialized = true;
        }

        public static SoundEffect SfxSilence
        {
            get
            {
                // If not initialized, returns null.
                if (!_initialized)
                    return null;

                // Holds informations about a file stream.
                var info = Application.GetResourceStream(new Uri(@"Assets\sounds\silence.wav", UriKind.Relative));

                // Create the SoundEffect from the Stream
                _sfxSilence = SoundEffect.FromStream(info.Stream);
                return _sfxSilence;
            }
        }

        public static void PlayError(string errorMessage = "")
        {
            var error = Application.GetResourceStream(new Uri(@"Assets\sounds\error.wav", UriKind.Relative));
            SoundEffect.FromStream(error.Stream).Play();
            var message = string.Format("Sorry, an error occurred while attempting to play the sound." + Environment.NewLine + "{0}", errorMessage);
            MessageBox.Show(message);
        }

        public static void PlaySound(Uri soundEffectUri)
        {
            // If not initialized, returns null.
            if (!_initialized || soundEffectUri == null)
                return;

            // Holds informations about a file stream.
            var info = Application.GetResourceStream(soundEffectUri);

            try
            {
                // Create the SoundEffect from the Stream
                SoundEffect.FromStream(info.Stream).Play();
            }
            catch (Exception ex)
            {
                SoundEffects.PlayError(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
    }
}