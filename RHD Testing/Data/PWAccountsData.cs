namespace RHD_Testing.Data
{
    public class PWAccountsData
    {
        public string FormNo { get; set; } = "4797";
        public string PRFormNo { get; set; } = "13";
        public string WorkName { get; set; } = "";
        public string CashBookVoucherNo { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now;
        public List<WorkItem> WorkItems { get; set; } = new();
        public string PayTakaDate { get; set; } = "";
        public string SubdivisionalOfficer { get; set; } = "";
        public string DivisionalAccountant { get; set; } = "";
        public string DivisionalOfficer { get; set; } = "";
        public string OfficerPreparingBill { get; set; } = "";
        public string OfficerPreparingPayment { get; set; } = "";
    }

    public class WorkItem
    {
        public string ContractorName { get; set; } = "";
        public string WorkDescription { get; set; } = "";
        public string BookNo { get; set; } = "";
        public string PageNo { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now;
        public string WrittenOrderReference { get; set; } = "";
        public string ActualCompletion { get; set; } = "";
        public string Quantity { get; set; } = "";
        public string Rate { get; set; } = "";
        public string Unit { get; set; } = "";
        public string Amount { get; set; } = "";
        public string InFigure { get; set; } = "";
        public string InWords { get; set; } = "";
        public string PayeesAcknowledgement { get; set; } = "";
        public string DatedSignature { get; set; } = "";
        public string ModeOfPayment { get; set; } = "";
        public string PaidBy { get; set; } = "";
    }
}
