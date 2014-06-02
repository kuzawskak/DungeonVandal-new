using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media;
using DungeonVandal;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

namespace Game.Panels
{
    /// <summary>
    /// Panel z ustawieniami dźwięku
    /// </summary>
    public partial class AudioSettingsPanel : UserControl
    {
        /// <summary>
        /// Odtwarzacz muzyki w tle gry
        /// </summary>
        static MediaPlayer mp;
        /// <summary>
        /// Odtwarzacz pojedynczych dźwięków w grze
        /// </summary>
        static MediaPlayer sp;

        /// <summary>
        /// Czy dzwieki są wyciszone
        /// </summary>
        static bool is_muted;
        /// <summary>
        /// Informacja czy zakonczyć odtwarzanie muzyki
        /// </summary>
        public static bool music_stop = false;
         /// <summary>
         /// Informacja czy zakonczyc odtwarzanie pojedynczych dzwiekow
         /// </summary>
        public static bool sound_stop = false;


        /// <summary>
        /// Konstruktor Panelu z ustawieniami audio ( generowany z Windows Forms)
        /// </summary>
        public AudioSettingsPanel()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Funkcja odtwarzania muzyki w tle
        /// </summary>
        public static void MusicFunc()
        {
            if (!music_stop)
            {
                mp = new MediaPlayer();
                mp.Open(new System.Uri(Path.GetFullPath(@"background_music.wav")));
                mp.Volume = (double)MusicVolumeTrackBar.Value / 10;
                if(!is_muted)
                mp.Play();
            }
        
           
            

        }
        /// <summary>
        /// Funkcja odtwarzania pojedynczych dzwiekow
        /// </summary>
        public static void SoundFunc()
        {

            if (!sound_stop)
            {
                sp = new MediaPlayer();
                sp.Open(new System.Uri(Path.GetFullPath(@"found.wav")));
                sp.Volume = (double)musicVolTrackbar.Value / 10;
                if(!is_muted)
                sp.Play();
            }
            
            
        }
        
        /// <summary>
        /// Funkcja rozpoczynajaca dzialanieMediaPlayera z muzyka w tle
        /// </summary>
        public void playBackgroundMusic()
        {

            music_stop = false;
            if (MuteCheckbox.Checked)
                is_muted = true;
            MusicFunc();
        }

        /// <summary>
        /// Funkcja rozpoczynajaca działąnie MediaPlayera z pojedynczym dzwiekiem
        /// </summary>
        public void startSoundPlayer()
        {

            sound_stop = false;
            if (MuteCheckbox.Checked)
                is_muted = true;
            SoundFunc();
        }

        /// <summary>
        /// Zatrzymanie obu odtwarzaczy dzwieku
        /// </summary>
        public void stopPlayer()
        {
            sound_stop = true;
            music_stop = true;
            mp.Stop();
            sp.Stop();
      
        }

  

        private void audioLabel_Click(object sender, EventArgs e)
        {


        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Obsluga zdarzenia zmiany checkboxa do wylaczania dzwieku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuteCheckbox_CheckedChanged(object sender, EventArgs e)
        {

            if (MuteCheckbox.Checked)
            {
                mp.Volume = 0;
                sp.Volume = 0;
                is_muted = true;
            }
            else
            {
                mp.Volume = (double)MusicVolumeTrackBar.Value / 10;
                sp.Volume = (double)musicVolTrackbar.Value / 10;
                mp.Play();
                sp.Play();
                is_muted = false;
            }

        }

        /// <summary>
        /// Obsluga zdarzenia zmian na scrollbarze do zmiany glosnosci dzwieku w tle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MusicVolumeTrackBar_Scroll(object sender, EventArgs e)
        {
            mp.Volume = (double)MusicVolumeTrackBar.Value / 10;
        }

        /// <summary>
        /// Obsluga zdarzenia zmian na scrollbarze do zmiany glosnosci pojedynczego dzwieku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void musicVolTrackbar_Scroll(object sender, EventArgs e)
        {
            sp.Volume = (double)musicVolTrackbar.Value / 10;
            sp.Play();
        }

        
        /// <summary>
        /// Obluga zdarzenia wyjscia z manu ustawien dzwieku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            stopPlayer();

            this.Visible = false;          
            ((MenuForm)Parent).player.AudioSettings.applyChanges((double)MusicVolumeTrackBar.Value / 10, (double)musicVolTrackbar.Value / 10, MuteCheckbox.Checked,((MenuForm)Parent).player.Name);
            ((MenuForm)Parent).settings_panel.Visible = true;
           
        }
    }
}
