using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Features;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game.NonDestroyableObjects
{
    /// <summary>
    /// Obiekt niezniszczalny bez specjalnych wlasciwosci
    /// </summary>
    class CiezkiMur:Map.MapObject
    {
        /// <summary>
        /// Ścieżka do tekstury
        /// </summary>
        private string asset_name = "Textures\\ciezki_mur";

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="content">XNA Content</param>
        /// <param name="rectangle">Prostokąt okreżlający polozenia na mapie obiektów</param>
        /// <param name="x">indeks x w tablicy obiektów</param>
        /// <param name="y">indeks y w tablicy obiektów</param>
        public CiezkiMur(ContentManager content, Rectangle rectangle, int x, int y): base (content,rectangle,x,y)
        {
            TypeTag = Map.ElementType.CIEZKIMUR;
            texture = content.Load<Texture2D>(asset_name);
        }
    }
}
