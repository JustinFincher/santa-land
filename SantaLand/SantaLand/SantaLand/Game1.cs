using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SantaLand.Planets;

namespace SantaLand
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public const float PLANET_SIZE_RATIO = 10000.0f; //Defines how much bigger the planets are.

        GraphicsDeviceManager graphics;
        BasicEffect effect;
        List<GameObject> gameObjects;
        FrameRateCounter fpsCounter;
        public Matrix view;
        public Matrix projection;

        Vector3 lightDirection;

        NoClipCamera debugCam;
        Camera camera;
        Opportunity oppportunity;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferMultiSampling = true;

            gameObjects = new List<GameObject>();
         
            Content.RootDirectory = "Content";

            //FPS counter
            fpsCounter = new FrameRateCounter(this);
            Components.Add(fpsCounter);
            fpsCounter.ShowFPS = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            debugCam = new NoClipCamera(this);
            debugCam.Activate();
            camera = new Camera(this);
            CreateWorld();

            //view = Matrix.CreateLookAt(campos, Vector3.Zero, Vector3.Up);
            //projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 4.0f / 3.0f, 1, 500);

            debugCam.UpdateViewMatrix();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            effect = new BasicEffect(GraphicsDevice);
            effect.Projection = projection;
            effect.World = Matrix.Identity;

            lightDirection = new Vector3(3, -2, 5);
            lightDirection.Normalize();

            debugCam.UpdateViewMatrix();
            foreach (GameObject go in gameObjects)
                go.LoadContent(Content);

            foreach (GameObject go in gameObjects)
                go.Initialize();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            ProcessInput(gameTime);
            camera.UpdateViewMatrix(oppportunity);
            debugCam.ProcessInput(gameTime);

            //view = Matrix.CreateLookAt(campos, Vector3.Zero, Vector3.Up);
            foreach (GameObject go in gameObjects)
                go.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // Set states ready for 3D  
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.RasterizerState = new RasterizerState() { CullMode = CullMode.CullCounterClockwiseFace, FillMode = FillMode.Solid };

            effect.View = view;
            effect.World = Matrix.Identity;
            effect.TextureEnabled = true;

            foreach (GameObject go in gameObjects)
            {
                effect.World = Matrix.Identity;
                go.Draw(effect, effect.World);
            }

            base.Draw(gameTime);
        }

        void CreateWorld()
        {
            Skydome skydome = new Skydome(GraphicsDevice);
            gameObjects.Add(skydome);

            Sun sun = new Sun(GraphicsDevice);
            gameObjects.Add(sun);

            Mercury mercury = new Mercury(GraphicsDevice, sun);
            gameObjects.Add(mercury);

            Venus venus = new Venus(GraphicsDevice, sun);
            gameObjects.Add(venus); 

            Earth earth = new Earth(GraphicsDevice, sun);
            gameObjects.Add(earth);

            Mars mars = new Mars(GraphicsDevice, sun);
            gameObjects.Add(mars); 

            oppportunity = new Opportunity(this, Content.Load<Model>("Models/opportunity"), mercury);
            gameObjects.Add(oppportunity);  
        }

        public void ProcessInput(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            //toggle noclip and fps
            if (keyState.IsKeyDown(Keys.F1))
            {
                debugCam.Activate();
                camera.Deactivate();
            }
            if (keyState.IsKeyDown(Keys.F2))
            {
                debugCam.Deactivate();
                camera.Activate();
            }
            if (keyState.IsKeyDown(Keys.F3))
                fpsCounter.ShowFPS = true;
            if (keyState.IsKeyDown(Keys.F4))
                fpsCounter.ShowFPS = false;
        }

        
    }
}
