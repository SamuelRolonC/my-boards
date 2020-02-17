using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoards.Services
{
    public class ArrayStringToInt
    {
        public static int[] Execute(string[] strArray)
        {
            int[] newTagIds = new int[strArray.Length];

            for (int i = 0; i < strArray.Length; i++)
            {
                newTagIds[i] = int.Parse(strArray[i]);
            }

            return newTagIds;
        }
    }
}
