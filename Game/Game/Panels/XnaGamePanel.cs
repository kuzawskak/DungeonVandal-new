using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Game.Panels
{
    /// <summary>
    /// Panel na któryj wyświetlana jest gra (renederowana w PictureBox)
    /// </summary>
    public partial class XnaGamePanel : UserControl
    {
        /// <summary>
        /// Konstruktor panelu z grą (generowany z Windows Forms)
        /// </summary>
        public XnaGamePanel()
        {
            InitializeComponent();
        }
    }
}
