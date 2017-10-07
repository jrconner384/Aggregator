using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Aggregator
{
    public static class RichTextBoxExtensions
    {
        public static void WriteInColor(this RichTextBox box, string text, SolidColorBrush color)
        {
            TextRange range = new TextRange(box.Document.ContentEnd, box.Document.ContentEnd)
            {
                Text = $"{text}"
            };
            range.ApplyPropertyValue(TextElement.ForegroundProperty, color);
        }

        public static void WriteLineInColor(this RichTextBox box, string text, SolidColorBrush color)
        {
            TextRange range = new TextRange(box.Document.ContentEnd, box.Document.ContentEnd)
            {
                Text = $"\n{text}"
            };
            range.ApplyPropertyValue(TextElement.ForegroundProperty, color);
        }
    }
}
