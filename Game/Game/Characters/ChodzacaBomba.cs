using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Game.Characters
{
   
    /// <summary>
    /// Klasa przeciwnika poruszajacego sie 2 razy szybciej niz Vandal, 
    /// gdy trafi na jakis obiekt wybucha
    /// </summary>
    class ChodzacaBomba : Enemy
    {

        private bool explode = false;
        private bool die = false;
        /// <summary>
        /// Czynnik losowy w ruchu
        /// </summary>
        Random rand = new Random();

        /// <summary>
        /// Sciezka do tekstury
        /// </summary>
        const string asset_name = "Textures\\chodzaca_bomba";

        /// <summary>
        /// Scieżka do tektsury z animacja wybuchu 
        /// </summary>
        const string explosion_animation = "Textures\\explosion";

        private int current_frame = 0;
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="content">XNA Content</param>
        /// <param name="rectangle">Prostokąt określający pozycję na mapie</param>
        /// <param name="x">indeks w tablicy obiektów</param>
        /// <param name="y">indeks w tablicy obiektów</param>
        public ChodzacaBomba(ContentManager content, Rectangle rectangle, int x, int y)
            : base(content, rectangle, x, y)
        {
            this.points = 200;
            TypeTag = Map.ElementType.CHODZACABOMBA;
            texture = content.Load<Texture2D>(asset_name);
            current_direction = Game.direction.down;
            this.move_frequency = 10;
        }


        /// <summary>
        /// Poruszanie - ustalenie aktualnego kierunku ruchu
        /// </summary>
        /// <param name="map">Mapa obiektów</param>
        public void Move(Map.Map map)
        {
            List<Game.direction> available_dir = new List<Game.direction>();
            if (x > 1 && map.getObject(x - 1, y).GetType() == typeof(NonDestroyableObjects.Puste))
                available_dir.Add(Game.direction.left);
            if (x < map.getWidth() - 1 && map.getObject(x + 1, y).GetType() == typeof(NonDestroyableObjects.Puste))
                available_dir.Add(Game.direction.right);
            if (y > 1 && map.getObject(x, y - 1).GetType() == typeof(NonDestroyableObjects.Puste))
                available_dir.Add(Game.direction.up);
            if (x < map.getHeight() - 1 && map.getObject(x, y + 1).GetType() == typeof(NonDestroyableObjects.Puste))
                available_dir.Add(Game.direction.down);
            if (available_dir.Capacity > 0)
            {
                int rand_dir = rand.Next(0, available_dir.Capacity - 1);
                current_direction = (Game.direction)(rand_dir);
            }
            else current_direction = Game.direction.none;

        }


        /// <summary>
        /// Aktualizacja stanu Bloba na mapie
        /// Poruszanie jeśli jest ustalony kierunek ruchu
        /// </summary>
        /// <param name="gametime">Czas gry</param>
        /// <param name="map">Mapa obiektów</param>
        public override void Update(GameTime gametime, Map.Map map)
        {
            if (gametime.TotalGameTime.Milliseconds % move_frequency == 0)
            {
                int add_x = 0;
                int add_y =0 ;
                Move(map);
                if (!explode && !die)
                {
                    switch (current_direction)
                    {
                        case Game.direction.down:
                            add_x = 0;
                            add_y = 1;
                            collision_obj = map.getObject(x, y + 1);
                            break;
                        case Game.direction.left:
                            add_x = -1;
                            add_y = 0;
                            collision_obj = map.getObject(x - 1, y);
                            break;
                        case Game.direction.right:
                            add_x = 1;
                            add_y = 0;
                            collision_obj = map.getObject(x + 1, y);
                            break;
                        case Game.direction.up:
                            add_x = 0;
                            add_y = -1;
                            collision_obj = map.getObject(x, y - 1);
                            break;
                        default:
                            break;
                    }
                    if (collision_obj.GetType() != typeof(NonDestroyableObjects.Puste) && collision_obj.GetType() != typeof(DestroyableObjects.Ziemia))
                        FireBomb(map);
                    if (collision_obj.GetType() == typeof(NonDestroyableObjects.Puste))
                        MoveInDirection(add_x, add_y, map);
                }
                else if (explode)
                {
                    if (current_frame == 16) { die = true; explode = false; }
                    else
                        src_rectangle = new Rectangle((current_frame++) * 64, 0, 64, 64);
                    
                }
                else if (die)
                {
                    Die(map);
                }
        
            }
         

        }

       
 


        /// <summary>
        /// Reakcja na zabicie
        /// Pozostawia po sobie puste pole
        /// </summary>
        /// <param name="map"></param>
        public override void Die(Map.Map map)
        {
            map.AddPlayersPoints(points);
            map.setObject(x, y, new NonDestroyableObjects.Puste(content, this.rectangle, this.x, this.y));
        }

        /// <summary>
        /// Reakcja na napotkanie innego obiektu - wybuch
        /// </summary>
        /// <param name="map"></param>
        public void FireBomb(Map.Map map)
        {
            explode = true;
            map.AddPlayersPoints(points);
            texture = content.Load<Texture2D>(explosion_animation);

        }


    }



}

