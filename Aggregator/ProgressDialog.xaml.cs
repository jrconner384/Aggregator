using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Aggregator
{
    /// <summary>
    /// Interaction logic for ProgressDialog.xaml
    /// </summary>
    public partial class ProgressDialog
    {
        private readonly string pathToTxt;
        private readonly string searchCriteria;
        private readonly string outputFileName;
        private readonly DateTime startTime;
        private readonly List<string> results;

        private int numFiles;

        private const string INFO = "[INFO]";
        private const string WARNING = "[WARNING]";
        private const string ERROR = "[ERROR]";

        private ProgressDialog()
        {
            InitializeComponent();
            Console.SetOut(new ProgressPanel(progressTextBox));
            startTime = DateTime.Now;
            results = new List<string>();
        }

        public ProgressDialog(string path, string criteria, string format)
            : this()
        {
            pathToTxt = path;
            searchCriteria = criteria;
            outputFileName = $"{criteria}-{DateTime.Now}.{format}";
        }

        public void Parse()
        {
            try
            {
                BeginParse();
                WriteSummary();
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
            }
            openFileButton.IsEnabled = true;
            finishButton.IsEnabled = true;
        }

        private void BeginParse()
        {
            List<string> files = Directory.GetFiles(pathToTxt, "*.txt").ToList();
            numFiles = files.Count;
            if (numFiles > 0)
            {
                WriteInfo($"Found {numFiles} .txt files.");
                files.ForEach(ParseFile);
            }
            else
            {
                WriteWarning("No .txt files found.");
            }
        }

        #region Service calls
        private void ParseFile(string filename)
        {
            WriteInfo($"Parsing {filename}.");
            FileParser parser = new FileParser(filename, searchCriteria);
            results.AddRange(parser.Parse());
        }
        #endregion Service calls

        #region Writers
        private void WriteInfo(string info)
        {
            progressTextBox.WriteLineInColor($"{INFO} {info}", Brushes.Black);
        }

        private void WriteWarning(string warning)
        {
            progressTextBox.WriteLineInColor($"{WARNING} {warning}", Brushes.DarkGoldenrod);
        }

        private void WriteError(string error)
        {
            progressTextBox.WriteLineInColor($"{ERROR} {error}", Brushes.Red);
        }

        private void WriteSummary()
        {
            StringBuilder summary = new StringBuilder();
            summary.AppendLine("Summary:");
            summary.AppendLine($"\t{numFiles} .txt files parsed for lines containing \"{searchCriteria}\".");
            summary.AppendLine($"\tFound {results.Count} matches in {numFiles} files.");
            summary.AppendLine($"\tCompleted in {DateTime.Now.Subtract(startTime).TotalSeconds} seconds.");
            progressTextBox.WriteLineInColor(summary.ToString(), Brushes.Green);
        }
        #endregion Writers

        #region Events
        private void finishButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }
        #endregion Events
    } // C:\Users\jconner\Documents\Test txts
}
