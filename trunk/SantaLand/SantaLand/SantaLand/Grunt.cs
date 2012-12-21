using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SkinnedModel;

namespace SantaLand
{
    internal class Grunt : Vehicle
    {
        private AnimationPlayer animationPlayer;
        private SkinningData skinningData;
        private Matrix[] boneTransforms;

        public Grunt(SantaLand game, Model model, Planet planet, float scale)
            : base(game, model, planet, scale)
        {
            speed = 0.15f;
            turnSpeed = 0.5f;
            LoadContent();    
        }

        public void LoadContent()
        {
            

            // Look up our custom skinning information.
            skinningData = model.Tag as SkinningData;

            if (skinningData == null)
                throw new InvalidOperationException
                    ("This model does not contain a SkinningData tag.");

            boneTransforms = new Matrix[skinningData.BindPose.Count];

            // Create an animation player, and start decoding an animation clip.
            animationPlayer = new AnimationPlayer(skinningData);

            AnimationClip clip = skinningData.AnimationClips["Take 001"];

            animationPlayer.StartClip(clip);
        }

        public override void Update(GameTime gameTime)
        {
            //ProcessInput(gameTime);

            //CalculatePosition(gameTime);
            //CalculateRotation();

            //HandleInput();

            //UpdateCamera(gameTime);

            // Read gamepad inputs.
            /*float headRotation = currentGamePadState.ThumbSticks.Left.X;
            float armRotation = Math.Max(currentGamePadState.ThumbSticks.Left.Y, 0);
            
            // Read keyboard inputs.
            if (currentKeyboardState.IsKeyDown(Keys.PageUp))
                headRotation = -1;
            else if (currentKeyboardState.IsKeyDown(Keys.PageDown))
                headRotation = 1;

            if (currentKeyboardState.IsKeyDown(Keys.Space))
                armRotation = 0.5f;

            // Create rotation matrices for the head and arm bones.
            Matrix headTransform = Matrix.CreateRotationX(headRotation);
            Matrix armTransform = Matrix.CreateRotationY(-armRotation);
            */
            // Tell the animation player to compute the latest bone transform matrices.
            animationPlayer.UpdateBoneTransforms(gameTime.ElapsedGameTime, true);

            // Copy the transforms into our own array, so we can safely modify the values.
            animationPlayer.GetBoneTransforms().CopyTo(boneTransforms, 0);

            // Modify the transform matrices for the head and upper-left arm bones.
            int headIndex = skinningData.BoneIndices["Head"];
            int armIndex = skinningData.BoneIndices["L_UpperArm"];
            /*
            boneTransforms[headIndex] = headTransform*boneTransforms[headIndex];
            boneTransforms[armIndex] = armTransform*boneTransforms[armIndex];
            */
            // Tell the animation player to recompute the world and skin matrices.
            animationPlayer.UpdateWorldTransforms(Matrix.Identity, boneTransforms);
            animationPlayer.UpdateSkinTransforms();

            base.Update(gameTime);
        }

        public override void Draw(BasicEffect basicEffect, Matrix parentWorld)
        {
            GraphicsDevice device = game.GraphicsDevice;

            Matrix[] bones = animationPlayer.GetSkinTransforms();

            // Render the skinned mesh.
            
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (SkinnedEffect skinnedEffect in mesh.Effects)
                {

                    skinnedEffect.SetBoneTransforms(bones);

                    skinnedEffect.View = game.view;
                    skinnedEffect.Projection = game.projection;
                    skinnedEffect.World =
                            Matrix.CreateRotationY((float)Math.PI) *
                            Matrix.CreateScale(scale) *
                            Matrix.CreateFromQuaternion(rotation) *
                            Matrix.CreateTranslation(position);

                    skinnedEffect.EnableDefaultLighting();

                    skinnedEffect.SpecularColor = new Vector3(0.25f);
                    skinnedEffect.SpecularPower = 16;
                }

                mesh.Draw();
            }

        }
    }
}
