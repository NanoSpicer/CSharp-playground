using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Shapes;

namespace MainApplicationPractice2 {
    /// <summary>
    /// Lógica de interacción para ScoresOfPlayer.xaml
    /// </summary>
    public partial class ScoresOfPlayer : Window {


        private readonly static IEnumerable<long> DAYS = (from score in MainWindow.DATABASE.Playerscores
                                                          select score.Day).Distinct().OrderBy(p => p);

        private long PlayerID { get; }

        private long EditedID;
        private int EditedIndex;
        private bool IsUserEditing = false;

        public ScoresOfPlayer(long PlayerID) {
            this.PlayerID = PlayerID;
            InitializeComponent();

            CallReload();


            DaySelector.ItemsSource = DAYS;
            DaySelector.SelectedIndex = 0;
        }

        private void INFO_BeginningEdit(object sender, DataGridBeginningEditEventArgs e) {

            var selected_item = INFO.SelectedItem;
            if (selected_item != null) {
                EditedIndex = (int)INFO.SelectedIndex;
                EditedID = (long) selected_item.GetType().GetProperty("Id").GetValue(selected_item, null);
                IsUserEditing = true;
            }
        }

        private void INFO_CurrentCellChanged(object sender, EventArgs e) {
            // Connect to a PostgreSQL database
            
            var selected_item = INFO.SelectedItem;//INFO.Items.GetItemAt(EditedIndex);
            if (selected_item != null && IsUserEditing) {
                long NewScore;

                try {
                    NewScore = (long)selected_item.GetType().GetProperty("Score").GetValue(selected_item, null);
                }catch(Exception) { return; }
                

                var command = MainWindow.DATABASE.CreateCommand();
                command.CommandText = "UPDATE Playerscore SET score=" + NewScore + " WHERE id = " + EditedID;
                var affected_rows = command.ExecuteNonQuery();
                //MessageBox.Show("Updated " + affected_rows + " rows -> Value: " + NewScore);
                command.Dispose();
                CallReload();
                IsUserEditing = false;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            var selected_item = INFO.SelectedItem;
            if (selected_item != null) {
                long id;

                try {
                    id = (long)selected_item.GetType().GetProperty("Id").GetValue(selected_item, null);
                } catch (Exception) { return; }
                // Connect to a PostgreSQL database
                var command = MainWindow.DATABASE.CreateCommand();
                command.CommandText = "DELETE FROM Playerscore WHERE id = " + id;
                var affected_rows = command.ExecuteNonQuery();
                command.Dispose();

                CallReload();
                MessageBox.Show("Deleted " + affected_rows + " rows.");
            } else {
                SystemSounds.Beep.Play();
            }
        }


        private void CallReload() {

            List<DataModel.Playerscore> a = (from score in MainWindow.DATABASE.Playerscores
                                             where (score.IdPlayer == PlayerID)
                                             select score).ToList<DataModel.Playerscore>();

            INFO.ItemsSource = a;

        }

        private void InsertButton_Click(object sender, RoutedEventArgs e) {
            
            try {
                int day = int.Parse(DaySelector.Text);
                int new_score = int.Parse(TXT_NewScore.Text);
                // Connect to a PostgreSQL database
                var command = MainWindow.DATABASE.CreateCommand();
                command.CommandText = "INSERT INTO Playerscore (id,id_player,day,score) VALUES(default, " + PlayerID + ", " + day + ", " + new_score + ")";
                var affected_rows = command.ExecuteNonQuery();
                command.Dispose();
                CallReload();
                MessageBox.Show("Inserted " + affected_rows + " rows.");
            } catch (FormatException ex) {
                SystemSounds.Beep.Play();
                MessageBox.Show("The input within the textbox wasn't a number");
            } catch(NpgsqlException pk_violation) {
                SystemSounds.Hand.Play();
                MessageBox.Show(pk_violation.ToString());
            }

        }

        
    }
}
