using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

namespace ClassroomWeek5WPF {
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public BackgroundWorker bgWorker = new BackgroundWorker();

        public MainWindow() {
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork += WorkerWorkHandler;
            bgWorker.ProgressChanged += WorkerProgressUpdate;
            InitializeComponent();
        }

        private void WorkerProgressUpdate(object sender, ProgressChangedEventArgs e) {
            this.LBL_Progress.Text = e.ProgressPercentage + "%";
        }

        private void WorkerWorkHandler(object sender, DoWorkEventArgs e) {
            int number = (int) e.Argument;
            int aux = 1;
            while (aux <= number && !bgWorker.CancellationPending) {
                bgWorker.ReportProgress(aux*100/number);
                // This is done to get around the exception thrown because of the ownership of the object LISTVIEW.
                if (isPrime(aux)) this.Dispatcher.Invoke(() => {
                    this.LISTVIEW_PrimeContainer.Items.Add(aux);
                });  
                aux++;
            }
        }
        

        private void BTN_Compute_Click(object sender, RoutedEventArgs e) {

            this.LISTVIEW_PrimeContainer.Items.Clear();
            string content = this.TXTBOX_Number.Text;
            
            /* PART 1
            try {
                int number = Int32.Parse(content);
                addPrimeNumbersToList(number);
            } catch(FormatException ex) {
                Console.WriteLine(ex);
                MessageBox.Show("The input should be a number");
            }*/

            try {
                int number = Int32.Parse(content);
                bgWorker.RunWorkerAsync(number);
            } catch (FormatException ex) {
                this.TXTBOX_Number.Text = "";
                MessageBox.Show("The input should be a number");
            }
        }

        private void BTN_Stop_Click(object sender, RoutedEventArgs e) {
            bgWorker.CancelAsync();
        }

        private void addPrimeNumbersToList(int number) {

            int aux = 1;
            while (aux <= number) {
                if (isPrime(aux)) this.LISTVIEW_PrimeContainer.Items.Add(aux);
                aux++;
            }
        }

        private bool isPrime(int number) {
            if (number == 1) return true;

            int count = number - 1;
            while (count > 1) {
                if (number % count == 0) return false;
                count--;
            }

            return true;
        }
    }
}
