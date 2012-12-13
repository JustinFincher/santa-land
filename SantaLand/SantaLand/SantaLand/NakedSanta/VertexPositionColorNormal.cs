using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NakedSanta
{
    public struct VertexPositionColorNormal : IVertexType
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Color Color;

        public VertexPositionColorNormal(Vector3 position, Color color, Vector3 normal)
        {
            this.Position = position;
            this.Color = color;
            this.Normal = normal;
        }

        public VertexPositionColorNormal(Vector3 position, Vector3 normal)
        {
            this.Position = position;
            this.Color = Color.Green;
            this.Normal = normal;
        }

        public VertexPositionColorNormal(Vector3 position, Color color)
        {
            this.Position = position;
            this.Color = color;
            this.Normal = Vector3.Zero;
        }

        public static readonly VertexDeclaration _decl = new VertexDeclaration(
            new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
            new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
            new VertexElement(24, VertexElementFormat.Color, VertexElementUsage.Color, 0)
            );

        public VertexDeclaration VertexDeclaration
        {
            get { return _decl; }
        }
    }
}
