using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Weapon;
using Game.Characters;

namespace Game
{
    /// <summary>
    /// Klasa gracza
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Nazwa gracza
        /// </summary>
        public string Name {get; private set;}

        /// <summary>
        /// Tryb rosnacego poziomu gry na podstawie inteligencji gracza (inteligencja wyliczana na podstawie wyników każdego poziomu)
        /// </summary>
        public bool CheckIntelligence { get; set; }

        /// <summary>
        /// Poziom inteligencji gracza wyliczony na podstawie ukonczonego ostatnio poziomu
        /// 3 poziomy: 0 - niski, 1 - średni ,2 - wysoki
        /// </summary>
        public int IntelligenceLevel { get; set; }

        /// <summary>
        /// Liczab rakiet
        /// </summary>
        public int Rackets  {get; set;}

        /// <summary>
        /// Liczab dynamitów
        /// </summary>
        public int Dynamite { get; set; }

        /// <summary>
        /// Liczba punktów
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Poziom trudności gry
        /// 1 - łatwy
        /// 2 - średni
        /// 3 - trudny
        /// </summary>
        public int DifficultyLevel { get;  set; }

        /// <summary>
        /// Kamsymalny poziom gry który może załadować
        /// </summary>
        public int MaxEnabledLevel { get; set; }

        /// <summary>
        /// Konstruktor z przypisaniem wszytkich pól na 0 (oprócz nazwy)
        /// </summary>
        /// <param name="name">Nazwa gracza</param>
        public Player(string name)
        {
            KeyboardSettings = new Settings.KeySettings();
            AudioSettings = new Settings.AudioSettings();
            this.IntelligenceLevel = 1;
            this.Name = name;
            this.Points = 0;
            this.Rackets = 0;
            this.Dynamite = 0;
            this.DifficultyLevel = 1;
            this.CheckIntelligence = false; 
        }

        /// <summary>
        /// Ustawienia klawiatury
        /// </summary>
        public Settings.KeySettings KeyboardSettings { get; set; }
        /// <summary>
        /// Ustawienia audio
        /// </summary>
        public Settings.AudioSettings AudioSettings { get; set; }

        /// <summary>
        /// Konstruktor bezparametrowy - ustawienia domyślne
        /// </summary>
        public Player()
        {
            this.IntelligenceLevel = 1;
            KeyboardSettings = new Settings.KeySettings();
            AudioSettings = new Settings.AudioSettings();
            DifficultyLevel = 1;
            this.CheckIntelligence = false;
        }

    }
}
