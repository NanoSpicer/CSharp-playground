using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using DataModel;
using System.Data;
using System.Dynamic;
using Npgsql;

namespace MainApplicationPractice2 {
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public static LppDB DATABASE = new LppDB();
        private ScoresOfPlayer ChildWindow;

        public MainWindow() {
            InitializeComponent();

            INFO.ItemsSource = (from player in DATABASE.Players
                                join score in DATABASE.Playerscores on player.Id equals score.IdPlayer into PlayerAndScores
                                select new {
                                    ID = player.Id,
                                    Name = player.Name,
                                    TeamName = player.Team.Name,
                                    TotalScore = PlayerAndScores.Sum(p => p.Score)
                                }).ToList();
        }


        private void BTN_SelectPlayer_Click(object sender, RoutedEventArgs e) {
            GoToSelectedItem();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            FilterByTextFields();
        }

        private void FilterByTextFields() {
            string txt_name = TXT_PName.Text.Trim().ToLower();
            string txt_team = TXT_TName.Text.Trim().ToLower();

            var filtered_players =
                from player in DATABASE.Players
                select player;

            if (txt_name.Length > 0) {
                filtered_players =
                    from player in filtered_players
                    where (player.Name.Trim().ToLower().Contains(txt_name))
                    select player;
            }

            if (txt_team.Length > 0) {
                filtered_players =
                    from player in filtered_players
                    where (player.Team.Name.Trim().ToLower().Contains(txt_team))
                    select player;
            }

            INFO.ItemsSource = from player in filtered_players
                               join score in DATABASE.Playerscores on player.Id equals score.IdPlayer into PlayerAndScores
                               select new {
                                   ID = player.Id,
                                   Name = player.Name,
                                   TeamName = player.Team.Name,
                                   TotalScore = PlayerAndScores.Sum(p => p.Score)
                               };
        }

        private void INFO_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            GoToSelectedItem();
        }

        private void GoToSelectedItem() {
            var selected_item = INFO.SelectedItem;
            if (selected_item != null) {
                long id = (long)selected_item.GetType().GetProperty("ID").GetValue(selected_item, null);
                ChildWindow = new ScoresOfPlayer(id);
                ChildWindow.Closed += ChildWindow_Closed;
                ChildWindow.Show();
                this.IsEnabled = false;
            } else {
                SystemSounds.Beep.Play();
            }
        }

        private void ChildWindow_Closed(object sender, EventArgs e) {
            FilterByTextFields();
            /*INFO.ItemsSource = (from player in DATABASE.Players
                                join score in DATABASE.Playerscores on player.Id equals score.IdPlayer into PlayerAndScores
                                select new {
                                    ID = player.Id,
                                    Name = player.Name,
                                    TeamName = player.Team.Name,
                                    TotalScore = PlayerAndScores.Sum(p => p.Score)
                                }).ToList();*/
            this.IsEnabled = true;
        }
    }
}
