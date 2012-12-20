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
        float minimumViewDistance = 0.1f;
        float maximumViewDistance = 10000;
        float cameraDelaySpeed = 1f;
        Vector3 thirdPersonReference = new Vector3(0, 5, -50);

        float cameraDistance = 50;
        public float CameraDistance
        {
            get { return cameraDistance; }
            set { cameraDistance = value; }
        }

        
        bool activated = false;
        SantaLand game;

        public Camera(SantaLand game)
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

        public void UpdateViewMatrix(GameObject cameraTarget)
        {
            if (activated)
            {
                cameraRotation = Quaternion.Lerp(cameraRotation, cameraTarget.rotation, cameraDelaySpeed);
                // Create a vector pointing the direction the camera is facing.
                Vector3 transformedReference = Vector3.Transform(thirdPersonReference, cameraRotation);
                // Calculate the position the camera is looking from.
                Vector3 cameraPosition = Vector3.Normalize(transformedReference) * cameraDistance + cameraTarget.position;

                float aspectRatio = game.GraphicsDevice.DisplayMode.AspectRatio;

                Matrix.CreatePerspectiveFieldOfView(
                    MathHelper.PiOver4,
                    aspectRatio,
                    minimumViewDistance,
                    maximumViewDistance,
                    out game.projection);

                game.view = Matrix.CreateLookAt(cameraPosition, cameraTarget.position, Matrix.CreateFromQuaternion(cameraTarget.rotation).Up);
            }
        }
    }
}
