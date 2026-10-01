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

namespace Sprites
{
    enum DefaultSpriteActions : uint
    {
        Stand,
        WalkUp,
        WalkDown,
        WalkLeft,
        WalkRight,
        Death
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Sprite spr = new Sprite();
        Texture2D[] _walk;
        Sprite vampiro = new Sprite();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            /*
            int aux = 4;
            _walk = new Texture2D[aux];
            for (int i = 0; i < aux; i++)
			{
                _walk[i] = Content.Load<Texture2D>(@"woman\0" + i);
                spr.KeyFrames.Add((uint)i, _walk[i]);
			}
            */
            spr.Position = new Vector2(80, 120);
            spr.DrawPivot = SpriteDrawPivot.Bottom;
            spr.KeyFrames.Add(
                0,
                new KeyFrame()
                {
                    ColCount = 3,
                    Texture = Content.Load<Texture2D>(@"charset"),
                    Frames = new FrameCollection(0, 1, 2),
                    Speed = 250
                }
            );
            spr.KeyFrames[0].Frames.AddAction(FrameAction.MoveToKey, 1);
            spr.KeyFrames.Add(
                1,
                new KeyFrame()
                {
                    ColCount = 7,
                    Texture = Content.Load<Texture2D>(@"morte"),
                    Frames = new FrameCollection(0, 1, 2, 3, 4, 5, 6)
                }
            );
            spr.KeyFrames[1].Frames.AddAction(FrameAction.Stop);

            vampiro.Position = new Vector2(200, 120);
            vampiro.DrawPivot = SpriteDrawPivot.Bottom;
            vampiro.KeyFrames.Add(
                0,
                Content.Load<Texture2D>(@"vampiro"));
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

            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.A))
                spr.ActualKeyFrame = 0;

            // TODO: Add your update logic here
            SpriteUpdateArgs args = new SpriteUpdateArgs() { GameTime = gameTime };

            spr.Update(args);
            vampiro.Update(args);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            SpriteDrawArgs args = new SpriteDrawArgs() { GameTime = gameTime, SpriteBatch = spriteBatch };

            spriteBatch.Begin();

            spr.Draw(args);
            vampiro.Draw(args);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
