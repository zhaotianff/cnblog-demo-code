using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace FlowDocumentPreviewAndPrint
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : WPFDevelopers.Net45x.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FlowDocument flowDocument = (FlowDocument)Application.LoadComponent(new Uri("FlowDocument1.xaml", UriKind.RelativeOrAbsolute));

                PreviewFlowDocument(flowDocument);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PreviewFlowDocument(FlowDocument flowDocument)
        {
            string temp = System.IO.Path.GetTempFileName();
            if (File.Exists(temp) == true)
                File.Delete(temp);

            XpsDocument xpsDoc = new XpsDocument(temp, FileAccess.ReadWrite);

            XpsDocumentWriter xpsWriter = XpsDocument.CreateXpsDocumentWriter(xpsDoc);

            xpsWriter.Write((flowDocument as IDocumentPaginatorSource).DocumentPaginator);

            docv.Document = xpsDoc.GetFixedDocumentSequence();

            xpsDoc.Close();
        }
    }
}
