using DesktopApp.DTO;
using DesktopApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ObservableCollection<CurrentMatches> currentMatchesObserver;
        private ObservableCollection<PickedMatches> pickedMatchesObserver;

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
            using (var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                var stream = await response.Content.ReadAsStreamAsync();

                if (response.IsSuccessStatusCode)
                {
                    list = DeserializeJsonFromStream<List<MatchDTO>>(stream);
                }

                return list;
            }

        }

        private async Task CurrentMatchObserver()
        {
            currentMatchesObserver = new ObservableCollection<CurrentMatches>();

            var matchDTO = await MatchDTODeserializeAsync();
           // listViewPopularMovies.ItemsSource = observer;

            foreach (MatchDTO m in matchDTO)
            {
                currentMatchesObserver.Add(new CurrentMatches
                {

                });



            }

        }


        private static T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default(T);
            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var jr = new JsonSerializer();
                var searchResult = jr.Deserialize<T>(jtr);
                return searchResult;
            }
        }


    }
}
