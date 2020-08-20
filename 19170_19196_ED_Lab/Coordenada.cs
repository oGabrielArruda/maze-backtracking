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

        public bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (this == obj)
                return true;
            if (this.GetType() != obj.GetType())
                return false;

            Coordenada coord = (Coordenada)obj;

            if (this.linha != coord.linha)
                return false;
            if (this.coluna != coord.coluna)
                return false;
            return true;
        }
    }
}
