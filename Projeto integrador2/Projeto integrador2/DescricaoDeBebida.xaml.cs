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
    /// Interação lógica para Descrição_do_produto.xam
    /// </summary>
    public partial class Descrição_do_produto : Page
    {
        public Descrição_do_produto()
        {
            InitializeComponent();
        }


        private void btnEntrar_CLick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnCapMl(object sender, RoutedEventArgs e)
        {
            var lista = ((App)Application.Current).ListaBebidas;
            if (sender is Button btn)
            {
                var valor = btn.Content.ToString() == "100ml" ? 6.00 : 12.00;
                lista.Add(new Bebidas(btn.Tag.ToString(), btn.Content.ToString(), valor, 1));
                NavigationService.Navigate(new Pedido());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
