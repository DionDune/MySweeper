using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MineSweeper
{
    internal class Textures
    {
        public List<Texture2D> TileNum { get; set; }
        public Texture2D Tile { get; set; }
        public Texture2D TileRevealedEmpty { get; set; }
        public Texture2D TileBomb { get; set; }
        public Texture2D TileFlagged { get; set; }

        public Texture2D White;

        public Textures()
        {
            TileNum = new List<Texture2D>();
        }
    }
}
