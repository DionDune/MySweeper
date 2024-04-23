using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MineSweeper
{
    internal class SlotFunctions
    {
        public static int getSurroundingBombCount(Grid Grid, GridSlot Slot)
        {
            Point XRange = new Point(Slot.Position.X - 1, Slot.Position.X + 1);
            Point YRange = new Point(Slot.Position.Y - 1, Slot.Position.Y + 1);


            int bombCount = 0;

            for (int y = YRange.X; y <= YRange.Y; y++)
            {
                for (int x = XRange.X; x <= XRange.Y; x++)
                {
                    //Not the current slot
                    if (x != Slot.Position.X || y != Slot.Position.Y)
                    {
                        // Not beyond bounds
                        if (x >= 0 && x <= Grid.Dimentions.X - 1 &&
                            y >= 0 && y <= Grid.Dimentions.Y - 1)
                        {
                            //Is Bomb
                            if (Grid.Slots[y][x].isBomb)
                            {
                                bombCount++;
                            }
                        }
                    }
                }
            }

            return bombCount;
        }

        public static void revealSlot(Grid Grid, GridSlot Slot, bool isPlayerMove)
        {
            
            if (Slot.isFlagged)
            {
                // Nullifies attempts to reveal flagged slots
                if (isPlayerMove)
                    return;

                // Deflag slot. Will occour when a flagged slot was not a bomb
                else
                {
                    Slot.isFlagged = false;
                }
            }
                

            if (Slot.isBomb && isPlayerMove)
            {
                Slot.isRevealed = true;

                // Lose Logic
            }
            else if (!Slot.isRevealed)
            {
                int bombCount = getSurroundingBombCount(Grid, Slot);

                Slot.surroundingBombCount = bombCount;

                if (bombCount == 0 && isPlayerMove)
                {
                    // Cascade reveal Logic
                }

                Slot.isRevealed = true;
            }
        }
        private static void hideSlot(Grid Grid, GridSlot Slot, bool removeFlags)
        {
            if (removeFlags)
            {
                Slot.isFlagged = false;
            }

            Slot.isRevealed = false;
        }
        public static void flagSlot(GridSlot Slot)
        {
            // Deflag slot
            if (Slot.isFlagged)
            {
                Slot.isFlagged = false;
                // Flag Counter Decrease

                if (Slot.isBomb)
                {
                    // Internal Bomb Counter Increase
                }
            }
            // Flag slot
            else
            {
                Slot.isFlagged = true;
                // Flag Counter Increase

                if (Slot.isBomb)
                {
                    // Internal Bomb Counter Decrease
                }
            }
        }

        public static void resetGrid(Settings Settings, Grid Grid)
        {
            Grid.Regenerate(Settings);
        }
        public static void revealGrid(Grid Grid)
        {
            foreach (List<GridSlot> Row in Grid.Slots)
            {
                foreach (GridSlot Slot in Row)
                {
                    revealSlot(Grid, Slot, false);
                }
            }
        }
        public static void hideGrid(Grid Grid)
        {
            foreach (List<GridSlot> Row in Grid.Slots)
            {
                foreach (GridSlot Slot in Row)
                {
                    hideSlot(Grid, Slot, true);
                }
            }
        }
    }
}
