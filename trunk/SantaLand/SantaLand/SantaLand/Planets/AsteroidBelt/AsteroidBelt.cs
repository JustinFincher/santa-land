using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SantaLand.Planets
{
    class AsteroidBelt : GameObject
    {
        public AsteroidBelt(GraphicsDevice graphicsDevice, Sphere orbiting) 
        {
            base.graphicsDevice = graphicsDevice;

            position = orbiting.position;
            Asteroid child;

            for (int i = 0; i < 500; i++)
            {
                child = new Asteroid(graphicsDevice, orbiting, new Vector3(orbiting.Radius + Constants.ASTEROID_BELT_DISTANCE_FROM_SUN, 0, 0));
                child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, MathHelper.TwoPi * i / 500);
                children.Add(child);
            }

            for (int i = 0; i < 300; i++)
            {
                child = new Asteroid(graphicsDevice, orbiting, new Vector3(orbiting.Radius + Constants.ASTEROID_BELT_DISTANCE_FROM_SUN + 1500, 0, 0));
                child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, MathHelper.TwoPi * i / 300);
                children.Add(child);
            }

            for (int i = 0; i < 450; i++)
            {
                child = new Asteroid(graphicsDevice, orbiting, new Vector3(orbiting.Radius + Constants.ASTEROID_BELT_DISTANCE_FROM_SUN + 2500, 0, 0));
                child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, MathHelper.TwoPi * i / 450);
                children.Add(child);
            }
        }
    }
}
