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

namespace TaskOneDesign
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
          // MouseEnter event handler for the cards
        private void CardMouseEnter(object sender, MouseEventArgs e)
        {
            Border card = sender as Border;
            if (card != null)
            {
                // Change card background color on hover (e.g., light gray)
                card.Background = new SolidColorBrush(Colors.LightGray);
            }
        }

        // MouseLeave event handler for the cards
        private void CardMouseLeave(object sender, MouseEventArgs e)
        {
            Border card = sender as Border;
            if (card != null)
            {
                // Reset card background color when mouse leaves
                card.Background = new SolidColorBrush(Colors.White);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Close the loading screen
            this.Close();

            // Assuming you have a main window named "MainWindowMain"
            ReplacingBooks replacingBooks = new ReplacingBooks();
            replacingBooks.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Feature Not Available Yet!!!","Notice",MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Feature Not Available Yet!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
