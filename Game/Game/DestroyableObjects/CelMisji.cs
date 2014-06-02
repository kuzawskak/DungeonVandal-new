using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.Features;

namespace Game.DestroyableObjects
{
    /// <summary>
    /// Klasa celu misji - Vandal po wkroczeniu na cel misji przechodzi do nastepnego etapu
    /// ( wprzypadku ostatniego etapu gry - wygrywa)
    /// </summary>
    class CelMisji:Map.MapObject,Zniszczalny
    {
        /// <summary>
        /// Liczba punktów naliczana za wkroczenie na cel misji
        /// </summary>
        int points;
        /// <summary>
        /// Ścieżka do tekstury
        /// </summary>
        private string asset_name;

        private bool deactivated = false;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="content">XNA content</param>
        /// <param name="rectangle">prostokąt reprezentujący obiekt na mapie</param>
        /// <param name="x">indeks x na tablicy obiektow</param>
        /// <param name="y">indeks y na tablicy obiektow</param>
        /// <param name="game_level">poziom gry</param>
        public CelMisji(ContentManager content, Rectangle rectangle, int x, int y,int game_level):base(content,rectangle,x,y)
        {
            deactivated = false;
            TypeTag = Map.ElementType.CELMISJI;
            this.IsAccesible = false;// true;
            switch (game_level)
            {
                case 1:
                    asset_name = "Textures\\level1_goal";
                    break;
                case 2:
                    asset_name = "Textures\\level2_goal";
                    break;
                case 3:
                    asset_name = "Textures\\level3_goal";
                    break;
                case 4:
                    asset_name = "Textures\\level4_goal";
                    break;
                case 5:
                    asset_name = "Textures\\level5_goal";
                    break;

            }
            texture = content.Load<Texture2D>(asset_name);      
            points = 200;
        
        }

        void Draw(GameTime gameTime) { }

        /// <summary>
        /// Reakcja na wejście Vandala
        /// </summary>
        /// <param name="map"></param>
        public void OnDestroy(Map.Map map)
        {
            map.AddPlayersPoints(points);
            if (!deactivated)
            {
                deactivated = true;
                asset_name = "Textures\\deactivated_goal";
                texture = content.Load<Texture2D>(asset_name);
            }
            else
                map.vandal.level_up = true;
        }
    }
}
