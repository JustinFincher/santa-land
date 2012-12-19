using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SantaLand.Planets
{
    class Sun : Sphere
    {
        public Sun(GraphicsDevice graphicsDevice) 
            : base(graphicsDevice)
        {
            position = new Vector3(0, 0, 0);
            scale = Vector3.One * Constants.SUN_SIZE;
        }

        public override void Update(GameTime gameTime)
        {
            rotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, 0.00006f);
            rotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, 0.000002f);

            base.Update(gameTime);
        }

        public override void LoadContent(ContentManager Content)
        {
            base.texture = Content.Load<Texture2D>("Textures/Planets/Sun/sunTexture");

            base.LoadContent(Content);
        }

        public override void Draw(BasicEffect effect, Matrix parentWorld)
        {
            objectWorld = Matrix.Identity;
            objectWorld = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(rotation) * Matrix.CreateTranslation(position);
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
}
