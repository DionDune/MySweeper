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
            if (Slot.isBomb && isPlayerMove)
            {
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
    }
}
