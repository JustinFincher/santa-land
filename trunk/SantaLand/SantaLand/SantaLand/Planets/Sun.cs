using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SantaLand.Planets
{
    class Sun : Planet
    {
        public Sun(GraphicsDevice graphicsDevice, Vector3 lightDirection) 
            : base(graphicsDevice, lightDirection)
        {
        }
    }
}
