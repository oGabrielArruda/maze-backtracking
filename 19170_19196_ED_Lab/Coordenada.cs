using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19170_19196_ED_Lab
{
    class Coordenada : IComparable<Coordenada>
    {
        private int linha, coluna;

        public Coordenada(int linha, int coluna)
        {
            this.linha = linha;
            this.coluna = coluna;
        }

        public int Linha { get => linha; set => linha = value; }
        public int Coluna { get => coluna; set => coluna = value; }

        public int CompareTo(Coordenada other)
        {
            throw new NotImplementedException();
        }
        
        public override string ToString()
        {
            return "Linha: " + linha + "; Coluna: " + coluna;
        }
    }
}
