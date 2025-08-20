using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using RHD_Testing.Data;

namespace RHD_Testing.Services
{
    public class PDFGenerateService
    {
        public static byte[] GeneratePDF(PWAccountsData data)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    // Legal size landscape: 14" x 8.5"
                    page.Size(new PageSize(356, 216, Unit.Millimetre));
                    page.Margin(6);

                    page.Header().Height(90).Column(column =>
                    {
                        // Top header
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"Bangladesh Form No. {data.FormNo}").FontSize(8);
                            row.RelativeItem().AlignRight().Text("").FontSize(8);
                        });

                        column.Item().Text($"P. R. Form No. {data.PRFormNo}").FontSize(8);

                        // Main title
                        column.Item().PaddingTop(2).AlignCenter().Text("P. W. Accounts Form 24-First and Final Bill")
                            .FontSize(12).Bold();
                        column.Item().AlignCenter().Text("(See Rul 206. B. F. R)").FontSize(9);

                        // Instruction paragraph - Complete text as per reference
                        column.Item().PaddingTop(3).Text(
                            "(Form contractor and suppliers- To be used when a single payment is made for a job or contract. I. e., only on its completion A single form may be used for making payments to several contractors of suppliers, if they relate to the same work in the same head of account, in the case of suppliers and are billed for at the same time.)")
                            .FontSize(7);

