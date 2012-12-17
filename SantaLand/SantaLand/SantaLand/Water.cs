using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SantaLand
{
    class Water : Sphere
    {
        private float waterSpeed = 0.0001f;
        private float maxWaterLevel = 1.0005f;
        private float minWaterLevel = 0.97f;
        private float waterLevel = 0.99f;

        public Water(GraphicsDevice graphicsDevice, Vector3 lightDirection)
            : base(graphicsDevice, lightDirection)
        {
        }

        public override void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Textures/water");
            
            base.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            if (waterLevel > maxWaterLevel || waterLevel < minWaterLevel) 
                waterSpeed = -waterSpeed;

            waterLevel += waterSpeed;

            scale = Vector3.One * waterLevel;
            
            base.Update(gameTime);
        }
    }
}
