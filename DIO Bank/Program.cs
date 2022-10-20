using System;
using DIO_Bank.Classes;


namespace DIO_Bank
{
    internal class Program
    {
        private static List<Conta> listaContas = new List<Conta>();
        private static List<string> listaLog = new List<string>();
        static void Main(string[] args)
        {
            //Conta contaCorrente = new Conta("203040", "Lucas Patricio", 200, 100, TipoConta.PessoaFisica);

            MenuPrincipal();

        }

        static void MenuPrincipal()
        {
            string opcao = "";
            while (opcao != "8")
            {
                Console.WriteLine("");
                Console.WriteLine("Bem vindo ao Banco DIO!");
                Console.WriteLine("| 1 | Nova Conta");
                Console.WriteLine("| 2 | Sacar");
                Console.WriteLine("| 3 | Depositar");
                Console.WriteLine("| 4 | Transferir");
                Console.WriteLine("| 5 | Extrato Geral");
                Console.WriteLine("| 6 | Lista de Contas");
                Console.WriteLine("| 7 | Limpar Tela");
                Console.WriteLine("| 8 | Sair");
                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        NovaConta();
                        break;
                    case "2":
                        SacarConta();
                        break;
                    case "3":
                        DepositarConta();
                        break;
                    case "4":
                        Transferir();
                        break;
                    case "5":
                        ExtratoTransacoes();
                        break;
                    case "6":
                        ListarContasCadastradas();
                        break;
                    case "7":
                        Console.Clear();
                        break;
                    case "8":
                        break;
                    default:
                        throw new NotImplementedException();

                }
            }
        }

        private static void NovaConta()
        {
            Console.Clear();
            Console.WriteLine("| NOVA CONTA |");

            Console.Write("N° Conta: ");
            string numeroConta = Console.ReadLine();

            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Saldo Inicial: ");
            decimal saldoInicial = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Limite Cheque Especial: ");
            decimal chequeEspecial = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Tipo Conta ( 1 - PF | 2 - PJ ): ");
            int tipoConta = Convert.ToInt32(Console.ReadLine());

            Conta conta = new Conta(numeroConta: numeroConta,
                nomeCliente: nome,
                saldoInicial: saldoInicial,
                chequeEspecial: chequeEspecial,
                tipoConta: (TipoConta)tipoConta);

            listaContas.Add(conta);
        }

        private static void SacarConta()
        {
            Console.Clear();
            Console.WriteLine("| SAQUE |");

            Console.Write("N° Conta: ");
            string numeroConta = Console.ReadLine();

            if (numeroConta == "")
            {
                Console.WriteLine("Número de conta inserido inválido!");
                return;
            }

            Console.Write("Valor Saque: ");
            decimal valorSaque = Convert.ToDecimal(Console.ReadLine());

            if (VerificaConta(numeroConta: numeroConta))
            {
                Conta contaRecebida = listaContas.FirstOrDefault(x => x.NumeroConta == numeroConta);
                if (contaRecebida.Sacar(valorSaque: valorSaque))
                {
                    listaLog.Add($"Saque: R$ {valorSaque}, Conta N° {numeroConta}");
                }
            }

        }

        private static void DepositarConta()
        {
            Console.Clear();
            Console.WriteLine("| DEPOSITO |");

            Console.Write("N° Conta: ");
            string numeroConta = Console.ReadLine();

            if (numeroConta == "")
            {
                Console.WriteLine("Número de conta inserido inválido!");
                return;
            }

            Console.Write("Valor Deposito: ");
            decimal valorDeposito = Convert.ToDecimal(Console.ReadLine());

            if (VerificaConta(numeroConta: numeroConta))
            {
                Conta contaRecebida = listaContas.FirstOrDefault(x => x.NumeroConta == numeroConta);
                contaRecebida.Depositar(valorDeposito: valorDeposito);
                listaLog.Add($"Deposito: R$ {valorDeposito}, Conta N° {numeroConta}");
            }
        }

        private static void Transferir()
        {
            Console.Clear();
            Console.WriteLine("| TRANSFERIR |");

            Console.Write("N° Conta Origem: ");
            string numeroContaOrigem = Console.ReadLine();

            Console.Write("N° Conta Destino: ");
            string numeroContaDestino = Console.ReadLine();

            Console.Write("Valor Transferencia: ");
            decimal valorTransferencia = Convert.ToDecimal(Console.ReadLine());


            if (VerificaConta(numeroConta: numeroContaOrigem) && VerificaConta(numeroConta: numeroContaDestino))
            {
                Conta contaOrigem = listaContas.FirstOrDefault(x => x.NumeroConta == numeroContaOrigem);
                Conta contaDestino = listaContas.FirstOrDefault(x => x.NumeroConta == numeroContaDestino);

                if (contaOrigem.Transferir(contaDestino: contaDestino,
                valorTransferir: valorTransferencia))
                {
                    listaLog.Add($"Transferido: R$ {valorTransferencia}, Conta Origem N° {numeroContaOrigem}, Conta Destino N° {numeroContaDestino}");
                }
            }
        }

        private static void ExtratoTransacoes()
        {
            Console.WriteLine("| EXTRATO |");
            foreach (string registro in listaLog)
            {
                Console.WriteLine(registro);
                Console.WriteLine("");
            }
        }

        private static void ListarContasCadastradas()
        {
            Console.WriteLine("| CONTAS |");
            foreach (Conta conta in listaContas)
            {
                Console.WriteLine(conta);
                Console.WriteLine("");
            }
        }

        private static bool VerificaConta(string numeroConta)
        {
            if (!listaContas.Exists(x => x.NumeroConta == numeroConta))
            {
                Console.WriteLine("Conta não encontrada.");
                return false;
            }

            return true;
        }
    }
}