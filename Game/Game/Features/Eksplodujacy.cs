using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Features
{
    /// <summary>
    /// Po zniszceniu obiekt generuje eksplozje
    /// </summary>
    public interface Eksplodujacy
    {
        /// <summary>
        /// Niszczenie eksplozja
        /// </summary>
        void Eksploduj();
    }
}
