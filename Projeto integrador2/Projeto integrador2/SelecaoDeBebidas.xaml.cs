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
    /// Interação lógica para SelecaoDeBebidas.xam
    /// </summary>
    public partial class SelecaoDeBebidas : Page
    {
        public SelecaoDeBebidas()
        {
            InitializeComponent();
        }

        private void ButtonCappuccino(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Descrição_do_produto());
        }
                
        private void btnVoltar_CLick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();

        }

       
    }
}