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
    public class SantaLand : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        BasicEffect effect;
        List<GameObject> gameObjects;
        FrameRateCounter fpsCounter;
        public Matrix view;
        public Matrix projection;

        Vector3 campos = new Vector3(0, 0, 20);

        NoClipCamera debugCam;
        Camera opportunityCam;
        Camera gruntCam;
        Opportunity oppportunity;
        Grunt grunt;
        List<Planet> planetList = new List<Planet>();
        int currentPlanet = 2;
        bool isKeyPressed = false;

        public SantaLand()
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
            debugCam = new NoClipCamera(this);//, new Vector3(0, 3000, Constants.ASTEROID_BELT_DISTANCE_FROM_SUN), 0, (float)Math.PI);
            debugCam.Activate();
            opportunityCam = new Camera(this);
            gruntCam = new Camera(this);
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
            opportunityCam.UpdateViewMatrix(oppportunity);
            gruntCam.UpdateViewMatrix(grunt);
            debugCam.ProcessInput(gameTime);

            campos = new Vector3(0, 0, campos.Z + 0.1f);
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

            Moon moon = new Moon(GraphicsDevice, earth);
            gameObjects.Add(moon);

            Mars mars = new Mars(GraphicsDevice, sun);
            gameObjects.Add(mars);

            Jupiter jupiter = new Jupiter(GraphicsDevice, sun);
            gameObjects.Add(jupiter);

            Saturn saturn = new Saturn(GraphicsDevice, sun);
            gameObjects.Add(saturn);

            Uranus uranus = new Uranus(GraphicsDevice, sun);
            gameObjects.Add(uranus);

            Neptune neptune = new Neptune(GraphicsDevice, sun);
            gameObjects.Add(neptune);

            AsteroidBelt asteroidbelt = new AsteroidBelt(GraphicsDevice, sun);
            gameObjects.Add(asteroidbelt);
            oppportunity = new Opportunity(this, Content.Load<Model>("Models/opportunity"), mercury, .1f);
            gameObjects.Add(oppportunity);

            grunt = new Grunt(this, Content.Load<Model>("Models/dude"), mercury, .1f);
            grunt.active = true;
            gameObjects.Add(grunt);

            //adding planets to list
            planetList.Add(mercury);
            planetList.Add(venus);
            planetList.Add(earth);
            planetList.Add(moon);
            planetList.Add(mars);
        }

        public void ProcessInput(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (keyState.IsKeyDown(Keys.Escape))
                this.Exit();

            if (opportunityCam.active)
                opportunityCam.CameraDistance = -mouse.ScrollWheelValue * 0.1f + opportunityCam.startDistance;
            if (grunt.active)
                gruntCam.CameraDistance = -mouse.ScrollWheelValue * 0.1f + gruntCam.startDistance;

            //activate noclip
            if (keyState.IsKeyDown(Keys.F1))
            {
                debugCam.Activate();
                opportunityCam.active = false;
                oppportunity.active = false;
                gruntCam.active = false;
                grunt.active = false;
            }
            //activate opportunity
            if (keyState.IsKeyDown(Keys.F2))
            {
                debugCam.Deactivate();
                opportunityCam.active = true;
                oppportunity.active = true;
                gruntCam.active = false;
                grunt.active = false;
            }
            //activate grunt
            if (keyState.IsKeyDown(Keys.F3))
            {
                debugCam.Deactivate();
                opportunityCam.active = false;
                oppportunity.active = false;
                gruntCam.active = true;
                grunt.active = true;
            }
            //show fps
            if (keyState.IsKeyDown(Keys.F11))
                fpsCounter.ShowFPS = true;
            //hide fps
            if (keyState.IsKeyDown(Keys.F12))
                fpsCounter.ShowFPS = false;

            //switch planet
            if (!isKeyPressed)
            {
                if (keyState.IsKeyDown(Keys.PageUp))
                {
                    if (currentPlanet < planetList.Count - 1)
                    {
                        currentPlanet++;
                        oppportunity.planet = planetList[currentPlanet];
                    }
                    isKeyPressed = true;
                }
                else if (keyState.IsKeyDown(Keys.PageDown))
                {
                    if (currentPlanet > 0)
                    {
                        currentPlanet--;
                        oppportunity.planet = planetList[currentPlanet];
                    }
                    isKeyPressed = true;
                }
            }

            if (keyState.IsKeyUp(Keys.PageUp) && keyState.IsKeyUp(Keys.PageDown))
                isKeyPressed = false; 
        }

        
    }
}
