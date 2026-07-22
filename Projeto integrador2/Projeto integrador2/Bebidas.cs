using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Projeto_integrador2
{
    public class Bebidas : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tamanho { get; set; }
        public double Valor { get; set; }

        private int quantidade;
        public int Quantidade
        {
            get => quantidade;
            set
            {
                if (quantidade != value)
                {
                    quantidade = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Subtotal));
                }
            }
        }

        public double Subtotal => Valor * Quantidade;

        public Bebidas(string nome, string tamanho, double valor, int quantidade)
        {
            Nome = nome;
            Tamanho = tamanho;
            Valor = valor;
            Quantidade = quantidade;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
