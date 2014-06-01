using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Features;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;
using System.Windows.Forms;

namespace Game.DestroyableObjects
{
    /// <summary>
    /// Obiekt zniszczalny , co losowy czas tworzy kolejne ameby dookola siebie na SŁABYCH polach
    /// </summary>
    class Ameba : Map.MapObject, Kruchy, Skazony, Niestabilny, Zniszczalny
    {
        /// <summary>
        /// czynnik losowy przy wyborze nastepnego pola dla nowej ameby
        /// </summary>
        Random rand = new Random();
        /// <summary>
        /// Dostepne komorki na mapie do wstawiania nowej ameby
        /// </summary>
        List<Map.MapObject> available_tiles;
        /// <summary>
        /// Losowy czas namnażania ameb
        /// </summary>
        private int random_new_ameba_time;
        /// <summary>
        /// punkty za zniszczenie
        /// </summary>
        private int points;
        /// <summary>
        /// sciezka do tekstury
        /// </summary>
        private string asset_name = "Textures\\ameba";
        /// <summary>
        /// stan  - czy nastepuje tworzenie nowej ameby
        /// </summary>
        private bool create_ameba = false;
        /// <summary>
        /// Timer do odmierzania czasu stworzenia nowej ameby
        /// </summary>
        private System.Timers.Timer timer;
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="content">XNA content</param>
        /// <param name="rectangle">prostokat polozenia na mapie</param>
        /// <param name="x">indeks x w tablicy obiektow</param>
        /// <param name="y">indeks y w tablicy obiektów</param>
        public Ameba(ContentManager content, Rectangle rectangle, int x, int y):base(content,rectangle,x,y)
        {
            TypeTag = Map.ElementType.AMEBA;   
            texture = content.Load<Texture2D>(asset_name);
            this.IsAccesible = false;
            this.points = 2;
            random_new_ameba_time = rand.Next(6, 10);
            timer = new System.Timers.Timer(random_new_ameba_time * 1000);
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Start();

        }

        /// <summary>
        /// Aktualizacja stanu Ameby
        /// </summary>
        /// <param name="gametime">czas gry</param>
        /// <param name="map">mapa obiektow</param>
        public override void Update(GameTime gametime, Map.Map map)
        {
            if (create_ameba)
            {
                available_tiles = new List<Map.MapObject>();

                if (x > 0 && y > 0 && map.getObject(x - 1, y - 1) is Features.Slaby)
                    available_tiles.Add(map.getObject(x - 1, y - 1));
                if (x > 0 && map.getObject(x - 1, y) is Features.Slaby)
                    available_tiles.Add(map.getObject(x - 1, y));
                if (x > 0 && y < map.getHeight() - 1 && map.getObject(x - 1, y + 1) is Features.Slaby)
                    available_tiles.Add(map.getObject(x - 1, y + 1));
                if (y > 0 && map.getObject(x, y - 1) is Features.Slaby)
                    available_tiles.Add(map.getObject(x, y - 1));
                if (y < map.getHeight() - 1 && map.getObject(x, y + 1) is Features.Slaby)
                    available_tiles.Add(map.getObject(x, y + 1));
                if (x < map.getWidth() - 1 && y > 0 && map.getObject(x + 1, y - 1) is Features.Slaby)
                    available_tiles.Add(map.getObject(x + 1, y - 1));
                if (x<map.getWidth()-1&&map.getObject(x + 1, y) is Features.Slaby)
                    available_tiles.Add(map.getObject(x + 1, y));
                if (x<map.getWidth()-1&&y<map.getHeight()-1&&map.getObject(x + 1, y + 1) is Features.Slaby)
                    available_tiles.Add(map.getObject(x + 1, y + 1));
                CreateNewAmeba(map);
                create_ameba = false;
            }

        }
        /// <summary>
        /// Zdarzenie na upływ czasu stworzenia nowej ameby - utworzenie i wystartowanie ponowne timera
        /// </summary>
        /// <param name="source">żródło sygnału</param>
        /// <param name="e">argument wyowalania</param>
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            create_ameba = true;
            timer.Start();
        }

        void Kruchy.Kolizja()
        {
           
        }

        void Skazony.Zabij(Map.Map map)
        {
           
        }

        void Niestabilny.Znikaj(Map.Map map)
        {
            
        }

        /// <summary>
        /// Stworzenie nowej ameby na dostepnym miejscu
        /// </summary>
        /// <param name="map">mapa obiektow</param>
        void CreateNewAmeba(Map.Map map)
        {
            //losuj dostepne puste miejsce
            if (available_tiles.Capacity > 0)
            {
                int random_pos = rand.Next(0, available_tiles.Capacity - 1);
                map.setObject(available_tiles[random_pos].x, available_tiles[random_pos].y, new DestroyableObjects.Ameba(content, new Rectangle(available_tiles[random_pos].x * rectangle.Width, available_tiles[random_pos].y * rectangle.Height, rectangle.Width, rectangle.Height), available_tiles[random_pos].x, available_tiles[random_pos].y));
            }
        }
        void OnExplosion()
        {

        }


        /// <summary>
        /// Reakcja na zniszczenie
        /// </summary>
        /// <param name="map">mapa obiektow</param>
        public void OnDestroy(Map.Map map)
        {
            map.AddPlayersPoints(points);
            map.setObject(x,y, new NonDestroyableObjects.Puste(content, this.Rectangle,x, y));
         
        }
    }
}
