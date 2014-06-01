using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Game.Settings;
using DungeonVandal;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework;
using XMLManager.XMLObjects;

namespace Game.Panels
{
    /// <summary>
    /// Panel z ustawieniami klawiatury (sterowania)
    /// </summary>
    
    public partial class KeyboardSettingsPanel : UserControl
    {
        /// <summary>
        /// Słownik zmian do zapisania
        /// </summary>
        Dictionary<State, Keys> keyboard_changes = new Dictionary<State, Keys>();
        /// <summary>
        /// obecny stan ustawień
        /// </summary>
        State current_state = State.NONE;
        /// <summary>
        /// Konstruktor panelu
        /// inicjalizacja kontrolek
        /// </summary>
        public KeyboardSettingsPanel()
        {
            InitializeComponent();         
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownHandler);
        }
        /// <summary>
        /// Załadowanie obecnych ustawień gracza
        /// </summary>
        public void LoaderPlayerSettings()
        {
            Settings.KeySettings current_settings = ((MenuForm)Parent).player.KeyboardSettings;
            button1.Text = current_settings.Up.ToString();
            button2.Text = current_settings.Down.ToString();
            button3.Text = current_settings.Right.ToString();
            button4.Text = current_settings.Left.ToString();
            button5.Text = current_settings.Block.ToString();
            button6.Text = current_settings.Dynamite.ToString();
            button7.Text = current_settings.Racket.ToString();


        }
        /// <summary>
        /// Reakcja na klikniecie przycisku zmiany klaiwsza do ruchu w górę
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            current_state = State.UP;
            
        }
        /// <summary>
        /// Reakcja na klikniecie przycisku zmiany klaiwsza do ruchu w dół
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            current_state = State.DOWN;
        }

        /// <summary>
        /// Reakcja na klikniecie przycisku zmiany klaiwsza do ruchu w prawo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            current_state = State.RIGHT;
        }

        /// <summary>
        /// Reakcja na klikniecie przycisku zmiany klaiwsza do ruchu w lewo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            current_state = State.LEFT;
        }

        /// <summary>
        /// Reakcja na klikniecie przycisku zmiany klawisza blokowania ruchu
         /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            current_state = State.BLOCK;
        }
        /// <summary>
        /// Reakcja na klikniecie przycisku zmiany klaiwsza zostawiania dynamitu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            current_state = State.DYNAMITE;
        }

        /// <summary>
        /// Reakcja na klikniecie przycisku zmiany klaiwsza wystrzelenia rakiety
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            current_state = State.RACKET;
        }

        /// <summary>
        /// Reakcja na zakonczenie wprowadzania zmian
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyUpHandler(object sender, KeyEventArgs e)
        {
            current_state = State.NONE;
        }

        /// <summary>
        /// Reakcja na wprowadzenie zmian w sterowaniu (wprowadzenie z klawiatury)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
            if (current_state != State.NONE)
            {
                switch (current_state)
                {
                    case State.UP:
                        button1.Text = e.KeyCode.ToString();
                   
                        break;
                    case State.DOWN:
                        button2.Text = e.KeyCode.ToString();
                        break;
                    case State.LEFT:
                        button4.Text = e.KeyCode.ToString();
                        break;
                    case State.RIGHT:
                        button3.Text = e.KeyCode.ToString();
                        break;
                    case State.DYNAMITE:
                        button6.Text = e.KeyCode.ToString();
                        break;
                    case State.RACKET:
                        button7.Text = e.KeyCode.ToString();
                        break;
                    case State.BLOCK:
                        button5.Text = e.KeyCode.ToString();
                        break;
                    
                }
                keyboard_changes.Add(current_state, e.KeyData);
            }
        }

        private void KeyboardSettingsPanel_Load(object sender, EventArgs e)
        {

        }

 
        /// <summary>
        /// Reakcja na wprowadzenie danych z klawiatury w przypadku Focusu na button1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!keyboard_changes.ContainsValue(e.KeyData))
            {
                button1.Text = e.KeyCode.ToString();
                TryAddNewSetting(current_state, e.KeyData);
            }
        }

        /// <summary>
        /// Reakcja na wprowadzenie danych z klawiatury w przypadku Focusu na button2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            if (!keyboard_changes.ContainsValue(e.KeyData))
            {
                button2.Text = e.KeyCode.ToString();
                TryAddNewSetting(current_state, e.KeyData);
            }
        }

        /// <summary>
        /// Reakcja na zakonczenie wprowadzania danych z klawiatury w przypadku Focusu na button1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_KeyUp(object sender, KeyEventArgs e)
        {
            current_state = State.NONE;
        }


        /// <summary>
        /// Reakcja na wprowadzenie danych z klawiatury w przypadku Focusu na button3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_KeyDown(object sender, KeyEventArgs e)
        {
            if (!keyboard_changes.ContainsValue(e.KeyData))
            {
                button3.Text = e.KeyCode.ToString();
                TryAddNewSetting(current_state, e.KeyData);
            }
        }

        private void button3_KeyUp(object sender, KeyEventArgs e)
        {
            current_state = State.NONE;
        }


        /// <summary>
        /// Reakcja na wprowadzenie danych z klawiatury w przypadku Focusu na button4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_KeyDown(object sender, KeyEventArgs e)
        {
            if (!keyboard_changes.ContainsValue(e.KeyData))
            {
                button4.Text = e.KeyCode.ToString();
                TryAddNewSetting(current_state, e.KeyData);
            }
        }



        private void button4_KeyUp(object sender, KeyEventArgs e)
        {
            current_state = State.NONE;
        }


        /// <summary>
        /// Reakcja na wprowadzenie danych z klawiatury w przypadku Focusu na button5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_KeyDown(object sender, KeyEventArgs e)
        {
            if (!keyboard_changes.ContainsValue(e.KeyData))
            {
                button5.Text = e.KeyCode.ToString();
                TryAddNewSetting(current_state, e.KeyData);
            }
        }

        private void button5_KeyUp(object sender, KeyEventArgs e)
        {
            current_state = State.NONE;
        }

        /// <summary>
        /// Reakcja na wprowadzenie danych z klawiatury w przypadku Focusu na button6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_KeyDown(object sender, KeyEventArgs e)
        {
            if (!keyboard_changes.ContainsValue(e.KeyData))
            {
                button6.Text = e.KeyCode.ToString();
                TryAddNewSetting(current_state, e.KeyData);
            }
        }
        
        /// <summary>
        /// Reakcja na wprowadzenie danych z klawiatury w przypadku Focusu na button7
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_KeyDown(object sender, KeyEventArgs e)
        {
            if (!keyboard_changes.ContainsValue(e.KeyData))
            {
                button7.Text = e.KeyCode.ToString();
                TryAddNewSetting(current_state, e.KeyData);
            }
        }

        private void button7_KeyUp(object sender, KeyEventArgs e)
        {
            current_state = State.NONE;
        }

        /// <summary>
        /// Obsługa wyjścia z menu ustawien klawiatury
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitButton_Click_1(object sender, EventArgs e)
        {
            //apply_settings

            ((MenuForm)Parent).player.KeyboardSettings.ApplyChanges(keyboard_changes,((MenuForm)Parent).player.Name);
            this.Visible = false;
            ((MenuForm)Parent).settings_panel.Visible = true;
       
        }


        private void button8_Click(object sender, EventArgs e)
        {
            //predefiniowane ustawienia - pokaz panel
            this.Visible = false;
            ((MenuForm)Parent).predefined_settings_panel.Visible = true;
           

        }

        private void button6_KeyUp(object sender, KeyEventArgs e)
        {
            current_state = State.NONE;
        }

        private void button2_KeyUp(object sender, KeyEventArgs e)
        {
            current_state = State.NONE;
        }

        private void button2_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                keyboard_changes = ((MenuForm)Parent).player.KeyboardSettings.ToMap();
                ClearSettingsChanges();
                LoaderPlayerSettings();
            }
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }



        private void button1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!keyboard_changes.ContainsValue(e.KeyData))
            {
                button1.Text = e.KeyCode.ToString();
                TryAddNewSetting(current_state, e.KeyData);
                current_state = State.NONE;
            }
        }


        private void button3_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!keyboard_changes.ContainsValue(e.KeyData))
            {
                button3.Text = e.KeyCode.ToString();
                TryAddNewSetting(current_state, e.KeyData);
                current_state = State.NONE;
            }

        }

        private void button2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!keyboard_changes.ContainsValue(e.KeyData))
            {
                button2.Text = e.KeyCode.ToString();
                TryAddNewSetting(current_state, e.KeyData);
                current_state = State.NONE;
            }
        }

        private void button4_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!keyboard_changes.ContainsValue(e.KeyData))
            {
                button4.Text = e.KeyCode.ToString();
                TryAddNewSetting(current_state, e.KeyData);
                current_state = State.NONE;
            }
        }

        private void button5_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!keyboard_changes.ContainsValue(e.KeyData))
            {
                button5.Text = e.KeyCode.ToString();
                TryAddNewSetting(current_state, e.KeyData);
                current_state = State.NONE;
            }
        }

        private void button6_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!keyboard_changes.ContainsValue(e.KeyData))
            {
                button6.Text = e.KeyCode.ToString();
                TryAddNewSetting(current_state, e.KeyData);
                current_state = State.NONE;
            }
        }

        private void button7_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            button7.Text = e.KeyCode.ToString();
            TryAddNewSetting(current_state, e.KeyData);
            current_state = State.NONE;
        }

        private void ClearSettingsChanges()
        {
            keyboard_changes.Clear();
        }

        private void TryAddNewSetting(State state, Keys keydata)
        {
            if(keyboard_changes.ContainsKey(state))
            keyboard_changes.Remove(state);
            keyboard_changes.Add(state, keydata);
        }


    }
}
