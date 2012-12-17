using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SantaLand
{
    abstract class GameObject
    {
        public Vector3 position = Vector3.Zero;
        public Quaternion rotation = Quaternion.Identity;
        public Vector3 scale = Vector3.One;
        public List<GameObject> children = new List<GameObject>();
        public Vector3 velocity = Vector3.Zero;

        protected VertexBuffer vertexBuffer;
        protected IndexBuffer indexBuffer;
        public Matrix objectWorld;
        protected GraphicsDevice graphicsDevice;
        protected VertexPositionNormalTexture[] vertices;
        protected int[] indices;
        protected Texture2D texture;

        protected Vector3 lightDirection;

        public virtual void Initialize()
        {
            
        }

        public virtual void LoadContent(ContentManager Content)
        {
            foreach (GameObject child in children)
                child.LoadContent(Content);
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
            effect.Texture = texture;

            effect.LightingEnabled = true; // turn on the lighting subsystem.
            effect.DirectionalLight0.DiffuseColor = new Vector3(0.8f, 0.8f, 0.8f); // a red light
            effect.DirectionalLight0.Direction = new Vector3(1, 0, 0);  // coming along the x-axis
            effect.AmbientLightColor = new Vector3(0.05f, 0.05f, 0.05f);
            effect.EmissiveColor = new Vector3(0.05f, 0.05f, 0.05f);

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
