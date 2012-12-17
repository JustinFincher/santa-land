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
        protected Texture2D heightMap;
        protected float rotationSpeed = 0.001f;
        protected float solarSpeed = 0.01f;
        protected Quaternion solarRotation = Quaternion.Identity;

        private float[,] heightData;

        public Planet(GraphicsDevice graphicsDevice, Vector3 lightDirection) 
        {
            base.graphicsDevice = graphicsDevice;
            base.lightDirection = lightDirection;
        }

        public override void Initialize()
        {
            LoadHeightData();
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

            base.Update(gameTime);
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

        protected override void InitializeVertices()
        {
            float yMax = planeHeight - 1;
            float xMax = planeWidth;

            vertices = new VertexPositionNormalTexture[planeWidth * planeHeight];
            for (int y = 0; y < planeHeight; y++)
            {
                for (int x = 0; x < planeWidth-1; x++)
                {
                    float radius = heightData[x, y] / -MathHelper.PiOver2;

                    float ringradius = radius * (float)Math.Sin(y * Math.PI / yMax);
                    Vector3 xyz = new Vector3((float)Math.Cos((xMax - x) * Math.PI * 2.0f / xMax) * ringradius, (float)Math.Cos(y * Math.PI / yMax) * radius, (float)Math.Sin((xMax - x) * Math.PI * 2.0f / xMax) * ringradius);

                    vertices[x + y * planeWidth] = new VertexPositionNormalTexture(xyz, Vector3.Forward, new Vector2(((float)x / (planeWidth + 1)), (1f - (float)y / planeHeight)));
                }
                //Sew the edges together
                vertices[planeWidth - 1 + y * planeWidth] = new VertexPositionNormalTexture(
                    vertices[0 + y * planeWidth].Position,
                    vertices[0 + y * planeWidth].Normal,
                    new Vector2(((float)planeWidth / (planeWidth)), (float)y / planeHeight));
            }
        }

        public override void Draw(BasicEffect effect, Matrix parentWorld)
        {
            objectWorld = Matrix.Identity;
            objectWorld = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(rotation) * Matrix.CreateTranslation(position) * Matrix.CreateFromQuaternion(solarRotation);
            effect.World = objectWorld * parentWorld;
            effect.Texture = texture;

            effect.LightingEnabled = true;
            effect.DirectionalLight0.DiffuseColor = new Vector3(0.01f, 0.01f, 0.01f);
            effect.DirectionalLight0.Direction = new Vector3(1, 0, 0);
            effect.AmbientLightColor = new Vector3(0.05f, 0.05f, 0.05f);
            effect.EmissiveColor = new Vector3(5f, 5f, 5f);

            graphicsDevice.SetVertexBuffer(vertexBuffer);
            graphicsDevice.Indices = indexBuffer;

            if (vertices != null && indices != null)
            {
                effect.CurrentTechnique.Passes[0].Apply();
                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertices.Length, 0, indices.Length / 3);
            }

            foreach (GameObject child in children)
                child.Draw(effect, objectWorld);
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
