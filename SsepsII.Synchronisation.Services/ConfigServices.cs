using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SsepsII.Synchronisation.DAL;

namespace SsepsII.Synchronisation.Services
{
    public static class ConfigServices
    {
        //public static string GetCurrentFinancialYear
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG currentFinancialYear = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("CurrentFinancialYear"));
        //            return currentFinancialYear != null ? currentFinancialYear.CONFIG_VALUE : string.Empty;
        //        }
        //    }
        //}

        //public static int GetCurrentPayrollPeriod
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG currentPayrollPeriod = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("ActivePayrollPeriod"));
        //            return currentPayrollPeriod != null ? int.Parse(currentPayrollPeriod.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        
        //public static int ClassifiedID
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG PayScaleID = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("ClassifiedID"));
        //            return PayScaleID != null ? int.Parse(PayScaleID.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}
        //public static int UnClassifiedID
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG PayScaleID = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("UnClassifiedID"));
        //            return PayScaleID != null ? int.Parse(PayScaleID.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int ProvisionalAppointmentID
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG PayScaleID = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("ProvisionalAppointmentID"));
        //            return PayScaleID != null ? int.Parse(PayScaleID.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int CSPayScaleID
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG PayScaleID = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("csPayScaleId"));
        //            return PayScaleID != null ? int.Parse(PayScaleID.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}
        //public static int LawyerPayScaleId
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG PayScaleID = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("lawyerPayScaleId"));
        //            return PayScaleID != null ? int.Parse(PayScaleID.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}
        //public static int MoRCLiasonPayScaleId
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG PayScaleID = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("MoRCLiasonPayScaleId"));
        //            return PayScaleID != null ? int.Parse(PayScaleID.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}
        //public static int JudiciaryPayScaleId
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG PayScaleID = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("judiciaryPayScaleId"));
        //            return PayScaleID != null ? int.Parse(PayScaleID.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}
        //public static int OFPayScaleID
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG PayScaleID = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("OFPayScaleId"));
        //            return PayScaleID != null ? int.Parse(PayScaleID.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}
        //public static int CPHPayScaleID
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG PayScaleID = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("CPHPayScaleId"));
        //            return PayScaleID != null ? int.Parse(PayScaleID.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int AbsencePayItemId
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG absencePayItemId = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("AbsencePayItemId"));
        //            return absencePayItemId != null ? int.Parse(absencePayItemId.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}
        //public static int OvertimePayItemId
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG overtimePayItemId = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("OvertimePayItemId"));
        //            return overtimePayItemId != null ? int.Parse(overtimePayItemId.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}
        //public static int ActingAllowancePayItemId
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG absencePayItemId = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("ActingAllowanceItemId"));
        //            return absencePayItemId != null ? int.Parse(absencePayItemId.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int SalaryAdvancePayItemId
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG absencePayItemId = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("SalaryAdvancePayItemId"));
        //            return absencePayItemId != null ? int.Parse(absencePayItemId.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int SalaryArrearsPayItemId
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG salaryArrearsPayItemId = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("SalaryArrearsPayItemId"));
        //            return salaryArrearsPayItemId != null ? int.Parse(salaryArrearsPayItemId.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int LeaveEmployeeEventID
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG absencePayItemId = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("LeaveEventId"));
        //            return absencePayItemId != null ? int.Parse(absencePayItemId.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int SuspensionEmployeeEventID
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG absencePayItemId = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("SuspensionEventId"));
        //            return absencePayItemId != null ? int.Parse(absencePayItemId.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int TerminationEventID
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG absencePayItemId = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals(Constant.SSEPS_CONFIG_TERMINATION_EVENTID));
        //            return absencePayItemId != null ? int.Parse(absencePayItemId.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int IncrementEventId
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG incrementEventId = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals(Constant.SSEPS_CONFIG_INCREMENT_EVENTID));
        //            return incrementEventId != null ? int.Parse(incrementEventId.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int EnrolmentEventID
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG absencePayItemId = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals(Constant.SSEPS_CONFIG_ENROLLMENT_EVENTID));
        //            return absencePayItemId != null ? int.Parse(absencePayItemId.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int LeaveWithHalfPay
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG absencePayItemId = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("TerminationEventId"));
        //            return absencePayItemId != null ? int.Parse(absencePayItemId.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int LeaveWithoutPay
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG absencePayItemId = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("LeaveWithoutPay"));
        //            return absencePayItemId != null ? int.Parse(absencePayItemId.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int SuspensionWithHalfPay
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG absencePayItemId = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("SuspensionWithHalfPay"));
        //            return absencePayItemId != null ? int.Parse(absencePayItemId.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int SuspensionWithoutPay
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG absencePayItemId = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("SuspensionWithoutPay"));
        //            return absencePayItemId != null ? int.Parse(absencePayItemId.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int ActingEmployeeEventID
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG absencePayItemId = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("ActingEventId"));
        //            return absencePayItemId != null ? int.Parse(absencePayItemId.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int NoEffectOnSalaryPayID
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG noEffectOnSalaryPayID = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals(Constant.SSEPS_CONFIG_NO_EFFECT_ON_SALARY));
        //            return noEffectOnSalaryPayID != null ? int.Parse(noEffectOnSalaryPayID.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int ActingPromotionRouteClassified
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG noEffectOnSalaryPayID = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("ActingPromotionRouteClassified",StringComparison.OrdinalIgnoreCase));
        //            return noEffectOnSalaryPayID != null ? int.Parse(noEffectOnSalaryPayID.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}
        //public static int ActingPromotionRouteUnclassified
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG noEffectOnSalaryPayID = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("ActingPromotionRouteUnclassified", StringComparison.OrdinalIgnoreCase));
        //            return noEffectOnSalaryPayID != null ? int.Parse(noEffectOnSalaryPayID.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}
        public static int SyncStatusNormal
        {
            get
            {
                using (SsepsIISynEntities ents = new SsepsIISynEntities())
                {
                    SSEPS_CONFIG syncStatusNormal = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("SyncStatusNormal"));
                    return syncStatusNormal != null ? int.Parse(syncStatusNormal.CONFIG_VALUE) : 0;
                }
            }
        }

        public static int SyncStatusApproved
        {
            get
            {
                using (SsepsIISynEntities ents = new SsepsIISynEntities())
                {
                    SSEPS_CONFIG syncStatusApproved = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("SyncStatusApproved"));
                    return syncStatusApproved != null ? int.Parse(syncStatusApproved.CONFIG_VALUE) : 0;
                }
            }
        }

        public static int SyncStatusRejected
        {
            get
            {
                using (SsepsIISynEntities ents = new SsepsIISynEntities())
                {
                    SSEPS_CONFIG syncStatusRejected = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("SyncStatusRejected"));
                    return syncStatusRejected != null ? int.Parse(syncStatusRejected.CONFIG_VALUE) : 0;
                }
            }
        }

        //public static int HalfSalaryPayID
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG absencePayItemId = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("HalfSalaryPayId"));
        //            return absencePayItemId != null ? int.Parse(absencePayItemId.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int NoSalaryPayID
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG absencePayItemId = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("NoSalaryPayId"));
        //            return absencePayItemId != null ? int.Parse(absencePayItemId.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int PromotionEventId
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG absencePayItemId = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("PromotionEventId"));
        //            return absencePayItemId != null ? int.Parse(absencePayItemId.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int PensionPayItemId
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG pensionPayItemId = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("PensionPayItemId"));
        //            return pensionPayItemId != null ? int.Parse(pensionPayItemId.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static string PayItemCodeLoan
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("PayItemCodeLoan"));
        //            return payitemCode != null ? payitemCode.CONFIG_VALUE : string.Empty;
        //        }
        //    }
        //}

        //public static string PayItemCodeBasic
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals(Constant.SSEPS_CONFIG_PAYITEM_BASIC));
        //            return payitemCode != null ? payitemCode.CONFIG_VALUE : string.Empty;
        //        }
        //    }
        //}

        //public static string PayItemCola
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals(Constant.SSEPS_CONFIG_PAYITEM_COLA));
        //            return payitemCode != null ? payitemCode.CONFIG_VALUE : string.Empty;
        //        }
        //    }
        //}

        //public static string PayItemCodeOvertime
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals(Constant.SSEPS_CONFIG_PAYITEM_OVERTIME));
        //            return payitemCode != null ? payitemCode.CONFIG_VALUE : string.Empty;
        //        }
        //    }
        //}
        //public static string PayItemCodeOverTimeHourNormal
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals(Constant.SSEPS_CONFIG_PAYITEM_OVERTIME_HOURNORMAL));
        //            return payitemCode != null ? payitemCode.CONFIG_VALUE : string.Empty;
        //        }
        //    }
        //}
        //      public static string PayItemCodeOverTimeHourWeedEnd
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals(Constant.SSEPS_CONFIG_PAYITEM_OVERTIME_HOURWEEKEND));
        //            return payitemCode != null ? payitemCode.CONFIG_VALUE : string.Empty;
        //        }
        //    }
        //}
        
        //public static string PayItemREP
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals(Constant.SSEPS_CONFIG_PAYITEM_REP));
        //            return payitemCode != null ? payitemCode.CONFIG_VALUE : string.Empty;
        //        }
        //    }
        //}

        //public static string PayItemRESP
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals(Constant.SSEPS_CONFIG_PAYITEM_RESP));
        //            return payitemCode != null ? payitemCode.CONFIG_VALUE : string.Empty;
        //        }
        //    }
        //}

        //public static string PayItemHOUSE
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals(Constant.SSEPS_CONFIG_PAYITEM_HOUSE));
        //            return payitemCode != null ? payitemCode.CONFIG_VALUE : string.Empty;
        //        }
        //    }
        //}

        //public static string PayItemACTING
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals(Constant.SSEPS_CONFIG_PAYITEM_ACTING));
        //            return payitemCode != null ? payitemCode.CONFIG_VALUE : string.Empty;
        //        }
        //    }
        //}

        //public static string PayItemPENSION
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals(Constant.SSEPS_CONFIG_PAYITEM_PENSION));
        //            return payitemCode != null ? payitemCode.CONFIG_VALUE : string.Empty;
        //        }
        //    }
        //}

        //public static string PayItemCodeARREAR
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals(Constant.SSEPS_CONFIG_PAYITEM_ARREAR));
        //            return payitemCode != null ? payitemCode.CONFIG_VALUE : string.Empty;
        //        }
        //    }
        //}

        //public static string PayItemCodeWTU
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals(Constant.SSEPS_CONFIG_PAYITEM_WTU));
        //            return payitemCode != null ? payitemCode.CONFIG_VALUE : string.Empty;
        //        }
        //    }
        //}

        //public static string PayItemCodeHEALTH
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals(Constant.SSEPS_CONFIG_PAYITEM_HEALTH));
        //            return payitemCode != null ? payitemCode.CONFIG_VALUE : string.Empty;
        //        }
        //    }
        //}

        //public static string PayItemPIT
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals(Constant.SSEPS_CONFIG_PAYITEM_PIT));
        //            return payitemCode != null ? payitemCode.CONFIG_VALUE : string.Empty;
        //        }
        //    }
        //}
        //public static string PayItemABSENCE
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals(Constant.SSEPS_CONFIG_PAYITEM_ABSENCE));
        //            return payitemCode != null ? payitemCode.CONFIG_VALUE : string.Empty;
        //        }
        //    }
        //}

        //public static string PayItemSPLM
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals(Constant.SSEPS_CONFIG_PAYITEM_SPLM));
        //            return payitemCode != null ? payitemCode.CONFIG_VALUE : string.Empty;
        //        }
        //    }
        //}
        //public static double ABSFACTOR
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("ABSFACTOR", StringComparison.OrdinalIgnoreCase));
        //            return payitemCode != null ? double.Parse(payitemCode.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}
        //public static double PENFACTOR
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("PENFACTOR", StringComparison.OrdinalIgnoreCase));
        //            return payitemCode != null ? double.Parse(payitemCode.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}
        //public static double TaxFreeLimit
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("TAXFREELIMIT", StringComparison.OrdinalIgnoreCase));
        //            return payitemCode != null ? double.Parse(payitemCode.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}
        //public static double TaxLimitOne
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("TAXLIMITONE", StringComparison.OrdinalIgnoreCase));
        //            return payitemCode != null ? double.Parse(payitemCode.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}
        //public static double TaxLimitTwo
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("TAXLIMITTWO", StringComparison.OrdinalIgnoreCase));
        //            return payitemCode != null ? double.Parse(payitemCode.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}
        //public static double TaxRateOne
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("TAXRATEONE", StringComparison.OrdinalIgnoreCase));
        //            return payitemCode != null ? double.Parse(payitemCode.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}
        //public static double TaxRateTwo
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("TAXRATETWO", StringComparison.OrdinalIgnoreCase));
        //            return payitemCode != null ? double.Parse(payitemCode.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}
        //public static string PayItemABSDAY
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG payitemCode = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("PayItemCodeABSDAY",StringComparison.OrdinalIgnoreCase));
        //            return payitemCode != null ? payitemCode.CONFIG_VALUE : string.Empty;
        //        }
        //    }
        //}
        //public static int ArrearsScopeEmployee
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG arrearsScope = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("ArrearsScopeEmployeeId"));
        //            return arrearsScope != null ? int.Parse(arrearsScope.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int ArrearsScopePayGrade
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG arrearsScope = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("ArrearsScopePayGradeId"));
        //            return arrearsScope != null ? int.Parse(arrearsScope.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int ArrearsScopePayScale
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG arrearsScope = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("ArrearsScopePayScaleId"));
        //            return arrearsScope != null ? int.Parse(arrearsScope.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int ArrearsScopeDirectorate
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG arrearsScope = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("ArrearsScopeDirectorateId"));
        //            return arrearsScope != null ? int.Parse(arrearsScope.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int ArrearsScopeMda
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG arrearsScope = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("ArrearsScopeMdaId"));
        //            return arrearsScope != null ? int.Parse(arrearsScope.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int ArrearsElementUnpaidSalary
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG arrearsElement = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("ArrearsElementUnpaidSalary"));
        //            return arrearsElement != null ? int.Parse(arrearsElement.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}

        //public static int ArrearsElementDifferenceInPay
        //{
        //    get
        //    {
        //        using (SsepsIISynEntities ents = new SsepsIISynEntities())
        //        {
        //            SSEPS_CONFIG arrearsElement = ents.SSEPS_CONFIG.FirstOrDefault(x => x.CONFIG_KEY.Equals("ArrearsElementDifferenceInPay"));
        //            return arrearsElement != null ? int.Parse(arrearsElement.CONFIG_VALUE) : 0;
        //        }
        //    }
        //}
    }
}
