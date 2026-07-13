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
    /// Lógica interna para DadosdeVendas.xaml
    /// </summary>
    public partial class DadosdeVendas : Page
    {
        public DadosdeVendas()
        {
            InitializeComponent();
        }
                
        private void CadastrodeProduto(object sender, RoutedEventArgs e)
        {
            ConnectBD conect = new ConnectBD();
            NavigationService.Navigate(new CadastroDeProdutos()); 
        }

        private void BtnCadProd(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CadastroDeProdutos());
        }

        private void BtnVoltar(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Home());
        }

        private void BtnEstoque(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ControleDeEstoque());
        }

        private void BtnVendas(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ControleDeVendas());
        }
    }
}
