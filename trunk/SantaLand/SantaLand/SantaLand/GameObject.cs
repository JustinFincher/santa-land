using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SantaLand
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
        protected short[] indices;

        public virtual void Initialize()
        {
            
        }

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
            foreach (GameObject child in children)
                child.Draw(effect, objectWorld);
        }
    }
}
