﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace SantaLand
{
    class NoClipCamera
    {
        Vector3 cameraPosition = new Vector3(1000.0f, 100.0f, 0.0f);
        float leftrightRot = MathHelper.PiOver2;
        float updownRot = -MathHelper.Pi / 10.0f;
        const float rotationSpeed = 0.3f;
        const float moveSpeed = 1000.0f;
        MouseState originalMouseState;
        bool activated = false;
        Game1 game;

        public NoClipCamera(Game1 game)
        {
            this.game = game;
            Mouse.SetPosition(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2);
            originalMouseState = Mouse.GetState();
        }

        public void Activate()
        {
            Mouse.SetPosition(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2);
            activated = true;
        }

        public void Deactivate()
        {
            activated = false;
        }


        public void ProcessInput(GameTime gameTime)
        {
            ProcessInput(Keyboard.GetState(), (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void ProcessInput(float elapsedTime)
        {
            ProcessInput(Keyboard.GetState(), elapsedTime);
        }

        public void ProcessInput(KeyboardState keyState, float elapsedTime)
        {
            if (activated)
            {
                MouseState currentMouseState = Mouse.GetState();
                if (currentMouseState != originalMouseState)
                {
                    float xDifference = currentMouseState.X - originalMouseState.X;
                    float yDifference = currentMouseState.Y - originalMouseState.Y;
                    leftrightRot -= rotationSpeed * xDifference * elapsedTime;
                    updownRot -= rotationSpeed * yDifference * elapsedTime;
                    Mouse.SetPosition(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2);
                }

                Vector3 moveVector = new Vector3(0, 0, 0);
                if (keyState.IsKeyDown(Keys.W))
                    moveVector += new Vector3(0, 0, -1);
                if (keyState.IsKeyDown(Keys.S))
                    moveVector += new Vector3(0, 0, 1);
                if (keyState.IsKeyDown(Keys.D))
                    moveVector += new Vector3(1, 0, 0);
                if (keyState.IsKeyDown(Keys.A))
                    moveVector += new Vector3(-1, 0, 0);
                if (keyState.IsKeyDown(Keys.Space))
                    moveVector += new Vector3(0, 1, 0);
                if (keyState.IsKeyDown(Keys.LeftShift))
                    moveVector += new Vector3(0, -1, 0);

                AddToCameraPosition(moveVector * elapsedTime * .2f);
            }
        }

        private void AddToCameraPosition(Vector3 vectorToAdd)
        {
            Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);
            Vector3 rotatedVector = Vector3.Transform(vectorToAdd, cameraRotation);
            cameraPosition += moveSpeed * rotatedVector;

            UpdateViewMatrix();
        }

        public void UpdateViewMatrix()
        {
            Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);

            Vector3 cameraOriginalTarget = new Vector3(0, 0, -1);
            Vector3 cameraRotatedTarget = Vector3.Transform(cameraOriginalTarget, cameraRotation);
            Vector3 cameraFinalTarget = cameraPosition + cameraRotatedTarget;

            Vector3 cameraOriginalUpVector = new Vector3(0, 1, 0);
            Vector3 cameraRotatedUpVector = Vector3.Transform(cameraOriginalUpVector, cameraRotation);

            float aspectRatio = game.GraphicsDevice.DisplayMode.AspectRatio;

            Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                aspectRatio,
                0.1f,
                100000.0f,
                out game.projection);

            game.view = Matrix.CreateLookAt(cameraPosition, cameraFinalTarget, cameraRotatedUpVector);
        }
    }
}
