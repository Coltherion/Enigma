namespace Enigma
{
    public class Symbols
    { 
        public char[] List { get; } = new char[63];

        public Symbols()
        {
            int asciiNum = 48;

            for (int i = 0; i < List.Length; i++)
            {
                if (i == 0)
                    this.List[i] = (char)32;
                else
                {
                    this.List[i] = (char)asciiNum;
                    asciiNum++;
                    if (asciiNum == 58) asciiNum = 65;
                    if (asciiNum == 91) asciiNum = 97;
                }
            }
        }

    }
}
