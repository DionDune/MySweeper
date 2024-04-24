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

        public Point FontSize { get; set; }


        public Settings()
        {
            GridDimentions = new Point(60, 35);
            gridBombDensity = 0.18F;

            slotRenderDimentions = new Point(26, 26);

            FontSize = new Point(52, 52);
        }
    }
}
