using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SantaLand
{
    class Skydome : Sphere
    {
        public Skydome(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {
            planeHeight = 10;
            planeWidth = 20;
            scale *= 3000f;
        }

        public override void LoadContent(ContentManager Content)
        {
            base.texture = Content.Load<Texture2D>("Textures/skymap");

            base.LoadContent(Content);
        }

        protected override void InitializeVertices()
        {
            float yMax = planeHeight - 1;
            float xMax = planeWidth;

            vertices = new VertexPositionNormalTexture[planeWidth * planeHeight];
            for (int y = planeHeight - 1; y >= 0; y--)
            {
                for (int x = 0; x < planeWidth; x++)
                {
                    float radius = 360 / MathHelper.PiOver2;

                    float ringradius = radius * (float)Math.Sin(y * Math.PI / yMax);
                    Vector3 xyz = new Vector3((float)Math.Cos((xMax - x) * Math.PI * 2.0f / xMax) * ringradius, (float)Math.Cos(y * Math.PI / yMax) * radius, (float)Math.Sin((xMax - x) * Math.PI * 2.0f / xMax) * ringradius);

                    vertices[x + y * planeWidth] = new VertexPositionNormalTexture(xyz, Vector3.Forward, new Vector2(((float)x / (planeWidth + 1)), (1f - ((float)y / planeHeight))));
                }
                //Sew the edges together
                vertices[planeWidth - 1 + y * planeWidth] = new VertexPositionNormalTexture(
                    vertices[0 + y * planeWidth].Position,
                    vertices[0 + y * planeWidth].Normal,
                    new Vector2(((float)planeWidth / (planeWidth)), (float)y / planeHeight));
            }
        }

        public override void Draw(BasicEffect effect, Matrix parentWorld)
        {
            objectWorld = Matrix.Identity;
            objectWorld = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(rotation) * Matrix.CreateTranslation(position);// * Matrix.CreateFromQuaternion(solarRotation);
            effect.World = objectWorld * parentWorld;
            effect.Texture = texture;

            effect.LightingEnabled = true;
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
