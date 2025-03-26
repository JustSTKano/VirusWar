using System.IO;
using System.Text.Json;
using VirusWar.Data.Cells;
using VirusWar.Data.DTO;

namespace VirusWar.Engine
{
    /// <summary>
    /// 
    /// </summary>
    class MapStorage
    {
        /// <summary>
        /// Путь до файлов уровней
        /// </summary>
        public string Pathlevel;
        /// <summary>
        /// Размерность игрового поля
        /// </summary>
        public (int x, int y) Size { get; private set; }
        /// <summary>
        /// Ячейка поля
        /// </summary>
        public MapCell[,] Map { get; private set; } = new MapCell[0, 0];
        
      




        /// <summary>
        /// Проверка наличия файлов
        /// </summary>
        /// <param name="levelsFolder"></param
        public MapStorage(string levelsFolder)
        {
            Pathlevel = levelsFolder;
            if (Directory.Exists(Pathlevel))
            {
                ReadJsMap();
            }
        }

        public void ReadJsMap()
        {
            var fileJson = File.ReadAllText($"{Pathlevel}Info.json");
            
            JSRead? JsMain = JsonSerializer.Deserialize<JSRead>(fileJson);

            var MapList = JsMain.Map;

            Size = (MapList.Count, MapList[0].Count);
            Map = new MapCell[Size.x, Size.y];


            for (int i = 0; i < Size.x; i++)
            {
                var linestr = MapList[i];

                for (int j = 0; j < Size.y; j++)
                {
                    Map[j, i] = new MapCell
                    {
                        Type = linestr[j],
                        Status = StatusEnum.None,
                        Belong = BelongEnum.None,
                        IsCheck = false
                    };

                }
            }
            foreach (var a in JsMain.Positions)
            {
                Map[a.y, a.x].Status = StatusEnum.Virus;
                Map[a.y, a.x].Belong = BelongEnum.FirstPlayer;
                Map[a.x, a.y].Status = StatusEnum.Virus;
                Map[a.x, a.y].Belong = BelongEnum.SecondPlayer;              
            }
        }







    }
}
