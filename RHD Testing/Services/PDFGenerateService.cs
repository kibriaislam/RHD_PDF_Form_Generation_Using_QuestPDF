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
                        // Top header with form numbers
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"Bangladesh Form No. {data.FormNo}").FontSize(9);
                            row.RelativeItem().AlignRight().Text("").FontSize(9);
                        });

                        column.Item().Text($"P. R. Form No. {data.PRFormNo}").FontSize(9);

                        // Main title
                        column.Item().PaddingTop(5).AlignCenter().Text("P. W. Accounts Form 24-First and Final Bill")
                            .FontSize(14).Bold();

                        column.Item().AlignCenter().Text("(See Rul 206. B. F. R)")
                            .FontSize(10);

                        // Instruction paragraph
                        column.Item().PaddingTop(8).Text(
                            "(Form contractor and suppliers- To be used whena single payment is made for a job or contract. I. e, only on its completion A single form may be used for making payments to several contractors of suppliers, if they relate to the same work to the same head of account, in the case of suppliers and are billed for at the same time.)")
                            .FontSize(8);

                        // Work name and cash book voucher
                        column.Item().PaddingTop(8).Row(row =>
                        {
                            row.RelativeItem(4).Text($"Name of work ( in the case of bills for work done) {data.WorkName}")
                                .FontSize(9);
                            row.RelativeItem(2).Text($"Cash Book Voucher No. {data.CashBookVoucherNo}")
                                .FontSize(9);
                            row.RelativeItem(1).Text($"Dated {data.Date:dd/MM/yyyy}")
                                .FontSize(9);
                        });

                        // Main table
                        column.Item().PaddingTop(10).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2.5f);  // Name of contractor
                                columns.RelativeColumn(2.5f);  // Items of work
                                columns.RelativeColumn(1.2f);  // Book No
                                columns.RelativeColumn(1.2f);  // Page No
                                columns.RelativeColumn(1.2f);  // Date
                                columns.RelativeColumn(1.8f);  // Written order
                                columns.RelativeColumn(1.8f);  // Actual completion
                                columns.RelativeColumn(1.2f);  // Quantity
                                columns.RelativeColumn(1f);    // Rate
                                columns.RelativeColumn(1f);    // Unit
                                columns.RelativeColumn(1.5f);  // Amount
                                columns.RelativeColumn(1.5f);  // In figure
                                columns.RelativeColumn(1.5f);  // In words
                                columns.RelativeColumn(1.8f);  // Payee's acknowledgement
                                columns.RelativeColumn(1.8f);  // Dated signature
                                columns.RelativeColumn(1.8f);  // Mode of payment
                                columns.RelativeColumn(1.2f);  // Paid by
                            });

                            // Combined Headers (first row and second row)
                            table.Header(header =>
                            {
                                // First header row
                                header.Cell().RowSpan(2).Border(1).Padding(2)
                                    .AlignMiddle().Text("Name of contractor or supplier and refer ence to Agreement").FontSize(7).Bold();
                                header.Cell().RowSpan(2).Border(1).Padding(2)
                                    .AlignMiddle().Text("Items of work or supplies (grouped under \"sub-head\" and \"sub-works\" of estimate)").FontSize(7).Bold();
                                header.Cell().ColumnSpan(2).Border(1).Padding(2)
                                    .AlignCenter().Text("Reference to recorded measurements and date").FontSize(7).Bold();
                                header.Cell().ColumnSpan(2).Border(1).Padding(2)
                                    .AlignCenter().Text("Date").FontSize(7).Bold();
                                header.Cell().RowSpan(2).Border(1).Padding(2)
                                    .AlignMiddle().Text("Written order to commence of work").FontSize(7).Bold();
                                header.Cell().RowSpan(2).Border(1).Padding(2)
                                    .AlignMiddle().Text("Actual completion of work").FontSize(7).Bold();
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
                                    .AlignMiddle().Text("Payee's acknow- ledgement (with date)").FontSize(7).Bold();
                                header.Cell().RowSpan(2).Border(1).Padding(2)
                                    .AlignMiddle().Text("Dated certificate of disbursement or witness").FontSize(7).Bold();
                                header.Cell().RowSpan(2).Border(1).Padding(2)
                                    .AlignMiddle().Text("Mode of pay ment cash or cheque (No & date)").FontSize(7).Bold();
                                header.Cell().RowSpan(2).Border(1).Padding(2)
                                    .AlignMiddle().Text("Paid by me").FontSize(7).Bold();

                                // Second header row (sub-headers)
                                header.Cell().Border(1).Padding(2)
                                    .AlignCenter().Text("Book No").FontSize(7).Bold();
                                header.Cell().Border(1).Padding(2)
                                    .AlignCenter().Text("Page No").FontSize(7).Bold();
                             
                                header.Cell().Border(1).Padding(2)
                                    .AlignCenter().Text("In words").FontSize(7).Bold();
                                header.Cell().Border(1).Padding(2)
                                 .AlignCenter().Text("In figure").FontSize(7).Bold();
                            });

                            // Data rows (create at least 15 empty rows for the form)
                            for (int i = 0; i < Math.Max(15, data.WorkItems.Count); i++)
                            {
                                var item = i < data.WorkItems.Count ? data.WorkItems[i] : new WorkItem();

                                table.Cell().Border(1).Padding(2).Height(25)
                                    .Text(item.ContractorName).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25)
                                    .Text(item.WorkDescription).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25)
                                    .Text(item.BookNo).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25)
                                    .Text(item.PageNo).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25)
                                    .Text(item.Date != DateTime.MinValue ? item.Date.ToString("dd/MM/yy") : "").FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25)
                                    .Text(item.WrittenOrderReference).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25)
                                    .Text(item.ActualCompletion).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25)
                                    .Text(item.Quantity).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25)
                                    .Text(item.Rate).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25)
                                    .Text(item.Unit).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25)
                                    .Text(item.Amount).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25)
                                    .Text(item.InFigure).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25)
                                    .Text(item.InWords).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25)
                                    .Text(item.PayeesAcknowledgement).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25)
                                    .Text(item.DatedSignature).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25)
                                    .Text(item.ModeOfPayment).FontSize(8);
                                table.Cell().Border(1).Padding(2).Height(25)
                                    .Text(item.PaidBy).FontSize(8);
                            }

                            // Total row
                            table.Cell().Border(1).Padding(2).Height(25)
                                .Text("Total-").FontSize(8).Bold();
                            for (int i = 0; i < 16; i++)
                            {
                                table.Cell().Border(1).Padding(2).Height(25).Text("").FontSize(8);
                            }
                        });

                        // Bottom signature section
                        column.Item().PaddingTop(10).Column(bottomSection =>
                        {
                            bottomSection.Item().Row(row =>
                            {
                                row.RelativeItem().Text("Dated").FontSize(9);
                                row.RelativeItem(3).AlignCenter().Text("Subdivisional Officer, Signature").FontSize(9);
                                row.RelativeItem().AlignRight().Text("Officer preparning the bill").FontSize(9);
                            });

                            bottomSection.Item().Row(row =>
                            {
                                row.RelativeItem().Text("").FontSize(9);
                                row.RelativeItem(3).AlignCenter().Text("Subdivision, Rank }").FontSize(9);
                                row.RelativeItem().Text("").FontSize(9);
                            });

                            bottomSection.Item().PaddingTop(5).Text("Pay Taka").FontSize(9);

                            bottomSection.Item().PaddingTop(5).Row(row =>
                            {
                                row.RelativeItem().Text("Dated").FontSize(9);
                                row.RelativeItem().Text("Dated initials of Divisional Accountant").FontSize(9);
                                row.RelativeItem().Text("Divisional Officer,").FontSize(9);
                                row.RelativeItem().Text("Signatural }").FontSize(9);
                                row.RelativeItem().Text("Officer preparising payment").FontSize(9);
                            });

                            bottomSection.Item().Row(row =>
                            {
                                row.RelativeItem().Text("").FontSize(9);
                                row.RelativeItem().Text("").FontSize(9);
                                row.RelativeItem().Text("").FontSize(9);
                                row.RelativeItem().Text("Division Rank").FontSize(9);
                                row.RelativeItem().Text("").FontSize(9);
                            });
                        });

                        // Bottom instructions
                        column.Item().PaddingTop(8).Column(instructions =>
                        {
                            instructions.Item().Text("* In the case of payments to suppliers a red ink entry should made accross the page, above the entries relating thereto, in one of the following forms, applicable to the case :-")
                                .FontSize(7);
                            instructions.Item().Text("(1) \" Stock \" (2) Purchases -for stock\" (3) Purchases for direct issue to work.........................(4) \" Purchases for the work...............................for issue to contractor ..................\"")
                                .FontSize(7);
                            instructions.Item().Text("There two columns are not to be filled up in the case of piece-work agreements...")
                                .FontSize(7);
                            instructions.Item().Text("In the case of works the accounts of which are kept by sub-heads the amounts relating to all times of work failling under the same \"sub-head\" should to be totalled in red ink")
                                .FontSize(7);
                            instructions.Item().Text("Payment should be attested by some known person when the payee's acknowledgment is given by a mark, seal or thumb impression.")
                                .FontSize(7);
                            instructions.Item().Text("The person actually Making the payment should nitial (and date ) in this column against each payment.")
                                .FontSize(7);
                            instructions.Item().Text("** This signature is necessary only when the Officer authorising payment is not the officer who prepares the bill.")
                                .FontSize(7);
                        });
                    });
                });
            }).GeneratePdf();
        }
    }
}
