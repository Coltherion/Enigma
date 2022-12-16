using System;
using System.IO;
using System.Linq;

namespace Enigma
{
    public class Key
    {
        public string Value { get; set; }
        public string hiddenValue { get; set; }
        public char[] List { get; } = new char[63];

        private Symbols symbols;

        private readonly string KeyFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "key.dat");

        public Key(Symbols _symbols)
        {
            symbols = _symbols;
            
            //check if file exists
            if (!File.Exists(KeyFilePath))
            {
                GenerateNewKey();
                SaveKey();
                this.List = this.Value.ToCharArray();
            }
            else
                using (BinaryReader br = new BinaryReader(new FileStream("key.dat", FileMode.Open)))
                {
                    this.Value = br.ReadString();
                    this.List = this.Value.ToCharArray();
                }

            for (int i = 0; i < this.List.Length; i++)
            {
                hiddenValue += (char)42;
            }
        }

        public void GenerateNewKey()
        {
            Random rnd = new Random();
            int[] rndNumSeq = new int[63];
            int rndNum;

            for (int i = 0; i < rndNumSeq.Length; i++)
            {
                if (i == rndNumSeq.Length - 1)
                {
                    rndNum = rnd.Next(0, rndNumSeq.Length - 1);
                    int tempNum = rndNumSeq[rndNum];
                    rndNumSeq[rndNum] = 0;
                    rndNumSeq[i] = tempNum;
                }
                else
                {
                    int maxAttempts = 0;
                    do
                    {
                        rndNum = rnd.Next(0, rndNumSeq.Length - 1);
                        maxAttempts++;
                        if (maxAttempts > rndNumSeq.Length - 1) break;
                    } while (rndNumSeq.Contains(rndNum));
                    rndNumSeq[i] = rndNum;
                }
            }

            int sym = 0;

            foreach (int num in rndNumSeq)
            {
                this.List[num] = symbols.List[sym];
                sym++;
            }

            this.Value = new string(this.List);
        }

        public void SaveKey()
        {
            using (BinaryWriter bw = new BinaryWriter(new FileStream("key.dat", FileMode.Create)))
            {
                bw.Write(this.Value);
            }
        }

        public void ShowKey()
        {
            
        }


    }
}
