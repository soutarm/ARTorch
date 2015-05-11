using System;
using System.Collections.Generic;
using System.Linq;

namespace ARTorch.Classes
{
    public class TorchPack
    {
        public string PackName { get; set; }
        public List<TorchItem> Torches { get; set; }
        public string ProductId { get; set; }
        public bool IsOwned { get; set; }
    }
    
    public static class TorchPacks
    {

        public enum Pack
        {
            Torches = 1,
            SciFi,
            Gross,
            Animals,
        }

        public static TorchPack TorchesPack()
        {
            var soundList = new List<TorchItem>();
            soundList.Add(new TorchItem
                              {
                                  Title = "Default",
                                  OffSoundLocation = AppSettings.SoundOffSettingDefault,
                                  OnSoundLocation = AppSettings.SoundOnSettingDefault,
                                  OffImageLocation = AppSettings.ImageOffSettingDefault,
                                  OnImageLocation = AppSettings.ImageOnSettingDefault,
                              });
            soundList.Add(new TorchItem
                              {
                                  Title = "Flashlight",
                                  OffSoundLocation = "assets/sounds/torches/flashlightoff.wav",
                                  OnSoundLocation = "assets/sounds/torches/flashlighton.wav",
                                  OffImageLocation = "/assets/images/torches/flashlightoff.png",
                                  OnImageLocation = "/assets/images/torches/flashlighton.png",
                              });
            soundList.Add(new TorchItem
                              {
                                  Title = "Maglite",
                                  OffSoundLocation = "assets/sounds/torches/magliteoff.wav",
                                  OnSoundLocation = "assets/sounds/torches/magliteon.wav",
                                  OffImageLocation = "/assets/images/torches/magliteoff.png",
                                  OnImageLocation = "/assets/images/torches/magliteon.png",
                              });
            soundList.Add(new TorchItem
                              {
                                  Title = "Large Torch",
                                  OffSoundLocation = "assets/sounds/torches/largetorchoff.wav",
                                  OnSoundLocation = "assets/sounds/torches/largetorchon.wav",
                                  OffImageLocation = "/assets/images/torches/largetorchoff.png",
                                  OnImageLocation = "/assets/images/torches/largetorchon.png",
                              });
            soundList.Add(new TorchItem
                            {
                                Title = "Old School",
                                OffSoundLocation = "assets/sounds/torches/oldschooloff.wav",
                                OnSoundLocation = "assets/sounds/torches/oldschoolon.wav",
                                OffImageLocation = "/assets/images/torches/oldschooloff.png",
                                OnImageLocation = "/assets/images/torches/oldschoolon.png",
                            });

            return new TorchPack {PackName = "Torches", Torches = soundList.OrderBy(s => s.Title).ToList(), ProductId = string.Empty, IsOwned = true};
        }

        public static TorchPack GrossPack()
        {
            var soundList = new List<TorchItem>();
            soundList.Add(new TorchItem
                              {
                                  Title = "Burp",
                                  OffSoundLocation = "assets/sounds/gross/burpoff.wav",
                                  OnSoundLocation = "assets/sounds/gross/burpon.wav",
                                  OffImageLocation = "/assets/images/gross/burpoff.png",
                                  OnImageLocation = "/assets/images/gross/burpon.png",
                              });
            soundList.Add(new TorchItem
                              {
                                  Title = "Cough",
                                  OffSoundLocation = "assets/sounds/gross/coughoff.wav",
                                  OnSoundLocation = "assets/sounds/gross/coughon.wav",
                                  OffImageLocation = "/assets/images/gross/coughoff.png",
                                  OnImageLocation = "/assets/images/gross/coughon.png",
                              });
            soundList.Add(new TorchItem
                              {
                                  Title = "Fart",
                                  OffSoundLocation = "assets/sounds/gross/fartoff.wav",
                                  OnSoundLocation = "assets/sounds/gross/farton.wav",
                                  OffImageLocation = "/assets/images/gross/fartoff.png",
                                  OnImageLocation = "/assets/images/gross/farton.png",
                              });
            soundList.Add(new TorchItem
                                {
                                    Title = "Gagging",
                                    OffSoundLocation = "assets/sounds/gross/gaggingoff.wav",
                                    OnSoundLocation = "assets/sounds/gross/gaggingon.wav",
                                    OffImageLocation = "/assets/images/gross/gaggingoff.png",
                                    OnImageLocation = "/assets/images/gross/gaggingon.png",
                                });
            soundList.Add(new TorchItem
                              {
                                  Title = "Sneeze",
                                  OffSoundLocation = "assets/sounds/gross/sneezeoff.wav",
                                  OnSoundLocation = "assets/sounds/gross/sneezeon.wav",
                                  OffImageLocation = "/assets/images/gross/sneezeoff.png",
                                  OnImageLocation = "/assets/images/gross/sneezeon.png",
                              });
            return new TorchPack { PackName = "Gross", Torches = soundList.OrderBy(s => s.Title).ToList(), ProductId = "torchpackgross", IsOwned = false };
        }

