using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SantaLand.Planets
{
    class Water : Sphere
    {
        public float WaterSpeed { get; set; }
        public float MaxWaterLevel { get; set; }
        public float MinWaterLevel { get; set; }
        public float WaterLevel { get; set; }

        public Water(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {
            WaterSpeed = 0.00001f;
            MaxWaterLevel = 1.0005f;
            MinWaterLevel = 0.97f;
            WaterLevel = 0.99f;
        }

        public override void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Textures/Planets/Earth/water");
            
            base.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            if (WaterLevel > MaxWaterLevel || WaterLevel < MinWaterLevel) 
                WaterSpeed = -WaterSpeed;

            WaterLevel += WaterSpeed;

            scale.Normalize();
            scale = Vector3.One * WaterLevel;
            
            base.Update(gameTime);
        }
    }
}
