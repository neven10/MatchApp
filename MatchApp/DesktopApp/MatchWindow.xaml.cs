using DesktopApp.Model;
using MatchApp.Shared;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for MatchWindow.xaml
    /// </summary>
    public partial class MatchWindow : Window
    {
        private static HttpClient httpClient;

        public ObservableCollection<CurrentMatches> CurrentMatchesCollection { get; set; }
        public ObservableCollection<PickedMatches> PickedMatchCollection { get; set; }
        public string SportFilter { get; set; } = "All";

        public string EventIDSenderString { get; set; }

        public MatchWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            PickedMatchCollection = new ObservableCollection<PickedMatches>();
            pickedMatchesListView.ItemsSource = PickedMatchCollection;


        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

            await RefreshMatches(TimeSpan.FromSeconds(1), true);

        }


        private async Task RefreshMatches(TimeSpan interval, bool initiate)
        {

            while (initiate)
            {
                await Task.Delay(interval);
                await GetCurrentMatches(SportFilter);
                await PostPickedMatches();
                await Task.Run(() => SyncSelectedMatches());
                RefreshLabel.Content = "Refreshing...";
                await Task.Delay(interval);
                pickedMatchesListView.Items.Refresh();
                RefreshLabel.Content = "Refreshed";




            }

        }

        private void SyncSelectedMatches()
        {

            foreach (var currentMatches in CurrentMatchesCollection)
            {
                foreach (var pickedMatches in PickedMatchCollection)
                {
                    if (currentMatches.EventID == pickedMatches.EventID)
                    {
                        pickedMatches.CurrentMinutes = currentMatches.CurrentMinutes;
                        pickedMatches.StakeValueOne = currentMatches.StakeValueOne;
                        pickedMatches.StakeValueX = currentMatches.StakeValueX;
                        pickedMatches.StakeValueTwo = currentMatches.StakeValueTwo;
                    }
                }
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

            Button button = sender as Button;
            EventIDSenderString = button.Tag.ToString();
            foreach (var cm in CurrentMatchesCollection)
            {
                if (cm.EventID == button.Tag.ToString())
                {
                    PickedMatchCollection.Add(
                new PickedMatches
                {
                    EventID = cm.EventID,
                    Sport = cm.Sport,
                    HomeTeam = cm.HomeTeam,
                    AwayTeam = cm.AwayTeam,
                    Score = cm.Score,
                    StartTime = cm.StartTime,
                    FinishTime = cm.FinishTime,
                    CurrentMinutes = cm.CurrentMinutes,
                    StakeValueOne = cm.StakeValueOne,
                    StakeValueX = cm.StakeValueX,
                    StakeValueTwo = cm.StakeValueTwo,
                    IsPause = cm.IsPause,
                    IsBlocked = false
                });
                }
            }
        }


        public async Task PostPickedMatches()
        {
            httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44315/api/Match/PostSelectedMatches");
            List<PickedMatches> list = new List<PickedMatches>();
            list = PickedMatchCollection.ToList();
            var json = JsonConvert.SerializeObject(list);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            request.Content = stringContent;
            var response = await httpClient.SendAsync(request);
        }


        private void TestButton_Click(object sender, RoutedEventArgs e)
        {

            foreach (var m in PickedMatchCollection)
            {
                Debug.WriteLine("{0},{1},{2},{3}", m.EventID, m.StakeValueOne, m.SelectedStakeValueOne, m.IsBlocked);
                Debug.WriteLine("{0},{1},{2},{3}", m.EventID, m.StakeValueTwo, m.SelectedStakeValueTwo, m.IsBlocked);
                Debug.WriteLine("{0},{1},{2},{3}", m.EventID, m.StakeValueX, m.SelectedStakeValueX, m.IsBlocked);
                Debug.WriteLine("{0}", m.Status);

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Document document = CreateDocument();
            document.UseCmykColor = true;
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(false)
            {
                Document = document
            };
            pdfRenderer.RenderDocument();
            string filename = @"D:\test\Matches.pdf";
            pdfRenderer.PdfDocument.Save(filename);
            Process.Start(filename);


        }

        private Document CreateDocument()
        {
            Document document = new Document();        
            MigraDoc.DocumentObjectModel.Style style = document.AddStyle("MyBulletList", "Normal");
            style.ParagraphFormat.LeftIndent = "0.5cm";
            style.ParagraphFormat.ListInfo = new ListInfo
            {
                ContinuePreviousList = true,
                ListType = ListType.BulletList1
            };

            var section = document.AddSection();
            foreach(var  s in PickedMatchCollection)
            {
    
                section.AddParagraph(string.Format("{0} - {1} : {2} -- {3}", s.HomeTeam, s.AwayTeam, s.Score, s.Status));
            }

            return document;
        }



    }
}
