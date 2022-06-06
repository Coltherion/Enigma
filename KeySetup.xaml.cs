using System.Windows;


namespace Enigma
{
    /// <summary>
    /// Interaction logic for KeySetup.xaml
    /// </summary>
    public partial class KeySetup : Window
    {
        public KeySetup()
        {
            InitializeComponent();
            keyField.Text = MainWindow.keyHiddenValue;
        }

        private void NewKeySetup(object sender, RoutedEventArgs e)
        {
            MainWindow.GenerateNewKey(MainWindow.symbols);
            keyField.Text = MainWindow.keyValue;
        }

        private void ApplyKeySetup(object sender, RoutedEventArgs e)
        {
            MainWindow.SaveKey();
        }

        private void OkKeySetup(object sender, RoutedEventArgs e)
        {
            MainWindow.SaveKey();
            this.Close();
        }
        
        private void CancelKeySetup(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowHideKey(object sender, RoutedEventArgs e)
        {
            if (showHideKey.IsChecked == true)
                keyField.Text = MainWindow.keyValue;
            else if (showHideKey.IsChecked == false)
                keyField.Text = MainWindow.keyHiddenValue;
        }
    }
}
