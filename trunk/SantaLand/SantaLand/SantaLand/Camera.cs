using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SantaLand
{
    class Camera
    {
        Quaternion cameraRotation = Quaternion.Identity;
        float cameraDelaySpeed = 0;
        Vector3 thirdPersonReference = new Vector3(10, 0, 0);
        bool activated = false;
        Game1 game;

        public Camera(Game1 game)
        {
            this.game = game;
        }

        public void Activate()
        {
           activated = true;
        }

        public void Deactivate()
        {
            activated = false;
        }

        public void UpdateViewMatrix(Vehicle cameraTarget)
        {
            if (activated)
            {
                cameraRotation = Quaternion.Lerp(cameraRotation, cameraTarget.rotation, cameraDelaySpeed);
                // Create a vector pointing the direction the camera is facing.
                Vector3 transformedReference = Vector3.Transform(thirdPersonReference, cameraRotation);
                // Calculate the position the camera is looking from.
                Vector3 cameraPosition = transformedReference + cameraTarget.GetWorldPos();

                float aspectRatio = game.GraphicsDevice.DisplayMode.AspectRatio;

                Matrix.CreatePerspectiveFieldOfView(
                    MathHelper.PiOver4,
                    aspectRatio,
                    0.1f,
                    100000.0f,
                    out game.projection);

                game.view = Matrix.CreateLookAt(cameraPosition, cameraTarget.GetWorldPos(), Matrix.CreateFromQuaternion(cameraTarget.rotation).Up);
            }
        }
    }
}
