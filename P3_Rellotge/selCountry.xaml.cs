﻿using System;
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
    /// Lógica de interacción para selCountry.xaml
    /// </summary>
    public partial class selCountry : Window
    {
        public selCountry(List<DiferenciaHoraria> list)
        {
            InitializeComponent();
            lbList.ItemsSource = list;
            lbList.DisplayMemberPath = "Pais";
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            lbList.Focus();
        }

        public DiferenciaHoraria Country
        {
            get { return ((DiferenciaHoraria)lbList.SelectedItem); }
        }
    }
}
