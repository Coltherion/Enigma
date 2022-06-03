using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Enigma
{
    /// <summary>
    /// Interaction logic for KeySetup.xaml
    /// </summary>
    public partial class KeySetup : Window
    {
        public Key key { get; set; }
        
        public KeySetup(Key _key)
        {
            InitializeComponent();
            key = _key;
            keyField.Text = key.hiddenValue;
        }

        private void NewKeySetup(object sender, RoutedEventArgs e)
        {
            key.GenerateNewKey();
            keyField.Text = key.Value;
        }

        private void ApplyKeySetup(object sender, RoutedEventArgs e)
        {
            key.SaveKey();
        }

        private void OkKeySetup(object sender, RoutedEventArgs e)
        {
            key.SaveKey();
            this.Close();
        }
        
        private void CancelKeySetup(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowHideKey(object sender, RoutedEventArgs e)
        {
            if (showHideKey.IsChecked == true)
                keyField.Text = key.Value;
            else if (showHideKey.IsChecked == false)
                keyField.Text = key.hiddenValue;
        }
    }
}
