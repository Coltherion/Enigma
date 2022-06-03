using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    public class Converter
    {
        private Key key;
        private Symbols symbols;

        public Converter(Symbols _symbols, Key _key)
        {
            key = _key;
            symbols = _symbols;    
        }

        public string Encipher(string text)
        {
            char[] chars = text.ToCharArray();
            
            for (int i = 0; i < chars.Length ; i++)
            {
                int pos = Array.IndexOf(key.List, chars[i]);
                chars[i] = symbols.List[pos];
            } 

            return new string(chars);
        }

        public string Decipher(string text)
        {
            char[] chars = text.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                int pos = Array.IndexOf(symbols.List, chars[i]);
                chars[i] = key.List[pos];
            }

            return new string(chars);
        }
    }
}
