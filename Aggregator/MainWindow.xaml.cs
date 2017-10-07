using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace Aggregator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void criteriaTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (criteriaTextBox.Text == "Enter search criteria")
            {
                criteriaTextBox.Text = "";
            }
        }

        private void directoryTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (directoryTextBox.Text == "Path to TXT files")
            {
                directoryTextBox.Text = "";
            }
        }

        private void directoryButton_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.ShowDialog();
                directoryTextBox.Text = dialog.SelectedPath;
            }
        }

        private void goButton_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(directoryTextBox.Text))
            {
                ProgressDialog dialog = new ProgressDialog(directoryTextBox.Text, criteriaTextBox.Text, GetOutputFormat());
                dialog.Show();
                dialog.Parse();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(@"Please select a valid directory.", @"Directory not found.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private string GetOutputFormat()
        {
            string format = "";
            if (csvRadioButton.IsChecked != null && csvRadioButton.IsChecked.Value)
            {
                format = "csv";
            }
            else if (txtRadioButton.IsChecked != null && txtRadioButton.IsChecked.Value)
            {
                format = "txt";
            }
            else if (xlsRadioButton.IsChecked != null && xlsRadioButton.IsChecked.Value)
            {
                format = "xls";
            }
            return format;
        }
    }
}
