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
using System.IO;
using Microsoft.Win32;

namespace KST_name_sorting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //run all function
            save_toTXT(sorting_name(load_string(load_txt())));
        }

        //open filedialog to load name in .txt file
        public string load_txt()
        {
            OpenFileDialog openfileDialog = new OpenFileDialog();

            openfileDialog.DefaultExt = ".txt";
            openfileDialog.Filter = "Text documents (.txt)|*.txt";

            string filePath = "";

            Nullable<bool> result = openfileDialog.ShowDialog();

            //check if the opendialog open up a file and get the file location
            if(result == true)
            {
                string fileName = openfileDialog.FileName;
                filePath = openfileDialog.InitialDirectory + fileName;
            }

            //return the filepath to be used on next function
            return filePath;
        }

        //load up the string inside txt file that was loaded
        public List<string> load_string(string filePath)
        {
            List<string> names = new List<string>();
            names = File.ReadAllLines(filePath).ToList();

            //print unsorted name to textbox
            unsortedTextBox.Text = String.Join(Environment.NewLine, names);

            //return the unsorted name in List to be sorted in next function
            return names;
        }

        //sorting function
        public List<string> sorting_name(List<string> names)
        {
            string[] fullName = names.ToArray();

            //reverse the name for easy sorting
            for (int i = 0; i < fullName.Length; i++)
            {
                string[] SeparatedName = fullName[i].Split(' ');
                Array.Reverse(SeparatedName);
                fullName[i] = String.Join(" ", SeparatedName);
            }

            Array.Sort(fullName, StringComparer.InvariantCulture);

            //restore the sorted reversed name back to original name
            for (int i = 0; i < fullName.Length; i++)
            {
                string[] SeparatedName = fullName[i].Split(' ');
                Array.Reverse(SeparatedName);
                fullName[i] = String.Join(" ", SeparatedName);
            }

            names = fullName.ToList();

            //print the sorted name into textbox
            sortedTextBox.Text = String.Join(Environment.NewLine, names);

            //return the sorted name in List to be written into .txt file in next function
            return names;
        }

        //write the sorted names into .txt file
        public void save_toTXT(List<string> names)
        {
            System.IO.File.WriteAllLines(System.AppDomain.CurrentDomain.BaseDirectory + "/sorted-names-list.txt", names.ToArray());
            System.Windows.MessageBox.Show("Sorting name finished!");
        }
    }
}
