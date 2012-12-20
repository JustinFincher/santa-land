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

            int numberPerRow = 500;

            for (int i = 0; i < numberPerRow; i++)
            {
                child = new Asteroid(graphicsDevice, orbiting, new Vector3(orbiting.Radius + Constants.ASTEROID_BELT_DISTANCE_FROM_SUN, 0, 0));
                child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, MathHelper.TwoPi * i / numberPerRow);
                if (Random.GetInstance().NextDouble() > 0.5)
                    child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, (MathHelper.TwoPi / 720) * (float) Random.GetInstance().NextDouble());
                else 
                    child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, (MathHelper.TwoPi / 720) * (float) -Random.GetInstance().NextDouble());
                children.Add(child);
            }

            numberPerRow = 321;

            for (int i = 4; i < numberPerRow; i++)
            {
                child = new Asteroid(graphicsDevice, orbiting, new Vector3(orbiting.Radius + Constants.ASTEROID_BELT_DISTANCE_FROM_SUN + 1500, 0, 0));
                child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, MathHelper.TwoPi * i / numberPerRow); 
                if (Random.GetInstance().NextDouble() > 0.5)
                    child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, (MathHelper.TwoPi / 720) * (float)Random.GetInstance().NextDouble());
                else
                    child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, (MathHelper.TwoPi / 720) * (float)-Random.GetInstance().NextDouble());
                children.Add(child);
            }

            numberPerRow = 423;

            for (int i = 2; i < numberPerRow; i++)
            {
                child = new Asteroid(graphicsDevice, orbiting, new Vector3(orbiting.Radius + Constants.ASTEROID_BELT_DISTANCE_FROM_SUN + 2500, 0, 0));
                child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, MathHelper.TwoPi * i / numberPerRow); 
                if (Random.GetInstance().NextDouble() > 0.5)
                    child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, (MathHelper.TwoPi / 720) * (float)Random.GetInstance().NextDouble());
                else
                    child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, (MathHelper.TwoPi / 720) * (float)-Random.GetInstance().NextDouble());
                children.Add(child);
            }

            numberPerRow = 52;

            for (int i = 1; i < numberPerRow; i++)
            {
                child = new Asteroid(graphicsDevice, orbiting, new Vector3(orbiting.Radius + Constants.ASTEROID_BELT_DISTANCE_FROM_SUN + 3000, 0, 0));
                child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, MathHelper.TwoPi * i / numberPerRow);
                if (Random.GetInstance().NextDouble() > 0.5)
                    child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, (MathHelper.TwoPi / 720) * (float)Random.GetInstance().NextDouble());
                else
                    child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, (MathHelper.TwoPi / 720) * (float)-Random.GetInstance().NextDouble());
                children.Add(child);
            }

            numberPerRow = 342;

            for (int i = 5; i < numberPerRow; i++)
            {
                child = new Asteroid(graphicsDevice, orbiting, new Vector3(orbiting.Radius + Constants.ASTEROID_BELT_DISTANCE_FROM_SUN + 3500, 0, 0));
                child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, MathHelper.TwoPi * i / numberPerRow);
                if (Random.GetInstance().NextDouble() > 0.5)
                    child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, (MathHelper.TwoPi / 720) * (float)Random.GetInstance().NextDouble());
                else
                    child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, (MathHelper.TwoPi / 720) * (float)-Random.GetInstance().NextDouble());
                children.Add(child);
            }

            for (int i = 0; i < numberPerRow; i++)
            {
                child = new Asteroid(graphicsDevice, orbiting, new Vector3(orbiting.Radius + Constants.ASTEROID_BELT_DISTANCE_FROM_SUN + 4500, 0, 0));
                child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, MathHelper.TwoPi * i / numberPerRow);
                if (Random.GetInstance().NextDouble() > 0.5)
                    child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, (MathHelper.TwoPi / 720) * (float)Random.GetInstance().NextDouble());
                else
                    child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, (MathHelper.TwoPi / 720) * (float)-Random.GetInstance().NextDouble());
                children.Add(child);
            }

            numberPerRow = 321;

            for (int i = 4; i < numberPerRow; i++)
            {
                child = new Asteroid(graphicsDevice, orbiting, new Vector3(orbiting.Radius + Constants.ASTEROID_BELT_DISTANCE_FROM_SUN + 5500, 0, 0));
                child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, MathHelper.TwoPi * i / numberPerRow);
                if (Random.GetInstance().NextDouble() > 0.5)
                    child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, (MathHelper.TwoPi / 720) * (float)Random.GetInstance().NextDouble());
                else
                    child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, (MathHelper.TwoPi / 720) * (float)-Random.GetInstance().NextDouble());
                children.Add(child);
            }

            numberPerRow = 423;

            for (int i = 2; i < numberPerRow; i++)
            {
                child = new Asteroid(graphicsDevice, orbiting, new Vector3(orbiting.Radius + Constants.ASTEROID_BELT_DISTANCE_FROM_SUN + 6500, 0, 0));
                child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, MathHelper.TwoPi * i / numberPerRow);
                if (Random.GetInstance().NextDouble() > 0.5)
                    child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, (MathHelper.TwoPi / 720) * (float)Random.GetInstance().NextDouble());
                else
                    child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, (MathHelper.TwoPi / 720) * (float)-Random.GetInstance().NextDouble());
                children.Add(child);
            }

            numberPerRow = 52;

            for (int i = 1; i < numberPerRow; i++)
            {
                child = new Asteroid(graphicsDevice, orbiting, new Vector3(orbiting.Radius + Constants.ASTEROID_BELT_DISTANCE_FROM_SUN + 7000, 0, 0));
                child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, MathHelper.TwoPi * i / numberPerRow);
                if (Random.GetInstance().NextDouble() > 0.5)
                    child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, (MathHelper.TwoPi / 720) * (float)Random.GetInstance().NextDouble());
                else
                    child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, (MathHelper.TwoPi / 720) * (float)-Random.GetInstance().NextDouble());
                children.Add(child);
            }

            numberPerRow = 342;

            for (int i = 5; i < numberPerRow; i++)
            {
                child = new Asteroid(graphicsDevice, orbiting, new Vector3(orbiting.Radius + Constants.ASTEROID_BELT_DISTANCE_FROM_SUN + 7500, 0, 0));
                child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, MathHelper.TwoPi * i / numberPerRow);
                if (Random.GetInstance().NextDouble() > 0.5)
                    child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, (MathHelper.TwoPi / 720) * (float)Random.GetInstance().NextDouble());
                else
                    child.solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, (MathHelper.TwoPi / 720) * (float)-Random.GetInstance().NextDouble());
                children.Add(child);
            }
        }
    }
}
