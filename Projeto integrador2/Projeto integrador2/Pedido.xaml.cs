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
    /// Interação lógica para Pedido.xam
    /// </summary>
    public partial class Pedido : Page
    {
        public Pedido()
        {
            InitializeComponent();
            DgPedido.ItemsSource = ((App)Application.Current).ListaBebidas;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnRemove(object sender, RoutedEventArgs e)
        {
            int index = DgPedido.SelectedIndex;

            if (index >= 0 && index < ((App)Application.Current).ListaBebidas.Count)
            {
                ((App)Application.Current).ListaBebidas.RemoveAt(index);
                DgPedido.Items.Refresh();
            }
        }

        private void BtnFinalizar(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FinalizarCompra());
        }
    }
}
