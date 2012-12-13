using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NakedSanta
{
    abstract class GameObject
    {
        public Vector3 position;
        public Quaternion rotation = Quaternion.Identity;
        public Vector3 scale = Vector3.One;
        public List<GameObject> children = new List<GameObject>();
        public Vector3 velocity = Vector3.Zero;

        protected VertexBuffer vertexBuffer;
        protected IndexBuffer indexBuffer;
        protected Matrix objectWorld;
        protected GraphicsDevice graphicsDevice;
        protected VertexPositionColorNormal[] vertices;
        protected short[] indices;

        public virtual void LoadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (GameObject child in children)
                child.Update(gameTime);
        }

        public virtual void Draw(BasicEffect effect, Matrix parentWorld)
        {
            objectWorld = Matrix.Identity;
            objectWorld = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(rotation) * Matrix.CreateTranslation(position);
            effect.World = objectWorld * parentWorld;
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
}
