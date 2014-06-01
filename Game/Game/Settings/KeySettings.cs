using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using XMLManager.XMLObjects;

namespace Game.Settings
{
    /// <summary>
    /// Aktualny stan wyboru ustawień ( determinuje który klawisz jest zmieniany)
    /// </summary>
    public enum State { 
        /// <summary>
        /// Żaden klawisza aktualnie nie jest zmieniany
        /// </summary>
        NONE, 
        /// <summary>
        /// Zmiana klawisza ruchu w górę
        /// </summary>
        UP, 
        /// <summary>
        /// Zmiana klawisza ruchu w dół
        /// </summary>
        DOWN, 
        /// <summary>
        /// Zmiana klawisza ruchu w lewo
        /// </summary>
        LEFT, 
        /// <summary>
        /// Zmiana klawisza ruchu w prawo
        /// </summary>
        RIGHT,
        /// <summary>
        /// Zmiana klawisza zmiany kierunku ruchu (blokada ruchu)
        /// </summary>
        BLOCK, 
        /// <summary>
        /// Zmiana klawisza wyrzucenia dynamitu
        /// </summary>
        DYNAMITE, 
        /// <summary>
        /// Zmiana klawisza wyrzucenia rakiety
        /// </summary>
        RACKET };
   
    /// <summary>
    /// Klasa ustawien sterowania
    /// </summary>
    public class KeySettings
    {
        /// <summary>
        /// Klawisz poruszania w górę
        /// </summary>
        public Keys Up { get; private set; }
        /// <summary>
        /// Klawicz poruszania w dół
        /// </summary>
        public Keys Down { get; private set; }
        /// <summary>
        /// Klawisz poruszania w lewo
        /// </summary>
        public Keys Left { get; private set; }
        /// <summary>
        /// Klawisz poruszania w prawo
        /// </summary>
        public Keys Right { get; private set; }
        /// <summary>
        /// Klawisz blokowania ruchu (zmiana kierunku poruszania w miejscu)
        /// </summary>
        public Keys Block { get; private set; }
        /// <summary>
        /// Klawisz odpalenia dynamitu
        /// </summary>
        public Keys Dynamite { get; private set; }
        /// <summary>
        /// Klawisz odpalenia rakiety
        /// </summary>
        public Keys Racket { get; private set; }
        /// <summary>
        /// Klawisz pauzy
        /// </summary>
        public Keys Pause { get; private set; }

        /// <summary>
        /// Konstruktor pobierający bezpośrednie wartości klawiszy
        /// </summary>
        /// <param name="UpKey">klawisz ruchu w górę</param>
        /// <param name="DownKey">klawisz ruchu w dół</param>
        /// <param name="RightKey">klawisz ruchu w prawo</param>
        /// <param name="LeftKey">klawisz ruchu w lewo</param>
        /// <param name="RacketKey">klawisz wystrzału rakiety</param>
        /// <param name="DynamiteKey">klawisz uruchomienia dynamitu</param>
        /// <param name="PauseKey">klawisz pauzy</param>
        /// <param name="BlockKey">klawisz blokowania ruchu</param>
        public  KeySettings(Keys UpKey, Keys DownKey, Keys RightKey, Keys LeftKey, Keys RacketKey, Keys DynamiteKey, Keys PauseKey,Keys BlockKey)
        {
            this.Up = UpKey;
            this.Down = DownKey;
            this.Right = RightKey;
            this.Left = LeftKey;
            this.Racket = RacketKey;
            this.Dynamite = DynamiteKey;
            this.Pause = PauseKey;
            this.Block = BlockKey;
        }





