using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Aggregator
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Aggregator"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Aggregator;assembly=Aggregator"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:ProgressPanel/>
    ///
    /// </summary>
    public class ProgressPanel : TextWriter
    {
        private readonly RichTextBox textBox;

        public ProgressPanel(RichTextBox textBox)
        {
            textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            textBox.IsReadOnly = true;
            textBox.VerticalContentAlignment = VerticalAlignment.Top;
            textBox.HorizontalContentAlignment = HorizontalAlignment.Left;
            this.textBox = textBox;
        }

        public override void Write(char value)
        {
            textBox.AppendText(value.ToString());
        }

        public override void Write(string value)
        {
            textBox.AppendText(value);
        }

        public override Encoding Encoding => Encoding.ASCII;
    }
}
