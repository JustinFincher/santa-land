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

namespace SantaLand
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        BasicEffect effect;
        List<GameObject> gameObjects;
        FrameRateCounter fpsCounter;
        Matrix view;
        Matrix projection;

        NoClipCamera debugCam;

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
            effect = new BasicEffect(GraphicsDevice);
            effect.Projection = projection;
            effect.World = Matrix.Identity;
            // Set states ready for 3D  
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.RasterizerState = new RasterizerState() { CullMode = CullMode.CullCounterClockwiseFace, FillMode = FillMode.Solid };

            debugCam = new NoClipCamera(GraphicsDevice, ref projection, ref view);
            debugCam.Activate();

            CreateWorld();

            foreach (GameObject go in gameObjects)
                go.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            debugCam.UpdateViewMatrix();
            foreach (GameObject go in gameObjects)
                go.LoadContent();
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

            debugCam.ProcessInput(gameTime);

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
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            effect.View = view;
            effect.World = Matrix.Identity;
            effect.EnableDefaultLighting();
            effect.TextureEnabled = true;

            foreach (GameObject go in gameObjects)
                go.Draw(effect, effect.World);

            base.Draw(gameTime);
        }

        void CreateWorld()
        {
            Planet mars = new Planet(GraphicsDevice, Content.Load<Texture2D>("Textures/Planets/Mars/marsHeightmap"), Content.Load<Texture2D>("Textures/Planets/Mars/marsTexture"));
            mars.LoadHeightData();
            gameObjects.Add(mars);
        }
    }
}
