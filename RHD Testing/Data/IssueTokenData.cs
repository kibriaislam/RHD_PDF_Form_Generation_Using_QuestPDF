namespace RHD_Testing.Data
{
    public class IssueTokenData
    {
        public string? TokenNo { get; set; }
        public string? Date { get; set; }
        public string? HRNo { get; set; }
        public string? OfficeType { get; set; }
        public string? VehicleNo { get; set; }
        public string? OfficeName { get; set; }
        public List<IssueTokenItem> Items { get; set; } = new List<IssueTokenItem>();
    }

    public class IssueTokenItem
    {
        public string? ItemName { get; set; }
        public string? Size { get; set; }
        public string? Quantity { get; set; }
    }
}
