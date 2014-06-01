using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;

namespace Game.Settings
{
    /// <summary>
    /// Klasa ustawień audio
    /// </summary>
    public class AudioSettings
    {
        /// <summary>
        /// domyślna głośnośc muzyki w tle
        /// </summary>
        const double DEFAULT_MUSIC_VOLUME = 0.5;
        /// <summary>
        /// domyslna głośnośc pojedunczych dżwieków
        /// </summary>
        const double DEFAULT_SOUND_VOLUME = 0.5;
        /// <summary>
        /// Domyslna wartośc czy wyciszony
        /// </summary>
        const bool DEFAULT_IS_MUTED = false;

        /// <summary>
        /// głośnośc muzyki w tle gry
        /// </summary>
        public double MusicVolume {get; private set;}
        /// <summary>
        /// Głośność pojedynczych dźwiekow gry
        /// </summary>
        public double SoundVolume { get; private set; }
        /// <summary>
        /// Czy dźwieki gry są wyciszone
        /// </summary>
        public bool IsMuted { get; private set; }

        /// <summary>
        /// Domyslne ustawienia
        /// </summary>
        public AudioSettings()
        {
            MusicVolume = DEFAULT_MUSIC_VOLUME;
            SoundVolume = DEFAULT_SOUND_VOLUME;
            IsMuted = DEFAULT_IS_MUTED;
        }

        /// <summary>
        /// Konstruktor z ustalonymi paramterami
        /// </summary>
        /// <param name="music_volume">głośność dźwięków w tle</param>
        /// <param name="sound_volume">głośność pojedynczych dzwięków</param>
        /// <param name="is_muted">czy wyciszony</param>
        public AudioSettings(double music_volume, double sound_volume, bool is_muted)
        {
            MusicVolume = music_volume;
            SoundVolume = sound_volume;
            IsMuted = is_muted;
        }


        /// <summary>
        /// Zastsowanie zmian ustawień dżwięku i zapis do pliku z ustawieniami gracza
        /// </summary>
        /// <param name="music_volume">głośność dźwięków w tle</param>
        /// <param name="sound_volume">głośność pojedynczych dzwięków</param>
        /// <param name="is_muted">czy wyciszony</param>
        /// <param name="player_name">nazwa gracza</param>
        public void applyChanges(double music_volume, double sound_volume, bool is_muted, string player_name)
        {

            //zapisz w pliku xml dla gracza
            IAsyncResult result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
            //załadowanie pliku z wynikami dla odpowiedniego poziomu

            XMLManager.XMLObjects.Settings.SettingsData saved_data = XMLManager.XMLObjects.Settings.LoadSettings(result, player_name);
            saved_data.MusicVolume = music_volume;
            saved_data.AudioVolume = sound_volume;
            saved_data.isMuted = is_muted;
            MusicVolume = music_volume;
            SoundVolume = sound_volume;
            IsMuted = is_muted;
            XMLManager.XMLObjects.Settings.settingsToSave = saved_data;
            XMLManager.XMLObjects.Settings.SaveToDevice(result, player_name);
        }
    }
}
