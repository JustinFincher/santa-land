using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SantaLand
{
    class Planet : Sphere
    {
        protected Sphere orbiting;
        protected Texture2D heightMap;
        protected float rotationSpeed = 0;
        protected float solarSpeed = 0;
        protected Vector3 distanceToPrimary = Vector3.Zero;
        public Quaternion solarRotation = Quaternion.Identity;

        public Planet(GraphicsDevice graphicsDevice, Sphere orbiting) 
        {
            base.graphicsDevice = graphicsDevice;
            this.orbiting = orbiting;
        }

        public override void Initialize()
        {
            if(heightMap != null) LoadHeightData();
            InitializeVertices();
            InitializeIndices();
            SetNormals();

            vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionNormalTexture), vertices.Length, BufferUsage.WriteOnly);
            indexBuffer = new IndexBuffer(graphicsDevice, typeof(int), indices.Length, BufferUsage.WriteOnly);

            vertexBuffer.SetData<VertexPositionNormalTexture>(vertices);
            indexBuffer.SetData<int>(indices);
            
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            rotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, rotationSpeed);
            solarRotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, solarSpeed);
            CalculatePosition();

            base.Update(gameTime);
        }

        private void CalculatePosition()
        {
            Matrix positionMatrix = Matrix.Identity;
            positionMatrix =
                Matrix.CreateTranslation(distanceToPrimary) *
                Matrix.CreateFromQuaternion(solarRotation) * 
                Matrix.CreateTranslation(orbiting.position);

            position = positionMatrix.Translation;
        }

        private void LoadHeightData()
        {
            Color[] heightMapColors = new Color[heightMap.Width * heightMap.Height];
            heightMap.GetData(heightMapColors);

            int numberOfWidthVertices = heightMap.Width / planeWidth;
            int numberOfHeightVertices = heightMap.Height / planeHeight;

            heightData = new float[planeWidth, planeHeight];
            for (int x = 0; x < planeWidth; x++)
                for (int y = 0; y < planeHeight; y++)
                    heightData[x, planeHeight-y-1] = 360 - (float)PlanetHelper.RGB2HSL(heightMapColors[(x * numberOfWidthVertices) + ((y * numberOfHeightVertices) * (numberOfHeightVertices * planeWidth))]).hue / 32;
        }
    }

    static class PlanetHelper
    {
        public struct HSL
        {
            public double hue, s, l;
        }

        public static HSL RGB2HSL(Color c1)
        {
            double themin, themax, delta;
            HSL c2;
            themin = Math.Min(c1.R, Math.Min(c1.G, c1.B));
            themax = Math.Max(c1.R, Math.Max(c1.G, c1.B));
            delta = themax - themin;
            c2.l = (themin + themax) / 2;
            c2.s = 0;
            if (c2.l > 0 && c2.l < 1)
                c2.s = delta / (c2.l < 0.5 ? (2 * c2.l) : (2 - 2 * c2.l));
            c2.hue = 0;
            if (delta > 0)
            {
                if (themax == c1.R && themax != c1.G)
                    c2.hue += (c1.G - c1.B) / delta;
                if (themax == c1.G && themax != c1.B)
                    c2.hue += (2 + (c1.B - c1.R) / delta);
                if (themax == c1.B && themax != c1.R)
                    c2.hue += (4 + (c1.R - c1.G) / delta);
                c2.hue *= 60;
            }
            return (c2);
        }
    }
}
