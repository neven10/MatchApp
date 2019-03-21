using DesktopApp.DTO;
using DesktopApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for MatchWindow.xaml
    /// </summary>
    public partial class MatchWindow : Window
    {
        private static HttpClient httpClient;

        public ObservableCollection<CurrentMatches> CurrentMatchesCollection { get; set; }
        public ObservableCollection<PickedMatches> pickedMatchesObserver;

        public MatchWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
         
        }




        private async Task<List<MatchDTO>> MatchDTODeserializeAsync()
        {

            var list = new List<MatchDTO>();
            httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44315/api/Match/All");
            var response = await httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<MatchDTO>>(content);        
            
            
        }

        private async Task GetCurrentMatches()
        {
            CurrentMatchesCollection = new ObservableCollection<CurrentMatches>();

            var matchDTO = await MatchDTODeserializeAsync();
            currentMatchesListView.ItemsSource = CurrentMatchesCollection;

            foreach (MatchDTO m in matchDTO)
            {
                CurrentMatchesCollection.Add(new CurrentMatches { EventID = m.EventID});
        

            }

        }
      

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await GetCurrentMatches();
        }
    }
}
