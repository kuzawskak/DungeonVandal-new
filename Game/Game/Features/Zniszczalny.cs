using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Features
{
    /// <summary>
    /// Obiekty które mozna zniszczyc za pomoca eksplozji
    /// </summary>
    public interface Zniszczalny
    {
        /// <summary>
        /// Reakacja na zniszczenie obiektu
        /// </summary>
        /// <param name="map">mapa obiektów</param>
        void OnDestroy(Map.Map map);
    }


}
