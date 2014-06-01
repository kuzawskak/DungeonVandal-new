using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Game;
using DungeonVandal;

namespace Game.Panels
{
    /// <summary>
    /// Panel z predefiniowanymi ustawieniami klawiatury do wyboru
    /// </summary>
    public partial class PredefinedSettingsPanel : UserControl
    {
        /// <summary>
        /// Konstrukotr panelu z predefiniowanymi ustawieniami klawiatury
        /// (generowany z Windows Forms)
        /// </summary>
        public PredefinedSettingsPanel()
        {
            InitializeComponent();
        }



        /// <summary>
        /// Zaladuj ustawienie predefiniowane #1 i wroc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settings1Button_Click(object sender, EventArgs e)
        {
            ((MenuForm)Parent).player.KeyboardSettings = new Settings.KeySettings(1);
            ((MenuForm)Parent).keyboard_settings_panel.Visible = true;
            this.Visible = false;
            

        }

        /// <summary>
        /// Zaladuj ustawienie predefiniowane #2 i wroc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settings2Button_Click(object sender, EventArgs e)
        {
            ((MenuForm)Parent).player.KeyboardSettings = new Settings.KeySettings(2);
            ((MenuForm)Parent).keyboard_settings_panel.Visible = true;
            this.Visible = false;
        }

        /// <summary>
        /// Zaladuj ustawienie predefiniowane #3 i wroc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settings3Button_Click(object sender, EventArgs e)
        {
            ((MenuForm)Parent).player.KeyboardSettings = new Settings.KeySettings(3);
            ((MenuForm)Parent).keyboard_settings_panel.Visible = true;
            this.Visible = false;
        }

        /// <summary>
        /// Wroc do menu ustawien klawiatury bez zmian
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backButton_Click(object sender, EventArgs e)
        {
           // this.Parent.Controls.Remove(this);
        }



        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Visible = false;
                ((MenuForm)Parent).keyboard_settings_panel.Visible = true;
            }
        }
    }
}
