using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWar.Data.DTO
{
    internal class JSRead
    {
        public List<List<int>> Map { get; set; }
        public List<Position> Positions { get; set; }
    }

    internal class Position
    {
        public int x { get; set; }
        public int y { get; set; }
    }
}
