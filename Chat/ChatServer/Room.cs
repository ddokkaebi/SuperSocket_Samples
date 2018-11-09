using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    class Room
    {
        public int Index { get; private set; }
        public int Number { get; private set; }

        int MaxUserCount = 0;

        public void Init(int index, int number, int maxUserCount)
        {
            Index = index;
            Number = number;
            MaxUserCount = maxUserCount;
        }
    }
}
