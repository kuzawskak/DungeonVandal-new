using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game.Map
{
    /// <summary>
    /// Klasa elementu mapy
    /// </summary>
    public class MapObject
    {
        /// <summary>
        /// Sciezka do teksury
        /// </summary>
        protected Texture2D texture;
        /// <summary>
        /// Prostokata reprezentujacy obiekt na mapie
        /// </summary>
        public Rectangle rectangle;

        /// <summary>
        /// Prostokat aktualnej klatki animacji w przypadku animowanej tekstury
        /// </summary>
        public Rectangle? src_rectangle;
        /// <summary>
        /// Xna content
        /// </summary>
        protected ContentManager content;

        /// <summary>
        /// czy mozna wejsc na ten obiekt
        /// </summary>
        public bool IsAccesible
        {
            get;
            protected set;
        }
        /// <summary>
        /// Współrzędna x na obiektu w tablicy reprezentującej mapę
        /// </summary>
        public int x { get;  set; }
        /// <summary>
        /// Współrzędna y obiektu w tablicy reprezentującej mapę
        /// </summary>
        public int y { get; set; }

        /// <summary>
        /// Getter/setter dla tekstury
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        /// <summary>
        /// Getter /setter dla prostokata
        /// </summary>
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }
        /// <summary>
        /// Konstruktor bezparametrowy
        /// </summary>
        public MapObject() { }

        /// <summary>
        /// getter/cetter dla typu
        /// </summary>
        public ElementType TypeTag { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="content">xna content</param>
        /// <param name="rectangle">prostokat na mapie</param>
        /// <param name="x">indeks z w tablicy obiektow</param>
        /// <param name="y">indeks y w tablicy obiektow</param>
        public MapObject(ContentManager content, Rectangle rectangle, int x, int y)
        {
            this.content = content;
            this.rectangle = rectangle;
            this.x = x;
            this.y = y;

        }

        /// <summary>
        /// Rysowanie obiektu na mapie
        /// </summary>
        /// <param name="spritebatch"></param>
        public void Draw(SpriteBatch spritebatch)
        {

            spritebatch.Draw(texture, rectangle, Color.White);
        }

        /// <summary>
        /// Aktualizacja stanu obiektu
        /// </summary>
        /// <param name="gametime">czas gry</param>
        /// <param name="map">mapa obiektow</param>
        public virtual void Update(GameTime gametime, Map map)
        {
        }

        void onExplosion()
        {
            //default do nothing
        }
        void Die()
        {
            //change itself to empty firld
            //get map and 
        }
    }
}
