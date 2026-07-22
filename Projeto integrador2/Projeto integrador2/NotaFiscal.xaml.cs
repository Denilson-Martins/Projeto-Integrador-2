using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public partial class NotaFiscal : Page
    {
        public NotaFiscal(ObservableCollection<Bebidas> itens, string total, string formaPagamento)
        {
            InitializeComponent();

            DgItens.ItemsSource = itens;

            TxtTotal.Text = total;
            TxtFormaPagamento.Text = formaPagamento;
        }

        private void BtnVoltarInicio(object sender, RoutedEventArgs e)
        {

            ((App)Application.Current).ListaBebidas.Clear();

            NavigationService.Navigate(new Home());
        }
    }
}
