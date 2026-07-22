using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Projeto_integrador2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ObservableCollection<Bebidas> ListaBebidas { get; set; } = new ObservableCollection<Bebidas>();
    }

}
