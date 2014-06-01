using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.NonDestroyableObjects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Game.NonDestroyableObjects
{
    /// <summary>
    /// Niezniszczalne pole, po którym moga się swobodnie poruszac bohater i przeciwnicy
    /// oraz moga spadac na tej sciezce obiekty reagujace na grawitacje
    /// </summary>
    class Puste:Map.MapObject
    {
        /// <summary>
        /// Sciezka do tekstury
        /// </summary>
        const string asset_name = "Textures\\puste";

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="content">XNA Content</param>
        /// <param name="rectangle">Prostokat okreslajacy pozycje na ekranie gry</param>
        /// <param name="x"> Indeks x na mapie obiektow</param>
        /// <param name="y"> Indeks y na mapie obiektow</param>
        public Puste(ContentManager content, Rectangle rectangle,int x, int y):base(content,rectangle,x,y)
        {
            TypeTag = Map.ElementType.PUSTE;
            this.texture = content.Load<Texture2D>(asset_name);
            IsAccesible = true;

        }


   
    }
}
