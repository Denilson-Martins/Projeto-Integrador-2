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
    /// Interação lógica para Home.xam
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        private void BtnLogAdm(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login());
        }

        private void btnEntrar_CLick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Escolher());
        }

        private void TelaAdm(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DadosdeVendas());
        }
    }
}
