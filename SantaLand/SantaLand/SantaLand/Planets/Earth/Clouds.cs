using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SantaLand.Planets
{
    class Clouds : Sphere
    {
        public Clouds(GraphicsDevice graphicsDevice, Vector3 lightDirection) 
            : base(graphicsDevice, lightDirection)
        {
            position = new Vector3(0, 0, 0);
            scale = Vector3.One * 1.01f;
        }

        public override void LoadContent(ContentManager Content)
        {
            base.texture = Content.Load<Texture2D>("Textures/Planets/Earth/clouds");
        }

        public override void Update(GameTime gameTime)
        {
            rotation *= Quaternion.CreateFromAxisAngle(Vector3.Forward, 0.001f);
            rotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, 0.0006f);

            base.Update(gameTime);
        }

        public override void Draw(BasicEffect effect, Matrix parentWorld)
        {
            graphicsDevice.BlendState = BlendState.AlphaBlend;
            base.Draw(effect, parentWorld);
        }
    }
}
