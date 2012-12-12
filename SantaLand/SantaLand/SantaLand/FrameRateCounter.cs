using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class FrameRateCounter : DrawableGameComponent
{
    SpriteBatch spriteBatch;
    SpriteFont spriteFont;

    private int frameRate = 0;
    private int frameCounter = 0;
    TimeSpan elapsedTime = TimeSpan.Zero;

    bool showFPS;
    public bool ShowFPS
    {
        get { return showFPS; }
        set { showFPS = value; }
    }

    public FrameRateCounter(Game game)
        : base(game)
    {
    }


    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        spriteFont = Game.Content.Load<SpriteFont>("FrameRateCounterFont");
        base.LoadContent();
    }

    protected override void UnloadContent()
    {
        base.UnloadContent();
    }

    public override void Update(GameTime gameTime)
    {
        elapsedTime += gameTime.ElapsedGameTime;

        if (elapsedTime > TimeSpan.FromSeconds(1))
        {
            elapsedTime -= TimeSpan.FromSeconds(1);
            frameRate = frameCounter;
            frameCounter = 0;
        }
    }


    public override void Draw(GameTime gameTime)
    {
        if (ShowFPS)
        {
            frameCounter++;

            string fps = string.Format("fps: {0}", frameRate);

            spriteBatch.Begin();

            spriteBatch.DrawString(spriteFont, fps, new Vector2(33, 33), Color.Black);
            spriteBatch.DrawString(spriteFont, fps, new Vector2(32, 32), Color.White);

            spriteBatch.End();
        }
        base.Draw(gameTime);
    }
}
