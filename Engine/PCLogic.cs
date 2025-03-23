using System.Windows.Controls;
using VirusWar.Data.Cells;

namespace VirusWar.Engine
{
    public static class PCLogic
    {

        internal static void PCtry(MapStorage Storage, Canvas Canvas, BelongEnum player)
        {
            MapInfo.AllMapChecker(Storage, player);
            while (MapInfo.GameStatus && (MapInfo.GetPlayerTurn() == player))
            {
                PCStep(Storage, Canvas);
                MapInfo.AllMapChecker(Storage, player);
                //if (!MapInfo.AllMapChecker(Storage, player)) { MessageBox.Show("Сорян GG"); break; }
            }
        }

        internal static void PCStep(MapStorage Storage, Canvas Canvas)
        {
            var temp = GetCord();
            //Trace.WriteLine($"X = {temp.x}, Y = {temp.y}");
            // Render.RenderMap(Reader, Canvas);
            if (MapInfo.CheckPossibleCoord(Storage, temp)) MapInfo.ReWrite(Storage, temp, Canvas);
            
        }


        public static(int x, int y) GetCord()
        {
            Random rnd = new Random();
            //int value = rnd.Next(1,10);
            return (rnd.Next(0, 11), rnd.Next(0, 11));
        }



    }
}
