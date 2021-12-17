using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace AdventOfCode2021.Excercises
{

    internal class Packet
    {
        public int Version { get; set; }
        
        public long LiteralValue { get; set; }

        public int EndingIndex { get; set; }

    }

    internal class Day16Part1 : ExcerciseBase
    {
        public Day16Part1()
        {
            Day = 16;
            Part = 1;
        }

        

        public override void Run()
        {
            string input = ReadFullFile("data\\day16.txt");
            string binary = ConvertToBinary(input);

            BitArray bits = new BitArray(binary.Length);
            for (int j = 0; j < binary.Length; j++)
            {
                if(binary[j] == '0') bits.Set(j, false);
                else bits.Set(j, true);
            }
            
            
            //Console.WriteLine($"Processing {input} in binary {binary}");

            var packet = ReadPacket(bits, 0);

            
            ProcessPacket(packet);
        }

        protected virtual void ProcessPacket(Packet packet)
        {
            Console.WriteLine($"Total vesion sum:  {packet.Version} " );

        }

        private int ReadTypeId(BitArray bits, int packetStartIndex)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 3; i < 6; i++)
            {
                sb.Append(bits.Get(packetStartIndex + i) ? "1" : "0");
            }

            return BinaryToInt(sb.ToString());
        }

        private int ReadVersion(BitArray bits, int packetStartIndex)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                sb.Append(bits.Get(packetStartIndex + i) ? "1" : "0");
            }

            return BinaryToInt(sb.ToString());
        }

        private Packet ReadPacket(BitArray bits, int packetStartIndex)
        {
            int typeId = ReadTypeId(bits, packetStartIndex);


            Packet packet = null;

            if (typeId == 4)
            {
                packet = ReadLiteralValue(bits, packetStartIndex);
            }
            else
            {
                packet = ReadOperatorPacket(bits, packetStartIndex, typeId);
            }

            return packet;
        }


        private long CalculateValue(int typeId, List<long> values)
        {
            long calculatedValue = 0;

            if (typeId == 0)
            {
                long sum = 0;
                foreach (long value in values)
                {
                    sum += value;
                }
                calculatedValue = sum;
            }
            else if (typeId == 1)
            {
                long product = 1;
                foreach (long value in values)
                {
                    product *= value;
                }
                calculatedValue = product;
            }
            else if (typeId == 2)
            {
                long min = long.MaxValue;
                foreach (long value in values)
                {
                    if (value < min)
                        min = value;
                }
                calculatedValue = min;
            }
            else if (typeId == 3)
            {
                long max = long.MinValue;
                foreach (long value in values)
                {
                    if (value > max)
                        max = value;
                }
                calculatedValue = max;
            }
            else if (typeId == 5)
            {
                if (values[0] > values[1]) calculatedValue = 1L;
                else calculatedValue = 0L;
            }
            else if (typeId == 6)
            {
                if (values[0] < values[1]) calculatedValue = 1L;
                else calculatedValue = 0L;
            }
            else if (typeId == 7)
            {
                if (values[0] == values[1]) calculatedValue = 1L;
                else calculatedValue = 0L;
            }

            return calculatedValue;
        }


        private Packet ProcessSubpacketsUsingTotalLength(BitArray bits, int version, int packetStartIndex, int typeId)
        {
            int versionSum = 0;
            int endingIndex = 0;

            StringBuilder sb = new StringBuilder();
            for (int i = packetStartIndex + 8; i < packetStartIndex + 22; i++)
            {
                sb.Append(bits.Get(i) ? "1" : "0");
            }

            int totalLength = BinaryToInt(sb.ToString());

            int cumulativeLengthOfPrevPackets = 0;
            List<long> subpacketValues = new List<long>();
            while (totalLength > 0)
            {
                Packet pd = ReadPacket(bits, packetStartIndex + 22 + cumulativeLengthOfPrevPackets);
                int lengthOfPreviousPacket = pd.EndingIndex - (packetStartIndex + 22 + cumulativeLengthOfPrevPackets) + 1;
                versionSum += pd.Version;
                endingIndex = pd.EndingIndex;
                cumulativeLengthOfPrevPackets += lengthOfPreviousPacket;
                totalLength -= lengthOfPreviousPacket;
                subpacketValues.Add(pd.LiteralValue);
            }

            long calculatedValue = CalculateValue(typeId, subpacketValues);

            return new Packet{ Version = versionSum + version, EndingIndex = endingIndex, LiteralValue = calculatedValue };
        }

        private Packet ProcessSubpacketsUsingPacketCount(BitArray bits, int version, int packetStartIndex, int typeId)
        {
            int versionSum = 0;
            int endingIndex = 0;

            StringBuilder sb = new StringBuilder();
            for (int i = packetStartIndex + 8; i < packetStartIndex + 18; i++)
            {
                sb.Append(bits.Get(i) ? "1" : "0");
            }

            int numSubpackets = BinaryToInt(sb.ToString());

            int cumulativeLengthOfPrevPackets = 0;
            List<long> subpacketValues = new List<long>();
            while (numSubpackets > 0)
            {
                Packet pd = ReadPacket(bits, packetStartIndex + 18 + cumulativeLengthOfPrevPackets);
                int lengthOfPreviousPacket = pd.EndingIndex - (packetStartIndex + 18 + cumulativeLengthOfPrevPackets) + 1;
                versionSum += pd.Version;
                endingIndex = pd.EndingIndex;
                cumulativeLengthOfPrevPackets += lengthOfPreviousPacket;
                subpacketValues.Add(pd.LiteralValue);

                numSubpackets--;
            }

            long calculatedValue = CalculateValue(typeId, subpacketValues);

            return new Packet { Version = versionSum + version, EndingIndex = endingIndex, LiteralValue = calculatedValue };
        }

        private Packet ReadOperatorPacket(BitArray bits, int packetStartIndex, int typeId)
        {
            int version = ReadVersion(bits, packetStartIndex);
            int lengthTypeId = bits.Get(packetStartIndex + 6) ? 1 : 0;

            if (lengthTypeId == 0)
            {
                return ProcessSubpacketsUsingTotalLength(bits, version, packetStartIndex, typeId);
            }
            else
            {
                return ProcessSubpacketsUsingPacketCount(bits, version, packetStartIndex, typeId);
            }
        }

        private Packet ReadLiteralValue(BitArray bits, int packetStartIndex)
        {
            StringBuilder sb = new StringBuilder();
            int lastBitIndex = 0;
            for (int i = 6; i < bits.Length; i += 5)
            {
                bool prefix = bits.Get(packetStartIndex + i);
                sb.Append(bits.Get(i + 1 + packetStartIndex) ? "1" : "0");
                sb.Append(bits.Get(i + 2 + packetStartIndex) ? "1" : "0");
                sb.Append(bits.Get(i + 3 + packetStartIndex) ? "1" : "0");
                sb.Append(bits.Get(i + 4 + packetStartIndex) ? "1" : "0");

                if (!prefix)
                {
                    lastBitIndex = i + 4 + packetStartIndex;
                    break;
                }
            }

            Packet p = new Packet();
            p.Version = ReadVersion(bits, packetStartIndex);
            p.LiteralValue = BinaryToLong(sb.ToString());
            p.EndingIndex = lastBitIndex;
            return p;

        }
        private int BinaryToInt(string input)
        {
            return Convert.ToInt32(input, 2);
        }

        private long BinaryToLong(string input)
        {
            return Convert.ToInt64(input, 2);
        }




        private string ConvertToBinary(string input)
        {

            Dictionary<char, string> mapping = new Dictionary<char, string>();
            mapping['0'] = "0000";
            mapping['1'] = "0001";
            mapping['2'] = "0010";
            mapping['3'] = "0011";
            mapping['4'] = "0100";
            mapping['5'] = "0101";
            mapping['6'] = "0110";
            mapping['7'] = "0111";
            mapping['8'] = "1000";
            mapping['9'] = "1001";
            mapping['A'] = "1010";
            mapping['B'] = "1011";
            mapping['C'] = "1100";
            mapping['D'] = "1101";
            mapping['E'] = "1110";
            mapping['F'] = "1111";

            string result = "";
            for (int j = 0; j < input.Length; j++)
            {
                result += mapping[input[j]];
            }

            return result;
        }
    }
}
