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

        private float speed = 1;
        private Planet planet;
        private Vector3 planetaryPosition = Vector3.Zero;
        private float longitude = 0;
        private float latitude = 0;

        public Vehicle(Game1 game, Model model, Planet planet)
        {
            this.model = model;
            this.game = game;
            this.planet = planet;
            scale = Vector3.One * 3f;
            float driveDirection = 0;
        }

        public override void Update(GameTime gameTime)
        {
            ProcessInput(gameTime);

            CalculatePosition();
            CalculateRotation();

            base.Update(gameTime);
        }

        private void ProcessInput(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Up))
                latitude -= speed;
            if (keyState.IsKeyDown(Keys.Down))
                latitude += speed;
            if (keyState.IsKeyDown(Keys.Left))
                longitude -= speed;
            if (keyState.IsKeyDown(Keys.Right))
                longitude += speed;
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
                            Matrix.CreateTranslation(position);
                    }
                    mesh.Draw();
                }
            }
        }

        private void CalculatePlanetaryPosition()
        {
            float LAT = latitude * (float)Math.PI / 180;
            float LON = longitude * (float)Math.PI / 180;
            planetaryPosition.X = -planet.Radius * Game1.PLANET_SIZE_RATIO * (float)Math.Cos(LAT) * (float)Math.Cos(LON);
            planetaryPosition.Y = planet.Radius * Game1.PLANET_SIZE_RATIO * (float)Math.Sin(LAT);
            planetaryPosition.Z = planet.Radius * Game1.PLANET_SIZE_RATIO * (float)Math.Cos(LAT) * (float)Math.Sin(LON);
        }

        private void CalculateRotation()
        {
            Vector3 pointTo = Vector3.Normalize(planet.position - position);
            Vector3 rotAxis = Vector3.Cross(Matrix.Identity.Down, pointTo);
            pointTo.Normalize();
            rotAxis.Normalize();
            float rotAngle = (float)Math.Acos(Vector3.Dot(Matrix.Identity.Down, pointTo));
            rotation = Quaternion.CreateFromAxisAngle(rotAxis, rotAngle);

            rotation *= Quaternion.CreateFromRotationMatrix(Matrix.CreateFromAxisAngle(Matrix.CreateFromQuaternion(rotation).Left, (float)Math.PI));
        }

        private void CalculatePosition()
        {
            Matrix positionMatrix = Matrix.Identity;
            CalculatePlanetaryPosition();

            positionMatrix.Translation = planetaryPosition;
            positionMatrix = 
                positionMatrix * 
                Matrix.CreateFromQuaternion(planet.rotation) * 
                Matrix.CreateTranslation(planet.position);

            position = positionMatrix.Translation;
        }
    }
}
