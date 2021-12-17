using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Excercises
{
    internal class Day16Part2 : Day16Part1
    {
        public Day16Part2()
        {
            Day = 16;
            Part = 2;
        }

        protected override void ProcessPacket(Packet packet)
        {
            Console.WriteLine($"Result of calculation: {packet.LiteralValue}");
        }
    }
}
