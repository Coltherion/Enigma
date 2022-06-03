using System.Windows;


namespace Enigma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Converter Converter { get; set; }
        public Symbols symbols { get; set; }
        public Key key { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            symbols = new Symbols();
            key = new Key(symbols);
            Converter = new Converter(symbols, key);
        }

        private void KeySetupShow(object sender, RoutedEventArgs e)
        {
            KeySetup keySetup = new KeySetup(key);
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
           encipheredText.Text = Converter.Encipher(originalText.Text);
        }

        private void Decipher(object sender, RoutedEventArgs e)
        {
           originalText.Text = Converter.Decipher(encipheredText.Text);
        }
    }
}

