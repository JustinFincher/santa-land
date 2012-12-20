using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SantaLand.Planets
{
    class Asteroid : Planet
    {
        public Asteroid(GraphicsDevice graphicsDevice, Sphere orbiting, Vector3 distanceToPrimary)
            : base(graphicsDevice, orbiting)
        {
            planeWidth = 16;
            planeHeight = 8;

            this.distanceToPrimary = distanceToPrimary;

            scale = Vector3.One * 1f;
        }

        public override void LoadContent(ContentManager Content)
        {
            base.texture = Content.Load<Texture2D>("Textures/Planets/Asteroid/asteroidTextureHires");

            heightData = new float[planeWidth, planeHeight];

            for (int i = 0; i < planeWidth; i++)
            {
                for (int j = 0; j < planeHeight; j++)
                {
                    heightData[i, j] = (float) Random.GetInstance().NextDouble() * 360;
                }
            }

            base.LoadContent(Content);
        }
    }

    class Random
    {
        static private Random singleton;
        
        private System.Random random;

        private Random()
        {
            random = new System.Random();
        }

        public static Random GetInstance()
        {
            if (singleton == null) singleton = new Random();
            return singleton;
        }

        public double NextDouble() 
        {
            return random.NextDouble();
        }
    }
}
