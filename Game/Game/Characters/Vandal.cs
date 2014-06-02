using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Audio;
using Game.Features;
using System.Timers;


namespace Game.Characters
{
    /// <summary>
    /// Postac któej ruchem i zachowaniem steruje gracz
    /// </summary>
    public class Vandal : Map.MapObject
    {
        /// <summary>
        /// Czy jest żywy
        /// </summary>
        public bool is_alive { get; set; }
        /// <summary>
        /// Czy podniesiony poziom
        /// </summary>
        public bool level_up { get; set; }
        /// <summary>
        /// Czas pozostaly przez ktory Vandal jest niesmiertelny
        /// </summary>
        public bool is_immortal { get; set; }

        /// <summary>
        /// Kierunek ruchu
        /// </summary>
        public Game.direction current_direction { get; private set; }
        /// <summary>
        /// Kierunek patrzenia
        /// </summary>
        public Game.direction view_direction { get; private set; }

        /// <summary>
        /// Obiekt na mapie , z ktorym nastapila kolizja
        /// </summary>
        Map.MapObject collision_obj = null;

        /// <summary>
        /// Timer do sprawdzania niesmiertelnosci gracza
        /// </summary>
        private System.Timers.Timer immortal_timer;

        /// <summary>
        /// Scieżka do Tekstury
        /// </summary>
        private string asset_name;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="content">XNA content</param>
        /// <param name="rectangle">Prostokat pozycji na mapie</param>
        /// <param name="x">indeks x w tablicy obiektów</param>
        /// <param name="y">indeks y w tablicy obiektów</param>
        public Vandal(ContentManager content, Rectangle rectangle, int x, int y)
            : base(content, rectangle, x, y)
        {
            this.TypeTag = Map.ElementType.VANDAL;
            asset_name = "Textures\\vandal_right";
            texture = content.Load<Texture2D>(asset_name);
            this.is_alive = true;
            this.level_up = false;
            current_direction = Game.direction.none;
        }


        /// <summary>
        /// Ustawienie niesmertelnosci po podniesieniu Pola Silowego
        /// </summary>
        public void setImmortal()
        {
            is_immortal = true;
            immortal_timer = new System.Timers.Timer(5000);
            immortal_timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            immortal_timer.Start();
        }

        /// <summary>
        /// Utrata niesmiertelnosci po czasie 5 sekund
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            is_immortal = false;
            LoadCurrentTexture(null);
        }


        /// <summary>
        /// Upusc dynamit
        /// </summary>
        /// <param name="game_map">mapa obiektow</param>
        /// <param name="gametime">czas gry</param>
        public void LeftDynamite(Map.Map game_map, GameTime gametime)
        {
            int dyn_x = x - 1;
            int dyn_y = y;
            switch (view_direction)
            {
                case Game.direction.right:
                    dyn_x = x - 1;
                    dyn_y = y;
                    break;
                case Game.direction.left:
                    dyn_x = x + 1;
                    dyn_y = y;
                    break;
                case Game.direction.up:
                    dyn_x = x;
                    dyn_y = y + 1;
                    break;
                case Game.direction.down:
                    dyn_x = x;
                    dyn_y = y - 1;
                    break;


            }

            //nie zmieniamy pozycji Vandala (wstawiamy dynamit za Vandalem)
            collision_obj = game_map.getObject(dyn_x, dyn_y);
            if (collision_obj.GetType() == typeof(NonDestroyableObjects.Puste))
            {
                game_map.setObject(dyn_x, dyn_y, new Weapon.Dynamit(content, new Rectangle(dyn_x * this.rectangle.Width, dyn_y * this.rectangle.Height, this.rectangle.Width, this.rectangle.Height), dyn_x, dyn_y, gametime));
                game_map.addPlayersDynamites(-1);
            }

        }

        /// <summary>
        /// Wystrzel rakiete
        /// </summary>
        /// <param name="game_map">mapa obiektow</param>
        public void AttackWithRacket(Map.Map game_map)
        {
            Map.MapObject collision_obj = null;
            switch (view_direction)
            {
                case Game.direction.down:
                    collision_obj = game_map.getObject(x, y + 1);
                    break;
                case Game.direction.up:
                    collision_obj = game_map.getObject(x, y - 1);
                    break;
                case Game.direction.right:
                    collision_obj = game_map.getObject(x + 1, y);
                    break;
                case Game.direction.left:
                    collision_obj = game_map.getObject(x - 1, y);
                    break;
            }


            if (collision_obj != null && (collision_obj.GetType() == typeof(NonDestroyableObjects.Puste) || collision_obj.IsAccesible))
            {

                Map.MapObject racket = new Weapon.Racket(content, collision_obj.rectangle, collision_obj.x, collision_obj.y, view_direction);
                game_map.setObject(collision_obj.x, collision_obj.y, racket);
               
                SoundEffect fire_racket_effect = content.Load<SoundEffect>("Audio\\fire_racket");
                if (!game_map.player.AudioSettings.IsMuted)
                {
                    SoundEffect.MasterVolume = (float)game_map.player.AudioSettings.SoundVolume;
                    fire_racket_effect.Play();
                }
            }
        }

