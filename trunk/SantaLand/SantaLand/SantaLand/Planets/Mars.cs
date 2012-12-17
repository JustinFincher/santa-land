using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SantaLand
{
    class Mars : Planet
    {
        public Mars(GraphicsDevice graphicsDevice, Vector3 lightDirection) 
            : base(graphicsDevice, lightDirection)
        {
            position = new Vector3(1000, 0, 0);
            scale = Vector3.One * 0.053f;
            solarSpeed *= 1.1f;

            //children.Add(new Water(graphicsDevice, lightDirection));
        }

        public override void LoadContent(ContentManager Content)
        {
            base.heightMap = Content.Load<Texture2D>("Textures/Planets/Mars/marsHeightmap");
            base.texture = Content.Load<Texture2D>("Textures/Planets/Mars/marsTextureHires");

            base.LoadContent(Content);
        }
    }
}
