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
    /// Panel pomocy ( z opisem gry )
    /// </summary>
    public partial class HelpPanel : UserControl
    {
        /// <summary>
        /// Aktualnie wyświetlany slajd
        /// </summary>
        int current_pos = 0;
        /// <summary>
        /// Tablica ze ścieżkami plików graficznych przechowującymi slajdy
        /// </summary>
        string[] slides = { "slide1", "slide2", "slide3", "slide4", "slide41","slide42","slide5" };

        /// <summary>
        /// Konstruktor , inicjalizacja elemntów składowych ( w szczególności startowego slajdu)
        /// </summary>
        public HelpPanel()
        {
            current_pos = 0;
            InitializeComponent();
            System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Game.Resources", typeof(Resources).Assembly);
            pictureBox1.BackgroundImage = ((System.Drawing.Image)(temp.GetObject(slides[current_pos])));
        }

        /// <summary>
        /// Rozpoczyna pokaz slajdów pomocy od pocztaku
        /// </summary>
        public void startFromFirstSlide()
        {
            current_pos = 0;
            System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Game.Resources", typeof(Resources).Assembly);
            pictureBox1.BackgroundImage = ((System.Drawing.Image)(temp.GetObject(slides[current_pos])));
        }


        /// <summary>
        /// Ładowanie aktualnego slajdu
        /// </summary>
        public void LoadCurrentSlide()
        {
            current_pos = (current_pos+1)%slides.Length;
          System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Game.Resources", typeof(Resources).Assembly);
            pictureBox1.BackgroundImage = ((System.Drawing.Image)(temp.GetObject(slides[current_pos])));
        }

        /// <summary>
        /// Obsługa wyjścia z menu pomocy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            if (((MenuForm)Parent).GamePause)
            {
                ((MenuForm)Parent).pause_panel.Visible = true;
            }
            else
            {
                ((MenuForm)Parent).main_panel.Visible = true;
            }
        }

        /// <summary>
        /// Obsługa przejścia do nastepnego slajdu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextSlideButton_Click(object sender, EventArgs e)
        {
            LoadCurrentSlide();
        }
    }
}
