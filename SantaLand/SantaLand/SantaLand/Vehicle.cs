using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SantaLand
{
    class Vehicle : GameObject
    {
        protected Model model;
        protected Game1 game;

        private Planet planet;
        private float longitude;
        private float latitude;

        public Vehicle(Game1 game, Model model, Planet planet)
        {
            this.model = model;
            this.game = game;
            this.planet = planet;
            longitude = 0;
            latitude = 0;
            scale = Vector3.One * 0.01f;
        }

        public override void Update(GameTime gameTime)
        {
            ProcessInput(gameTime);

            float LAT = latitude * (float)Math.PI/180;
            float LON = longitude * (float)Math.PI/180;
            position.X = -planet.Radius * (float)Math.Cos(LAT) * (float)Math.Cos(LON);
            position.Y = planet.Radius * (float)Math.Sin(LAT);
            position.Z = planet.Radius * (float)Math.Cos(LAT) * (float)Math.Sin(LON);
            
            Vector3 pointTo = Vector3.Normalize(Vector3.Zero - position);
            Vector3 rotAxis = Vector3.Cross(Matrix.Identity.Down, pointTo);
            pointTo.Normalize();
            rotAxis.Normalize();
            float rotAngle = (float)Math.Acos(Vector3.Dot(Matrix.Identity.Down, pointTo));
            rotation = Quaternion.CreateFromAxisAngle(rotAxis, rotAngle);

            base.Update(gameTime);
        }

        private void ProcessInput(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Up))
                latitude -= 0.1f;
            if (keyState.IsKeyDown(Keys.Down))
                latitude += 0.1f;
            if (keyState.IsKeyDown(Keys.Left))
                longitude -= 0.1f;
            if (keyState.IsKeyDown(Keys.Right))
                longitude += 0.1f;
        }

        public override void Draw(BasicEffect basicEffect, Matrix parentWorld)
        {
            if (model != null)
            {
                Matrix[] transforms = new Matrix[model.Bones.Count];
                float aspectRatio = game.GraphicsDevice.Viewport.Width / game.GraphicsDevice.Viewport.Height;
                model.CopyAbsoluteBoneTransformsTo(transforms);

                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();

                        effect.View = game.view;
                        effect.Projection = game.projection;

                        effect.World =
                            Matrix.CreateScale(scale) *
                            Matrix.CreateFromQuaternion(rotation) * 
                            Matrix.CreateTranslation(position) *
                            Matrix.CreateFromQuaternion(planet.rotation) *
                            //parentWorld * 
                            //transforms[mesh.ParentBone.Index] * 
                            Matrix.CreateTranslation(planet.position);
                    }
                    mesh.Draw();
                }
            }
        }

        public Vector3 GetWorldPos()
        {
            return position + planet.position;
        }
    }
}
