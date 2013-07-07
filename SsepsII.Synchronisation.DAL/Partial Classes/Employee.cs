using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SsepsII.Synchronisation.DAL
{
    public partial class Employee
    {
        public int MdaID { get; set; }

        public string FullName 
        {
            get { return string.Format("{0} {1} {2}", this.givenName, this.otherNames, this.surName); }
        }

        public bool IsAssigned { get; set; }
    }

    public partial class ArrearsAssignment
    {
        private string[] MONTH_SHORTNAMES = new string[] { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        public int MdaId { get; set; }

        public int DirectorateId { get; set; }

        public int PayGradeId { get; set; }

        public int PayScaleId { get; set; }

        public int ArrearScopeId { get; set; }

        public int RepaymentCount { get; set; }

        public string EndorsedBy { get; set; }

        public DateTime EndorsedOn { get; set; }

        public string PayItemName
        {
            get
            {
                return PayItem.payItemName;
            }
        }

        public string StartPeriodText
        {
            get
            {
                return string.Format("{0} {1}", MONTH_SHORTNAMES[int.Parse(startPeriod.ToString().Substring(4))], startPeriod.ToString().Substring(0, 4));
            }
        }

        public string EndPeriodText
        {
            get
            {
                return string.Format("{0} {1}", MONTH_SHORTNAMES[int.Parse(endPeriod.ToString().Substring(4))], endPeriod.ToString().Substring(0, 4));
            }
        }
    }

    public partial class SalaryArrear
    {
        private string[] MONTH_SHORTNAMES = new string[] { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        public string StartPeriodText
        {
            get
            {
                return string.Format("{0} {1}", MONTH_SHORTNAMES[int.Parse(StartPeriod.ToString().Substring(4))], StartPeriod.ToString().Substring(0, 4));
            }
        }

        public string EndPeriodText
        {
            get
            {
                return string.Format("{0} {1}", MONTH_SHORTNAMES[int.Parse(EndPeriod.ToString().Substring(4))], EndPeriod.ToString().Substring(0, 4));
            }
        }
    }

    public class ArrearsPeriod
    {
        public int PeriodId { get; set; }

        public int Period
        {
            get { return int.Parse(string.Format("{0}{1}", Month.Year, Month.ToString("MM"))); }
        }

        public DateTime Month { get; set; }
    }
}
