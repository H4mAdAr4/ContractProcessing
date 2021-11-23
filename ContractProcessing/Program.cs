using System;
using System.Globalization;
using ContractProcessing.Entities;
using ContractProcessing.Entities.Services;

namespace ContractProcessing {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("CONTRACT ADMINISTRATION SYSTEM");
            Console.WriteLine("------------------------------");
            Console.WriteLine("Hello user! Please enter the contract data:");
            Console.Write("Number: ");
            int number = int.Parse(Console.ReadLine());
            Console.Write("Date (DD/MM/YYYY): ");
            DateTime date = DateTime.Parse(Console.ReadLine());
            Console.Write("Contract value: ");
            double value = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.Write("Number of due payments: ");
            int payments = int.Parse(Console.ReadLine());

            Contract contract = new Contract(number, date, value);
            ContractService contractService = new ContractService(new PaymentService());
            contractService.ProcessContract(contract, payments);

            Console.Clear();
            Console.WriteLine("CONTRACT ADMINISTRATION SYSTEM");
            Console.WriteLine("------------------------------");
            Console.WriteLine("DUE PAYMENTS:");
            foreach (Installment installment in contract.Installments) {
                Console.WriteLine(installment);
            }
            BillGeneratorService.GenerateBill(contract, payments);

            /*
                i could do a nice menu here asking the user if they want to do another operation, but im to lazy to do that ¯\_(ツ)_/¯
                i'll probably add it later when i feel like it, to be honest it will probably never happen.
             */
        }
    }
}
