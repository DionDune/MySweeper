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
            Point XRange = new Point(-1, 1);
            Point YRange = new Point(-1, 1);


            int bombCount = 0;

            for (int y = YRange.X; y <= YRange.Y; y++)
            {
                for (int x = XRange.X; x <= XRange.X; x++)
                {
                    if ((x != 0 || y != 0) &&
                        (
                        Slot.Position.X + x >= 0 &&
                        Slot.Position.X + x <= Grid.Dimentions.X - 1 &&
                        Slot.Position.Y + y >= 0 &&
                        Slot.Position.Y + y <= Grid.Dimentions.Y - 1
                        ))
                    {
                        if (Grid.Slots[Slot.Position.Y + y][Slot.Position.X + x].isBomb)
                            bombCount++;
                    }
                }
            }

            return bombCount;
        }

        public static void revealSlot(Grid Grid, GridSlot Slot)
        {
            if (Slot.isBomb)
            {
                // Lose Logic
            }
            else if (!Slot.isRevealed)
            {
                int bombCount = getSurroundingBombCount(Grid, Slot);

                Slot.surroundingBombCount = bombCount;

                if (bombCount == 0)
                {
                    // Cascade reveal Logic
                }

                Slot.isRevealed = true;
            }
        }
    }
}
