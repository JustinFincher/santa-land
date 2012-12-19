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
        protected SantaLand game;

        private float speed = 0.02f;
        private float turnSpeed = 0.02f;
        private Planet planet;
        private Vector3 planetaryPosition = Vector3.Zero;
        private float longitude = 0;
        private float latitude = 0;
        float yaw = 0;
        float pitch = 0;

        public Vehicle(SantaLand game, Model model, Planet planet)
        {
            this.model = model;
            this.game = game;
            this.planet = planet;
            scale = Vector3.One * 3f;
        }

        public override void Update(GameTime gameTime)
        {
            ProcessInput(gameTime);

            CalculatePosition();
            //CalculateRotation();

            base.Update(gameTime);
        }

        private void ProcessInput(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Up))
                pitch += speed;
            if (keyState.IsKeyDown(Keys.Down))
                pitch -= speed;

            if (keyState.IsKeyDown(Keys.Q))
                longitude -= speed;
            if (keyState.IsKeyDown(Keys.E))
                longitude += speed;

            if (keyState.IsKeyDown(Keys.Left))
            {
                yaw += turnSpeed;
            }
            if (keyState.IsKeyDown(Keys.Right))
            {
                yaw -= turnSpeed;
            }
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
            if (latitude > 89)
                latitude = 89;
            else if (latitude < -89)
                latitude = -89;
            if (longitude > 180)
                longitude = -180;
            else if (longitude < -180)
                longitude = 180;

            float LAT = latitude * (float)Math.PI / 180;
            float LON = longitude * (float)Math.PI / 180;
            planetaryPosition.X = -planet.Radius * (float)Math.Cos(LAT) * (float)Math.Cos(LON);
            planetaryPosition.Y = planet.Radius * (float)Math.Sin(LAT);
            planetaryPosition.Z = planet.Radius * (float)Math.Cos(LAT) * (float)Math.Sin(LON);
        }

        //private void CalculateRotation()
        //{
        //    Vector3 pointTo = Vector3.Normalize(planet.position - position);
        //    Vector3 rotAxis = Vector3.Cross(Matrix.Identity.Down, pointTo);
        //    pointTo.Normalize();
        //    rotAxis.Normalize();
        //    float rotAngle = (float)Math.Acos(Vector3.Dot(Matrix.Identity.Down, pointTo));
        //    rotation = Quaternion.CreateFromAxisAngle(rotAxis, rotAngle);

        //    rotation = Quaternion.CreateFromAxisAngle(pointTo, driveDirection) * rotation;
        //}

        private void CalculatePosition()
        {
            //Matrix positionMatrix = Matrix.Identity;
            //CalculatePlanetaryPosition();

            //positionMatrix.Translation = planetaryPosition;
            //positionMatrix = 
            //    positionMatrix * 
            //    Matrix.CreateFromQuaternion(planet.rotation) * 
            //    Matrix.CreateTranslation(planet.position);

            //position = positionMatrix.Translation;

            Matrix ultimatePosition = Matrix.Identity;
            ultimatePosition =
                Matrix.CreateTranslation(new Vector3(0, planet.Radius, 0)) *
                Matrix.CreateFromYawPitchRoll(yaw, pitch, 0) *
                Matrix.CreateFromQuaternion(planet.rotation) *
                Matrix.CreateTranslation(planet.position);


            position = ultimatePosition.Translation;
            rotation = Quaternion.CreateFromRotationMatrix(ultimatePosition);
        }
    }
}
