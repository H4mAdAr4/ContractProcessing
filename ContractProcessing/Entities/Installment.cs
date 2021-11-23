using System;
using System.Globalization;

namespace ContractProcessing.Entities {
    class Installment {
        public DateTime DueDate { get; private set; }
        public double Value { get; private set; }

        public Installment(DateTime dueDate, double value) {
            DueDate = dueDate;
            Value = value;
        }

        public override string ToString() {
            return DueDate.ToString("dd/MM/yyyy")
                + " - "
                + Value.ToString("F2", CultureInfo.InvariantCulture);
        }
    }
}
