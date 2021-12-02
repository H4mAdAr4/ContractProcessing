using System;
using System.IO;
using System.Globalization;

namespace ContractProcessing.Entities.Services {
    class BillGeneratorService {
        public static void GenerateBill(Contract contract, int payments) {
            try {
                string sourcePath = Path.GetFullPath("BillModel.txt");
                string targetPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Bill.txt";
                PaymentService pay = new PaymentService();
                var aux = pay.InterestRate.ToString("F2", CultureInfo.InvariantCulture);
                var aux2 = pay.PaymentFee.ToString("F2", CultureInfo.InvariantCulture);
                var value = contract.Value / payments;

                using (var sourcefile = File.OpenText(sourcePath)) {
                    File.Copy(sourcePath, targetPath);

                    using (var fileStream = new StreamWriter(targetPath)) {
                        string line = "";

                        while ((line = sourcefile.ReadLine()) != null) {
                            line = line.Replace("x%", aux.Substring(aux.Length - 2) + "%");
                            line = line.Replace("w%", aux2.Substring(aux2.Length - 2) + "%");
                            line = line.Replace(" X ", " " + pay.Interest(contract.Value, payments).ToString("F2", CultureInfo.InvariantCulture) + " ");
                            line = line.Replace(" W ", " " + pay.PayFee(pay.Interest(contract.Value, payments)).ToString("F2", CultureInfo.InvariantCulture) + " ");
                            line = line.Replace(" C ", " " + value + " ");
                            line = line.Replace(" D ", " " + contract.Installments[0].Value.ToString("F2", CultureInfo.InvariantCulture) + " ");
                            line = line.Replace(" G ", " " + Rng() + " ");
                            fileStream.WriteLine(line);
                        }
                        fileStream.Close();
                    }
                    sourcefile.Close();
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        private static string Rng() {
            Random rnd = new Random();
            string fullCode = "";
            for (int i = 0; i < 10; i++) {
                if (i == 6) {
                    string aux = rnd.Next(0, 9).ToString();
                    fullCode += $"{aux} ";
                } else if (i == 7) {
                    string aux = rnd.Next(0, 9999).ToString();
                    fullCode += $"{aux}";
                } else if (i == 0 || i == 2 || i == 4) {
                    string aux = rnd.Next(0, 99999).ToString();
                    fullCode += $"{aux}.";
                } else if (i == 8 || i == 9) {
                    string aux = rnd.Next(0, 99999).ToString();
                    fullCode += $"{aux}";
                } else {
                    string aux = rnd.Next(0, 99999).ToString();
                    fullCode += $"{aux} ";
                }
            }
            return fullCode;
        }
    }
}