        public static TorchPack AnimalsPack()
        {
            var soundList = new List<TorchItem>();
            soundList.Add(new TorchItem
                            {
                                Title = "Cat",
                                OffSoundLocation = "assets/sounds/Animals/catoff.wav",
                                OnSoundLocation = "assets/sounds/Animals/caton.wav",
                                OffImageLocation = "/assets/images/Animals/catoff.png",
                                OnImageLocation = "/assets/images/Animals/caton.png",
                            });
            soundList.Add(new TorchItem
                            {
                                Title = "Chicken",
                                OffSoundLocation = "assets/sounds/Animals/chickenoff.wav",
                                OnSoundLocation = "assets/sounds/Animals/chickenon.wav",
                                OffImageLocation = "/assets/images/Animals/chickenoff.png",
                                OnImageLocation = "/assets/images/Animals/chickenon.png",
                            });
            soundList.Add(new TorchItem
                            {
                                Title = "Dog",
                                OffSoundLocation = "assets/sounds/Animals/dogoff.wav",
                                OnSoundLocation = "assets/sounds/Animals/dogon.wav",
                                OffImageLocation = "/assets/images/Animals/dogoff.png",
                                OnImageLocation = "/assets/images/Animals/dogon.png",
                            });
            soundList.Add(new TorchItem
                            {
                                Title = "Duck",
                                OffSoundLocation = "assets/sounds/Animals/duckoff.wav",
                                OnSoundLocation = "assets/sounds/Animals/duckon.wav",
                                OffImageLocation = "/assets/images/Animals/duckoff.png",
                                OnImageLocation = "/assets/images/Animals/duckon.png",
                            });
            soundList.Add(new TorchItem
                            {
                                Title = "Goat",
                                OffSoundLocation = "assets/sounds/Animals/goatoff.wav",
                                OnSoundLocation = "assets/sounds/Animals/goaton.wav",
                                OffImageLocation = "/assets/images/Animals/goatoff.png",
                                OnImageLocation = "/assets/images/Animals/goaton.png",
                            });
            return new TorchPack { PackName = "Animals", Torches = soundList.OrderBy(s => s.Title).ToList(), ProductId = "torchpackanimals", IsOwned = false };
        }

        public static TorchPack SciFiPack()
        {
            var soundList = new List<TorchItem>();
            soundList.Add(new TorchItem
                            {
                                Title = "Blaster",
                                OffSoundLocation = "assets/sounds/SciFi/blasteroff.wav",
                                OnSoundLocation = "assets/sounds/SciFi/blasteron.wav",
                                OffImageLocation = "/assets/images/SciFi/blasteroff.png",
                                OnImageLocation = "/assets/images/SciFi/blasteron.png",
                            });
            soundList.Add(new TorchItem
                              {
                                  Title = "Lightsaber",
                                  OffSoundLocation = "assets/sounds/SciFi/lightsaberoff.wav",
                                  OnSoundLocation = "assets/sounds/SciFi/lightsaberon.wav",
                                  OffImageLocation = "/assets/images/SciFi/lightsaberoff.png",
                                  OnImageLocation = "/assets/images/SciFi/lightsaberon.png",
                              });
            soundList.Add(new TorchItem
                            {
                                Title = "Laser Pistol",
                                OffSoundLocation = "assets/sounds/SciFi/laserpistoloff.wav",
                                OnSoundLocation = "assets/sounds/SciFi/laserpistolon.wav",
                                OffImageLocation = "/assets/images/SciFi/laserpistoloff.png",
                                OnImageLocation = "/assets/images/SciFi/laserpistolon.png",
                            });
            soundList.Add(new TorchItem
                            {
                                Title = "Communicator",
                                OffSoundLocation = "assets/sounds/SciFi/communicatoroff.wav",
                                OnSoundLocation = "assets/sounds/SciFi/communicatoron.wav",
                                OffImageLocation = "/assets/images/SciFi/communicatoroff.png",
                                OnImageLocation = "/assets/images/SciFi/communicatoron.png",
                            });
            soundList.Add(new TorchItem
                            {
                                Title = "Phaser",
                                OffSoundLocation = "assets/sounds/SciFi/phaseroff.wav",
                                OnSoundLocation = "assets/sounds/SciFi/phaseron.wav",
                                OffImageLocation = "/assets/images/SciFi/phaseroff.png",
                                OnImageLocation = "/assets/images/SciFi/phaseron.png",
                            });
            return new TorchPack { PackName = "SciFi", Torches = soundList.OrderBy(s => s.Title).ToList(), ProductId = string.Empty, IsOwned = true };
        }

        public static TorchPack GetPack(Pack pack)
        {
            switch (pack)
            {
                case Pack.Torches:
                    return TorchesPack();
                case Pack.Gross:
                    return GrossPack();
                case Pack.Animals:
                    return AnimalsPack();
                case Pack.SciFi:
                    return SciFiPack();
                default:
                    throw new ArgumentOutOfRangeException("pack");
            }
        }
    }
}
