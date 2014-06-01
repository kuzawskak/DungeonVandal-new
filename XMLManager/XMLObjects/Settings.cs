using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Storage;

namespace XMLManager.XMLObjects
{
    /// <summary>
    /// Klasa potzrebna do zapisy ustawień gracza
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Device do przechowywania plików XNA
        /// </summary>
        public static StorageDevice device;
        /// <summary>
        /// Nazwa folderu z zapisanymi plikami dla XNA
        /// </summary>
        public static string containerName = "MyGamesStorage\\Settings";
        /// <summary>
        /// Nazwy plików ustawieniami (nazwa taka sama jak nazwa Playera)
        /// </summary>
        public static string filename;

        /// <summary>
        /// ustawienia do zapisu
        /// </summary>
        public static SettingsData settingsToSave;

        /// <summary>
        /// Klasa do zapisu najlepszych wyników
        /// </summary>
        [Serializable]
        public class SettingsData
        {
            /// <summary>
            /// poziom glosnosci dzwieku
            /// </summary>
            public double AudioVolume;
            /// <summary>
            /// poziom glosnosci muzyki
            /// </summary>
            public double MusicVolume;
            /// <summary>
            /// czy wyciszyc
            /// </summary>
            public bool isMuted;
            /// <summary>
            /// maksymalny poziom odblokowany
            /// </summary>
            public int MaxEnabledLevel;
            /// <summary>
            /// klawisz ruchu w gore
            /// </summary>
            public Keys UpKey;
            /// <summary>
            /// klaiwsz ruchu w dol
            /// </summary>
            public Keys DownKey;
            /// <summary>
            /// klawisz ruchu w prawo
            /// </summary>
            public Keys RightKey;
            /// <summary>
            /// klaiwsz ruchu w lewo
            /// </summary>
            public Keys LeftKey;
            /// <summary>
            /// klawisz blokowania ruchu
            /// </summary>
            public Keys BlockKey;
            /// <summary>
            /// klawisz wyrzucanaia dynamitu
            /// </summary>
            public Keys DynamiteKey;
            /// <summary>
            /// klawisz wyrzucania rakiety
            /// </summary>
            public Keys RacketKey;
            /// <summary>
            /// klaiwsz pauzy
            /// </summary>
            public Keys PauseKey;

            /// <summary>
            /// konstruktor przyjmujacy wszytkie parametry do zapisu
            /// </summary>
            /// <param name="maxlevel">maksymalny level do osagniecia</param>
            /// <param name="audio">glosnosc dzwiekow</param>
            /// <param name="music">glosnosc muzyki</param>
            /// <param name="mute">wyciszanie</param>
            /// <param name="u">klawisz ruchu w gore</param>
            /// <param name="d">klawisz ruchu w dol</param>
            /// <param name="r">lawisz ruchu w prawo</param>
            /// <param name="l">lawisz ruchu w lewo</param>
            /// <param name="blo">lawisz blokowania ruchu</param>
            /// <param name="dyn">klawiwsz wyrzucania dynamitu</param>
            /// <param name="rac">klawisz wyrzucania rakiety</param>
            /// <param name="pau">klawisz pauzy</param>
            public SettingsData(int maxlevel, double audio, double music, bool mute, Keys u, Keys d, Keys r, Keys l, Keys blo, Keys dyn, Keys rac, Keys pau)
            {
                MaxEnabledLevel = maxlevel;
                AudioVolume = audio;
                MusicVolume = music;
                isMuted = mute;
                UpKey = u;
                DownKey = d;
                RightKey = r;
                LeftKey = l;
                BlockKey = blo;
                DynamiteKey = dyn;
                RacketKey = rac;
                PauseKey = pau;

            }
            /// <summary>
            /// Konstruktor dla ustawien domyslnych
            /// </summary>
            /// <param name="def"></param>
            public SettingsData(int def)
            {
                AudioVolume = 0.5;
                MusicVolume = 0.5;
                isMuted = false;
                UpKey = Keys.Up;
                DownKey = Keys.Down;
                RightKey = Keys.Right;
                LeftKey = Keys.Left;
                BlockKey = Keys.Alt;
                DynamiteKey = Keys.Space;
                RacketKey = Keys.Return;
                PauseKey = Keys.Escape;
                MaxEnabledLevel = 1;

            }
            /// <summary>
            /// Konstruktor dla braku ustawien
            /// </summary>
            public SettingsData()
            {
                AudioVolume = 0;
                MusicVolume = 0;
                isMuted = false;
                UpKey = Keys.None;
                DownKey = Keys.None;
                RightKey = Keys.None;
                LeftKey = Keys.None;
                BlockKey = Keys.None;
                DynamiteKey = Keys.None;
                RacketKey = Keys.None;
                PauseKey = Keys.None;
                MaxEnabledLevel = 0;

            }
        }


        /// <summary>
        /// Ładowanie listy najlepszych wyników dla danego gracza
        /// </summary>
        /// <param name="result">obiekt typu IAsyncResult dający dostęp do XNA Storage</param>
        /// <param name="playerName">nazwa gracza</param>
        /// <returns></returns>
        public static SettingsData LoadSettings(IAsyncResult result, string playerName)
        {
            filename = playerName;
            device = StorageDevice.EndShowSelector(result);

            // Open a storage container.
            IAsyncResult result2 = device.BeginOpenContainer(containerName, null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result2);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            // Check to see whether the save exists.
            if (!container.FileExists(filename))
            {
                container.Dispose();
                return new SettingsData();
            }

            // Open the file.
            Stream stream = container.OpenFile(filename, FileMode.Open);

            // Read the data from the file.
            XmlSerializer serializer = new XmlSerializer(typeof(SettingsData));
            SettingsData data = (SettingsData)serializer.Deserialize(stream);

            // Close the file.
            stream.Close();

            // Dispose the container.
            container.Dispose();

            //Return SettingsData
            return data;
        }

        /// <summary>
        /// Zapis ustawien do pliku o nazwie gracza
        /// </summary>
        /// <param name="result">XNA result</param>
        /// <param name="playerName">nazwa gracza</param>
        public static void SaveToDevice(IAsyncResult result, string playerName)
        {
            filename = playerName;
            if (device != null && device.IsConnected)
            {
                IAsyncResult r = device.BeginOpenContainer(containerName, null, null);

                StorageContainer container = device.EndOpenContainer(r);
                if (container.FileExists(filename))
                    container.DeleteFile(filename);
                Stream stream = container.CreateFile(filename);
                XmlSerializer serializer = new XmlSerializer(typeof(SettingsData));
                serializer.Serialize(stream, settingsToSave);
                stream.Close();
                container.Dispose();
                result.AsyncWaitHandle.Close();
            }
        }


    }
}
