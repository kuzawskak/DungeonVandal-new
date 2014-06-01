using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Features
{
    /// <summary>
    /// Jesli takie obiekt spadnie (przeleci przynajmniej 1 kratkę) na bohatera lub przeciwnika -
    /// ten umiera
    /// </summary>
    public interface Ciezki
    {
        /// <summary>
        /// Zgniatanie obiektu na ktory spada
        /// </summary>
        void Zgniec();
    }
}
