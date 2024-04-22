using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MineSweeper
{
    internal class Settings
    {
        public Point GridDimentions { get; set; }
        public float gridBombDensity { get; set; }

        public Point slotRenderDimentions { get; set; }


        public Settings()
        {
            GridDimentions = new Point(20, 20);
            gridBombDensity = 0.4F;

            slotRenderDimentions = new Point(26, 26);
        }
    }
}
