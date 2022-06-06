using System;
using System.IO;
using System.Linq;
using System.Windows;


namespace Enigma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string KeyFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "key.dat");
        public static char[] symbols = new char[63];
        public static char[] key = new char[63];
        public static string keyValue;
        public static string keyHiddenValue;

        public MainWindow()
        {
            InitializeComponent();
            GenerateSymbols();
            GenerateKey(symbols);
        }

        private void GenerateSymbols()
        {
            int asciiNum = 48;

            for (int i = 0; i < symbols.Length; i++)
            {
                if (i == 0)
                    symbols[i] = (char)32;
                else
                {
                    symbols[i] = (char)asciiNum;
                    asciiNum++;
                    if (asciiNum == 58) asciiNum = 65;
                    if (asciiNum == 91) asciiNum = 97;
                }
            }
        }

        private void GenerateKey(char[] symbols)
        {
            if (!File.Exists(KeyFilePath))
            {
                GenerateNewKey(symbols);
                SaveKey();
            }
            else
                using (BinaryReader br = new BinaryReader(new FileStream("key.dat", FileMode.Open)))
                {
                    keyValue = br.ReadString();
                    key = keyValue.ToCharArray();
                }

            for (int i = 0; i < key.Length; i++)
            {
                keyHiddenValue += (char)42;
            }
        }

        public static void GenerateNewKey(char[] symbols)
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
                key[num] = symbols[sym];
                sym++;
            }

            keyValue = new string(key);
        }

        public static void SaveKey()
        {
            using (BinaryWriter bw = new BinaryWriter(new FileStream("key.dat", FileMode.Create)))
            {
                bw.Write(keyValue);
            }
        }

        private string Encipher(string text)
        {
            char[] chars = text.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                int pos = Array.IndexOf(key, chars[i]);
                chars[i] = symbols[pos];
            }

            return new string(chars);
        }

        private string Decipher(string text)
        {
            char[] chars = text.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                int pos = Array.IndexOf(symbols, chars[i]);
                chars[i] = key[pos];
            }

            return new string(chars);
        }

        private void KeySetupShow(object sender, RoutedEventArgs e)
        {
            KeySetup keySetup = new KeySetup();
            keySetup.ShowDialog();
        }

        void EraseText(object sender, RoutedEventArgs e)
        {
            originalText.Clear();
            encipheredText.Clear();
        }

        void ExitApp(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CopyOriginalText(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(originalText.Text);  
        }

        private void CopyEncipheredText(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(encipheredText.Text);
        }

        private void EraseOriginalText(object sender, RoutedEventArgs e)
        {
            originalText.Clear();
        }

        private void EraseEncipheredText(object sender, RoutedEventArgs e)
        {
            encipheredText.Clear();
        }

        private void Encipher(object sender, RoutedEventArgs e)
        {
            encipheredText.Text = Encipher(originalText.Text);
        }

        private void Decipher(object sender, RoutedEventArgs e)
        {
            originalText.Text = Decipher(encipheredText.Text);
        }
    }
}

