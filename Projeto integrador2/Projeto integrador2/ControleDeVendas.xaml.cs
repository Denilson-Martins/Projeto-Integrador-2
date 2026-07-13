using System;
using System.Windows;
using System.Windows.Controls;

namespace Projeto_integrador2
{
    /// <summary>
    /// Interação lógica para ControleDeVendas.xaml
    /// </summary>
    public partial class ControleDeVendas : Page
    {
        private readonly ConnectBD conect = new ConnectBD();

        public ControleDeVendas()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CarregarVendas();
        }

       
        private void CarregarVendas()
        {
            try
            {
                conect.Conectar(); 
                DgVendas.ItemsSource = conect.ListarVendas();
                DgItensVenda.ItemsSource = null; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível carregar as vendas do banco de dados.\n\n" + ex.Message,
                                 "Erro de conexão", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
        private void DgVendas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgVendas.SelectedItem is Venda venda)
            {
                try
                {
                    conect.Conectar();
                    DgItensVenda.ItemsSource = conect.ListarItensDaVenda(venda.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível carregar os itens da venda.\n\n" + ex.Message,
                                     "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            CarregarVendas();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new DadosdeVendas());
        }
    }
}
