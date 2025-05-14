using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Utils
{
    public sealed class SortingTools
    {
        public List<T> SortByNumber<T>(T[] obj) where T : Object
        {
            return obj.OrderBy(x => ExtractNumber(x.name)).ToList();
        }

        private int ExtractNumber(string name)
        {
            var numberString = new string(name.Where(char.IsDigit).ToArray());
            return int.TryParse(numberString, out var number) ? number : 0;
        }
    }
}