namespace ContractProcessing.Entities.Interfaces {
    interface IOnlinePayment {
        public double PayFee(double value);
        public double Interest(double value, int months);
    }
}
