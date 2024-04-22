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

            // Ensure function does not check beyond grid boundaries
            if (Slot.Position.X == 0)
            {
                XRange = new Point(0, XRange.Y);
            }
            if (Slot.Position.X == Grid.Dimentions.X - 1)
            {
                XRange = new Point(XRange.X, 0);
            }
            if (Slot.Position.Y == 0)
            {
                YRange = new Point(0, YRange.Y);
            }
            if (Slot.Position.Y == Grid.Dimentions.Y - 1)
            {
                YRange = new Point(YRange.X, 0);
            }


            int bombCount = 0;

            for (int y = YRange.X; y <= YRange.Y; y++)
            {
                for (int x = XRange.X; x <= XRange.X; x++)
                {
                    if (x != 0 || y != 0)
                    {
                        if (Grid.Slots[Slot.Position.Y + y][Slot.Position.X + x].isBomb)
                            bombCount++;
                    }
                }
            }

            return bombCount;
        }

        public void revealSlot(Grid Grid, GridSlot Slot)
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
