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

        private Texture2D texture;

        public Planet(GraphicsDevice graphicsDevice, Texture2D texture) 
        {
            this.graphicsDevice = graphicsDevice;
            this.texture = texture;

            InitializeVertices();
            InitializeIndices();

            vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionNormalTexture), vertices.Length, BufferUsage.WriteOnly);
            indexBuffer = new IndexBuffer(graphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);

            vertexBuffer.SetData<VertexPositionNormalTexture>(vertices);
            indexBuffer.SetData<short>(indices);
        }

        public void Draw()
        {
            graphicsDevice.SetVertexBuffer(vertexBuffer);
            graphicsDevice.Indices = indexBuffer;
            graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertices.Length, 0, indices.Length / 3);
        }

        private void InitializeVertices()
        {
            int count = 0;
            for (int i = 0; i < planeHeight; i++)
            {
                for (int j = 0; j < planeWidth; j++)
                {
                    //front right
                    vertices[count] = new VertexPositionNormalTexture(new Vector3(i + 0, 0, j + 0), Vector3.Up, new Vector2(i / planeWidth, j / planeHeight));
                    //front left
                    vertices[count] = new VertexPositionNormalTexture(new Vector3(i + 0, 0, j + 0), Vector3.Up, new Vector2(i / planeWidth, j / planeHeight));
                    //back right
                    vertices[count] = new VertexPositionNormalTexture(new Vector3(i + 0, 0, j + 0), Vector3.Up, new Vector2(i / planeWidth, j / planeHeight));
                    //back left
                    vertices[count] = new VertexPositionNormalTexture(new Vector3(i + 0, 0, j + 0), Vector3.Up, new Vector2(i / planeWidth, j / planeHeight));

                    count++;
                }
            }
        }

        private void InitializeIndices()
        {
            indices = new short[planeWidth * planeHeight * 6];
            for (int i = 0, v = 0; i < indices.Length; i += 6, v += 4)
            {
                indices[i] = (short)v;           // front left;
                indices[i + 1] = (short)(v + 3); // back left;
                indices[i + 2] = (short)(v + 2); // back right;

                indices[i + 3] = (short)v;       // front left;
                indices[i + 4] = (short)(v + 2); // back left;
                indices[i + 5] = (short)(v + 1); // back right;  
            }
        }
    }



    static class PlanetHelper
    {
        struct HSL
        {
            public double h, s, l;
        }

        static HSL RGB2HSL(Color c1)
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
            c2.h = 0;
            if (delta > 0)
            {
                if (themax == c1.R && themax != c1.G)
                    c2.h += (c1.G - c1.B) / delta;
                if (themax == c1.G && themax != c1.B)
                    c2.h += (2 + (c1.B - c1.R) / delta);
                if (themax == c1.B && themax != c1.R)
                    c2.h += (4 + (c1.R - c1.G) / delta);
                c2.h *= 60;
            }
            return (c2);
        }
    }
}
