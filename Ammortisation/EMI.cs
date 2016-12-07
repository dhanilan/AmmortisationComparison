using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammortisation
{
    public class EMI
    {
        public double Principal { get; set; }
        public double RateOfInterest { get; set; }

        public double MonthlyFixedRepaymentGreaterThanEMI { get; set; }

        public int LoanTenureMonths { get; set; }
        public InterestCalculationBasis InterestCalculationBasis { get; set; }

        private double RateOfInterestPerMonth
        {
            get
            {

                return RateOfInterest / 12 / 100;
            }
        }

        public EMI()
        {

        }

        public double GetEMIAmount()
        {
            var oneplusrpowerN = Math.Pow(1 + RateOfInterestPerMonth, LoanTenureMonths);
            return Principal * RateOfInterestPerMonth * (oneplusrpowerN / (oneplusrpowerN - 1));
        }

        public List<MontlyBreakUp> GetBreakUp()
        {
            var result = new List<MontlyBreakUp>();

            var principalLeft = Principal;
            var emi = GetEMIAmount();

            for (int i = 0;( i < LoanTenureMonths )&& (principalLeft > 0); i++)
            {
                var interestforMonthI = GetInterestForAmount(principalLeft);
                var principalforMonthI = MonthlyFixedRepaymentGreaterThanEMI>0 ?(MonthlyFixedRepaymentGreaterThanEMI - interestforMonthI)   :(emi - interestforMonthI);
                principalLeft = principalLeft< principalforMonthI?0: principalLeft - principalforMonthI;
                result.Add(new MontlyBreakUp() { InterestComponent = interestforMonthI, Month = i, PrincipalComponent = principalforMonthI, PrincipalLeft= principalLeft });
            }
            return result;
        }

        public double GetInterestForAmount(double principal)
        {
            return principal * this.RateOfInterestPerMonth;
        }
    }

    public enum InterestCalculationBasis
    {

        Monthly,
        Yearly
    }

    public class MontlyBreakUp
    {
        public int Month { get; set; }
        public double InterestComponent { get; set; }
        public double PrincipalComponent { get; set; }
        public double PrincipalLeft { get; set; }
    }
}
