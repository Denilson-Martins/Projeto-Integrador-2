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
    /// Interação lógica para Lanches.xam
    /// </summary>
    public partial class SelecaoDeLanches : Page
    {
        public SelecaoDeLanches()
        {
            InitializeComponent();
        }

        private void BtnCoxinha(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DescriçãoDeLanche());

        }

        
        private void BtnVoltar(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            ((App)Application.Current).ListaBebidas.Clear();

            
        }
    }
}
