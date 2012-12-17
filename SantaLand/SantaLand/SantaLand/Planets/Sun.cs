using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SantaLand.Planets
{
    class Sun : Sphere
    {
        public Sun(GraphicsDevice graphicsDevice, Vector3 lightDirection) 
            : base(graphicsDevice, lightDirection)
        {
            position = new Vector3(0, 0, 0);
        }

        public override void LoadContent(ContentManager Content)
        {
            base.texture = Content.Load<Texture2D>("Textures/Planets/Sun/sunTexture");

            base.LoadContent(Content);
        }
    }
}
