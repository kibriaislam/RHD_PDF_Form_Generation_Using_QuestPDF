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
                    // Legal size: 8.5" x 14"
                    page.Size(new PageSize(216, 356, Unit.Millimetre));
                    page.Margin(8);

                    page.Content().Column(column =>
                    {
                        // Top header
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"Bangladesh Form No. {data.FormNo}").FontSize(9);
                            row.RelativeItem().AlignRight().Text("").FontSize(9);
                        });

                        column.Item().Text($"P. R. Form No. {data.PRFormNo}").FontSize(9);

                        // Main title
                        column.Item().PaddingTop(5).AlignCenter().Text("P. W. Accounts Form 24-First and Final Bill")
                            .FontSize(14).Bold();
                        column.Item().AlignCenter().Text("(See Rul 206. B. F. R)").FontSize(10);

                        // Instruction paragraph
                        column.Item().PaddingTop(8).Text(
                            "(Form contractor and suppliers- To be used when a single payment is made for a job or contract... )")
                            .FontSize(8);

                        // Work details
                        column.Item().PaddingTop(8).Row(row =>
                        {
                            row.RelativeItem(4).Text($"Name of work (in the case of bills for work done) {data.WorkName}")
                                .FontSize(9);
                            row.RelativeItem(2).Text($"Cash Book Voucher No. {data.CashBookVoucherNo}")
                                .FontSize(9);
                            row.RelativeItem(1).Text($"Dated {data.Date:dd/MM/yyyy}")
                                .FontSize(9);
                        });

                        // Main table
                        column.Item().PaddingTop(10).Table(table =>
                        {
                            // Match the column proportions to the original form
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2.5f);  // Contractor name
                                columns.RelativeColumn(2.5f);  // Items of work
                                columns.RelativeColumn(1f);    // Book No
                                columns.RelativeColumn(1f);    // Page No
                                columns.RelativeColumn(1f);    // Date
                                columns.RelativeColumn(1.5f);  // Written order
                                columns.RelativeColumn(1.5f);  // Actual completion
                                columns.RelativeColumn(1.2f);  // Quantity
                                columns.RelativeColumn(1f);    // Rate
                                columns.RelativeColumn(1f);    // Unit
                                columns.RelativeColumn(1.5f);  // Amount
                                columns.RelativeColumn(1.5f);  // In figure
                                columns.RelativeColumn(1.5f);  // In words
                                columns.RelativeColumn(1.8f);  // Payee's ack
                                columns.RelativeColumn(1.8f);  // Dated cert
                                columns.RelativeColumn(1.8f);  // Mode of payment
                                columns.RelativeColumn(1.2f);  // Paid by me
                            });

                            // HEADER
                            table.Header(header =>
                            {
                                // First header row
                                header.Cell().RowSpan(2).Border(1).Padding(2)
                                    .AlignMiddle().Text("Name of contractor or supplier and reference to Agreement").FontSize(7).Bold();
                                header.Cell().RowSpan(2).Border(1).Padding(2)
                                    .AlignMiddle().Text("Items of work or supplies (grouped under \"sub-head\" and \"sub-works\" of estimate)").FontSize(7).Bold();
                                header.Cell().ColumnSpan(3).Border(1).Padding(2)
                                    .AlignCenter().Text("Reference to recorded measurements and date").FontSize(7).Bold();
                                header.Cell().ColumnSpan(2).Border(1).Padding(2)
                                    .AlignCenter().Text("Date").FontSize(7).Bold();
                                header.Cell().RowSpan(2).Border(1).Padding(2)
                                    .AlignMiddle().Text("Quantity").FontSize(7).Bold();
                                header.Cell().RowSpan(2).Border(1).Padding(2)
                                    .AlignMiddle().Text("Rate").FontSize(7).Bold();
                                header.Cell().RowSpan(2).Border(1).Padding(2)
                                    .AlignMiddle().Text("Unit").FontSize(7).Bold();
                                header.Cell().RowSpan(2).Border(1).Padding(2)
                                    .AlignMiddle().Text("Amount").FontSize(7).Bold();
                                header.Cell().ColumnSpan(2).Border(1).Padding(2)
                                    .AlignCenter().Text("Total amount payable to the contractor or supplier").FontSize(7).Bold();
                                header.Cell().RowSpan(2).Border(1).Padding(2)
                                    .AlignMiddle().Text("Payee's acknowledgement (with date)").FontSize(7).Bold();
                                header.Cell().RowSpan(2).Border(1).Padding(2)
                                    .AlignMiddle().Text("Dated certificate of disbursement or witness").FontSize(7).Bold();
                                header.Cell().RowSpan(2).Border(1).Padding(2)
                                    .AlignMiddle().Text("Mode of payment cash or cheque (No & date)").FontSize(7).Bold();
                                header.Cell().RowSpan(2).Border(1).Padding(2)
                                    .AlignMiddle().Text("Paid by me").FontSize(7).Bold();

                                // Second header row
                                header.Cell().Border(1).Padding(2)
                                    .AlignCenter().Text("Book No").FontSize(7).Bold();
                                header.Cell().Border(1).Padding(2)
                                    .AlignCenter().Text("Page No").FontSize(7).Bold();
                                header.Cell().Border(1).Padding(2)
                                    .AlignCenter().Text("Date").FontSize(7).Bold();
                                header.Cell().Border(1).Padding(2)
                                    .AlignCenter().Text("Written order to commence of work").FontSize(7).Bold();
                                header.Cell().Border(1).Padding(2)
                                    .AlignCenter().Text("Actual completion of work").FontSize(7).Bold();
                                header.Cell().Border(1).Padding(2)
                                    .AlignCenter().Text("In figure").FontSize(7).Bold();
                                header.Cell().Border(1).Padding(2)
                                    .AlignCenter().Text("In words").FontSize(7).Bold();
                            });

                            // DATA ROWS
                            for (int i = 0; i < Math.Max(15, data.WorkItems.Count); i++)
                            {
                                var item = i < data.WorkItems.Count ? data.WorkItems[i] : new WorkItem();

                                table.Cell().Border(1).Padding(2).Height(25).Text(item.ContractorName).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25).Text(item.WorkDescription).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25).Text(item.BookNo).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25).Text(item.PageNo).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25).Text(item.Date != DateTime.MinValue ? item.Date.ToString("dd/MM/yy") : "").FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25).Text(item.WrittenOrderReference).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25).Text(item.ActualCompletion).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25).Text(item.Quantity).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25).Text(item.Rate).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25).Text(item.Unit).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25).Text(item.Amount).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25).Text(item.InFigure).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25).Text(item.InWords).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25).Text(item.PayeesAcknowledgement).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25).Text(item.DatedSignature).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25).Text(item.ModeOfPayment).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25).Text(item.PaidBy).FontSize(8);
                            }

                            // TOTAL ROW
                            table.Cell().Border(1).Padding(2).Height(25).Text("Total-").FontSize(8).Bold();
                            for (int i = 0; i < 16; i++)
                                table.Cell().Border(1).Padding(2).Height(25).Text("").FontSize(8);
                        });

                        // Signature section
                        column.Item().PaddingTop(10).Text("This is a template comment").FontSize(9);
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Dated").FontSize(9);
                            row.RelativeItem(3).AlignCenter().Text("Subdivisional Officer, Signature").FontSize(9);
                            row.RelativeItem().AlignRight().Text("Officer preparning the bill").FontSize(9);
                        });

                        // Bottom instructions
                        column.Item().PaddingTop(8).Text("* In the case of payments to suppliers ...").FontSize(7);
                    });
                });
            }).GeneratePdf();
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
