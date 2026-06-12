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


        private void btnVoltar_CLick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();

        }

        private void BtnCapMl(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                var valor = btn.Content.ToString() == "100ml" ? 6f : 12f;
                ((App)Application.Current).ListaBebidas.Add(new Bebidas(btn.Tag.ToString(), btn.Content.ToString(), valor));
                NavigationService.Navigate(new Pedido());
            }
        }


    }
}
