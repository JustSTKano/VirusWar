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
        /// Передаточные координаты окрестности вокруг ячейки
        /// </summary>
        public readonly (int x, int y)[] Local = [( -1, -1 ), ( -1, 0 ), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)];
        /// <summary>
        /// Статус игры
        /// </summary>
        public bool IsGameStatus = false;
        /// <summary>
        /// Переменная хода
        /// </summary>
        public int StepCount = 0;
        /// <summary>
        /// Выбор режима игры(Ну бля пока так). 0 - игроки, 1-против компа, 2-комп против компа(тупо отладка)
        /// </summary>
        public int Mode = 1;
        /// <summary>
        /// Игрок, сделавший последний ход.
        /// </summary>
        public  BelongEnum LastPlayerStep = BelongEnum.None;
        /// <summary>
        /// Текущий игрок
        /// </summary>
        public BelongEnum CurrentPlayer = BelongEnum.None;




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
