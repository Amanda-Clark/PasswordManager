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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordManager
{
    /// <summary>
    /// Interaction logic for ShowPasswords.xaml
    /// </summary>
    public partial class ShowPasswords : UserControl
    {
        public ShowPasswords()
        {
            InitializeComponent();
        }

        private void allpasswords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void allpasswords_CellDoubleClick(object sender, RoutedEventArgs e)
        {
            var cell = sender as DataGridCell;
            
            // Do stuff with your cell
        }
    }
}
