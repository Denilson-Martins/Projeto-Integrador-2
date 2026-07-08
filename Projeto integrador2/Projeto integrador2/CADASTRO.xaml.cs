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
    /// Interação lógica para CADASTRO.xam
    /// </summary>
    public partial class CADASTRO : Page
    {
        public  CADASTRO()
        {
            InitializeComponent();
        }

        private void BtnVoltar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
            
       
        private void BtnCadastro(object sender, RoutedEventArgs e)
        {
            if (txbUser.Text == "ADM" && txbSenha.Text == "123456" && txbEmail.Text == "Administrador@gmail.com")
            {

                NavigationService.Navigate(new CadastroDeProdutos());

            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txbEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}
