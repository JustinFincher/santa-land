﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SantaLand.Planets
{
    class Venus : Planet
    {
        public Venus(GraphicsDevice graphicsDevice, Sphere orbiting) 
            : base(graphicsDevice, orbiting)
        {
            position = new Vector3(10800 + orbiting.Radius, 0, 0);
            scale = Vector3.One * 0.0000866f;
            solarSpeed *= 0.1f;

            //children.Add(new Water(graphicsDevice, lightDirection));
        }

        public override void LoadContent(ContentManager Content)
        {
            base.heightMap = Content.Load<Texture2D>("Textures/Planets/Mars/marsHeightmap");
            base.texture = Content.Load<Texture2D>("Textures/Planets/Venus/venusTexture");

            base.LoadContent(Content);
        }
    }
}
