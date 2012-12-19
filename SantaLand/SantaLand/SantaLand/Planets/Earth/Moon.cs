using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SantaLand.Planets
{
    class Moon : Planet
    {
        public Moon(GraphicsDevice graphicsDevice, Sphere orbiting) 
            : base(graphicsDevice, orbiting)
        {
            distanceToPrimary = new Vector3(orbiting.Radius + Constants.MOON_DISTANCE_FROM_EARTH, 0, 0);
            scale = Vector3.One * Constants.MOON_RELATIVE_SIZE;
            solarSpeed = Constants.EARTH_SOLAR_SPEED * (365.25f / Constants.MOON_NUMBER_OF_EARTH_DAYS_TO_ORBIT);
            rotationSpeed = solarSpeed * Constants.MOON_NUMBER_OF_SPINS_PER_ORBIT;
        }

        public override void LoadContent(ContentManager Content)
        {
            base.heightMap = Content.Load<Texture2D>("Textures/Planets/Mars/marsHeightmap");
            base.texture = Content.Load<Texture2D>("Textures/Planets/Moon/moonTexture");

            base.LoadContent(Content);
        }
    }
}
