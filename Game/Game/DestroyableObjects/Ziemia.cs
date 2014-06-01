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
    /// Klasa obiektu Ziemia (zajmuje większość mapy)
    /// 
    /// </summary>
    class Ziemia : Map.MapObject,Slaby,Zniszczalny
    {
        /// <summary>
        /// Punkty naliczane za zniszczenie obiektu
        /// </summary>
        int points;
        /// <summary>
        /// Nazwa assetu dla XNA Content
        /// </summary>
        const string asset_name = "Textures\\ziemia";

        /// <summary>
        /// Znikanie obiektu po przejściu Vandala
        /// </summary>
        /// <param name="map"></param>
        public void Znikaj(Map.Map map)
        {       
            int x_ind = (int)(this.rectangle.X / this.Rectangle.Width);
            int y_ind = (int)(this.rectangle.Y / this.Rectangle.Height);
            map.setObject(x_ind, y_ind, new NonDestroyableObjects.Puste(content, new Rectangle((x_ind) * this.rectangle.Width, y_ind * this.Rectangle.Height, this.rectangle.Width, this.rectangle.Width),x_ind,y_ind));
        }
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="content">XNA Content</param>
        /// <param name="rectangle">prostokąt reprezentujący obiekt na mapie</param>
        /// <param name="x">indeks x w tablicy obiektow</param>
        /// <param name="y">indeks y w tablicy obiektow</param>
        public Ziemia(ContentManager content, Rectangle rectangle,int x, int y): base (content,rectangle,x,y)
        {
            TypeTag = Map.ElementType.ZIEMIA;
            this.points = 1;
            texture = content.Load<Texture2D>(asset_name);
            IsAccesible = true;

        }

        /// <summary>
        /// Aktualizacja stanu obiektu w zależności od tego czy Vandal po nim chodzi
        /// </summary>
        /// <param name="gametime">czas gry</param>
        /// <param name="map">mapa obiektow</param>
        public override void Update(GameTime gametime, Map.Map map)
        {
            int x = rectangle.X/rectangle.Width;
            int y = rectangle.Y/rectangle.Width;
            if (map.is_vandal_on_rectangle(x, y))
            {               
                this.Znikaj(map);
            }
        }
        void Draw(GameTime gameTime) { }

        /// <summary>
        /// Obsługa zniszczenia obiektu
        /// </summary>
        /// <param name="map"></param>
        public void OnDestroy(Map.Map map)
        {
            map.AddPlayersPoints(points);
            map.setObject(rectangle.X / rectangle.Width, rectangle.Y / rectangle.Height, new NonDestroyableObjects.Puste(content, this.Rectangle, rectangle.X / rectangle.Width, rectangle.Y / rectangle.Height));            
            
        }
    }
}
