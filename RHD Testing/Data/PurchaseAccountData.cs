namespace RHD_Testing.Data
{
    public class PurchaseAccountData
    {
        public string PANumber { get; set; }
        public DateTime Date { get; set; }
        public string ContractNo { get; set; }
        public string SupplierName { get; set; }
        public string WorkName { get; set; }
        public DateTime DateOfReceipt { get; set; }
        public string MBReference { get; set; }
        public string MaterialDetails { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime DivisionalAccountantDate { get; set; }
        public DateTime SubDivisionalOfficerDate { get; set; }
    }
}
