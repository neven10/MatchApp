using DesktopApp;
using DesktopApp.Model;
using MatchApp.Shared;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace LoginWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static HttpClient httpClient;
        public User User { get; set; }

        string Error { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            User = new User();
            DataContext = this.User;


            
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
            if (!Popup_NewUser.IsOpen)
            {
                Popup_NewUser.IsOpen = true;
            }
        }

        private void Btn_CancelPopup_Click(object sender, RoutedEventArgs e)
        {
            if (Popup_NewUser.IsOpen)
            {
                ClearPopUpTextboxes();
                Popup_NewUser.IsOpen = false;
            }
        }

        private void ClearPopUpTextboxes()
        {
            Textbox_NewUserName.Text = "";
            Textbox_NewPassword.Password = "";
            Textbox_NewEmail.Text = "";
            Textbox_NewName.Text = "";
            Textbox_NewSurname.Text = "";
        }

        private async void Btn_RegisterNewUser_Click(object sender, RoutedEventArgs e)
        {

            User.UserName = Textbox_NewUserName.Text;
            User.Password = Textbox_NewPassword.Password;
            User.Email = Textbox_NewEmail.Text;
            User.Name = Textbox_NewName.Text;
            User.Surname = Textbox_NewSurname.Text;
         

            httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44315/api/Users/PostUser");
            var json = JsonConvert.SerializeObject(User);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            request.Content = stringContent;
            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);


            if (Popup_NewUser.IsOpen)
            {
                ClearPopUpTextboxes();
                Popup_NewUser.IsOpen = false;
            }

        }


        private async void LocalLoginButton_Click(object sender, RoutedEventArgs e)
        {
            var user = new UserDTO
            {
                UserName = LocalUserNameTextBox.Text,
                Password = LocalPasswordBox.Password,

            };
            httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(user);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44315/api/Users/Auth")
            {
                Content = stringContent,

            };
            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            LabelErrorLogin.Content = LocalUserNameTextBox.Text +" - "+ response.StatusCode;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MatchWindow matchWindow = new MatchWindow();
                matchWindow.Show();
                this.Close();
            }




        }
    }
}
