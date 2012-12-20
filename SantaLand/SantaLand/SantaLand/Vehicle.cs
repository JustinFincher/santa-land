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

        public bool active = false;
        private float speed = 0.003f;
        private float turnSpeed = 0.015f;
        public Planet planet;
        string turnDirection;
        string throttle;
        float modelOffset;

        Quaternion planetaryPosition = Quaternion.Identity;

        public Vehicle(SantaLand game, Model model, Planet planet)
        {
            this.model = model;
            this.game = game;
            this.planet = planet;
            scale = Vector3.One * 1f;
            modelOffset = 2.7f * scale.X;
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
            if (active)
            {
                KeyboardState keyState = Keyboard.GetState();

                if (keyState.IsKeyDown(Keys.W))
                    throttle = "forward";
                else if (keyState.IsKeyDown(Keys.S))
                    throttle = "reverse";
                else
                    throttle = "";

                if (keyState.IsKeyDown(Keys.A))
                    turnDirection = "left";
                else if (keyState.IsKeyDown(Keys.D))
                    turnDirection = "right";
                else
                    turnDirection = "";
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

        private void CalculatePosition()
        {
            //Create rotational quaternions with ancient wizard magic 
            Quaternion throttleQuat = new Quaternion((float)Math.Sin(speed / planet.scale.X), 0, 0, (float)Math.Cos(speed / planet.scale.X));
            Quaternion turningQuat = new Quaternion(0, (float)Math.Sin(turnSpeed), 0, (float)Math.Cos(turnSpeed));

            //Transform the objects planetary position depending on current movement using quaternion magic
            if (turnDirection == "left")
                planetaryPosition = planetaryPosition * turningQuat;
            else if (turnDirection == "right")
                planetaryPosition = planetaryPosition * Quaternion.Inverse(turningQuat);
            if (throttle == "forward")
                planetaryPosition = planetaryPosition * throttleQuat;
            else if (throttle == "reverse")
                planetaryPosition = planetaryPosition * Quaternion.Inverse(throttleQuat);

            //Matrix representing the objects position on the planet
            Matrix coordMatrix = Matrix.Identity;
            coordMatrix =
                Matrix.CreateTranslation(new Vector3(0, planet.Radius, 0)) *
                Matrix.CreateFromQuaternion(planetaryPosition);
            Vector3 coords = coordMatrix.Translation;
            coords.Normalize();

            //translate the planetary position to latitude and longitude
            float latitude = (float)Math.Asin(coords.Y) * (float)(180.0 / Math.PI);
            float longitude = -((float)Math.Atan2(coords.Z, coords.X) * (float)(180.0 / Math.PI));

            //get the height data from the objects current lat lon
            int x = (int)(((longitude + 180) / (360)) * (planet.planeWidth -1));
            int y = (int)(((latitude + 90) / 180) * (planet.planeHeight -1));
            float terrainOffset = planet.heightData[x,y] * planet.scale.X / MathHelper.PiOver2;

            //creating a matrix representing the objects place in the global world
            Matrix newWorld = Matrix.Identity;
            newWorld =
                Matrix.CreateTranslation(new Vector3(0, modelOffset + terrainOffset , 0)) *
                Matrix.CreateFromQuaternion(planetaryPosition) *
                Matrix.CreateFromQuaternion(planet.rotation) *
                Matrix.CreateTranslation(planet.position);

            position = newWorld.Translation;
            rotation = Quaternion.CreateFromRotationMatrix(newWorld);
        }
    }
}
