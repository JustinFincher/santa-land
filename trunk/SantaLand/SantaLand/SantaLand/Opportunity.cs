using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace SantaLand
{
    class Opportunity : Vehicle
    {
        public Opportunity(SantaLand game, Model model, Planet planet, float scale) 
            : base (game, model, planet, scale)
        {
            speed = 0.15f;
            turnSpeed = 0.5f;
        }
    }
}
