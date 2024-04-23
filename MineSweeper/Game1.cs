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
        Textures Textures;
        Grid Grid;

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
            Textures.TileNum.Add(Content.Load<Texture2D>("TileNum1"));
            Textures.TileNum.Add(Content.Load<Texture2D>("TileNum2"));
            Textures.TileNum.Add(Content.Load<Texture2D>("TileNum3"));
            Textures.TileNum.Add(Content.Load<Texture2D>("TileNum4"));
            Textures.TileNum.Add(Content.Load<Texture2D>("TileNum5"));
            Textures.TileNum.Add(Content.Load<Texture2D>("TileNum6"));
            Textures.TileNum.Add(Content.Load<Texture2D>("TileNum7"));
            Textures.TileNum.Add(Content.Load<Texture2D>("TileNum8"));
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



            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}