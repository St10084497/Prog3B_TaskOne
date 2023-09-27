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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TaskOneDesign
{
    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        private DispatcherTimer timer;
        private double rotationAngle;

        public Splash()
        {
            InitializeComponent();
            InitializeLoadingScreen();
        }

        private void InitializeLoadingScreen()
        {
            rotationAngle = 0;

            // Create a DispatcherTimer to update the spinner animation
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10); // Update every 10 milliseconds
            timer.Tick += Timer_Tick;
            timer.Start();

            // Schedule the CloseLoadingScreen method to run after 5 seconds
            DispatcherTimer closeTimer = new DispatcherTimer();
            closeTimer.Interval = TimeSpan.FromSeconds(5);
            closeTimer.Tick += (sender, e) =>
            {
                closeTimer.Stop(); // Stop the timer
                CloseLoadingScreen(); // Execute the CloseLoadingScreen method
            };
            closeTimer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the rotation angle (adjust the speed by changing the increment)
            rotationAngle += 2; // Adjust the value to control the speed of rotation

            // Apply the rotation to the spinner
            RotateTransform rotateTransform = new RotateTransform(rotationAngle, spinnerPath.ActualWidth / 2, spinnerPath.ActualHeight / 2);
            spinnerPath.RenderTransform = rotateTransform;

            if (rotationAngle >= 360)
            {
                rotationAngle = 0; // Reset the angle when it completes a full rotation
            }
        }

        private void CloseLoadingScreen()
        {
            // Close the loading screen
            this.Hide();

            // Assuming you have a main window named "MainWindowMain"
            MainWindow mainWindowMain = new MainWindow();
            mainWindowMain.Show();
        }
    }
}
