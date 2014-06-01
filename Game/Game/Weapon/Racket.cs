using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Features;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Windows.Forms;

namespace Game.Weapon
{
    /// <summary>
    /// Klasa rakiety
    /// W zależności od konstruktora - jest bronią do zebrania lub bronią odpaloną
    /// </summary>
    class Racket : Weapon, Zniszczalny
    {


        /// <summary>
        /// Scieżka do tekstury
        /// </summary>
        private string asset_name = "Textures\\racket";

        /// <summary>
        /// Kierunek ruchu wystrzelonej rakiety
        /// </summary>
        Game.direction direction;

        /// <summary>
        /// Konstruktor dla rakiety do zebrania
        /// </summary>
        /// <param name="content"></param>
        /// <param name="rectangle">Prostokat reprezentujacy obiekt na mapie</param>
        /// <param name="x">indeks x na mapie obiektów</param>
        /// <param name="y">indeks y na mapie obiektów</param>
        public Racket(ContentManager content, Rectangle rectangle, int x, int y)
            : base(content, rectangle, x, y)
        {
            this.points = 50;
            this.is_fired = false;
            this.TypeTag = Map.ElementType.RACKET;
            this.texture = content.Load<Texture2D>(asset_name);
        }


        /// <summary>
        /// Konstruktor dla odpalonej rakiety
        /// </summary>
        /// <param name="content">XNA Content</param>
        /// <param name="rectangle">Prostokat reprezentujacy obiekt na mapie</param>
        /// <param name="x">indeks x na mapie obiektów</param>
        /// <param name="y">indeks y na mapie obiektów</param>
        /// <param name="direction">kierunek poruszania odpalonej rakiety</param>
        public Racket(ContentManager content, Rectangle rectangle, int x, int y, Game.direction direction)
            : base(content, rectangle, x, y)
        {
            this.points = 50;
            this.is_fired = true;
            this.direction = direction;
            this.TypeTag = Map.ElementType.RACKET;

            switch (direction)
            {
                case Game.direction.down:
                    texture = content.Load<Texture2D>("Textures\\racket_down");
                    break;
                case Game.direction.up:
                    texture = content.Load<Texture2D>("Textures\\racket_up");
                    break;
                case Game.direction.left:
                    texture = content.Load<Texture2D>("Textures\\racket_left");
                    break;
                case Game.direction.right:
                    texture = content.Load<Texture2D>("Textures\\racket_right");
                    break;

            }
        }

        /// <summary>
        /// Reakcja na zanlezienie przez Vandala
        /// </summary>
        /// <param name="map">Mapa obiektów</param>
        public override void OnFound(Map.Map map)
        {
            if (!map.player.AudioSettings.IsMuted)
            {
                SoundEffect.MasterVolume = (float)map.player.AudioSettings.SoundVolume;
                this.found_soundEffect.Play();
            }
            map.addPlayersRacket(1);
        }


