using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Projeto_integrador2
{
    /// <summary>
    /// Interação lógica para ControleDeEstoque.xaml
    /// </summary>
    public partial class ControleDeEstoque : Page
    {
        private readonly ConnectBD conect = new ConnectBD();
        private ObservableCollection<Produto> produtos = new ObservableCollection<Produto>();
        private int? produtoSelecionadoId = null; // guarda o Id do produto carregado no formulário para edição

        public ControleDeEstoque()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CarregarProdutos();
        }

        // Busca os produtos no MySQL e alimenta o DataGrid
        private void CarregarProdutos()
        {
            try
            {
                conect.Conectar(); // garante que a conexão com o banco está aberta
                produtos = new ObservableCollection<Produto>(conect.ListarProdutos());
                DgCe.ItemsSource = produtos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível carregar o estoque do banco de dados.\n\n" + ex.Message,
                                 "Erro de conexão", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Ao selecionar uma linha da tabela, carrega os dados do produto no formulário
        // para permitir editar e clicar em "Atualizar Produto"
        private void DgCe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgCe.SelectedItem is Produto produto)
            {
                produtoSelecionadoId = produto.Id;
                TxtNomeProduto.Text = produto.Nome;
                TxtTamanho.Text = produto.Tamanho;
                TxtPreco.Text = produto.Preco.ToString("F2");
                TxtQuantidade.Text = produto.Quantidade.ToString();
            }
        }

        // Disparado quando o usuário termina de editar uma célula do DataGrid.
        // Como só a coluna "Quantidade" é editável, aqui já salvamos direto no MySQL.
        private void DgCe_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction != DataGridEditAction.Commit)
                return;

            // Espera o binding terminar de atualizar o objeto Produto antes de ler o novo valor
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (e.Row.Item is Produto produto)
                {
                    try
                    {
                        conect.AtualizarQuantidade(produto.Id, produto.Quantidade);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Não foi possível atualizar a quantidade no banco.\n\n" + ex.Message,
                                         "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                        CarregarProdutos(); // desfaz a alteração visual, recarregando do banco
                    }
                }
            }));
        }

        // Adiciona um novo produto ao estoque a partir do formulário
        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNomeProduto.Text))
            {
                MessageBox.Show("Informe o nome do produto.");
                return;
            }

            if (!int.TryParse(TxtQuantidade.Text, out int quantidade))
            {
                MessageBox.Show("Quantidade inválida. Digite um número inteiro.");
                return;
            }

            if (!double.TryParse(TxtPreco.Text, out double preco))
            {
                MessageBox.Show("Preço inválido. Use um número, ex: 12.50");
                return;
            }

            try
            {
                conect.Conectar();
                conect.InsertProduto(TxtNomeProduto.Text.Trim(), quantidade, TxtTamanho.Text.Trim(), preco);
                LimparFormulario();
                CarregarProdutos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível adicionar o produto.\n\n" + ex.Message,
                                 "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Salva no MySQL as alterações feitas no formulário para o produto selecionado na grade
        private void BtnAtualizarProduto_Click(object sender, RoutedEventArgs e)
        {
            if (produtoSelecionadoId == null)
            {
                MessageBox.Show("Clique em um produto na tabela para selecioná-lo antes de atualizar.");
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtNomeProduto.Text))
            {
                MessageBox.Show("Informe o nome do produto.");
                return;
            }

            if (!int.TryParse(TxtQuantidade.Text, out int quantidade))
            {
                MessageBox.Show("Quantidade inválida. Digite um número inteiro.");
                return;
            }

            if (!double.TryParse(TxtPreco.Text, out double preco))
            {
                MessageBox.Show("Preço inválido. Use um número, ex: 12.50");
                return;
            }

            try
            {
                conect.Conectar();
                conect.AtualizarProduto(produtoSelecionadoId.Value, TxtNomeProduto.Text.Trim(),
                                         quantidade, TxtTamanho.Text.Trim(), preco);
                LimparFormulario();
                CarregarProdutos();
                MessageBox.Show("Produto atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível atualizar o produto.\n\n" + ex.Message,
                                 "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Remove o produto selecionado no DataGrid
        private void BtnRemover_Click(object sender, RoutedEventArgs e)
        {
            if (DgCe.SelectedItem is Produto produto)
            {
                var resultado = MessageBox.Show($"Remover \"{produto.Nome}\" do estoque?",
                                                 "Confirmar remoção", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        conect.Conectar();
                        conect.ExcluirProduto(produto.Id);
                        CarregarProdutos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Não foi possível remover o produto.\n\n" + ex.Message,
                                         "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um produto na lista para remover.");
            }
        }

        private void BtnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            CarregarProdutos();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new DadosdeVendas());
        }

        private void LimparFormulario()
        {
            TxtNomeProduto.Text = "";
            TxtTamanho.Text = "";
            TxtPreco.Text = "";
            TxtQuantidade.Text = "";
            produtoSelecionadoId = null;
        }
    }
}
