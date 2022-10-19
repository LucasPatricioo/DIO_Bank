using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIO_Bank.Classes
{
    public class Conta
    {
        public string NumeroConta { get; private set; }
        private string NomeCliente { get; set; }
        private decimal Saldo { get; set; }
        private decimal ChequeEspecial { get; set; }
        private TipoConta Tipo { get; set; }

        public Conta(string numeroConta, string nomeCliente, decimal saldoInicial, decimal chequeEspecial, TipoConta tipoConta)
        {
            this.NumeroConta = numeroConta;
            this.NomeCliente = nomeCliente;
            this.Saldo = saldoInicial;
            this.ChequeEspecial = chequeEspecial;
            this.Tipo = tipoConta;
        }


        public bool Sacar(decimal valorSaque)
        {
            if (valorSaque > (this.Saldo + this.ChequeEspecial))
            {
                Console.WriteLine("Saldo Insuficiente!");
                return false;
            }

            this.Saldo -= valorSaque;
            Console.WriteLine($"O saldo atual após o saque na conta N° {this.NumeroConta} é de R${this.Saldo}");
            return true;
        }

        public void Depositar(decimal valorDeposito)
        {
            this.Saldo += valorDeposito;

            Console.WriteLine($"O saldo atual após o deposito na conta N° {this.NumeroConta} é de R${this.Saldo}");
        }

        public bool Transferir(Conta contaDestino, decimal valorTransferir)
        {
            if (this.Sacar(valorTransferir))
            {
                contaDestino.Depositar(valorTransferir);
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"N° Conta: {this.NumeroConta}");
            sb.AppendLine($"Nome: {this.NomeCliente}");
            sb.AppendLine($"Saldo: R$ {this.Saldo}");
            sb.AppendLine($"Cheque Especial: R$ {this.ChequeEspecial}");
            return sb.ToString();
        }

    }
}
