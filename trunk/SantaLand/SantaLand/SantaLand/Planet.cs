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
        private int planeWidth = 1024;
        private int planeHeight = 512;

        private Texture2D heightMap;

        private float[,] heightData;

        public Planet(GraphicsDevice graphicsDevice, Texture2D heightMap, Texture2D texture, Vector3 lightDirection) 
        {
            base.graphicsDevice = graphicsDevice;
            this.heightMap = heightMap;
            base.texture = texture;
            base.lightDirection = lightDirection;

            LoadHeightData();
            InitializeVertices();
            InitializeIndices();
            SetNormals();

            vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionNormalTexture), vertices.Length, BufferUsage.WriteOnly);
            indexBuffer = new IndexBuffer(graphicsDevice, typeof(int), indices.Length, BufferUsage.WriteOnly);

            vertexBuffer.SetData<VertexPositionNormalTexture>(vertices);
            indexBuffer.SetData<int>(indices);
        }

        public void LoadHeightData()
        {
            Color[] heightMapColors = new Color[heightMap.Width * heightMap.Height];
            heightMap.GetData(heightMapColors);

            int numberOfWidthVertices = heightMap.Width / planeWidth;
            int numberOfHeightVertices = heightMap.Height / planeHeight;

            heightData = new float[planeWidth, planeHeight];
            for (int x = 0; x < planeWidth; x++)
                for (int y = 0; y < planeHeight; y++)
                    heightData[x, y] = 360 - (float)PlanetHelper.RGB2HSL(heightMapColors[(x * numberOfWidthVertices) + ((y * numberOfHeightVertices) * (numberOfHeightVertices * planeWidth))]).hue / 32;
        }

        private void InitializeVertices()
        {
            float yMax = planeHeight - 1;
            float xMax = planeWidth;

            vertices = new VertexPositionNormalTexture[planeWidth * planeHeight];
            for (int y = 0; y < planeHeight; y++)
            {
                for (int x = 0; x < planeWidth - 1; x++)
                {
                    float radius = heightData[x, y] / -MathHelper.PiOver2;

                    float ringradius = radius * (float) Math.Sin(y * Math.PI / yMax);
                    Vector3 xyz = new Vector3((float)Math.Cos((xMax - x) * Math.PI * 2.0f / xMax) * ringradius, (float)Math.Cos(y * Math.PI / yMax) * radius, (float) Math.Sin((xMax - x) * Math.PI * 2.0f / xMax) * ringradius);

                    vertices[x + y * planeWidth] = new VertexPositionNormalTexture(xyz, Vector3.Forward, new Vector2(((float)x / (planeWidth+1)), (float) y / planeHeight));
                }
                //Sew the edges together
                vertices[planeWidth - 1 + y * planeWidth] = new VertexPositionNormalTexture(
                    vertices[0 + y * planeWidth].Position,
                    vertices[0 + y * planeWidth].Normal,
                    new Vector2(((float)planeWidth / (planeWidth)), (float)y / planeHeight));
            }
        }

        private void SetNormals()
        {
            for (int i = 0; i < indices.Length; i += 3) 
            {
                Vector3 v1 = vertices[indices[i+1]].Position - vertices[indices[i]].Position;
                Vector3 v2 = vertices[indices[i + 2]].Position - vertices[indices[i]].Position;
                Vector3 normal;
                Vector3.Cross(ref v2, ref v1, out normal);
                normal.Normalize();
                vertices[indices[i]].Normal += normal;
                vertices[indices[i + 1]].Normal += normal;
                vertices[indices[i + 2]].Normal += normal;
            }

            foreach (VertexPositionNormalTexture v in vertices)
                v.Normal.Normalize();
        }
        
        private void InitializeIndices()
        {
            indices = new int[(planeWidth - 1) * (planeHeight) * 6];
            int i = 0;
            for (int y = 0; y < planeHeight - 1; y++)
            {
                for (int x = 0; x < planeWidth - 1; x++, i += 6)
                {
                    indices[i] = x + y * planeWidth; // bottom left;
                    indices[i + 1] = x + (y + 1) * planeWidth; // top left;
                    indices[i + 2] = x + 1 + (y + 1) * planeWidth; // top right;

                    indices[i + 3] = x + y * planeWidth; // bottom left;
                    indices[i + 4] = x + 1 + (y + 1) * planeWidth; // top right;
                    indices[i + 5] = x + 1 + y * planeWidth; // bottom right;
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
