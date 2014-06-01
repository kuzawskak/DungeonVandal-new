using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game.Features
{
    /// <summary>
    /// Taki obiekt zabija bohatera jesli ten sie do niego zblizy na odleglosc jednaj kratki (dookoła)
    /// </summary>
    public interface Skazony
    {
        /// <summary>
        /// Zabijanie "na odleglosc"
        /// </summary>
        /// <param name="map"></param>
        void Zabij(Map.Map map);
    }
}