                        // Work details - All in one line as per reference
                        column.Item().PaddingTop(3).Row(row =>
                        {
                            row.RelativeItem(4).Text($"Name of work (in the case of bills for work done): {data.WorkName}")
                                .FontSize(8);
                            row.RelativeItem(2).Text($"Cash Book Voucher No. {data.CashBookVoucherNo}")
                                .FontSize(8);
                            row.RelativeItem(1).AlignRight().Text($"Dated {data.Date:dd/MM/yyyy}")
                                .FontSize(8);
                        });
                    });

                    page.Footer().Height(80).Column(column =>
                    {
                        // Signature section with comment positioned between
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Dated").FontSize(8);
                            row.RelativeItem(1).AlignCenter().Text("This is to certify that the supply of goods has been done as per terms & condition of the schedule").FontSize(6);
                            row.RelativeItem(3).AlignCenter().Text("Subdivisional Officer, Signature").FontSize(8);
                            row.RelativeItem().AlignRight().Text("Officer preparing the bill").FontSize(8);
                        });

                        // Additional fields row
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Pay Taka").FontSize(8);
                            row.RelativeItem(1).AlignCenter().Text("Subdivision, Rank").FontSize(8);
                            row.RelativeItem(3).AlignCenter().Text("").FontSize(8);
                            row.RelativeItem().AlignRight().Text("Officer preparising payment").FontSize(8);
                        });

                        // Third row
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Dated").FontSize(8);
                            row.RelativeItem(1).AlignCenter().Text("Dated initials of Divisional Accountant").FontSize(8);
                            row.RelativeItem(1).AlignCenter().Text("Divisional Officer,").FontSize(8);
                            row.RelativeItem(1).AlignCenter().Text("Signatural").FontSize(8);
                            row.RelativeItem().AlignRight().Text("").FontSize(8);
                        });

                        // Fourth row
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text("").FontSize(8);
                            row.RelativeItem(1).AlignCenter().Text("").FontSize(8);
                            row.RelativeItem(1).AlignCenter().Text("").FontSize(8);
                            row.RelativeItem(1).AlignCenter().Text("Division Bank").FontSize(8);
                            row.RelativeItem().AlignRight().Text("").FontSize(8);
                        });

                        // Add space between signature lines and instruction text
                        column.Item().PaddingTop(8);

                        // Bottom instructions - positioned from left to right with proper line breaks
                        column.Item().Text("* In the case of payments to suppliers a red ink entry should made across the page, above the entries relating thereto. in one of the following forms, applicable to the case :- (1)\" Stock,\" (2) Purchases -for stock \" (3) Purchases for direct issue to work......(4) \" Purchases for the work. for issue to contractor. There two columns are not to be filled up in the case ot piece-work agreements.\r\nIn the case of works the accounts of which are kept by sub-heads the amounts relating to all times of work failing under the same \"sub-head\" should to be totaled in red ink Payment should be attested by some known person when the payee's acknowledgement is given by a mark, seal or thumb Impression.\r\nThe person actually Making the payment should nitial (and date) in this column against each payment.\r\n**This signature is necessary only when the Officer authorising payment is not the officer who prepares the bill.").FontSize(4);
                    });

                    page.Content().PaddingTop(3).Table(table =>
                    {
                        // Optimized column proportions for landscape
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2.2f);  // Contractor name
                            columns.RelativeColumn(2.8f);  // Items of work
                            columns.RelativeColumn(0.8f);  // Book No
                            columns.RelativeColumn(0.8f);  // Page No
                            columns.RelativeColumn(0.9f);  // Date
                            columns.RelativeColumn(1.3f);  // Written order
                            columns.RelativeColumn(1.3f);  // Actual completion
                            columns.RelativeColumn(0.9f);  // Quantity
                            columns.RelativeColumn(0.8f);  // Rate
                            columns.RelativeColumn(0.7f);  // Unit
                            columns.RelativeColumn(1.0f);  // Amount
                            columns.RelativeColumn(1.0f);  // In figure
                            columns.RelativeColumn(1.5f);  // In words
                            columns.RelativeColumn(1.4f);  // Payee's ack
                            columns.RelativeColumn(1.4f);  // Dated cert
                            columns.RelativeColumn(1.3f);  // Mode of payment
                            columns.RelativeColumn(1.0f);  // Paid by me
                        });

                        // HEADER - repeated on each page
                        table.Header(header =>
                        {
                            // First header row
                            header.Cell().RowSpan(2).Border(1).Padding(1)
                                .AlignMiddle().Text("Name of contractor or supplier and reference to Agreement").FontSize(6).Bold();
                            header.Cell().RowSpan(2).Border(1).Padding(1)
                                .AlignMiddle().Text("Items of work or supplies (grouped under \"sub-head\" and \"sub-works\" of estimate)").FontSize(6).Bold();
                            header.Cell().ColumnSpan(3).Border(1).Padding(1)
                                .AlignCenter().Text("Reference to recorded measurements and date").FontSize(6).Bold();
                            header.Cell().ColumnSpan(2).Border(1).Padding(1)
                                .AlignCenter().Text("Date").FontSize(6).Bold();
                            header.Cell().RowSpan(2).Border(1).Padding(1)
                                .AlignMiddle().Text("Quantity").FontSize(6).Bold();
                            header.Cell().RowSpan(2).Border(1).Padding(1)
                                .AlignMiddle().Text("Rate").FontSize(6).Bold();
                            header.Cell().RowSpan(2).Border(1).Padding(1)
                                .AlignMiddle().Text("Unit").FontSize(6).Bold();
                            header.Cell().RowSpan(2).Border(1).Padding(1)
                                .AlignMiddle().Text("Amount").FontSize(6).Bold();
                            header.Cell().ColumnSpan(2).Border(1).Padding(1)
                                .AlignCenter().Text("Total amount payable to the contractor or supplier").FontSize(6).Bold();
                            header.Cell().RowSpan(2).Border(1).Padding(1)
                                .AlignMiddle().Text("Payee's acknowledgement (with date)").FontSize(6).Bold();
                            header.Cell().RowSpan(2).Border(1).Padding(1)
                                .AlignMiddle().Text("Dated certificate of disbursement or witness").FontSize(6).Bold();
                            header.Cell().RowSpan(2).Border(1).Padding(1)
                                .AlignMiddle().Text("Mode of payment cash or cheque (No & date)").FontSize(6).Bold();
                            header.Cell().RowSpan(2).Border(1).Padding(1)
                                .AlignMiddle().Text("Paid by me").FontSize(6).Bold();

                            // Second header row
                            header.Cell().Border(1).Padding(1)
                                .AlignCenter().Text("Book No").FontSize(6).Bold();
                            header.Cell().Border(1).Padding(1)
                                .AlignCenter().Text("Page No").FontSize(6).Bold();
                            header.Cell().Border(1).Padding(1)
                                .AlignCenter().Text("Date").FontSize(6).Bold();
                            header.Cell().Border(1).Padding(1)
                                .AlignCenter().Text("Written order to commence of work").FontSize(6).Bold();
                            header.Cell().Border(1).Padding(1)
                                .AlignCenter().Text("Actual completion of work").FontSize(6).Bold();
                            header.Cell().Border(1).Padding(1)
                                .AlignCenter().Text("In figure").FontSize(6).Bold();
                            header.Cell().Border(1).Padding(1)
                                .AlignCenter().Text("In words").FontSize(6).Bold();
                        });

                        // Check if there's any data to display
                        if (data.WorkItems != null && data.WorkItems.Count > 0)
                        {
                            // Calculate total number of rows needed (actual data count)
                            int totalRows = data.WorkItems.Count;
                            int rowsPerPage = 4; // As requested
                            int totalPages = (int)Math.Ceiling((double)totalRows / rowsPerPage);

                            // DATA ROWS with pagination
                            for (int pageIndex = 0; pageIndex < totalPages; pageIndex++)
                            {
                                int startIndex = pageIndex * rowsPerPage;
                                int endIndex = Math.Min(startIndex + rowsPerPage, totalRows);

                                // Add data rows for current page
                                for (int i = startIndex; i < endIndex; i++)
                                {
                                    var item = data.WorkItems[i];

                                    table.Cell().Border(1).Padding(1).Height(25).Text(item.ContractorName ?? "").FontSize(6);
                                    table.Cell().Border(1).Padding(1).Height(25).Text(item.WorkDescription ?? "").FontSize(6);
                                    table.Cell().Border(1).Padding(1).Height(25).Text(item.BookNo ?? "").FontSize(6);
                                    table.Cell().Border(1).Padding(1).Height(25).Text(item.PageNo ?? "").FontSize(6);
                                    table.Cell().Border(1).Padding(1).Height(25).Text(item.Date != DateTime.MinValue ? item.Date.ToString("dd/MM/yy") : "").FontSize(6);
                                    table.Cell().Border(1).Padding(1).Height(25).Text(item.WrittenOrderReference ?? "").FontSize(6);
                                    table.Cell().Border(1).Padding(1).Height(25).Text(item.ActualCompletion ?? "").FontSize(6);
                                    table.Cell().Border(1).Padding(1).Height(25).Text(item.Quantity ?? "").FontSize(6);
                                    table.Cell().Border(1).Padding(1).Height(25).Text(item.Rate ?? "").FontSize(6);
                                    table.Cell().Border(1).Padding(1).Height(25).Text(item.Unit ?? "").FontSize(6);
                                    table.Cell().Border(1).Padding(1).Height(25).Text(item.Amount ?? "").FontSize(6);
                                    table.Cell().Border(1).Padding(1).Height(25).Text(item.InFigure ?? "").FontSize(6);
                                    table.Cell().Border(1).Padding(1).Height(25).Text(item.InWords ?? "").FontSize(6);
                                    table.Cell().Border(1).Padding(1).Height(25).Text(item.PayeesAcknowledgement ?? "").FontSize(6);
                                    table.Cell().Border(1).Padding(1).Height(25).Text(item.DatedSignature ?? "").FontSize(6);
                                    table.Cell().Border(1).Padding(1).Height(25).Text(item.ModeOfPayment ?? "").FontSize(6);
                                    table.Cell().Border(1).Padding(1).Height(25).Text(item.PaidBy ?? "").FontSize(6);
                                }

                                // Force page break after each set of 4 rows (except last page)
                                if (pageIndex < totalPages - 1)
                                {
                                    table.Cell().ColumnSpan(17).PageBreak();
                                }
                            }

                            // Add TOTAL ROW on the last page when there's data
                            // Calculate totals from all work items
                            var totalAmount = data.WorkItems.Sum(x =>
                            {
                                if (decimal.TryParse(x.Amount?.Replace(",", ""), out decimal amount))
                                    return amount;
                                return 0;
                            });

                            var totalInFigure = totalAmount.ToString("N2");
                            var totalInWords = ConvertAmountToWords(totalAmount);

                            table.Cell().Border(1).Padding(1).Height(22).Text("Total-").FontSize(7).Bold();
                            // Empty cells until Amount column
                            for (int i = 0; i < 9; i++)
                                table.Cell().Border(1).Padding(1).Height(22).Text("").FontSize(6);

                            // Total amount
                            table.Cell().Border(1).Padding(1).Height(22).Text(totalInFigure).FontSize(7).Bold();
                            table.Cell().Border(1).Padding(1).Height(22).Text(totalInFigure).FontSize(7).Bold();
                            table.Cell().Border(1).Padding(1).Height(22).Text(totalInWords).FontSize(6).Bold();

                            // Remaining empty cells
                            for (int i = 0; i < 4; i++)
                                table.Cell().Border(1).Padding(1).Height(22).Text("").FontSize(6);
                        }
                        else
                        {
                            // No data case - show only the Total row with zeros
                            table.Cell().Border(1).Padding(1).Height(22).Text("Total-").FontSize(7).Bold();
                            // Empty cells until Amount column
                            for (int i = 0; i < 9; i++)
                                table.Cell().Border(1).Padding(1).Height(22).Text("").FontSize(6);

                            // Zero total amount
                            table.Cell().Border(1).Padding(1).Height(22).Text("0.00").FontSize(7).Bold();
                            table.Cell().Border(1).Padding(1).Height(22).Text("0.00").FontSize(7).Bold();
                            table.Cell().Border(1).Padding(1).Height(22).Text("Zero").FontSize(6).Bold();

                            // Remaining empty cells
                            for (int i = 0; i < 4; i++)
                                table.Cell().Border(1).Padding(1).Height(22).Text("").FontSize(6);
                        }
                    });
                });
            }).GeneratePdf();
        }

        // Helper method to convert amount to words (implement as needed)
        private static string ConvertAmountToWords(decimal amount)
        {
            // Implement your number to words conversion logic here
            // This is a placeholder - you should implement the actual conversion
            return $"{amount:N2} (In Words)";
        }
        public static byte[] GeneratePurchaseAccountForm()
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);

                    page.Content().Column(column =>
                    {
                        // Header section with P.A.No and Date
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().AlignLeft().Text("P.A.No.........................").FontSize(10);
                            row.RelativeItem().AlignRight().Text("Date......................").FontSize(10);
                        });

                        column.Item().PaddingTop(20);

                        // Bangladesh Form No.
                        column.Item().AlignLeft().Text("Bangladesh Form No. 2917").FontSize(10);

                        column.Item().PaddingTop(15);

                        // Contract No section
                        column.Item().AlignCenter().Text("Contract No:-").FontSize(11).Bold();

                        column.Item().PaddingTop(10);

                        // Main title
                        column.Item().AlignCenter().Text("PURCHASE ACCOUNT FOR MATARIALS").FontSize(14).Bold();

                        column.Item().PaddingTop(25);

                        // Form fields
                        column.Item().Column(formColumn =>
                        {
                            // Field 1
                            formColumn.Item().PaddingBottom(8).Text("1. Name of Supplier.....................................................................................................................").FontSize(10);

                            // Field 2  
                            formColumn.Item().PaddingBottom(8).Text("2. Name of work for which supplied..........................................................................................").FontSize(10);

                            // Field 3
                            formColumn.Item().PaddingBottom(8).Text("3. Date of receipt......................................................................................................................").FontSize(10);

                            // Field 4
                            formColumn.Item().PaddingBottom(8).Text("4. Reference to M.B. number and page number..........................................................................").FontSize(10);

                            // Field 5
                            formColumn.Item().PaddingBottom(15).Text("5. Details of materials................................................................................................................").FontSize(10);
                        });

                        // Table section
                        column.Item().Column(tableColumn =>
                        {
                            // Headers section
                            tableColumn.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(4); // Details column
                                    columns.RelativeColumn(1); // Amount column  
                                    columns.RelativeColumn(1.2f); // Amount paid column
                                });

                                // First header row
                                table.Cell().Border(1).BorderColor(Colors.Black).Padding(5);
                                table.Cell().Border(1).BorderColor(Colors.Black).Padding(5)
                                    .AlignCenter().Text("Amount").FontSize(10);
                                table.Cell().Border(1).BorderColor(Colors.Black).Padding(5)
                                    .AlignCenter().Text("Amount paid or\nPurchases made\nduring the month").FontSize(9);

                                // Second header row (Tk.)
                                table.Cell().Border(1).BorderColor(Colors.Black).Padding(5);
                                table.Cell().Border(1).BorderColor(Colors.Black).Padding(5)
                                    .AlignCenter().Text("Tk.").FontSize(10);
                                table.Cell().Border(1).BorderColor(Colors.Black).Padding(5)
                                    .AlignCenter().Text("Tk.").FontSize(10);
                            });

                            // Main content area - with column separator between Details and Amount
                            tableColumn.Item().Row(mainRow =>
                            {
                                // Left side: Details area only
                                mainRow.RelativeItem(4).Border(1).BorderColor(Colors.Black).Height(300);

                                // Middle: Amount area - with left border (column line)
                                mainRow.RelativeItem(1).Border(1).BorderColor(Colors.Black).Height(300);

                                // Right side: Amount paid area - with left border
                                mainRow.RelativeItem(1.2f).Border(1).BorderColor(Colors.Black).Height(300);
                            });
                        });

                        column.Item().PaddingTop(30);

                        // Bottom signature section
                        column.Item().Row(row =>
                        {
                            // Left side - Divisional Accountant
                            row.RelativeItem().Column(leftColumn =>
                            {
                                leftColumn.Item().PaddingBottom(30).Text(""); // Space for signature
                                leftColumn.Item().AlignCenter().Text("Divisional Accountant").FontSize(10);
                                leftColumn.Item().PaddingTop(10).AlignCenter().Text("Date.").FontSize(10);
                            });

                            // Right side - Sub-Divisional Officer  
                            row.RelativeItem().Column(rightColumn =>
                            {
                                rightColumn.Item().PaddingBottom(30).Text(""); // Space for signature
                                rightColumn.Item().AlignCenter().Text("Sub-Divisional Officer").FontSize(10);
                                rightColumn.Item().PaddingTop(10).AlignCenter().Text("Date.").FontSize(10);
                            });
                        });
                    });
                });
            }).GeneratePdf();
        }





    }
}
