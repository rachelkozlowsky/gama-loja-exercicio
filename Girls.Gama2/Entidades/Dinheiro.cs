using System;
using System.Collections.Generic;
using System.Text;
using Girls.Gama2.Interfaces;

namespace Girls.Gama2.Entidades
{
    public class Dinheiro : Pagamento, IPagamento

    {
        private const double Desconto = 0.05;
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
        var desconto = Valor * Desconto;
        Valor -= desconto;
        var troco = ValorRecebido - Valor;
        DataPagamento = DateTime.Now;
       
    }
    }
}
