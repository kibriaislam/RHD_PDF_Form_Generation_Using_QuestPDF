using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public static class PurchaseAccountPdfGenerator
{
    public static byte[] GeneratePurchaseAccountForm()
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1.5f, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                // Main title
                page.Header()
                    .AlignCenter()
                    .Text("PURCHASE ACCOUNT FOR MATERIALS")
                    .Bold()
                    .FontSize(16)
                    .Underline()
                    ;

                page.Content()
                    .Column(column =>
                    {
                        // Form fields section
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text("1. Name of Supplier......");
                            row.RelativeItem().Text("3. Date of receipt......");
                        });

                        column.Item().PaddingTop(10).Text("2. Name of work for which supplied......");

                        column.Item().PaddingTop(10).Text("4. Reference to M.B. number and page number......");

                        column.Item().PaddingTop(10).Text("5. Details of materials......");

                        // Amount table
                        column.Item().PaddingTop(15).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            // Table header
                            table.Header(header =>
                            {
                                header.Cell().BorderBottom(1).PaddingBottom(5).AlignCenter().Text("Amount");
                                header.Cell().BorderBottom(1).PaddingBottom(5).AlignCenter().Text("Amount paid or Purchase made during the month");
                            });

                            // Table content
                            table.Cell().PaddingTop(8).Text("TL");
                            table.Cell().PaddingTop(8).Text("TL");
                        });

                        // Divider line (longer and centered)
                        column.Item().PaddingTop(20).AlignCenter().LineHorizontal(0.5f, Unit.Inch);

                        // Subject For Part I section
                        column.Item().PaddingTop(20).Column(subColumn =>
                        {
                            subColumn.Item().Text("Subject For Part I:").Bold();

                            subColumn.Item().PaddingTop(5).Text("- Awarded:  ");
                            subColumn.Item().PaddingLeft(15).Text("- Acknowledgment:  ");
                            subColumn.Item().PaddingLeft(15).Text("- Authorised by:  ");
                            subColumn.Item().PaddingLeft(15).Text("- Authorised by:  ");
                        });
                    });
            });
        });

        using var stream = new MemoryStream();
        document.GeneratePdf(stream);
        return stream.ToArray();
    }
}