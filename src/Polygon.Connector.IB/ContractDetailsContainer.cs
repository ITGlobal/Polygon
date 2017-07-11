using System.Collections.Generic;
using System.Linq;
using IBApi;
using ITGlobal.DeadlockDetection;

namespace Polygon.Connector.InteractiveBrokers
{
    /// <summary>
    ///     Потокобезопасный контейнер для ContractDetails, пришедших по запросу
    /// </summary>
    internal sealed class ContractDetailsContainer
    {
        private readonly ILockObject syncRoot = DeadlockMonitor.Cookie<ContractDetailsContainer>();
        private readonly Dictionary<int, ContractDetailsAcumulator> contractDetailsByTicker = new Dictionary<int, ContractDetailsAcumulator>();

        public void Store(int tickerId, Contract contractStub)
        {
            using (syncRoot.Lock())
            {
                contractDetailsByTicker[tickerId] = new ContractDetailsAcumulator(contractStub);
            }
        }

        public bool AddContractDetails(int tickerId, ContractDetails contractDetails)
        {
            using (syncRoot.Lock())
            {
                ContractDetailsAcumulator acumulator;
                var result = contractDetailsByTicker.TryGetValue(tickerId, out acumulator);
                if (result)
                {
                    acumulator.AddDetails(contractDetails);
                }
                return result;
            }
        }


        public bool TryGetContractDetails(int tickerId, out ContractDetails contractDetails)
        {
            using (syncRoot.Lock())
            {
                ContractDetailsAcumulator acumulator;
                contractDetailsByTicker.TryGetValue(tickerId, out acumulator);
                contractDetails = acumulator?.BestDetails;
                return contractDetails != null;
            }
        }

        public void RemoveTickerId(int tickerId)
        {
            using (syncRoot.Lock())
            {
                contractDetailsByTicker.Remove(tickerId);
            }
        }

        #region ContractDetailsAcumulator

        private class ContractDetailsAcumulator
        {
            private readonly Contract contractStub;
            private readonly IList<ContractDetails> detailsList = new List<ContractDetails>();

            public ContractDetailsAcumulator(Contract contractStub)
            {
                this.contractStub = contractStub;
            }

            public void AddDetails(ContractDetails contractDetails)
            {
                detailsList.Add(contractDetails);
            }

            public ContractDetails BestDetails
            {
                get
                {
                    IEnumerable<ContractDetails> list = detailsList;

                    // Эта логика должна помочь разрулить конфликты
                    if (!string.IsNullOrEmpty(contractStub.PrimaryExch) && (contractStub.Exchange == "BEST" || contractStub.Exchange == "SMART"))
                    {
                        list = list.Where(_ => _.Summary.PrimaryExch == contractStub.PrimaryExch);
                    }

                    return list.FirstOrDefault();
                }
            }
        }

        #endregion
    }
}
