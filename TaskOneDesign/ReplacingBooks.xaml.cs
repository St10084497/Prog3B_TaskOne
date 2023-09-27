using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TaskOneDesign
{
    /// <summary>
    /// Interaction logic for ReplacingBooks.xaml
    /// </summary>
    public partial class ReplacingBooks : Window
    {
        private ObservableCollection<DeweyDecimal> listView1Data;
        private ObservableCollection<DeweyDecimal> listView2Data;
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private DispatcherTimer timer;
        private DateTime startTime;

        public ReplacingBooks()
        {
            InitializeComponent();
            InitializeData();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the timer display (optional)
            TimeSpan elapsedTime = DateTime.Now - startTime;
            TimerTextBlock.Text = elapsedTime.ToString(@"mm\:ss");
        }

        private void InitializeData()
        {
            // Initialize data for ListView 1 (you can replace this with your own data)
            listView1Data = new ObservableCollection<DeweyDecimal>();
            
            listView1.ItemsSource = listView1Data;

            // Initialize empty data for ListView 2
            listView2Data = new ObservableCollection<DeweyDecimal>();
            listView2.ItemsSource = listView2Data;
        }

        private void MoveToListView2_Click(object sender, RoutedEventArgs e)
        {
            // Move selected item from ListView 1 to ListView 2
            if (listView1.SelectedItem != null)
            {
                DeweyDecimal selectedDeweyDecimal = listView1.SelectedItem as DeweyDecimal;
                listView1Data.Remove(selectedDeweyDecimal);
                listView2Data.Add(selectedDeweyDecimal);
            }
        }

        private void MoveToListView1_Click(object sender, RoutedEventArgs e)
        {
            // Move selected item from ListView 2 to ListView 1
            if (listView2.SelectedItem != null)
            {
                DeweyDecimal selectedDeweyDecimal = listView2.SelectedItem as DeweyDecimal;
                listView2Data.Remove(selectedDeweyDecimal);
                listView1Data.Add(selectedDeweyDecimal);
            }
        }
       

        private void GenerateDeweyDecimal_Click(object sender, RoutedEventArgs e)
        {
            listView1Data.Clear();
            listView2Data.Clear();
            // Start the timer when the "Generate" button is clicked
            timer.Start();
            startTime = DateTime.Now;
            CompleteButton.IsEnabled = true;
            GenerateDeweyDecimal.Visibility = Visibility.Hidden;


            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                double randomValue = (random.Next(9000, 99999) / 100.0); // Generate a random decimal number between 100.00 and 999.99
                string randomLetters = GenerateRandomLetters(random,alphabet,3); // Generate 3 random letters
                DeweyDecimal newDeweyDecimal = new DeweyDecimal
                {
                    Value = randomValue.ToString("F2") +" "+ randomLetters
                };
                listView1Data.Add(newDeweyDecimal);
            }
        }

        static string GenerateRandomLetters(Random random, string alphabet, int count)
        {
            char[] letters = new char[count];

            for (int i = 0; i < count; i++)
            {
                // Select a random index within the alphabet
                int index = random.Next(0, alphabet.Length);

                // Add the randomly selected letter to the result
                letters[i] = alphabet[index];
            }

            return new string(letters);
        }


        private void CheckOrder_Click(object sender, RoutedEventArgs e)
        {
            // Check the order of items in ListView 2
            bool isOrdered = IsListViewOrdered(listView2Data);
            if (listView2.Items.Count == 10)
            {
                // The ListBox listView2 has exactly 10 items.
                // You can perform any actions you need here.

           
            if (isOrdered)
            {
                MessageBox.Show("Items in ListView 2 are ordered.");
                CompleteButton.Visibility = Visibility.Visible;
                
               

            }
            else
            {
                MessageBox.Show("Items in ListView 2 are not ordered.");
                    CompleteButton.Visibility = Visibility.Visible;
                }
            }
            
        }

        private bool IsListViewOrdered(ObservableCollection<DeweyDecimal> listViewData)
        {
            for (int i = 1; i < listViewData.Count; i++)
            {
                string currentValue = listViewData[i].Value;
                string previousValue = listViewData[i - 1].Value;

                // Extract the numeric part from the values
                double currentNumericPart = double.Parse(Regex.Match(currentValue, @"[\d.]+").Value);
                double previousNumericPart = double.Parse(Regex.Match(previousValue, @"[\d.]+").Value);

                if (currentNumericPart < previousNumericPart)
                {
                    return false; // The numeric part is not sorted in ascending order
                }

                // If the numeric parts are equal, compare the full values to consider the letters
                if (currentNumericPart == previousNumericPart && string.Compare(currentValue, previousValue) < 0)
                {
                    return false; // The full values are not sorted in ascending order
                }
            }
            return true;
        }
        private string CheckPlacement(TimeSpan elapsedTime)
        {
            bool isOrdered = IsListViewOrdered(listView2Data);
            TimeSpan firstPlace = TimeSpan.FromSeconds(50);    // Adjust the time thresholds as needed
            TimeSpan secondPlace = TimeSpan.FromSeconds(55);
            TimeSpan thirdPlace = TimeSpan.FromSeconds(60);
            if (isOrdered)
            {       

                            
            if (elapsedTime <= firstPlace)
            {
                return "1st Place";
            }
            else if (elapsedTime <= secondPlace)
            {
                return "2nd Place";
            }
            else if (elapsedTime <= thirdPlace)
            {
                return "3rd Place";
            }
            else
            {
                return "Never made placement";
            }
            }
            else
            {
                return("Task Incomplete");
                
            }
        }

        private void CompleteButton_Click_1(object sender, RoutedEventArgs e)
        {
            // Stop the timer when the "Complete" button is clicked
            timer.Stop();

            // Calculate the elapsed time
            TimeSpan elapsedTime = DateTime.Now - startTime;

            // Check placement based on the elapsed time
            string placement = CheckPlacement(elapsedTime);

            MessageBox.Show(placement, "Congratulations");
            TimerTextBlock.Text = "  ";
            GenerateDeweyDecimal.Visibility = Visibility.Visible;
            listView1Data.Clear();
            listView2Data.Clear();

        }
    }

    public class DeweyDecimal
    {
        public string Value { get; set; }
    }
}

