using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SantaLand
{
    class Planet : GameObject
    {
        private int planeWidth = 100;
        private int planeHeight = 50;

        private Texture2D heightMap;

        private float[,] heightData;

        public Planet(GraphicsDevice graphicsDevice, Texture2D heightMap) 
        {
            this.graphicsDevice = graphicsDevice;
            this.heightMap = heightMap;

            InitializeVertices();
            InitializeIndices();

            vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionNormalTexture), vertices.Length, BufferUsage.WriteOnly);
            indexBuffer = new IndexBuffer(graphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);

            vertexBuffer.SetData<VertexPositionColor>(vertices);
            indexBuffer.SetData<int>(indices);
        }

        public void LoadHeightData()
        {
            planeWidth = heightMap.Width;
            planeHeight = heightMap.Height;

            Color[] heightMapColors = new Color[planeWidth * planeHeight];
            heightMap.GetData(heightMapColors);

            heightData = new float[planeWidth, planeHeight];
            for (int x = 0; x < planeWidth; x++)
                for (int y = 0; y < planeHeight; y++)
                    heightData[x, y] = (float) PlanetHelper.RGB2HSL(heightMapColors[x + y * planeWidth]).hue;
        }

        private void InitializeVertices()
        {
            vertices = new VertexPositionColor[planeWidth * planeHeight];
            for (int x = 0; x < planeWidth; x++)
            {
                for (int y = 0; y < planeHeight; y++)
                {
                    vertices[x + y * planeWidth].Position = new Vector3(x, heightData[x, y], -y);
                    vertices[x + y * planeWidth].Color = Color.White;
                }
            }
        }

        private void InitializeIndices()
        {
            indices = new int[(planeWidth - 1) * (planeHeight - 1) * 6];
            int counter = 0;
            for (int y = 0; y < planeHeight - 1; y++)
            {
                for (int x = 0; x < planeWidth - 1; x++)
                {
                    int lowerLeft = x + y * planeWidth;
                    int lowerRight = (x + 1) + y * planeWidth;
                    int topLeft = x + (y + 1) * planeWidth;
                    int topRight = (x + 1) + (y + 1) * planeWidth;

                    indices[counter++] = topLeft;
                    indices[counter++] = lowerRight;
                    indices[counter++] = lowerLeft;

                    indices[counter++] = topLeft;
                    indices[counter++] = topRight;
                    indices[counter++] = lowerRight;
                }
            }
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
