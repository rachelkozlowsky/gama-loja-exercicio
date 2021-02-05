using Girls.Gama2.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Girls.Gama2
{
    class Program
    {
        private static List<Boleto> listaBoletos;
        private static List<Dinheiro> listaDinheiros;
        private static List<Geladeira> listaGeladeira;
        private static List<Televisao> listaTelevisao;
//        private static List<Produtos> listaProdutos;
        static void Main(string[] args)
        {
            listaBoletos = new List<Boleto>();
            listaDinheiros = new List<Dinheiro>();
            listaGeladeira = new List<Geladeira>();
            listaTelevisao = new List<Televisao>();
  //          listaProdutos = new List<Produtos>();

            while (true)
            {
                Console.WriteLine("================== Loja das meninas da Gama Academy ============================");
                Console.WriteLine("Selecione uma opção");
                Console.WriteLine("1-Reservar Produto | 2-Consultar Reserva | 3- Finalizar Compra | 4-Pagamento | 5-Relatório");

                var opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        CadastrarProduto();
                        break;
                    case 2:
                        ConsultaCarrinho();
                        break;
                    case 3:
                        Comprar();
                        break;
                    case 4:
                        Pagamento();
                        break;
                    case 5:
                        Relatorio();
                        break;
                    default:
                        break;
                }
            }
        }

        public static void CadastrarProduto()
        {
            Console.WriteLine("-----------  RESERVAR PRODUTO  ------------");
            Console.WriteLine("1-Televisão | 2-Geladeira:");

            var opcao = int.Parse(Console.ReadLine());
            
            Console.WriteLine("\nMarca:");
            var marca = Console.ReadLine();

            Console.WriteLine("\nModelo:");
            var modelo = Console.ReadLine();

            Console.WriteLine("\nPreço:");
            var preco = Double.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    CadTelevisao(marca, modelo, preco);
                    break;
                case 2:
                    CadGeladeira(marca, modelo, preco);
                    break;
                default:
                    break;
            }
            Console.WriteLine("-------------- FIM RESERVAR PRODUTO  ------------");
        }

        public static void CadTelevisao(string marca, string modelo, double preco)
        {
            var televisao = new Televisao(preco, marca, modelo);
            televisao.Promocao();

            Console.WriteLine($"O código para pagamento desse produto foi gerado e o" +
                              $" desconto aplicado. \nPreço: {televisao.Preco} \nCódigo de pagamento: {televisao.Id} ");

            listaTelevisao.Add(televisao);
        }

        public static void CadGeladeira(string marca, string modelo, double preco)
        {
            var geladeira = new Geladeira(preco, marca, modelo);
            geladeira.Promocao();

            Console.WriteLine($"O código para pagamento desse produto foi gerado e o" +
                              $" desconto aplicado. \nPreço: {geladeira.Preco} \nCódigo de pagamento: {geladeira.Id} ");

            listaGeladeira.Add(geladeira);
        }
        
        public static void ConsultaCarrinho()
        {
            Console.WriteLine("---------------------------- PRODUTOS RESERVADOS ----------------------------");
            Console.WriteLine("\n----------------------------      Geladeira      ----------------------------");

            foreach (var item in listaGeladeira)
            {
                Console.WriteLine($"\nMarca: {item.Marca} \nModelo: {item.Modelo} \nPreço: {item.Preco} \nCódigo para pagamento: {item.Id}");

            }
            Console.WriteLine("\n----------------------------      Televisão      ----------------------------");

            foreach (var item in listaTelevisao)
            {
                Console.WriteLine($"\nMarca: {item.Marca} \nModelo: {item.Modelo} \nPreço: {item.Preco} \nCódigo para pagamento: {item.Id}");

            }
            Console.WriteLine("---------------------------- FIM DE PRODUTOS RESERVADOS ----------------------------\n\n");

            Console.WriteLine("1-Finalizar compra | 2-Voltar:");

            var opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Comprar();
                    break;
                case 2:
                    break;
                default:
                    break;
            }


        }

        public static void Comprar()
        {
            
            Console.WriteLine("Digite o código de pagamento do produto:");
            var codigoProduto = Guid.Parse(Console.ReadLine());

            var geladeira = listaGeladeira
                .Where(item => item.Id == codigoProduto)
                .FirstOrDefault();

            var televisao = listaTelevisao
                .Where(item => item.Id == codigoProduto)
                .FirstOrDefault();
         
            var valor = geladeira != null ? geladeira.Preco : televisao.Preco;

            if (geladeira != null)
            {
                Console.WriteLine($"\nMarca: {geladeira.Marca} \nModelo: {geladeira.Modelo} \nPreço com desconto: {geladeira.Preco}");
            }
            else if (televisao != null)
            {
                Console.WriteLine($"\nMarca: {televisao.Marca} \nModelo: {televisao.Modelo} \nPreço: {televisao.Preco}");
            }
            else
            {
                Console.WriteLine("Produto não encontrado");
                return;
            }

            Console.WriteLine("Escolha a forma de pagamento:");

            Console.WriteLine("1-Dinheiro | 2-Boleto");

            var opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    PagamentoDinheiro();
                    break;
                case 2:
                    PagamentoBoleto();
                    break;
                default:
                    break;
            }


            
     
        }

        public static void PagamentoDinheiro()
        {
            Console.WriteLine("Digite o valor da compra:");
            var valor = double.Parse(Console.ReadLine());

            Console.WriteLine("Digite o valor recebido pelo Cliente:");
            var valor_recebido = double.Parse(Console.ReadLine());

            Console.WriteLine("Digite o CPF do cliente:");
            var cpf = Console.ReadLine();

            Console.WriteLine("Preeencha uma descrição caso necessário");
            var descricao = Console.ReadLine();

            var dinheiro = new Dinheiro(cpf, valor, descricao, valor_recebido);
            var troco = valor_recebido - valor;
            dinheiro.Pagar();
            Console.WriteLine($"Troco: {troco}");
             Console.WriteLine($"Pagamento realizado {dinheiro.DataPagamento} com sucesso \n");

            listaDinheiros.Add(dinheiro);
        }


        public static void PagamentoBoleto()
        {
            Console.WriteLine("Digite o valor da compra:");
            var valor = double.Parse(Console.ReadLine());

            Console.WriteLine("Digite o CPF do cliente:");
            var cpf = Console.ReadLine();

            Console.WriteLine("Preeencha uma descrição caso necessário");
            var descricao = Console.ReadLine();

            var boleto = new Boleto(cpf, valor, descricao);
            boleto.GerarBoleto();

            Console.WriteLine($"Boleto gerado com sucesso com o número {boleto.CodigoBarra} com data de vencimento para o dia {boleto.DataVencimento} ");

            listaBoletos.Add(boleto);
        }
        
 //       public static void ProdutosVendidos()
  //      {
    //        Console.WriteLine("========== PRODUTOS VENDIDOS ============");
      //      listaProdutos = new List<Produtos>();
            
        //    var produtos = listaProdutos
          //      .ToList();

          //  foreach (var item in produtos)
            //{
              //  Console.WriteLine("\n");
                //Console.WriteLine($"Produto: {item.Marca}\nValor:{item.Modelo}\n");
            //}

            //Console.WriteLine("========== FIM PRODUTOS VENDIDOS ============ \n");

            
        //}

        public static void Pagamento()
        {
            Console.WriteLine("Digite o código de barras:");
            var numero = Guid.Parse(Console.ReadLine());

            var boleto = listaBoletos
                            .Where(item => item.CodigoBarra == numero)
                            .FirstOrDefault();

            if(boleto is null)
            {
                Console.WriteLine($"Boleto de código {numero} não encontrado!");
                return;
            }

            if(boleto.EstaPago())
            {
                Console.WriteLine($"Boleto já foi pago no dia {boleto.DataPagamento}");
                return;
            }

            if (boleto.EstaVencido())
            {
                boleto.CalcularJuros();
                Console.WriteLine($"Boleto está vencido, terá acrescimo de 10% === R$ {boleto.Valor}");
            }

            boleto.Pagar();
            Console.WriteLine($"Boleto de código {numero} foi pago com sucesso");
        }

        public static void Relatorio()
        {
            Console.WriteLine("Qual opção de relatório:");
            Console.WriteLine("1-Pagamentos em Dinheiro | 2-Pagamentos em Boleto");

            var opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    PagamentosDinheiro();
                    break;
                case 2:
                    PagamentosBoletos();
                    break;
    //            case 3:
    //                ProdutosVendidos();
    //                break;
                default:
                    break;
            }

        }


        public static void PagamentosDinheiro()
        {
            Console.WriteLine("========== Pagamentos em Dinheiro ============");
            var dinheiros = listaDinheiros.ToList();

            foreach (var item in dinheiros)
            {
                Console.WriteLine("\n");
                Console.WriteLine($"CPF: {item.Cpf}\nValor:{item.Valor}\nData Pagamento: {item.DataPagamento} ==");
            }

            Console.WriteLine("========== FIM Pagamentos em Dinheiro ============ \n");
        }

        public static void PagamentosBoletos()
        {
                Console.WriteLine("1-Pagos | 2-À pagar | 3-Vencidos");

                var opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        BoletosPagos();
                        break;
                    case 2:
                        BoletosAPagar();
                        break;
                    case 3:
                        BoletosVencidos();
                        break;
                    default:
                        break;
                }
        }


        public static void BoletosPagos()
        {
            Console.WriteLine("========== Boletos pagos ============");
            var boletos = listaBoletos
                            .Where(item => item.Confirmacao)
                            .ToList();

            foreach (var item in boletos)
            {
                Console.WriteLine("\n");
                Console.WriteLine($"Codigo de Barra: {item.CodigoBarra}\nValor:{item.Valor}\nData Pagamento: {item.DataPagamento} ==");
            }

            Console.WriteLine("========== Boletos pagos ============ \n");
        }

        public static void BoletosAPagar()
        {
            Console.WriteLine("========== Boletos à pagar ============");
            var boletos = listaBoletos
                            .Where(item => item.Confirmacao == false
                                    && item.DataVencimento > DateTime.Now)
                            .ToList();

            foreach (var item in boletos)
            {
                Console.WriteLine("\n ====");
                Console.WriteLine($"Codigo de Barra: {item.CodigoBarra}\nValor:{item.Valor}\nData Pagamento: {item.DataPagamento} ==");
            }

            Console.WriteLine("========== Boletos à pagar ============ \n");
        }

        public static void BoletosVencidos()
        {
            Console.WriteLine("========== Boletos vencidos ============");
            var boletos = listaBoletos
                            .Where(item => item.Confirmacao == false
                                    && item.DataVencimento < DateTime.Now)
                            .ToList();

            foreach (var item in boletos)
            {
                Console.WriteLine("\n ====");
                Console.WriteLine($"Codigo de Barra: {item.CodigoBarra}\nValor:{item.Valor}\nData Pagamento: {item.DataPagamento} ==");
            }

            Console.WriteLine("========== Boletos vencidos ============ \n");
        }
    }
}

