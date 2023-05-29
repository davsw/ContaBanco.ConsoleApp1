using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaBanco.ConsoleApp1
{
    public class ContaCorrente
    {
        public double saldo;
        public int numero;
        public int limite;
        public bool ehEspecial;
        public Movimentacao[] movimentacoes;

        public bool Sacar(double quantia)
        {
            if (quantia > saldo + limite)
                return false;

            if (quantia < saldo + limite)
            {
                double novoSaldo = saldo - quantia;

                saldo = novoSaldo;

                Movimentacao movimentacao = new Movimentacao();
                movimentacao.valor = quantia;
                movimentacao.tipo = "Débito";
                movimentacao.descricao = "Débito de R$" + quantia + " reais";

                int posicaoVazia = PegaPosicaoVazia();

                movimentacoes[posicaoVazia] = movimentacao;
            }

            return true;
        }


        public void Depositar(double quantia)
        {
            double novoSaldo = saldo + quantia;

            saldo = novoSaldo;

            Movimentacao movimentacao = new Movimentacao();
            movimentacao.valor = quantia;
            movimentacao.tipo = "Crédito";
            movimentacao.descricao = "Crédito de R$" + quantia + " reais";

            int posicaoVazia = PegaPosicaoVazia();

            movimentacoes[posicaoVazia] = movimentacao;
        }


        public void TransferirPara(ContaCorrente contaDestino, double quantia)
        {
            bool conseguiuSacar = this.Sacar(quantia);

            if (conseguiuSacar)
                contaDestino.Depositar(quantia);
        }

        public void ExibirExtrato()
        {
            Console.WriteLine("Numero da conta {0}", numero);

            Console.WriteLine("Movimentações: ");

            foreach (Movimentacao movimentacao in movimentacoes)
            {
                if (movimentacao != null)
                    Console.WriteLine(movimentacao.descricao);
            }

            Console.WriteLine("Saldo atual: {0}", saldo + limite);
        }


        public int PegaPosicaoVazia()
        {
            for (int i = 0; i < movimentacoes.Length; i++)
            {
                if (movimentacoes[i] == null)
                    return i;
            }

            return -1;
        }

    }
}