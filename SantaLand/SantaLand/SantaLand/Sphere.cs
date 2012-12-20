using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SantaLand
{
    class Sphere : GameObject
    {
        public float[,] heightData = null;

        public float Radius
        {
            get
            {
                return (360 / MathHelper.PiOver2) * scale.X;
            }
        }

        public int planeWidth = 1024;
        public int planeHeight = 512;

        protected Sphere() {}

        public Sphere(GraphicsDevice graphicsDevice)
        {
            base.graphicsDevice = graphicsDevice;

            InitializeVertices();
            InitializeIndices();
            SetNormals();

            vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionNormalTexture), vertices.Length, BufferUsage.WriteOnly);
            indexBuffer = new IndexBuffer(graphicsDevice, typeof(int), indices.Length, BufferUsage.WriteOnly);

            vertexBuffer.SetData<VertexPositionNormalTexture>(vertices);
            indexBuffer.SetData<int>(indices);
        }

        protected virtual void InitializeVertices()
        {
            float yMax = planeHeight - 1;
            float xMax = planeWidth;

            vertices = new VertexPositionNormalTexture[planeWidth * planeHeight];
            for (int y = planeHeight-1; y >= 0; y--)
            {
                int x;

                for (x = 0; x < planeWidth; x++)
                {
                    float radius = GetHeightData(x, y) / -MathHelper.PiOver2;

                    float ringradius = radius * (float)Math.Sin(y * Math.PI / yMax);
                    Vector3 xyz = new Vector3((float)Math.Cos((xMax - x) * Math.PI * 2.0f / xMax) * ringradius, (float)Math.Cos(y * Math.PI / yMax) * radius, (float)Math.Sin((xMax - x) * Math.PI * 2.0f / xMax) * ringradius);

                    vertices[x + y * planeWidth] = new VertexPositionNormalTexture(xyz, Vector3.Forward, new Vector2(((float)x / (planeWidth + 1)), (1f - ((float)y / planeHeight))));
                }
                //Sew the edges together
                vertices[planeWidth - 1 + y * planeWidth] = new VertexPositionNormalTexture(
                    vertices[0 + y * planeWidth].Position,
                    vertices[0 + y * planeWidth].Normal,
                    new Vector2(1, (1f - (float)y / planeHeight)));
            }
        }

        private float GetHeightData(int x, int y)
        {
            if (heightData != null) return heightData[x, y];
            return 360;
        }

        protected virtual void SetNormals()
        {
            for (int i = 0; i < indices.Length; i += 3)
            {
                Vector3 v1 = vertices[indices[i + 1]].Position - vertices[indices[i]].Position;
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

        protected virtual void InitializeIndices()
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
}
