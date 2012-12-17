using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SantaLand.Planets
{
    class Earth : Planet
    {
        public Earth(GraphicsDevice graphicsDevice, Vector3 lightDirection) 
            : base(graphicsDevice, lightDirection)
        {
            position = new Vector3(700, 0, 0);
            scale = Vector3.One * 0.1f;

            Water water = new Water(graphicsDevice, lightDirection);
            water.MinWaterLevel = 0.9818f;
            water.MaxWaterLevel = 1.0001f;

            children.Add(water);
            children.Add(new Moon(graphicsDevice, lightDirection));
        }

        public override void LoadContent(ContentManager Content)
        {
            base.heightMap = Content.Load<Texture2D>("Textures/Planets/Earth/earthHeightMap");
            base.texture = Content.Load<Texture2D>("Textures/Planets/Earth/earthTextureHires");

            base.LoadContent(Content);
        }
    }
}
