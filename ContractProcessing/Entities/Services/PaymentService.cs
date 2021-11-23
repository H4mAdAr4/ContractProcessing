using ContractProcessing.Entities.Interfaces;

namespace ContractProcessing.Entities.Services {
    class PaymentService : IOnlinePayment {
        public double InterestRate { get; set; }
        public double PaymentFee { get; set; }

        public PaymentService() {
            InterestRate = 0.01;
            PaymentFee = 0.02;
        }

        public double Interest(double value, int payments) {
            return (value * InterestRate) * payments;
        }

        public double PayFee(double value) {
            return value * PaymentFee;
        }
    }
}
