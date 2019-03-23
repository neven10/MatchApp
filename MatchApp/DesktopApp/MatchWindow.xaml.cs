using DesktopApp.DTO;
using DesktopApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for MatchWindow.xaml
    /// </summary>
    public partial class MatchWindow : Window
    {
        private static HttpClient httpClient;
            
        public ObservableCollection<CurrentMatches> CurrentMatchesCollection { get; set; }
        public ObservableCollection<CurrentMatches> FilteredMatchCollection { get; set; }
        public ObservableCollection<PickedMatches> PickedMatchCollection { get; set; }
        public string SportFilter { get; set; } = "All";

        public string EventIDSenderString { get; set; }
        public string StakeSenderString { get; set; }

        public MatchWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            PickedMatchCollection = new ObservableCollection<PickedMatches>();

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshMatches(TimeSpan.FromSeconds(5), true);
        }



        private async Task RefreshMatches(TimeSpan interval, bool initiate)
        {
            while(initiate)
            {
                await Task.Delay(interval);
                await GetCurrentMatches(SportFilter);  
            }

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

        private async Task GetCurrentMatches(string sportFilter)
        {
            CurrentMatchesCollection = new ObservableCollection<CurrentMatches>();

            var matchDTO = await MatchDTODeserializeAsync();
            currentMatchesListView.ItemsSource = CurrentMatchesCollection;

            List<MatchDTO> filteredMatchDto = new List<MatchDTO>();
            switch (sportFilter)
            {
                case "All":
                    filteredMatchDto = matchDTO;
                    break;
                case "Football":
                    filteredMatchDto = matchDTO.Where(x => x.Sport == "Football").ToList();
                    break;
                case "Hockey":
                    filteredMatchDto = matchDTO.Where(x => x.Sport == "Hockey").ToList();
                    break;
            }
            foreach (MatchDTO m in filteredMatchDto)
            {                           
                    CurrentMatchesCollection.Add
                        (new CurrentMatches
                        {
                            EventID = m.EventID,
                            Sport = m.Sport,
                            HomeTeam = m.HomeTeam,
                            AwayTeam = m.AwayTeam,
                            Score = m.Score,
                            StartTime = m.StartTime,
                            FinishTime = m.FinishTime,
                            CurrentMinutes = m.CurrentMinutes,
                            IsPause = m.IsPause,
                            StakeValueOne = m.StakeValueOne,
                            StakeValueTwo = m.StakeValueTwo,
                            StakeValueX = m.StakeValueX
                            
                      
                            
                        });
                

            }


        }

        private void Btn_AllFilter_Click(object sender, RoutedEventArgs e)
        {
            SportFilter = "All";
        }

        private void Btn_FootballFilter_Click(object sender, RoutedEventArgs e)
        {
            SportFilter = "Football";
        }

        private void Btn_HockeyFilter_Click(object sender, RoutedEventArgs e)
        {
            SportFilter = "Hockey";
        }
      

        private void StakeClick(object sender, RoutedEventArgs e)
        {
           
            pickedMatchesListView.ItemsSource = PickedMatchCollection;
            Button button = sender as Button;
            EventIDSenderString = button.Tag.ToString();
            StakeSenderString = button.Content.ToString();     

            foreach (var cm in CurrentMatchesCollection)
            {
                if (cm.EventID == button.Tag.ToString())
                {
                    PickedMatchCollection.Add(
                new PickedMatches
                {
                    Sport = cm.Sport,
                    HomeTeam = cm.HomeTeam,
                    AwayTeam = cm.AwayTeam,
                    Score = cm.Score,
                    CurrentMinutes = cm.CurrentMinutes,
                    StakeKey = button.Name,
                    StakeValue = button.Content.ToString()
                    
                    

                });
                }
            }
        }
    }
}
