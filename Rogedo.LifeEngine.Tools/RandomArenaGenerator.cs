using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
namespace Rogedo.LifeEngine.Tools
{
    public class RandomArenaGenerator
    {        
        public List<Point> Execute(int dimensions, int cells)
        {
            List<Point> retVal = new List<Point>();
            string dataStream = string.Empty;

            while (retVal.Count < cells)
            {
                while (dataStream.Length < 2)
                    dataStream = GetDefaultDataStream(dimensions);

                int x = Convert.ToInt32(dataStream.Substring(0, 1));
                int y = Convert.ToInt32(dataStream.Substring(1, 1));

                Point newPoint = new Point(x, y);

                retVal.Add(newPoint);

                dataStream = dataStream.Substring(2);
            }

            return retVal.Distinct().OrderBy(p => p.X).ThenBy(q => q.Y).ToList();
        }

        private string GetDefaultDataStream(int dimensions)
        {
            List<string> badGuys = new List<string>() { "a", "b", "c", "d", "e", "f", "-" };

            if (dimensions < 10)
            {
                for (int x = dimensions; x < 10; x++)
                {
                    badGuys.Add(x.ToString());
                }
            }
            string rawDataStream = Guid.NewGuid().ToString();

            foreach (var badGuy in badGuys)
            {
                rawDataStream = rawDataStream.Replace(badGuy, "");
            }

            return rawDataStream;
        }
    }
}
