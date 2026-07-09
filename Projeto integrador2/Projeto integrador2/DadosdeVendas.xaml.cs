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
    public partial class DadosdeVendas : Window
    {
        public DadosdeVendas()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CadastrodeProduto(object sender, RoutedEventArgs e)
        {
            ConnectBD conect = new ConnectBD();
            //NavigationService.Navigate(new DadosdeVendas()); 
        }
    }
}
