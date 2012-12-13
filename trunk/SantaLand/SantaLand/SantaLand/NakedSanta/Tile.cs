using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NakedSanta
{
    class Tile : GameObject
    {
        private const int FR = 0;
        private const int FL = 1;
        private const int BR = 2;
        private const int BL = 3;

        private int size = 1;

        Color color;

        public Tile(GraphicsDevice graphicsDevice, Color color, Vector3 position)
        {
            this.color = color;
            base.position = position;
            base.graphicsDevice = graphicsDevice;

            InitializeVertices();
            InitializeIndices();

            vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColorNormal), vertices.Length, BufferUsage.WriteOnly);
            indexBuffer = new IndexBuffer(graphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);

            vertexBuffer.SetData<VertexPositionColorNormal>(vertices);
            indexBuffer.SetData<short>(indices);
        }

        private void InitializeVertices()
        {
            vertices = new VertexPositionColorNormal[4];

            //front right
            vertices[FR] = new VertexPositionColorNormal(new Vector3(position.X + size, position.Y, position.Z), color);
            vertices[FR].Normal = Vector3.Up;
            //front left
            vertices[FL] = new VertexPositionColorNormal(new Vector3(position.X, position.Y, position.Z), color);
            vertices[FL].Normal = Vector3.Up;
            //back right
            vertices[BR] = new VertexPositionColorNormal(new Vector3(position.X + size, position.Y, position.Z - size), color);
            vertices[BR].Normal = Vector3.Up;
            //back left
            vertices[BL] = new VertexPositionColorNormal(new Vector3(position.X, position.Y, position.Z - size), color);
            vertices[BL].Normal = Vector3.Up;
        }

        private void InitializeIndices()
        {
            indices = new short[6];

            indices[0] = FL; // front left;
            indices[1] = BL; // back left;
            indices[2] = BR; // back right;

            indices[3] = BR; // front left;
            indices[4] = FR; // back left;
            indices[5] = FL; // back right;
        }
    }
}
