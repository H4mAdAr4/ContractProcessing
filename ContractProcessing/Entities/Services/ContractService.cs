using System;
using ContractProcessing.Entities.Interfaces;

namespace ContractProcessing.Entities.Services {
    class ContractService {
        private IOnlinePayment _onlinePayment;

        public ContractService(IOnlinePayment onlinePayment) {
            _onlinePayment = onlinePayment;
        }

        public void ProcessContract(Contract contract, int payments) {
            double value = contract.Value / payments;
            for(int i = 1; i <= payments; i++) {
                DateTime date = contract.Date.AddMonths(i);
                double newValue = value + _onlinePayment.Interest(value, i);
                double fullValue = newValue + _onlinePayment.PayFee(newValue);
                //value += _onlinePayment.PaymentFee(value + _onlinePayment.Interest(value, i)); This doesn't work (._.)
                //I'll find a faster and better way to do it later
                contract.AddInstallment(new Installment(date, fullValue));
            }
        }
    }
}
