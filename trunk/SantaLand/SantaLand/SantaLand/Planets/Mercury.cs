using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SantaLand.Planets
{
    class Mercury : Planet
    {
        public Mercury(GraphicsDevice graphicsDevice, Sphere orbiting) 
            : base(graphicsDevice, orbiting)
        {
            planeHeight = 256;
            planeWidth = 128;

            distanceToPrimary = new Vector3(orbiting.position.X + Constants.MERCURY_DISTANCE_FROM_SUN + orbiting.Radius, orbiting.position.Y, orbiting.position.Z);
            scale = Vector3.One * Constants.MERCURY_RELATIVE_SIZE;
            solarSpeed = Constants.EARTH_SOLAR_SPEED * (365.25f / Constants.MERCURY_NUMBER_OF_EARTH_DAYS_TO_ORBIT);
            rotationSpeed = solarSpeed * Constants.MERCURY_NUMBER_OF_SPINS_PER_ORBIT;
        }

        public override void LoadContent(ContentManager Content)
        {
            base.heightMap = Content.Load<Texture2D>("Textures/Planets/Mars/marsHeightmap");
            base.texture = Content.Load<Texture2D>("Textures/Planets/Mercury/MercuryTexture");

            base.LoadContent(Content);
        }
    }
}
