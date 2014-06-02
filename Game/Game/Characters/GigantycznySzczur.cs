using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using Game.Features;

namespace Game.Characters
{

    /// <summary>
    /// Klasa przciwnika, ktory po zauwazeniu Vandala goni go
    /// w przeciwnym przypadku porusza sie losowo
    /// potrafi drazyc w ziemi
    /// Bardzo wolny - 2 razy wolniejszy niz gracz
    /// </summary>
    class GigantycznySzczur : Enemy
    {
        /// <summary>
        /// Czynnik losowy w ruchu
        /// </summary>
        Random rand = new Random();

        /// <summary>
        /// Scieżka do tekstury
        /// </summary>
        const string asset_name = "Textures\\gigantyczny_szczur";

        /// <summary>
        /// Konstruktor 
        /// </summary>
        /// <param name="content">XNA Content</param>
        /// <param name="rectangle">Prostokąt określający pozycję na mapie</param>
        /// <param name="x">indeks w tablicy obiektów</param>
        /// <param name="y">indeks w tablicy obiektów</param>
        public GigantycznySzczur(ContentManager content, Rectangle rectangle, int x, int y)
            : base(content, rectangle, x, y)
        {
            this.points = 500;
            this.current_direction = Game.direction.down;
            TypeTag =Map.ElementType.GIGANTYCZNYSZCZUR;
            texture = content.Load<Texture2D>(asset_name);
            this.move_frequency = 40;
        }

        /// <summary>
        /// Funkcja dla AI szczura (sprawdza czy Vandal jest w zasiegu wzroku szczura - jesli tak - to wyznacza kierunk ruc)
        /// </summary>
        /// <param name="map">Mapa obiektów</param>
        /// <returns>Punkt reprezentujacy kierunek poruszania Gigantycznego Szczura</returns>
        public Point isVandalInSight(Map.Map map)
        {        
            int x_move = 0;
            int y_move = 0;
            int vandal_x = map.vandal.x;
            int vandal_y = map.vandal.y;
            if (vandal_x == this.x)
            {
                int lower_ind = vandal_y<this.y?vandal_y:this.y;
                int higher_ind = vandal_y<this.y?this.y:vandal_y;
                for (int i = lower_ind + 1; i < higher_ind; i++)
                {
                    if (map.getObject(this.x, i).GetType() != typeof(NonDestroyableObjects.Puste))
                    {
                        y_move = 0;
                        break;
                    }
                    else y_move = lower_ind == this.y ? 1 : -1;
                }

             
            }
            if (vandal_y == this.y)
            {
                int lower_ind = vandal_x < this.x ? vandal_x : this.x;
                int higher_ind = vandal_x < this.x ? this.x : vandal_x;
                for (int i = lower_ind + 1; i < higher_ind; i++)
                {
                    if (map.getObject(i, this.y).GetType() != typeof(NonDestroyableObjects.Puste))
                    {
                        x_move = 0;
                        break;
                    }
                   else x_move = lower_ind == this.x ? 1 : -1;
                }
              
            }
            return new Point(x_move, y_move);

        }

        /// <summary>
        /// Poruszanie - ustalenie aktualnego kierunku ruchu
        /// </summary>
        /// <param name="map">mapa obiektów</param>
       public void Move(Map.Map map)
        {
            Point p = isVandalInSight(map);
            if ((p.X != 0 && p.Y == 0) || (p.X == 0 && p.Y != 0)) 
            {
                //gonitwa
                move_frequency = 40;
                if ((this.x - map.GetVandal().x) < 0)
                {
                    current_direction=Game.direction.right;
                }
                else if ((this.x - map.GetVandal().x) > 0)
                {

                    current_direction = Game.direction.left;
                }
                else if ((this.y - map.GetVandal().y) > 0)
                {

                    current_direction = Game.direction.up;
                }
                else if ((this.y - map.GetVandal().y) < 0)
                {

                    current_direction = Game.direction.down;
                }
                

           }
            else//ruch losowy
            {
                move_frequency = 80;
                List<Game.direction> available_dir = new List<Game.direction>();
                if (x > 1 && (map.getObject(x - 1, y).GetType() == typeof(NonDestroyableObjects.Puste) || map.getObject(x - 1, y).GetType() == typeof(DestroyableObjects.Ziemia)))
                    available_dir.Add(Game.direction.left);
                if (x < map.getWidth() - 1 && (map.getObject(x + 1, y).GetType() == typeof(NonDestroyableObjects.Puste) || map.getObject(x + 1, y).TypeTag == Map.ElementType.ZIEMIA))
                    available_dir.Add(Game.direction.right);
                if (y > 1 && (map.getObject(x, y - 1).GetType() == typeof(NonDestroyableObjects.Puste) || map.getObject(x, y - 1).GetType() == typeof(DestroyableObjects.Ziemia)))
                    available_dir.Add(Game.direction.up);
                if (x < map.getHeight() - 1 && (map.getObject(x, y + 1).GetType() == typeof(NonDestroyableObjects.Puste) || map.getObject(x, y + 1).GetType() == typeof(DestroyableObjects.Ziemia)))
                    available_dir.Add(Game.direction.down);
                if (available_dir.Capacity > 0)
                {

                    int rand_dir = rand.Next(0, available_dir.Capacity - 1);
                    current_direction = (Game.direction)(rand_dir);

                }
                else
                {

                    current_direction = Game.direction.none;
                }
            }
        }

        /// <summary>
        /// Aktualizacja stanu szczura
        /// </summary>
        /// <param name="gametime">czas gry</param>
        /// <param name="map">mapa obiektów</param>
        public override void Update(GameTime gametime, Map.Map map)
       {
           Move(map);
            if (gametime.TotalGameTime.Milliseconds % move_frequency == 0)
            {
                
                switch (current_direction)
                {
                    case Game.direction.down:
                     
                        MoveInDirection(0, 1, map);
                        break;
                    case Game.direction.left:
                      
                        MoveInDirection(-1, 0, map);
                        break;
                    case Game.direction.right:
                      
                        MoveInDirection(1, 0, map);
                        break;
                    case Game.direction.up:
                      
                        MoveInDirection(0, -1, map);
                        break;
                    default:
                        break;
                }

            }

        }

        /// <summary>
        /// Reakcja na pokonanie
        /// </summary>
        /// <param name="map">mapa obiektów</param>
         public override void Die(Map.Map map)
        {
            map.AddPlayersPoints(points);
            FireBomb(map);
        }

        /// <summary>
        /// Wyrzucenie bomby
        /// </summary>
        /// <param name="map">mapa obiektów</param>
        public void FireBomb(Map.Map map)
        {
            map.setObject(x, y, new NonDestroyableObjects.Puste(content, this.Rectangle,x, y));
        }


    }
}
