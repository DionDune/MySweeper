using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace MineSweeper
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Settings Settings;
        Grid Grid;

        Texture2D Texture_White;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1800;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();


            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Settings = new Settings();
            Grid = new Grid(
                Settings.GridDimentions, 
                Settings.gridBombDensity
                );

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Procedurally Creating and Assigning a 1x1 white texture to Color_White
            Texture_White = new Texture2D(GraphicsDevice, 1, 1);
            Texture_White.SetData(new Color[1] { Color.White });
        }




        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();



            Point SlotPos = new Point(
                (_graphics.PreferredBackBufferWidth / 2) - ((Grid.Slots[0].Count() * Settings.slotRenderDimentions.X) / 2),
                (_graphics.PreferredBackBufferHeight / 2) - ((Grid.Slots.Count() * Settings.slotRenderDimentions.Y) / 2)
                );


            // Grid Base
            _spriteBatch.Draw(Texture_White, new Rectangle(SlotPos.X,
                                                           SlotPos.Y,
                                                           Grid.Dimentions.X * Settings.slotRenderDimentions.X, 
                                                           Grid.Dimentions.Y * Settings.slotRenderDimentions.Y),
                                                           Color.Gray);
            foreach (List<GridSlot> SlotRow in Grid.Slots)
            {
                foreach (GridSlot Slot in SlotRow)
                {
                    _spriteBatch.Draw(Texture_White, new Rectangle(SlotPos.X + (Slot.Position.X * Settings.slotRenderDimentions.X) + Settings.slotRenderBorder,
                                                                   SlotPos.Y + (Slot.Position.Y * Settings.slotRenderDimentions.Y) + Settings.slotRenderBorder,
                                                                   Settings.slotRenderDimentions.X - (Settings.slotRenderBorder * 2),
                                                                   Settings.slotRenderDimentions.Y - (Settings.slotRenderBorder * 2)),
                                                                   Color.White);

                    if (Slot.isRevealed)
                    {
                        if (Slot.isBomb)
                        {
                            _spriteBatch.Draw(Texture_White, new Rectangle(SlotPos.X + (Slot.Position.X * Settings.slotRenderDimentions.X) + (Settings.slotRenderDimentions.X / 4),
                                                                   SlotPos.Y + (Slot.Position.Y * Settings.slotRenderDimentions.Y) + (Settings.slotRenderDimentions.Y / 4),
                                                                   Settings.slotRenderDimentions.X / 2,
                                                                   Settings.slotRenderDimentions.Y / 2),
                                                                   Color.Red);
                        }
                    }
                }
            }



            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}