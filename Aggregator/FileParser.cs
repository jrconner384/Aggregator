using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Aggregator
{
    public class FileParser
    {
        private readonly string filename;
        private readonly string criteria;

        public FileParser(string filename, string criteria)
        {
            this.filename = filename;
            this.criteria = criteria;
        }

        public IEnumerable<string> Parse()
        {
            if (File.Exists(filename))
            {
                using (StreamReader reader = File.OpenText(filename))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Split(' ')[0] == criteria)
                        {
                            yield return line;
                        }
                    }
                }
            }
            else
            {
                throw new Exception($"The file {filename} does not exist.");
            }
        }
    }
}
