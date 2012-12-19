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
        public Mars(GraphicsDevice graphicsDevice, Sphere orbiting) 
            : base(graphicsDevice, orbiting)
        {
            distanceToPrimary = new Vector3(orbiting.position.X + 20700 + orbiting.Radius, orbiting.position.Y, orbiting.position.Z);
            scale = Vector3.One * 0.0000151f;
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
