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
using System.Windows.Shapes;

namespace P3_Rellotge
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class AddCountry : Window
    {
            public AddCountry(string question, string defaultCountry = "", string defaultDiff = "")
            {
                InitializeComponent();
                lblQuestion.Content = question;
                txtname.Text = defaultCountry;
                txtdiff.Text = defaultDiff;
        }

            private void btnDialogOk_Click(object sender, RoutedEventArgs e)
            {
                this.DialogResult = true;
            }

            private void Window_ContentRendered(object sender, EventArgs e)
            {
                txtname.SelectAll();
                txtname.Focus();
            }

            public string Name
            {
                get { return txtname.Text; }
            }

            public string Diff
            {
                get { return txtdiff.Text; }
            }
    }
}
