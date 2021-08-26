using System;
using System.Collections.Generic;
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

namespace ClassrookWeek6Exercice1 {

    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private List<Student> main_list;
        public MainWindow() {
            
            InitializeComponent();
            main_list = new List<Student>();
            dumpStudents();

            this.TXTBOX_Name.TextChanged += TXTBOX_TextChanged;
            this.TXTBOX_Surname.TextChanged += TXTBOX_TextChanged;
            this.TXTBOX_Degree.TextChanged += TXTBOX_TextChanged;
        }


        private void dumpStudents() {
            addStudent(new Student {
                Name = "Miquel",
                Surname = "Román",
                Degree = "IT"
            });
            addStudent(new Student {
                Name = "Joan",
                Surname = "Ramis",
                Degree = "IT"
            });
            addStudent(new Student {
                Name = "Pep",
                Surname = "Somera",
                Degree = "Magisterio"
            });

            addStudent(new Student {
                Name = "Toni",
                Surname = "Betzol",
                Degree = "Magisterio"
            });
            addStudent(new Student {
                Name = "Mia",
                Surname = "Bés Tutú",
                Degree = "Magisterio"
            });
            addStudent(new Student {
                Name = "Bartomeu",
                Surname = "Bernassa",
                Degree = "Castellà"
            });
            addStudent(new Student {
                Name = "Laura",
                Surname = "Liena",
                Degree = "Historia del Art"
            });
            addStudent(new Student {
                Name = "Pere",
                Surname = "Roblers",
                Degree = "Historia del Art"
            });
            addStudent(new Student {
                Name = "Tonina",
                Surname = "Germanor",
                Degree = "Castellà"
            });
            addStudent(new Student {
                Name = "Tofol",
                Surname = "Román",
                Degree = "Magisterio"
            });
            addStudent(new Student {
                Name = "Joanet",
                Surname = "Germanor",
                Degree = "IT"
            });
        }
        private void addStudent(Student item) {
            this.DATAGRID_Info.Items.Add(item);
            this.main_list.Add(item);
        }

        private void TXTBOX_TextChanged(object sender, TextChangedEventArgs e) {
            refillGrid();
        }

        private void refillGrid() {
            string txt_name = TXTBOX_Name.Text.Trim().ToLower();
            string txt_surname = TXTBOX_Surname.Text.Trim().ToLower();
            string txt_degree = TXTBOX_Degree.Text.Trim().ToLower();

            var filtered_students =
                from student in main_list
                select student;

            if(txt_name.Length > 0) {
                filtered_students =
                    from student in filtered_students
                    where (student.Name.Trim().ToLower().Contains(txt_name))
                    select student;
            }

            if (txt_surname.Length > 0) {
                filtered_students =
                    from student in filtered_students
                    where (student.Surname.Trim().ToLower().Contains(txt_surname))
                    select student;
            }

            if (txt_degree.Length > 0) {
                filtered_students =
                    from student in filtered_students
                    where (student.Degree.Trim().ToLower().Contains(txt_degree))
                    select student;
            }

            this.Dispatcher.Invoke(() => {
                this.DATAGRID_Info.Items.Clear();
                foreach (var item in filtered_students) {
                    this.DATAGRID_Info.Items.Add(item);
                }
            });
        }
    }

    public class Student {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Degree { get; set; }
    }
}