        /// <summary>
        /// Aktualizacja stanu rakiety
        /// </summary>
        /// <param name="gametime">Czas gry</param>
        /// <param name="map">Mapa obiektów</param>
        public override void Update(GameTime gametime, Map.Map map)
        {
            if (gametime.TotalGameTime.Milliseconds % 20 == 0 && is_fired)
            {

                int collision_x = x;
                int collision_y = y;
                switch (direction)
                {
                    case Game.direction.down:
                        texture = content.Load<Texture2D>("Textures\\racket_down");
                        collision_x = x;
                        collision_y = y + 1;
                        break;
                    case Game.direction.up:
                        texture = content.Load<Texture2D>("Textures\\racket_up");
                        collision_x = x;
                        collision_y = y - 1;
                        break;
                    case Game.direction.left:
                        texture = content.Load<Texture2D>("Textures\\racket_left");
                        collision_x = x - 1;
                        collision_y = y;
                        break;
                    case Game.direction.right:
                        texture = content.Load<Texture2D>("Textures\\racket_right");
                        collision_x = x + 1;
                        collision_y = y;
                        break;
                }

                if (map.getObject(collision_x, collision_y).GetType() != typeof(NonDestroyableObjects.Puste) && map.getObject(collision_x, collision_y) != this)
                {


                    if (map.getObject(collision_x, collision_y).GetType().IsSubclassOf(typeof(Characters.Enemy)))
                    {
                        (map.getObject(collision_x, collision_y) as Characters.Enemy).Die(map);
                        map.setObject(collision_x, collision_y, new NonDestroyableObjects.Puste(content, new Rectangle((collision_x) * rectangle.Width, collision_y * rectangle.Height, rectangle.Width, rectangle.Height), collision_x, collision_y));
                    }

                    //explosion
                    SoundEffect explosion_sound = content.Load<SoundEffect>("Audio\\explosion_sound");
                    if (!map.player.AudioSettings.IsMuted)
                    {
                        SoundEffect.MasterVolume = (float)map.player.AudioSettings.SoundVolume;
                        explosion_sound.Play();
                    }
                    int vandal_x = map.GetVandal().x;
                    int vandal_y = map.GetVandal().y;

                    if (x - 1 != vandal_x && y != vandal_y && map.getObject(x - 1, y) is Zniszczalny) (map.getObject(x - 1, y) as Zniszczalny).OnDestroy(map);

                    if (map.getObject(x - 1, y).GetType().IsSubclassOf(typeof(Characters.Enemy)))
                    {
                        (map.getObject(x - 1, y) as Characters.Enemy).Die(map);
                    }

                    if (x - 1 != vandal_x && y + 1 != vandal_y && map.getObject(x - 1, y + 1) is Zniszczalny)
                        (map.getObject(x - 1, y + 1) as Zniszczalny).OnDestroy(map);
                    if (map.getObject(x - 1, y + 1).GetType().IsSubclassOf(typeof(Characters.Enemy)))
                    {
                        (map.getObject(x - 1, y + 1) as Characters.Enemy).Die(map);
                    }

                    if (x - 1 != vandal_x && y - 1 != vandal_y && map.getObject(x - 1, y - 1) is Zniszczalny)
                        (map.getObject(x - 1, y - 1) as Zniszczalny).OnDestroy(map);
                    if (map.getObject(x - 1, y - 1).GetType().IsSubclassOf(typeof(Characters.Enemy)))
                    {
                        (map.getObject(x - 1, y - 1) as Characters.Enemy).Die(map);
                    }

                    if (x != vandal_x && y - 1 != vandal_y && map.getObject(x, y - 1) is Zniszczalny)
                        (map.getObject(x, y - 1) as Zniszczalny).OnDestroy(map);
                    if (map.getObject(x, y - 1).GetType().IsSubclassOf(typeof(Characters.Enemy)))
                    {
                        (map.getObject(x, y - 1) as Characters.Enemy).Die(map);
                    }

                    if (x != vandal_x && y + 1 != vandal_y && map.getObject(x, y + 1) is Zniszczalny)
                        (map.getObject(x, y + 1) as Zniszczalny).OnDestroy(map);
                    if (map.getObject(x, y + 1).GetType().IsSubclassOf(typeof(Characters.Enemy)))
                    {
                        (map.getObject(x, y + 1) as Characters.Enemy).Die(map);
                    }

                    if (x != vandal_x && y != vandal_y && map.getObject(x, y) is Zniszczalny)
                        (map.getObject(x, y) as Zniszczalny).OnDestroy(map);
                    if (map.getObject(x, y).GetType().IsSubclassOf(typeof(Characters.Enemy)))
                    {
                        (map.getObject(x, y) as Characters.Enemy).Die(map);
                    }

                    if (x + 1 != vandal_x && y != vandal_y && map.getObject(x + 1, y) is Zniszczalny)
                        (map.getObject(x + 1, y) as Zniszczalny).OnDestroy(map);
                    if (map.getObject(x + 1, y).GetType().IsSubclassOf(typeof(Characters.Enemy)))
                    {
                        (map.getObject(x + 1, y) as Characters.Enemy).Die(map);
                    }

                    if (x + 1 != vandal_x && y + 1 != vandal_y && map.getObject(x + 1, y + 1) is Zniszczalny)
                        (map.getObject(x + 1, y + 1) as Zniszczalny).OnDestroy(map);
                    if (map.getObject(x + 1, y + 1).GetType().IsSubclassOf(typeof(Characters.Enemy)))
                    {
                        (map.getObject(x + 1, y + 1) as Characters.Enemy).Die(map);
                    }

                    if (x + 1 != vandal_x && y - 1 != vandal_y && map.getObject(x + 1, y - 1) is Zniszczalny)
                        (map.getObject(x + 1, y - 1) as Zniszczalny).OnDestroy(map);
                    if (map.getObject(x + 1, y - 1).GetType().IsSubclassOf(typeof(Characters.Enemy)))
                    {
                        (map.getObject(x + 1, y - 1) as Characters.Enemy).Die(map);
                    }


                    map.setObject(x, y, new NonDestroyableObjects.Puste(content, this.rectangle, x, y));
                }
                else
                {
                    map.setObject(collision_x, collision_y, this);
                    map.setObject(x, y, new NonDestroyableObjects.Puste(content, this.rectangle, x, y));
                    this.x = collision_x;
                    this.y = collision_y;

                }
            }
        }


    }
}
