using System;
using System.Collections.Generic;
using System.Text;

namespace Girls.Gama2.Entidades
{
    public class Dinheiro
    {
        public Dinheiro(string cpf,
                     double valor,
                     string descricao,
                     double valor_recebido)
        {
            Cpf = cpf;
            Valor = valor;
            Descricao = descricao;
            ValorRecebido = valor_recebido;

           
        }
        public string Cpf { get; set; }
        public double Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        public string Descricao { get; set; }
        public double ValorRecebido { get; set; }

        public void Pagar()
        {
            DataPagamento = DateTime.Now;
            if (ValorRecebido > Valor)
            {
                var troco = ValorRecebido - Valor;
                Console.WriteLine($"Retornar troco: {troco}");
                return;
            }


        }
    }
}
