using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MineSweeper
{
    internal class Grid
    {
        public List<List<GridSlot>> Slots { get; set; }
        public Point Dimentions { get; set; }
        public Point ScreenOffset { get; set; }
        public bool isNew { get; set; }

        public Grid(Settings settings)
        {
            SetDimentions(settings);
            GenerateGrid(settings.gridBombDensity);
            SetScreenOffset(settings);
            isNew = true;
        }


        private void SetDimentions(Settings settings)
        {
            Dimentions = settings.GridDimentions;
        }
        private void GenerateGrid(float bombDensity)
        {
            Random random = new Random();

            Slots = new List<List<GridSlot>>();

            for (int y = 0; y < Dimentions.Y; y++)
            {
                Slots.Add(new List<GridSlot>());

                for (int x = 0; x < Dimentions.X; x++)
                {
                    Slots.Last().Add(new GridSlot(x, y));

                    if (random.Next(0, 100) < (int)(bombDensity * 100))
                    {
                        Slots.Last().Last().isBomb = true;
                    }
                }
            }
        }
        private void SetScreenOffset(Settings settings)
        {
            ScreenOffset = new Point(-((settings.slotRenderDimentions.X * Dimentions.X) / 2), 
                                     -((settings.slotRenderDimentions.Y * Dimentions.Y) / 2));
        }

        public void Regenerate(Settings settings)
        {
            SetDimentions(settings);
            GenerateGrid(settings.gridBombDensity);
            SetScreenOffset(settings);

            isNew = true;
        }


        public void genForCascade(Settings settings, Point SlotPosition)
        // Regenerates the grid untill the slot of SlotPosition has no immediate adjacent bombs.
        {

            Grid newGrid = new Grid(settings);
            GridSlot newSlot = newGrid.Slots[SlotPosition.Y][SlotPosition.X];

            while (SlotFunctions.getSurroundingBombCount(newGrid, newSlot) != 0 || newSlot.isBomb)
            {
                newGrid = new Grid(settings);
                newSlot = newGrid.Slots[newSlot.Position.Y][newSlot.Position.X];
            }

            Slots = newGrid.Slots;
        }
    }
    internal class GridSlot
    {
        public Point Position { get; set; }
        public bool isBomb { get; set; }
        public int surroundingBombCount { get; set; }
        
        public bool isFlagged { get; set; }
        public bool isRevealed { get; set; }

        public GridSlot(int X, int Y)
        {
            Position = new Point(X, Y);
            isBomb = false;
            surroundingBombCount = 0;

            isFlagged = false;
            isRevealed = false;
        }
    }
}
