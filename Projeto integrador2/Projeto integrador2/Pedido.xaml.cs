using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interação lógica para Pedido.xaml
    /// </summary>
    public partial class Pedido : Page
    {
        public Pedido()
        {
            InitializeComponent();
            DgPedido.ItemsSource = ((App)Application.Current).ListaBebidas;
            AtualizarSubtotal();
        }

        private void AtualizarSubtotal()
        {
            // Subtotal correto: soma de (Valor * Quantidade) de cada item
            double subtotal = ((App)Application.Current).ListaBebidas.Sum(b => b.Valor * b.Quantidade);
            TxtSubtotal.Text = subtotal.ToString("C2");
        }

        private void BtnVoltar(object sender, RoutedEventArgs e)
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
                AtualizarSubtotal();
            }
        }

        private void BtnFinalizar(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FinalizarCompra());
        }

        private void BtnEscolherBebida(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Escolher());
        }

        private void BtnAdicionar(object sender, RoutedEventArgs e)
        {
            int index = DgPedido.SelectedIndex;

            if (index < 0 || index >= ((App)Application.Current).ListaBebidas.Count)
            {
                MessageBox.Show("Selecione um item na lista antes de adicionar.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ((App)Application.Current).ListaBebidas[index].Quantidade++;
            DgPedido.Items.Refresh();
            AtualizarSubtotal();
        }

        private void BtnDiminuir(object sender, RoutedEventArgs e)
        {
            int index = DgPedido.SelectedIndex;

            if (index < 0 || index >= ((App)Application.Current).ListaBebidas.Count)
            {
                MessageBox.Show("Selecione um item na lista antes de remover.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var item = ((App)Application.Current).ListaBebidas[index];

            if (item.Quantidade > 1)
            {
                item.Quantidade--;
                DgPedido.Items.Refresh();
                AtualizarSubtotal();
            }
            else
            {
               
                var resultado = MessageBox.Show(
                    $"A quantidade de '{item.Nome}' é 1. Deseja remover o item do pedido?",
                    "Remover item?",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (resultado == MessageBoxResult.Yes)
                {
                    ((App)Application.Current).ListaBebidas.RemoveAt(index);
                    DgPedido.Items.Refresh();
                    AtualizarSubtotal();
                }
            }
        }
    }
}
