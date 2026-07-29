using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public partial class Pedido : Page
    {
        private readonly ConnectBD conect = new ConnectBD();

        public Pedido()
        {
            InitializeComponent();
            DgPedido.ItemsSource = ((App)Application.Current).ListaBebidas;
            AtualizarSubtotal();
        }

        private void AtualizarSubtotal()
        {
            double subtotal = ((App)Application.Current).ListaBebidas.Sum(b => b.Valor * b.Quantidade);
            TxtSubtotal.Text = subtotal.ToString("C2");
        }

        private void BtnVoltar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            

        }

        private void BtnRemove(object sender, RoutedEventArgs e)
        {
            if (DgPedido.SelectedItem is Bebidas item)
            {
                ((App)Application.Current).ListaBebidas.Remove(item);
            }
            else
            {
                MessageBox.Show("Selecione um item na lista antes de remover.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            AtualizarSubtotal();
        }

        private void BtnFinalizar(object sender, RoutedEventArgs e)
        {
            var itens = ((App)Application.Current).ListaBebidas;

            if (itens == null || itens.Count == 0)
            {
                MessageBox.Show("Não há nenhum item selecionado. Adicione ao menos um item ao pedido antes de finalizar.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (itens.Any(item => item.Quantidade <= 0))
            {
                MessageBox.Show("Existem itens com quantidade 0. Ajuste a quantidade ou remova o item antes de finalizar o pedido.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var prodDisponivel = true;
            string msg = "Não foi possivel realizar o pedido. Estoque disponivel:\n";
            foreach (var itm in itens)
            {
                var qnt = conect.CheckQuantidade(itm.Nome, itm.Tamanho);
                if (qnt <= itm.Quantidade)
                {
                    prodDisponivel = false;
                    msg += $"{itm.Nome} {itm.Tamanho} = {qnt}" + Environment.NewLine;
                }
            }

            if (!prodDisponivel)
            {
                MessageBox.Show(msg);
                return;
            }

            NavigationService.Navigate(new FinalizarCompra());
        }

        private void BtnEscolherBebida(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Escolher());
        }

        private void BtnAdicionar(object sender, RoutedEventArgs e)
        {
            if (!(DgPedido.SelectedItem is Bebidas item))
            {
                MessageBox.Show("Selecione um item na lista antes de adicionar.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            item.Quantidade++;
            AtualizarSubtotal();
        }

        private void BtnDiminuir(object sender, RoutedEventArgs e)
        {
            if (!(DgPedido.SelectedItem is Bebidas item))
            {
                MessageBox.Show("Selecione um item na lista antes de remover.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (item.Quantidade > 1)
            {
                item.Quantidade--;
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
                    ((App)Application.Current).ListaBebidas.Remove(item);
                }
            }
            AtualizarSubtotal();
        }

        private void DgPedido_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
