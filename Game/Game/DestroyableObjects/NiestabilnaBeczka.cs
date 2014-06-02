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
    /// Obiekt zniszczalny za pomoca eksplozji
    /// eksploduje przy zniszczeniu , reaguje na grawitacje i mozna przesuwac
    /// </summary>
    class NiestabilnaBeczka : Map.MapObject, ReagujeNaGrawitacje,  Zniszczalny, Eksplodujacy,Skazony
    {

        private bool explode = false;
        private bool die = false;
        /// <summary>
        /// Scieżka do tektsury z animacja wybuchu 
        /// </summary>
        const string explosion_animation = "Textures\\explosion";
        /// <summary>
        /// Predkosc spadania
        /// </summary>
        int velocity = 1;

        /// <summary>
        /// Tekstura dla Content XNA
        /// </summary>
        private string asset_name = "Textures\\niestabilna_beczka";
        private int current_frame = 0;
        /// <summary>
        /// Punkty naliczane za zniszczenie kamienia
        /// </summary>
        private int points;
        /// <summary>
        /// Aktualny stan (czy spada czy nie)
        /// </summary>
        private bool is_falling;

        /// <summary>
        /// Konstruktor domyślny
        /// </summary>
        /// <param name="content">XNA content</param>
        /// <param name="rectangle">Prostokąt określający na mapie</param>
        /// <param name="x">Współrzedna x na tablicy obiektów</param>
        /// <param name="y">Współrzędna y na tablicy obiektów</param>
        public NiestabilnaBeczka(ContentManager content, Rectangle rectangle, int x, int y)
            : base(content, rectangle, x, y)
        {
            this.points = 30;
            TypeTag = Map.ElementType.NIESTABILNABECZKA;
            texture = content.Load<Texture2D>(asset_name);
            this.is_falling = false;
            IsAccesible = false;
        }

        /// <summary>
        /// Aktualizacja stanu na mapie z uwzględnieniem spadania
        /// </summary>
        /// <param name="gametime"></param>
        /// <param name="map"></param>
        public override void Update(GameTime gametime, Map.Map map)
        {
            if (explode)
            {
                if (current_frame == 16) { die = true; explode = false; }
                else
                    src_rectangle = new Rectangle((current_frame++) * 64, 0, 64, 64);
            }
            else if (die)
            {
                map.setObject(x, y, new NonDestroyableObjects.Puste(content, this.Rectangle, x, y));
            }
            else
            {
                int x_index = rectangle.X / rectangle.Width;
                int y_index = rectangle.Y / rectangle.Height;


                //sprawdzenie wszytkich dookola pol
                if (map.is_vandal_on_rectangle(x_index - 1, y_index) ||
                    map.is_vandal_on_rectangle(x_index + 1, y_index) ||
                    map.is_vandal_on_rectangle(x_index, y_index - 1) ||
                    map.is_vandal_on_rectangle(x_index, y_index + 1) ||
                    map.is_vandal_on_rectangle(x_index - 1, y_index - 1) ||
                    map.is_vandal_on_rectangle(x_index - 1, y_index + 1) ||
                    map.is_vandal_on_rectangle(x_index + 1, y_index + 1) ||
                    map.is_vandal_on_rectangle(x_index + 1, y_index - 1))
                {
                    Zabij(map);
                    return;
                }




                if (y_index == map.getHeight() - 1)
                {
                    if (is_falling)
                    {
                        map.setObject(x_index, y_index, this);
                        this.x = x_index;
                        this.y = y_index;
                        map.setObject(x_index, y_index - 1, new NonDestroyableObjects.Puste(content, rectangle, x_index, y_index - 1));
                        is_falling = false;
                        return;
                    }
                    else return;
                }
                if (is_falling)
                {
                    //nie spadl calkowice jeszcze na odpowiedni prosokat
                    if (rectangle.Y % rectangle.Height != 0)
                    {
                        Spadaj();
                    }
                    else
                    {
                        map.setObject(x_index, y_index, this);
                        this.x = x_index;
                        this.y = y_index;
                        map.setObject(x_index, y_index - 1, new NonDestroyableObjects.Puste(content, rectangle, x_index, y_index - 1));
                        if (y_index < map.getHeight() - 1 && map.getObject(x_index, y_index + 1).GetType() == typeof(NonDestroyableObjects.Puste))
                        {
                            Spadaj();
                        }
                        else is_falling = false;
                    }
                }
                else if (map.getObject(x_index, y_index + 1).GetType() == typeof(NonDestroyableObjects.Puste) && !map.is_vandal_on_rectangle(x_index, y_index + 1))
                {//rozpoczecie spadania
                    is_falling = true;
                    Spadaj();
                }
                else return;
            }
        }

        /// <summary>
        /// Efekt spadania z okreslona predkością
        /// </summary>
        public void Spadaj()
        {
            rectangle.Y += velocity;
        }


      


        public void Zgniec()
        {

        }
        /// <summary>
        /// Reakcja na zniszczenie 
        /// </summary>
        /// <param name="map"></param>
        public void OnDestroy(Map.Map map)
        {
            map.AddPlayersPoints(points);
            Eksploduj();
        }

        public void Zabij(Map.Map map)
        {
            if (!map.GetVandal().is_immortal)
                map.vandal.is_alive = false;
        }

        public void Eksploduj()
        {
            explode = true;
            texture = content.Load<Texture2D>(explosion_animation);
        }
    }
}
