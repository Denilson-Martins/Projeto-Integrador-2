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

    public partial class FinalizarCompra : Page
    {

        private string formaPagamentoSelecionada = null;

        public FinalizarCompra()
        {
            InitializeComponent();

            DgItens.ItemsSource = ((App)Application.Current).ListaBebidas;
            AtualizarTotal();
        }

        private void AtualizarTotal()
        {
            double total = ((App)Application.Current).ListaBebidas.Sum(b => b.Valor * b.Quantidade);
            TxtTotal.Text = total.ToString("C2");
        }

        private void PagamentoSelecionado(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rb)
            {
                formaPagamentoSelecionada = rb.Content.ToString();
                TxtAvisoPagamento.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnVoltar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnConfirmar(object sender, RoutedEventArgs e)
        {
            var itensDoPedido = ((App)Application.Current).ListaBebidas;

            if (itensDoPedido.Count == 0)
            {
                MessageBox.Show("Não há itens no pedido.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(formaPagamentoSelecionada))
            {
                TxtAvisoPagamento.Visibility = Visibility.Visible;
                MessageBox.Show("Selecione uma forma de pagamento antes de confirmar o pedido.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            double total = itensDoPedido.Sum(b => b.Valor * b.Quantidade);

            ConnectBD conect = new ConnectBD();

            try
            {
                conect.Conectar();

                
                conect.InserirVenda(formaPagamentoSelecionada, total, itensDoPedido);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível registrar a venda no banco de dados.\n\n" + ex.Message,
                                 "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NavigationService.Navigate(new NotaFiscal(
                itensDoPedido,
                TxtTotal.Text,
                formaPagamentoSelecionada));
        }
    }
}
