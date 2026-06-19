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
    /// Interação lógica para DescriçãoDeLanche.xam
    /// </summary>
    public partial class DescriçãoDeLanche : Page
    {
        public DescriçãoDeLanche()
        {
            InitializeComponent();
        }

        

        private void BtnTamanho(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                var valor = btn.Content.ToString() == "Grande" ? 5.50 : 9.00;
                ((App)Application.Current).ListaBebidas.Add(new Bebidas(btn.Tag.ToString(), btn.Content.ToString(), valor));
                NavigationService.Navigate(new Pedido());
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}
