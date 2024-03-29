﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SantaLand.Planets;

namespace SantaLand.Planets
{
    class Saturn : Planet
    {
        public Saturn(GraphicsDevice graphicsDevice, Sphere orbiting) 
            : base(graphicsDevice, orbiting)
        {
            distanceToPrimary = new Vector3(orbiting.position.X + Constants.SATURN_DISTANCE_FROM_SUN + orbiting.Radius, orbiting.position.Y, orbiting.position.Z);
            scale = Vector3.One * Constants.SATURN_RELATIVE_SIZE;
            solarSpeed = Constants.EARTH_SOLAR_SPEED * (365.25f / Constants.SATURN_NUMBER_OF_EARTH_DAYS_TO_ORBIT);
            rotationSpeed = solarSpeed * Constants.SATURN_NUMBER_OF_SPINS_PER_ORBIT;
        }

        public override void LoadContent(ContentManager Content)
        {
            //base.heightMap = Content.Load<Texture2D>("Textures/Planets/Jupiter/marsHeightmap");
            base.texture = Content.Load<Texture2D>("Textures/Planets/Saturn/saturnTexture");

            base.LoadContent(Content);
        }
    }
}
