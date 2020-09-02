using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19170_19196_ED_Lab
{
    class Labirinto
    {
        private char[ , ] matriz;
        private int[,] matrizMovimentos;
        private List<PilhaLista<Coordenada>> caminhosPossiveis = new List<PilhaLista<Coordenada>>();
        private int linhaAtual = 1, colunaAtual = 1;

        public List<PilhaLista<Coordenada>> CaminhosPossiveis 
        {
            get => caminhosPossiveis.Select(i => (PilhaLista<Coordenada>)i.Clone()).ToList(); 
            set => caminhosPossiveis = value; 
        }
        public char[,] Matriz { get => matriz; set => matriz = value; }


        /**
         * Construtor responsável por ler o arquivo e definir a matriz do labirinto
         * @nomeArquivo é a string do caminho do arquivo txt
         */
        public Labirinto(string nomeArquivo)
        {
            inicializarMatrizMovimentos();
            lerArquivo(nomeArquivo);                
        }


        /**
         * Função que inicializa a matriz de movimentos utilizada.
         * Cada linha dessa matriz contém um par de valores, que representam o incremento
         * responsável pelos movimentos realizados no labirinto. (cima, baixo, lado etc)
         */
        private void inicializarMatrizMovimentos()
        {
            int[] movimentoLinha = { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] movimentoColuna = { 0, 1, 1, 1, 0, -1, -1, -1 };

            matrizMovimentos = new int[8, 2];
            for(int i = 0; i < movimentoLinha.Length; i++)
            {
                matrizMovimentos[i, 0] = movimentoLinha[i];
                matrizMovimentos[i, 1] = movimentoColuna[i];
            }
        }


        /**
         * Função responsável por ler um arquivo texto de labirinto e definir a matriz
         * @nomeArquivo a string que representa o caminho do arquivo txt
         */
        private void lerArquivo(string nomeArquivo)
        {
            StreamReader sr = new StreamReader(nomeArquivo);
            int colunas = int.Parse(sr.ReadLine());
            int linhas = int.Parse(sr.ReadLine());

            matriz = new char[linhas, colunas];

            for (int i = 0; i < linhas; i++)
            {
                string linhaArquivo = sr.ReadLine();
                for (int j = 0; j < colunas; j++)
                {

                    matriz[i, j] = linhaArquivo[j];
                }
            }
        }


        /**
         * Função que busca os caminhos no labirinto, exibindo cada passo feito.
         * @dgv dataGridView em que os passos serão exibidos
         */
        public void buscarCaminhos(DataGridView dgv)
        {
            PilhaLista<Coordenada> pilhaMovimentos = new PilhaLista<Coordenada>();
            Coordenada coordInicial = new Coordenada(1, 1);
            pilhaMovimentos.Empilhar(coordInicial);
            exibirMovimento(dgv, coordInicial, true);

            bool temCaminhoPossivel = true;

            while(temCaminhoPossivel)
            {
                bool seMoveu = false;
                tentarMover(pilhaMovimentos, ref seMoveu, dgv);

                if (!seMoveu)
                    tentarVoltar(ref temCaminhoPossivel, pilhaMovimentos, dgv);
            }
        }


        /**
         * Função que tenta realizar um movimento.
         * Ele inicia a tentativa de movimentos a partir do número marcado na posição da matriz.
         * Caso esteja vazia, ou seja, não se passou por aquela posição, as tentativas na matriz de movimentos se iniciam na linha 0. 
         * Caso já se tenha passado pela posição, ou seja, tenha voltado pra ela
         * inicia-se a tentativa a partir do número marcado, para tentar outros caminhos.
         * Caso tenha outros movimento possível a partir da posição, a variável seMoveu, passada por referência, é setada como true
         * Caso encontre a saída, ela chama a função que salva o caminho e continua o backtracking normalmente
         * @pilhaMovimentos pilha contendo os movimentos feitos
         * @seMoveu variável que controla o loop e indica o movimento
         * @dgv dataGridView para se exibir o movimento
         */
        private void tentarMover(PilhaLista<Coordenada> pilhaMovimentos, ref bool seMoveu, DataGridView dgv)
        {
            int start = 0;
            char c = matriz[linhaAtual, colunaAtual];
            if(c != ' ' && c != 'I')
            {
                string conversao = c + "";
                start = int.Parse(conversao);
            }
            

            for (int i = start+1; i < matrizMovimentos.GetLength(0) && !seMoveu; i++)
            {
                int possivelLinha = linhaAtual + matrizMovimentos[i,0];
                int possivelColuna = colunaAtual + matrizMovimentos[i, 1];

                char valorPosicao = matriz[possivelLinha, possivelColuna];
                if (estaVazio(valorPosicao))
                    mover(new Coordenada(possivelLinha, possivelColuna, i), dgv, ref seMoveu, pilhaMovimentos);
                else if (ehSaida(valorPosicao))
                    salvarCaminho(pilhaMovimentos, new Coordenada(possivelLinha, possivelColuna, i), dgv);
            }
        }

        /** Função que tenta realizar a volta (o backtracking de fato)
         * Caso ao desemplihar a posição atual a pilha fique vazia, setamos a variável temCaminhoPossivel, passada por referência, como false
         * @temCaminhoPossivel variavel que indica se a pilha ficou vazia (nao tem mais caminhos)
         * @pilhaMovimentos pilha com todos os movimentos realizados
         * @dgv dataGridView para exibir o movimento
         */
        private void tentarVoltar(ref bool temCaminhoPossivel, PilhaLista<Coordenada> pilhaMovimentos, DataGridView dgv) 
        {
            Coordenada coordAtual = pilhaMovimentos.Desempilhar();
            matriz[coordAtual.Linha, coordAtual.Coluna] = ' ';
            exibirMovimento(dgv, coordAtual, false);

            if (pilhaMovimentos.EstaVazia)
                temCaminhoPossivel = false;
            else
            {
                Coordenada coordAntiga = pilhaMovimentos.OTopo();
                linhaAtual = coordAntiga.Linha;
                colunaAtual = coordAntiga.Coluna;
            }
        }

        /**
         * Função responsável pelo movimento de fato, alterando as variáveis globais linhaAtual e colunaAtual.
         * Antes de se mover, marca-se a posição atual com o char indicando a direção do movimento.
         * @proxCoord coordenada para qual iremos nos mover.
         * @dgv dataGridView em que o movimento será exibido.
         * @seMoveu variável para indicar o movimento
         * @pilhaMovimentos pilha com todos os movimentos realizados.         
         */
        private void mover(Coordenada proxCoord, DataGridView dgv, ref bool seMoveu, PilhaLista<Coordenada> pilhaMovimentos)
        {
            
            matriz[linhaAtual, colunaAtual] = (proxCoord.Direcao + "")[0];
            linhaAtual = proxCoord.Linha;
            colunaAtual = proxCoord.Coluna;
            pilhaMovimentos.Empilhar(proxCoord);
            seMoveu = true;
            exibirMovimento(dgv, proxCoord, true);
        }

        /**
         * Função responsável por salvar o caminho feito (clonado) em uma lista
         * @pilhaMovimentos pilha contendo o caminho a ser salvo
         * @coord final a coordenada da posição 'S' (ultima posicao)
         * @dgv dataGridView a se exibir o movimento
         */
        private void salvarCaminho(PilhaLista<Coordenada> pilhaMovimentos, Coordenada coordFinal, DataGridView dgv)
        {
            pilhaMovimentos.Empilhar(coordFinal);
            caminhosPossiveis.Add(pilhaMovimentos.Clone());
            exibirMovimento(dgv, coordFinal, true);
            exibirMovimento(dgv, coordFinal, false);
            pilhaMovimentos.Desempilhar();
        }

        /**
         * Função responsável por exibir um movimento feito no dataGridView.
         * Caso seja um movimento de avanço, marcamos a cor verde
         * Caso for um movimento de retrocesso, marcamos a cor branca (cor original)
         * @dgv dataGridView em que o movimento será exibido.
         * @coord instância da classe coordenada com os valores do movimento a ser exibido
         * @avanco variavel para controlar a cor da exibicao
         */
        private void exibirMovimento(DataGridView dgv, Coordenada coord, bool avanco)
        {
            if (avanco)
                dgv.Rows[coord.Linha].Cells[coord.Coluna].Style.BackColor = Color.Green;
            else
                dgv.Rows[coord.Linha].Cells[coord.Coluna].Style.BackColor = Color.White;
            Thread.Sleep(100);
            Application.DoEvents();
        }

        private bool estaVazio(char c) => c == ' ';
        private bool ehSaida(char c) => c == 'S';
       
    }
}
