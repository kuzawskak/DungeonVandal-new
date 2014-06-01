using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Features;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game.DestroyableObjects
{
    /// <summary>
    /// Obiekt zniszczalny bez specjalnych wlasciwosci
    /// </summary>
    class LekkiMur : Map.MapObject,Zniszczalny
    {
        /// <summary>
        /// Punkty naliczane za zniszczenie lekkiego muru
        /// </summary>
        private int points;

        /// <summary>
        /// Scieżka do tekstury
        /// </summary>
        private string asset_name = "Textures\\lekki_mur";

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="content">XNA Content</param>
        /// <param name="rectangle">Prostokat określający pozycję na mapie</param>
        /// <param name="x">indeks x w tablicy obiektów</param>
        /// <param name="y">indeks y w tablicy obiektów</param>
        public LekkiMur(ContentManager content, Rectangle rectangle, int x, int y):base (content,rectangle,x,y)
        {
            this.points = 3;
            TypeTag = Map.ElementType.LEKKIMUR;
            texture = content.Load<Texture2D>(asset_name);
            this.IsAccesible = false;

        }

        void Draw(GameTime gameTime) { }


        /// <summary>
        /// Obsługa zniszczenia (dodanie punktów do puli zebranych)
        /// </summary>
        /// <param name="map"></param>
        public void OnDestroy(Map.Map map)
        {
            map.AddPlayersPoints(points);
            map.setObject(x, y, new NonDestroyableObjects.Puste(content, this.Rectangle, x, y));
        }
    }
}