        /// <summary>
        /// Zastosuj zmiany uwzglednione w Dictionary ze zmianami oraz zapisz je do pliku ustawien gracza
        /// </summary>
        /// <param name="key_changes">Zmiany do zastosowania</param>
        /// <param name="player_name">nazwa gracza</param>
        public void ApplyChanges(Dictionary<State, Keys> key_changes,String player_name)
        {
            //zapisz w pliku xml dla gracza
            IAsyncResult result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
            //załadowanie pliku z wynikami dla odpowiedniego poziomu

            XMLManager.XMLObjects.Settings.SettingsData saved_data = XMLManager.XMLObjects.Settings.LoadSettings(result, player_name);

            Pause = System.Windows.Forms.Keys.Escape;
            foreach (State s in key_changes.Keys)
            {
                switch (s)
                {
                    case State.UP:
                        Up = key_changes[s];
                        saved_data.UpKey = Up;
                        break;
                    case State.DOWN:
                        Down = key_changes[s];
                        saved_data.DownKey = Down;
                        break;
                    case State.RIGHT:
                        Right = key_changes[s];
                        saved_data.RightKey = Right;
                        break;
                    case State.LEFT:
                        Left = key_changes[s];
                        saved_data.LeftKey = Left;
                        break;
                    case State.DYNAMITE:
                        Dynamite = key_changes[s];
                        saved_data.DynamiteKey = Dynamite;
                        break;
                    case State.RACKET:
                        Racket = key_changes[s];
                        saved_data.RacketKey = Racket;
                        break;
                    case State.BLOCK:
                        Block = key_changes[s];
                        saved_data.BlockKey = Block;
                        break;
                    default: break;
                }
            }
            XMLManager.XMLObjects.Settings.settingsToSave = saved_data;
            XMLManager.XMLObjects.Settings.SaveToDevice(result, player_name);
        }

        /// <summary>
        /// Konwersja ustawien sterowania na słownik indeksowany STanem klawiszy
        /// </summary>
        /// <returns>Słownik z aktualnymi ustawieniami sterowania</returns>
        public Dictionary<State, Keys> ToMap()
        {
            Dictionary<State, Keys> dict = new Dictionary<State, Keys>();
            dict.Add(State.UP, Up);
            dict.Add(State.DOWN, Down);
            dict.Add(State.LEFT, Left);
            dict.Add(State.RIGHT, Right);
            dict.Add(State.DYNAMITE, Dynamite);
            dict.Add(State.BLOCK,Block);
            dict.Add(State.RACKET, Racket);
            return dict;

        }

        /// <summary>
        /// W przypadku kilku predefiniowanych ustawien - konstruktor z numerem ustawien
        /// </summary>
        /// <param name="number"></param>
        public KeySettings(int number)
        {
            Pause = System.Windows.Forms.Keys.Escape;
            switch (number)
            {
                case 1:
                    Up = Keys.W;
                    Down = Keys.S;
                    Left = Keys.A;
                    Right = Keys.D;
                    Block = Keys.Alt;
                    Dynamite = Keys.Space;
                    Racket = Keys.Return;
                    Pause = Keys.Escape;
                    break;
                case 2:
                    Up = Keys.Up;
                    Down = Keys.Down;
                    Left = Keys.Left;
                    Right = Keys.Right;
                    Block = Keys.Alt;
                    Dynamite = Keys.Space;
                    Racket = Keys.Return;
                    Pause = Keys.Escape;
                    break;
                case 3:
                    Up = Keys.NumPad8;
                    Down = Keys.NumPad2;
                    Left = Keys.NumPad4;
                    Right = Keys.NumPad6;
                    Block = Keys.Alt;
                    Dynamite = Keys.Space;
                    Racket = Keys.Return;
                    Pause = Keys.Escape;
                    break;
            }
        }

        /// <summary>
        /// Domyslne ustawienia (poruszanie strzałkami)
        /// </summary>
        public KeySettings()
        {
            Up = Keys.Up;
            Down = Keys.Down;
            Left = Keys.Left;
            Right = Keys.Right;
            Block = Keys.Alt;
            Dynamite = Keys.Space;
            Racket = Keys.Return;
            Pause = Keys.Escape;
        }
    }
}
