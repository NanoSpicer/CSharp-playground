using System;
using System.Collections.Generic;
using System.Data;
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

namespace ClassroomWeek6Exercice2 {
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public DataSet dataSet { get; set; }
        
        public MainWindow() {

            InitializeComponent();
            DataContext = this;
            
            dataSet = new DataSet();
            dataSet.Tables.Add("Students");
            dataSet.Tables["Students"].Columns.Add("Name");
            dataSet.Tables["Students"].Columns.Add("Surname");
            dataSet.Tables["Students"].Columns.Add("Degree_ID");

            dataSet.Tables.Add("Degrees");
            dataSet.Tables["Degrees"].Columns.Add("Degree_ID");
            dataSet.Tables["Degrees"].Columns.Add("Degree_Name");

            
            dumpTableStudents();
            dumpTableDegrees();

            DegreeColumn.ItemsSource = dataSet.Tables["Degrees"].DefaultView;

        }

        

        private void dumpTableDegrees() {
            addDegree(new Degree {
                Name = "IT",
                Degree_ID = 1
            });
            addDegree(new Degree {
                Name = "Magisteri",
                Degree_ID = 2
            });
            addDegree(new Degree {
                Name = "Historia del Art",
                Degree_ID = 3
            });
            addDegree(new Degree {
                Name = "Castellà",
                Degree_ID = 4
            });
        }

        private void addDegree(Degree item) {
            this.dataSet.Tables["Degrees"].Rows.Add(item.Degree_ID, item.Name);
        }
        private void addStudent(Student item) {
            dataSet.Tables["Students"].Rows.Add(item.Name, item.Surname, item.Degree_ID);
        }
        private void dumpTableStudents() {
            
            addStudent(new Student {
                Name = "Miquel",
                Surname = "Román",
                Degree_ID = 1
            });
            addStudent(new Student {
                Name = "Joan",
                Surname = "Ramis",
                Degree_ID = 1
            });
            addStudent(new Student {
                Name = "Pep",
                Surname = "Somera",
                Degree_ID = 2
            });

            addStudent(new Student {
                Name = "Toni",
                Surname = "Betzol",
                Degree_ID = 2
            });
            addStudent(new Student {
                Name = "Marina",
                Surname = "Llompart",
                Degree_ID = 2
            });
            addStudent(new Student {
                Name = "Bartomeu",
                Surname = "Bernassa",
                Degree_ID = 4
            });
            addStudent(new Student {
                Name = "Laura",
                Surname = "Liena",
                Degree_ID = 3
            });
            addStudent(new Student {
                Name = "Pere",
                Surname = "Roblers",
                Degree_ID = 3
            });
            addStudent(new Student {
                Name = "Tonina",
                Surname = "Germanor",
                Degree_ID = 4
            });
            addStudent(new Student {
                Name = "Tofol",
                Surname = "Román",
                Degree_ID = 2
            });
            addStudent(new Student {
                Name = "Joanet",
                Surname = "Germanor",
                Degree_ID = 1
            });
        }

        

        
    }



    public class Student {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Degree_ID { get; set; }
    }

    public class Degree {
        public string Name { get; set; }
        public int Degree_ID { get; set; }
    }
}
