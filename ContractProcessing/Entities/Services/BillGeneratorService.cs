using System;
using System.IO;
using System.Globalization;

namespace ContractProcessing.Entities.Services {
    class BillGeneratorService {
        delegate string LineReplacer(string s1, string s2);
        delegate string Substring(int i);
        delegate double Interest(double d, int i);
        delegate double Fee(double d);
        delegate string CIValue(string s, IFormatProvider p);
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
                        LineReplacer replace = line.Replace;
                        Substring substring1 = aux.Substring;
                        Substring substring2 = aux2.Substring;
                        Interest interest = pay.Interest;
                        Fee fee = pay.PayFee;
                        CIValue cValue = contract.Installments[0].Value.ToString;

                        while ((line = sourcefile.ReadLine()) != null) {
                            line = replace("x%", substring1(aux.Length - 2) + "%");
                            line = replace("w%", substring2(aux2.Length - 2) + "%");
                            line = replace(" X ", " " + interest(contract.Value, payments).ToString("F2", CultureInfo.InvariantCulture) + " ");
                            line = replace(" W ", " " + fee(interest(contract.Value, payments)).ToString("F2", CultureInfo.InvariantCulture) + " ");
                            line = replace(" C ", " " + value + " ");
                            line = replace(" D ", " " + cValue("F2", CultureInfo.InvariantCulture) + " ");
                            line = replace(" G ", " " + Rng() + " ");
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
