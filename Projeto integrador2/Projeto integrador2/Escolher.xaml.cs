using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projeto_integrador2
{
    /// <summary>
    /// Interação lógica para Escolher.xam
    /// </summary>
    public partial class Escolher : Page
    {
        public Escolher()
        {
            InitializeComponent();
        }

        private void BtnBebidas(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SelecaoDeBebidas());
        }

        private void BtnLanches(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Lanches());
        }
    }
}
