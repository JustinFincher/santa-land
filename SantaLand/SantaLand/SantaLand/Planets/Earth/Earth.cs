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
        public Earth(GraphicsDevice graphicsDevice, Sphere orbiting) 
            : base(graphicsDevice, orbiting)
        {
            planeWidth = 1024;
            planeHeight = 512;

            distanceToPrimary = new Vector3(orbiting.position.X + Constants.EARTH_DISTANCE_FROM_SUN + orbiting.Radius, orbiting.position.Y, orbiting.position.Z);
            scale = Vector3.One * Constants.PLANET_SIZE_RATIO;

            solarSpeed = Constants.EARTH_SOLAR_SPEED;
            rotationSpeed = solarSpeed * Constants.EARTH_NUMBER_OF_EARTH_DAYS_TO_ORBIT;

            Water water = new Water(graphicsDevice);
            water.MinWaterLevel = 0.9818f;
            water.MaxWaterLevel = 1.0001f;

            children.Add(water);
            children.Add(new Clouds(graphicsDevice));
        }

        public override void LoadContent(ContentManager Content)
        {
            base.heightMap = Content.Load<Texture2D>("Textures/Planets/Earth/earthHeightMap");
            base.texture = Content.Load<Texture2D>("Textures/Planets/Earth/earthTextureHires");

            base.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameObject child in children)
                child.UpdateLightDirection(position);
            
            base.Update(gameTime);
        }
    }
}
