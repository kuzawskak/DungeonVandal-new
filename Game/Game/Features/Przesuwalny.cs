using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace Game.Features
{
    /// <summary>
    /// Bohater moze przesunac taki obiekt
    /// Jezeli obiekt reaguje na grawitacje, mozna przesuwac go tylko w kierunku prostopadlym do wektora grawitacji
    /// </summary>
    public interface Przesuwalny
    {
        /// <summary>
        /// Ruch po przesunieciu
        /// </summary>
        /// <param name="map">mapa obiektow</param>
        /// <param name="x_vel">wektor x przesuniecia</param>
        /// <returns></returns>
        bool Przesun(Map.Map map,int x_vel);
    }
}
