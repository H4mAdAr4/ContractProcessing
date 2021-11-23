using System;
using System.Collections.Generic;

namespace ContractProcessing.Entities {
    class Contract{
        public int Number { get; private set; }
        public DateTime Date { get; private set; }
        public double Value { get; private set; }
        public List<Installment> Installments { get; private set; }

        public Contract(int number, DateTime date, double value) {
            Number = number;
            Date = date;
            Value = value;
            Installments = new List<Installment>();
        }

        public void AddInstallment(Installment installment) {
            Installments.Add(installment);
        }
    }
}
