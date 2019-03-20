using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace LoginWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
   
            AzureLoginGrid.Visibility = Visibility.Collapsed;
            SplitGrid.Visibility = Visibility.Collapsed;
            if (LocalLoginGrid.Visibility == Visibility.Visible)
            {
                LocalUserNameTextBox.Focus();
            }

        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
           
        }

        private void BtnActionMinimize_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnActionSystemInformation_OnClick(object sender, RoutedEventArgs e)
        {
            var systemInformationWindow = new SystemInformationWindow();
            systemInformationWindow.Show();
        }

        private void btnActionClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Thumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Left = Left + e.HorizontalChange;
            Top = Top + e.VerticalChange;
        }

        private void Btn_OpenNewUser_Click(object sender, RoutedEventArgs e)
        {
            if(!Popup_NewUser.IsOpen)
            {
                Popup_NewUser.IsOpen = true;
            }
        }

        private void Btn_CancelPopup_Click(object sender, RoutedEventArgs e)
        {
            if (Popup_NewUser.IsOpen)
            {
                Popup_NewUser.IsOpen = false;
            }
        }
    }
}