        /// <summary>
        /// Zmien kierunek poruszania/patrzenia
        /// </summary>
        /// <param name="game_map">mapa obiektów</param>
        public void changeDirectionToNext(Map.Map game_map)
        {
            current_direction = (Game.direction)(((int)current_direction + 1) % 3 + 1);
            view_direction = current_direction;
            LoadCurrentTexture(game_map);

        }

        /// <summary>
        /// Ładownaie odpowiedniej textury w zaleznosci od ustawienia bohatera na mapie i kierunku patrzenia
        /// </summary>
        /// <param name="game_map">mapa obiektów</param>
        public void LoadCurrentTexture(Map.Map game_map)
        {
            //collision_obj = game_map.getObject(rectangle.X / rectangle.Width, rectangle.Y / rectangle.Height);

            switch (current_direction)
            {
                case Game.direction.left:
                    asset_name = "Textures\\vandal_left";
                    break;
                case Game.direction.right:
                    asset_name = "Textures\\vandal_right";
                    break;
                case Game.direction.up:
                    asset_name = "Textures\\vandal_left";
                    break;
                case Game.direction.down:
                    asset_name = "Textures\\vandal_right";
                    break;
                default:
                    asset_name = "Textures\\vandal_right";
                    break;

            }

            if (is_immortal)
                asset_name += "_immortal";
            this.texture = content.Load<Texture2D>(asset_name);
        }

        void Draw(GameTime gameTime) { }

        /// <summary>
        /// Poruszanie w wyznaczonym kierunku
        /// </summary>
        /// <param name="add_x">przyrost indeksu w poziomie</param>
        /// <param name="add_y">przyrost indeksu w pionie</param>
        /// <param name="map">mapa obiektow</param>
        public void MoveInDirection(int add_x, int add_y, Map.Map map)
        {
            int new_x = x + add_x;
            int new_y = y + add_y;
            collision_obj = map.getObject(new_x, new_y);
            if (collision_obj.IsAccesible)
            {
                if (collision_obj is Weapon.Weapon)
                {
                    (collision_obj as Weapon.Weapon).OnFound(map);
                }

                if (collision_obj is Zniszczalny)
                    (collision_obj as Zniszczalny).OnDestroy(map);




                map.setObject(new_x, new_y, this);
                if (!(collision_obj is Eteryczny))
                    map.setObject(x, y, new NonDestroyableObjects.Puste(content, new Rectangle(x * this.rectangle.Width, y * this.rectangle.Height, this.rectangle.Width, this.rectangle.Height), x, y));

                x = new_x;
                y = new_y;
            }
            else if (add_y == 0 && collision_obj is Przesuwalny && (collision_obj as Przesuwalny).Przesun(map, add_x))
            {
                map.setObject(x, y, new NonDestroyableObjects.Puste(content, new Rectangle(x * this.rectangle.Width, y * this.rectangle.Height, this.rectangle.Width, this.rectangle.Height), x, y));
                map.setObject(new_x, new_y, this);
                x = new_x;
                y = new_y;
            }
            else if (!map.GetVandal().is_immortal && collision_obj.GetType().IsSubclassOf(typeof(Characters.Enemy)))
            {
                this.is_alive = false;
            }



        }


        /// <summary>
        /// Poruszanie - ustalenie aktualnego kierunku poruszania
        /// </summary>
        /// <param name="direction">kierunek poruszania</param>
        public void Move(Game.direction direction)
        {
            if (direction != Game.direction.none)
                view_direction = direction;
            current_direction = direction;

        }

        /// <summary>
        /// aktualizacja stanu Vandala
        /// </summary>
        /// <param name="gametime">czas gry</param>
        /// <param name="map">mapa obiektow</param>
        public override void Update(GameTime gametime, Map.Map map)
        {
            if (!is_immortal)
            {
                //sprawdzenie czy styka sie z wrogiem 
                if (!map.GetVandal().is_immortal &&
                    map.getObject(x, y).GetType() == typeof(Characters.Enemy) || map.getObject(x, y) is Skazony)
                { is_alive = false; return; }

            }

            LoadCurrentTexture(map);
            if(gametime.TotalGameTime.Milliseconds%20==0)
            switch (current_direction)
            {
                case Game.direction.down:
                    MoveInDirection(0, 1, map);
                    current_direction = Game.direction.none;
                    break;
                case Game.direction.left:
                    if (rectangle.X > 0)
                        MoveInDirection(-1, 0, map);
                    current_direction = Game.direction.none;
                    break;
                case Game.direction.right:
                    MoveInDirection(1, 0, map);
                    current_direction = Game.direction.none;
                    break;
                case Game.direction.up:
                    if (rectangle.Y > 0)
                        MoveInDirection(0, -1, map);
                    current_direction = Game.direction.none;
                    break;
                default:
                    MoveInDirection(0, 0, map);
                    current_direction = Game.direction.none;
                    break;


            }
        }

    }

}

