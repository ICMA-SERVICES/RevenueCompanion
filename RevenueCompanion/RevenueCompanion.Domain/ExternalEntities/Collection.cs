using System;
using System.ComponentModel.DataAnnotations;

namespace RevenueCompanion.Domain.ExternalEntities
{
    public partial class Collection
    {
        //public long CollectionID { get; set; }
        public string Provider { get; set; }
        public string Channel { get; set; }
        [Key]
        public string PaymentRefNumber { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Payername { get; set; }
        public string PayerId { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime? ChequeValueDate { get; set; }
        public string ChequeNumber { get; set; }
        public string ChequeStatus { get; set; }
        public string RevenueCode { get; set; }
        public string RevenueName { get; set; }
        public string AgencyCode { get; set; }
        public string AgencyName { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string Branchcode { get; set; }
        public string Branchname { get; set; }
        public string CurrencyCode { get; set; }
        public bool PushToReemsSuccessful { get; set; }
        public DateTime? PushToReemsOn { get; set; }
        public string PushToReemsBy { get; set; }
        public bool IsPushToReemsRequest { get; set; }
        public string UsedByPlatform { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
    }
}
