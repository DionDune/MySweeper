using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MineSweeper
{
    internal class InputHandler
    {
        private bool isClicking_MouseLeft;

        public InputHandler()
        {
            isClicking_MouseLeft = false;
        }



        public void Execute(Settings settings, Grid grid, GraphicsDeviceManager _graphics)
        {
            MouseClickHandler(settings, grid, _graphics);
        }

        private void MouseClickHandler(Settings settings, Grid grid, GraphicsDeviceManager _graphics)
        {
            bool isNewClick_Left = false;

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (!isClicking_MouseLeft)
                    isNewClick_Left = true;

                isClicking_MouseLeft = true;
            }
            else
                isClicking_MouseLeft = false;



            if (isNewClick_Left)
            {
                Point GridPosition = new Point(
                    (_graphics.PreferredBackBufferWidth / 2) + grid.ScreenOffset.X,
                    (_graphics.PreferredBackBufferHeight / 2) + grid.ScreenOffset.Y
                    );

                // If mouse click falls within the grid bounds
                if (Mouse.GetState().X > GridPosition.X && Mouse.GetState().X < GridPosition.X + (grid.Dimentions.X * settings.slotRenderDimentions.X) &&
                    Mouse.GetState().Y > GridPosition.Y && Mouse.GetState().Y < GridPosition.Y + (grid.Dimentions.Y * settings.slotRenderDimentions.Y))
                {
                    Point MouseGridPosition = new Point(
                        (Mouse.GetState().X - GridPosition.X) / settings.slotRenderDimentions.X,
                        (Mouse.GetState().Y - GridPosition.Y) / settings.slotRenderDimentions.Y
                        );

                    SlotFunctions.revealSlot(grid, grid.Slots[MouseGridPosition.Y][MouseGridPosition.X], true);
                }
            }
        }
    }
}
