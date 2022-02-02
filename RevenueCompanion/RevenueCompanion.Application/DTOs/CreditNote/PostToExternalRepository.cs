namespace RevenueCompanion.Application.DTOs.CreditNote
{
    public class PostToExternalRepository
    {
        public string platFormName { get; set; }
        public int appId { get; set; }
        public string assessmentNo { get; set; }
        public bool isAssessmentReversed { get; set; }
        public int proposedAmount { get; set; }
        public string year { get; set; }
        public string reason { get; set; }
        public string source { get; set; }
        public int sourceId { get; set; }
        public string requestedBy { get; set; }
        public string approvedBy { get; set; }
        public string payerUtin { get; set; }
        public string payerName { get; set; }
        public string paymentRefNumber { get; set; }
        public string revenueCode { get; set; }
        public string revenueName { get; set; }
        public string agencyCode { get; set; }
        public string agencyName { get; set; }
        public string SecretKey { get; set; }
        public string MerchantCode { get; set; }
    }
}
