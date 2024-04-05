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
using Newtonsoft.Json;

namespace Praktos2
{
    public partial class MainWindow : Window
    {
        private Dictionary<DateTime, List<ToDo>> ToDoNote;
        public MainWindow()
        {
            InitializeComponent();
            ToDoNote = new Dictionary<DateTime, List<ToDo>>();
            DatePicker.SelectedDate = DateTime.Today;
            LoadList();
            UpdateList();

            ToDoList.DisplayMemberPath = "Heading";
            ToDoList.SelectionChanged += ToDoList_Select;
        }

        private void Note_Add(object sender, RoutedEventArgs e)
        {
            DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.Today;

            ToDo NewNote = new(HeadingNote.Text, TextNote.Text, selectedDate);
            if (ToDoNote.ContainsKey(selectedDate))
            {
                ToDoNote[selectedDate].Add(NewNote);
            }
            else
            {
                ToDoNote[selectedDate] = new List<ToDo>() { NewNote };
            }
            HeadingNote.Text = "";
            TextNote.Text = "";
            UpdateList();
            SaveList();
        }

        private void Note_Save(object sender, RoutedEventArgs e)
        {
            ToDo SelectNote = (ToDo)ToDoList.SelectedItem;

            if (SelectNote != null)
            {
                SelectNote.Heading = HeadingNote.Text;
                SelectNote.DescrText = TextNote.Text;
                SelectNote.Date = DatePicker.SelectedDate ?? DateTime.Today;

                UpdateList();
                SaveList();
            }
        }

        private void Note_Delete(object sender, RoutedEventArgs e)
        {
            DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.Today;

            if (ToDoNote.ContainsKey(selectedDate))
            {
                ToDoNote[selectedDate].Remove((ToDo)ToDoList.SelectedItem);

                UpdateList();
                SaveList();
            }
        }

        private void ToDoList_Select(object sender, SelectionChangedEventArgs e)
        {
            if (ToDoList.SelectedItem != null)
            {
                ToDo selectNote = (ToDo)ToDoList.SelectedItem;

                HeadingNote.Text = selectNote.Heading;
                TextNote.Text = selectNote.DescrText;
            }
            else
            {
                HeadingNote.Text = "";
                TextNote.Text = "";
            }
        }

        private void UpdateList()
        {
            DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.Today;

            if (ToDoNote.ContainsKey(selectedDate))
            {
                ToDoList.ItemsSource = ToDoNote[selectedDate];
            }
            else
            {
                ToDoList.ItemsSource = null;
            }
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void SaveList()
        {
            Load.Serialize(ToDoNote, "ToDoList.json");
        }

        private void LoadList()
        {
            ToDoNote = Load.Deserialize("ToDoList.json");
        }
    }
}
