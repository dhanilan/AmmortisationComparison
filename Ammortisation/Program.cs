using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammortisation
{
    public class Program
    {
        static void Main(string[] args)
        {
            const double PrincipalAmount = 270000;//Loan Amount
            const double AnnualRateOfInterest = 10.5;//Loan Amount
            const int LoanTenure = 180;
            const int ExtendedLoanTenure = 240;

            var emicalcForNormalTenure =  new EMI()
            {
                LoanTenureMonths = LoanTenure,
                Principal = PrincipalAmount,
                RateOfInterest = AnnualRateOfInterest
            };

            var fixedEMI = emicalcForNormalTenure.GetEMIAmount();
           
            var emiwithExtendedTenure = new EMI()
            {
                LoanTenureMonths = ExtendedLoanTenure,
                Principal = PrincipalAmount,
                RateOfInterest = AnnualRateOfInterest,
                MonthlyFixedRepaymentGreaterThanEMI = fixedEMI
            };

            Console.WriteLine("==============RESULTS==============");

            Console.WriteLine("==============Months Taken to complete Loan: "+ emicalcForNormalTenure.GetBreakUp().Count);
            Console.WriteLine("==============Months Taken to complete Extended Loan: " + emiwithExtendedTenure.GetBreakUp().Count);

            Console.WriteLine("==============EMI for normal loan: " + fixedEMI);
            Console.WriteLine("==============EMI for extended loan: " + fixedEMI);
            Console.ReadLine();
        }
    }
}
