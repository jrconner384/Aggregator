using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using Microsoft.Isam.Esent.Collections.Generic;

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
        private readonly string resultsDirectory;
        private DateTime endTime;

        private int numFiles;

        private const string INFO = "[INFO]";
        private const string WARNING = "[WARNING]";
        private const string ERROR = "[ERROR]";

        private ProgressDialog()
        {
            InitializeComponent();
            Console.SetOut(new ProgressPanel(progressTextBox));
            resultsDirectory = $"Aggregator-{DateTime.Now.ToLongTimeString()}";
            startTime = DateTime.Now;
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
            using (var results = new PersistentDictionary<string, string>(resultsDirectory))
            {
                foreach (string s in parser.Parse())
                {
                    results.Add(resultsDirectory, s);
                }
            }
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
            int count;
            using (var results = new PersistentDictionary<string, string>(resultsDirectory))
            {
                count = results.Values.Count;
            }
            endTime = DateTime.Now;
            StringBuilder summary = new StringBuilder($"Summary: {numFiles} .txt files parsed for lines containing \"{searchCriteria}\".");
            summary.AppendLine($"Found {count} matches in {numFiles} files.");
            summary.AppendLine($"Completed in {endTime.Subtract(startTime):s} seconds.");
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
