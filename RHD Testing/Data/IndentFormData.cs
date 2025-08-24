namespace RHD_Testing.Data
{
    public class IndentFormData
    {
        public string IndentNo { get; set; }
        public DateTime? Date { get; set; }
        public string IssuedBy { get; set; }
        public string OnIndentNo { get; set; }
        public DateTime? IndentDate { get; set; }
        public List<IndentItem> Items { get; set; }
    }

    public class IndentItem
    {
        public string Description { get; set; }
        public string HeadAccount { get; set; }
        public string Quantity { get; set; }
        public string WorkName { get; set; }
    }
}
