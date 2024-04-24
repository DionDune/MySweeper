using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MineSweeper
{
    public class MySweeper : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Settings Settings;
        InputHandler InputHandler;
        Textures Textures;
        Grid Grid;


        public MySweeper()
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
            InputHandler = new InputHandler();
            Textures = new Textures();
            Grid = new Grid( Settings );

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Procedurally Creating and Assigning a 1x1 white texture to Color_White
            Textures.White = new Texture2D(GraphicsDevice, 1, 1);
            Textures.White.SetData(new Color[1] { Color.White });

            Textures.Tile = Content.Load<Texture2D>("Tile");
            Textures.TileRevealedEmpty = Content.Load<Texture2D>("TileEmpty");
            Textures.TileBomb = Content.Load<Texture2D>("TileBomb");
            Textures.TileFlagged = Content.Load<Texture2D>("TileFlag");

            Textures.CharDot = Content.Load<Texture2D>("CharDot");

            Textures.TileNum.Add(Content.Load<Texture2D>("TileNum1"));
            Textures.TileNum.Add(Content.Load<Texture2D>("TileNum2"));
            Textures.TileNum.Add(Content.Load<Texture2D>("TileNum3"));
            Textures.TileNum.Add(Content.Load<Texture2D>("TileNum4"));
            Textures.TileNum.Add(Content.Load<Texture2D>("TileNum5"));
            Textures.TileNum.Add(Content.Load<Texture2D>("TileNum6"));
            Textures.TileNum.Add(Content.Load<Texture2D>("TileNum7"));
            Textures.TileNum.Add(Content.Load<Texture2D>("TileNum8"));

            Textures.CharNum.Add(Content.Load<Texture2D>("Char0"));
            Textures.CharNum.Add(Content.Load<Texture2D>("Char1"));
            Textures.CharNum.Add(Content.Load<Texture2D>("Char2"));
            Textures.CharNum.Add(Content.Load<Texture2D>("Char3"));
            Textures.CharNum.Add(Content.Load<Texture2D>("Char4"));
            Textures.CharNum.Add(Content.Load<Texture2D>("Char5"));
            Textures.CharNum.Add(Content.Load<Texture2D>("Char6"));
            Textures.CharNum.Add(Content.Load<Texture2D>("Char7"));
            Textures.CharNum.Add(Content.Load<Texture2D>("Char8"));
            Textures.CharNum.Add(Content.Load<Texture2D>("Char9"));
        }




        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputHandler.Execute(Settings, Grid, _graphics);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();



            Point SlotPos = new Point(
                (_graphics.PreferredBackBufferWidth / 2) + Grid.ScreenOffset.X,
                (_graphics.PreferredBackBufferHeight / 2) + Grid.ScreenOffset.Y
                );


            //Slots
            foreach (List<GridSlot> SlotRow in Grid.Slots)
            {
                foreach (GridSlot Slot in SlotRow)
                {
                    _spriteBatch.Draw(Textures.Tile, new Rectangle(SlotPos.X + (Slot.Position.X * Settings.slotRenderDimentions.X),
                                                                   SlotPos.Y + (Slot.Position.Y * Settings.slotRenderDimentions.Y),
                                                                   Settings.slotRenderDimentions.X,
                                                                   Settings.slotRenderDimentions.Y),
                                                                   Color.White);

                    if (Slot.isRevealed)
                    {
                        if (Slot.isBomb)
                        {
                            _spriteBatch.Draw(Textures.TileBomb, new Rectangle(SlotPos.X + (Slot.Position.X * Settings.slotRenderDimentions.X),
                                                                            SlotPos.Y + (Slot.Position.Y * Settings.slotRenderDimentions.Y),
                                                                            Settings.slotRenderDimentions.X,
                                                                            Settings.slotRenderDimentions.Y),
                                                                            Color.White);
                        }
                        else if (Slot.surroundingBombCount > 0)
                        {
                            _spriteBatch.Draw(Textures.TileNum[Slot.surroundingBombCount - 1], new Rectangle(SlotPos.X + (Slot.Position.X * Settings.slotRenderDimentions.X),
                                                                            SlotPos.Y + (Slot.Position.Y * Settings.slotRenderDimentions.Y),
                                                                            Settings.slotRenderDimentions.X,
                                                                            Settings.slotRenderDimentions.Y),
                                                                            Color.White);
                        }
                        else if (Slot.surroundingBombCount == 0)
                        {
                            _spriteBatch.Draw(Textures.TileRevealedEmpty, new Rectangle(SlotPos.X + (Slot.Position.X * Settings.slotRenderDimentions.X),
                                                                            SlotPos.Y + (Slot.Position.Y * Settings.slotRenderDimentions.Y),
                                                                            Settings.slotRenderDimentions.X,
                                                                            Settings.slotRenderDimentions.Y),
                                                                            Color.White);
                        }
                    }
                    else if (Slot.isFlagged)
                    {
                        _spriteBatch.Draw(Textures.TileFlagged, new Rectangle(SlotPos.X + (Slot.Position.X * Settings.slotRenderDimentions.X),
                                                                            SlotPos.Y + (Slot.Position.Y * Settings.slotRenderDimentions.Y),
                                                                            Settings.slotRenderDimentions.X,
                                                                            Settings.slotRenderDimentions.Y),
                                                                            Color.White);
                    }
                }
            }

            //Flag Remaining Counter
            string FlagsRemaining = (Grid.getBombCount() - Grid.getFlagCount()).ToString();
            Point CounterPos = new Point(
                SlotPos.X + ((Grid.Dimentions.X * Settings.slotRenderDimentions.X) / 2) - ((FlagsRemaining.Length * Settings.FontSize.X) / 2),
                SlotPos.Y + (Grid.Dimentions.Y * Settings.slotRenderDimentions.Y)
                );
            for (int i = 0; i < FlagsRemaining.Length; i++)
            {
                _spriteBatch.Draw(Textures.CharNum[ int.Parse(FlagsRemaining[i].ToString()) ], new Rectangle(
                    CounterPos.X + (i * Settings.FontSize.X),
                    CounterPos.Y,
                    Settings.FontSize.X,
                    Settings.FontSize.Y
                    ), Color.Red);
            }



            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}