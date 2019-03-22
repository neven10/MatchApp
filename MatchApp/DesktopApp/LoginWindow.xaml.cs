using DesktopApp;
using DesktopApp.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace LoginWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static HttpClient httpClient;

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
            User user = new User
            {
                Guid = Guid.NewGuid().ToString(),
                UserName = Textbox_NewUserName.Text,
                Email = Textbox_NewEmail.Text,
                Name = Textbox_NewName.Text,
                Surname = Textbox_NewSurname.Text             
            };

             httpClient = new HttpClient();
             var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44315/api/Users/PostUser");
             var json = JsonConvert.SerializeObject(user);
             var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
             request.Content = stringContent;
             var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false); //(HttpMethod.Post, "api/Users/PostUser",user);
             var resonseMessage =  response.EnsureSuccessStatusCode();

        }

        private void LocalLoginButton_Click(object sender, RoutedEventArgs e)
        {
            MatchWindow matchWindow = new MatchWindow();


            matchWindow.Show();
            this.Close();
        }
    }
}
