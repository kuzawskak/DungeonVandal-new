using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DungeonVandal;

namespace Game.Panels
{
    /// <summary>
    /// Panel wyswietlany gdy gracz wygra ( przejdzie 5 etapów gry)
    /// </summary>
    public partial class Win : UserControl
    {
        /// <summary>
        /// Konstruktor panelu z wynikiem wygranej (generowany z WIndows Forms)
        /// </summary>
        public Win()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Ustawienie odpowiedniej liczby punktów dla aktualnego gracza
        /// </summary>
        /// <param name="points">liczba zdobytych punktów</param>
        public void setScoreLabel(int points)
        {
            this.scoreLabel.Text = "Twoj wynik to: " + points.ToString();
        }
        /// <summary>
        /// Obsługa wyscia w aktualnego panelu
        /// </summary>
        /// <param name="sender">źródło sygnału do wyjscia z panelu (button)</param>
        /// <param name="e">parametry wywołania</param>
        private void exitButton_Click(object sender, EventArgs e)
        {
            ((MenuForm)Parent).GamePause = true;

             this.Visible = false;
             ((MenuForm)Parent).main_panel.Visible = true;

        }

        /// <summary>
        /// Reakcja na wybór nowej gry
        /// </summary>
        /// <param name="sender">źródło sygnału do rozpoczecia nowej gry (button)</param>
        /// <param name="e">paramatery wywołania </param>
        private void newGameButton_Click(object sender, EventArgs e)
        {
            //start new game
            this.Visible = false;
            ((MenuForm)Parent).gameInstance.LoadNewGame();
            ((MenuForm)Parent).game_panel.Visible = true;
        }
    }
}
