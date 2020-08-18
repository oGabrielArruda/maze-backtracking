using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19170_19196_ED_Lab
{
    class Movimento : IComparable<Movimento>
    {
        private int linhaOrigem, colunaOrigem, linhaDestino, colunaDestino;

        public Movimento(int linhaOrigem, int colunaOrigem, int linhaDestino, int colunaDestino)
        {
            this.LinhaOrigem = linhaOrigem;
            this.ColunaOrigem = colunaOrigem;
            this.LinhaDestino = linhaDestino;
            this.ColunaDestino = colunaDestino;
        }

        public int LinhaOrigem { get => linhaOrigem; set => linhaOrigem = value; }
        public int ColunaOrigem { get => colunaOrigem; set => colunaOrigem = value; }
        public int LinhaDestino { get => linhaDestino; set => linhaDestino = value; }
        public int ColunaDestino { get => colunaDestino; set => colunaDestino = value; }

        public int CompareTo(Movimento other)
        {
            throw new NotImplementedException();
        }
    }
}
