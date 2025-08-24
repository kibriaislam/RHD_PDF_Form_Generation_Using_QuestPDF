using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RHD_Testing.Data;
using RHD_Testing.Services;
using static RHD_Testing.Services.PDFGenerateService;

namespace RHD_Testing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PWAFormController : ControllerBase
    {
        [HttpGet("generate-exact-pw-form24")]
        public IActionResult GenerateExactPWForm24()
        {
            var data = new PWAccountsData
            {
                FormNo = "4797",
                PRFormNo = "13",
                WorkName = "Construction of Road Bridge over Meghna River at Bhairab Point",
                CashBookVoucherNo = "CB-2025-001",
                Date = DateTime.Now,
                WorkItems = new List<WorkItem>
        {
            new WorkItem
            {
                ContractorName = "ABC Construction Ltd.",
                WorkDescription = "Excavation and earthwork for foundation",
                BookNo = "MB-001",
                PageNo = "15",
                Date = new DateTime(2025, 1, 15),
                WrittenOrderReference = "WO-2025-001 dated 10/01/2025",
                ActualCompletion = "Completed on 20/01/2025",
                Quantity = "500",
                Rate = "250.00",
                Unit = "Cubic Meter",
                Amount = "125,000.00",
                InFigure = "125,000.00",
                InWords = "One Lakh Twenty Five Thousand Taka Only",
                PayeesAcknowledgement = "Received by Ahmed Hassan on 25/01/2025",
                DatedSignature = "Signed 25/01/2025",
                ModeOfPayment = "Cheque No. 123456 dated 25/01/2025",
                PaidBy = "Treasury Officer"
            },
            new WorkItem
            {
                ContractorName = "XYZ Steel Works",
                WorkDescription = "Supply and installation of steel reinforcement",
                BookNo = "MB-002",
                PageNo = "28",
                Date = new DateTime(2025, 1, 20),
                WrittenOrderReference = "WO-2025-002 dated 18/01/2025",
                ActualCompletion = "Completed on 30/01/2025",
                Quantity = "15",
                Rate = "85,000.00",
                Unit = "Metric Ton",
                Amount = "1,275,000.00",
                InFigure = "1,275,000.00",
                InWords = "Twelve Lakh Seventy Five Thousand Taka Only",
                PayeesAcknowledgement = "Received by Mohammad Ali on 02/02/2025",
                DatedSignature = "Signed 02/02/2025",
                ModeOfPayment = "Bank Transfer BEFTN-789012",
                PaidBy = "Accounts Officer"
            },
            new WorkItem
            {
                ContractorName = "Delta Cement Suppliers",
                WorkDescription = "Supply of Portland cement for concrete work",
                BookNo = "MB-003",
                PageNo = "45",
                Date = new DateTime(2025, 2, 1),
                WrittenOrderReference = "WO-2025-003 dated 28/01/2025",
                ActualCompletion = "Delivered on 05/02/2025",
                Quantity = "200",
                Rate = "580.00",
                Unit = "Bags",
                Amount = "116,000.00",
                InFigure = "116,000.00",
                InWords = "One Lakh Sixteen Thousand Taka Only",
                PayeesAcknowledgement = "Received by Karim Uddin on 08/02/2025",
                DatedSignature = "Signed 08/02/2025",
                ModeOfPayment = "Cash Payment",
                PaidBy = "Site Engineer"
            },
            new WorkItem
            {
                ContractorName = "Prime Transport Co.",
                WorkDescription = "Transportation of construction materials",
                BookNo = "MB-004",
                PageNo = "52",
                Date = new DateTime(2025, 2, 5),
                WrittenOrderReference = "WO-2025-004 dated 03/02/2025",
                ActualCompletion = "Service completed on 10/02/2025",
                Quantity = "25",
                Rate = "3,200.00",
                Unit = "Trips",
                Amount = "80,000.00",
                InFigure = "80,000.00",
                InWords = "Eighty Thousand Taka Only",
                PayeesAcknowledgement = "Received by Rafiq Ahmed on 12/02/2025",
                DatedSignature = "Signed 12/02/2025",
                ModeOfPayment = "Cheque No. 654321 dated 12/02/2025",
                PaidBy = "Project Manager"
            },
                      new WorkItem
            {
                ContractorName = "Prime Transport Co.",
                WorkDescription = "Transportation of construction materials",
                BookNo = "MB-004",
                PageNo = "52",
                Date = new DateTime(2025, 2, 5),
                WrittenOrderReference = "WO-2025-004 dated 03/02/2025",
                ActualCompletion = "Service completed on 10/02/2025",
                Quantity = "25",
                Rate = "3,200.00",
                Unit = "Trips",
                Amount = "80,000.00",
                InFigure = "80,000.00",
                InWords = "Eighty Thousand Taka Only",
                PayeesAcknowledgement = "Received by Rafiq Ahmed on 12/02/2025",
                DatedSignature = "Signed 12/02/2025",
                ModeOfPayment = "Cheque No. 654321 dated 12/02/2025",
                PaidBy = "Project Manager"
            }
        }
            };

            var pdf = PDFGenerateService.GeneratePDF(data);
            return File(pdf, "application/pdf", $"pw-form24-exact-{DateTime.Now:yyyyMMdd}.pdf");
        }

        [HttpGet()]
        public IActionResult GetPurchaseAccount()
        {
            // Normally you’d get this from the database by id
            var data = new PurchaseAccountData
            {
                PANumber = "1234",
                Date = DateTime.Today,
                ContractNo = "C-5678",
                SupplierName = "ABC Suppliers Ltd.",
                WorkName = "Road Construction Work",
                DateOfReceipt = DateTime.Today,
                MBReference = "MB-45 / Page 12",
                MaterialDetails = "Cement, Bricks, Sand",
                Amount = 150000,
                AmountPaid = 145000,
                DivisionalAccountantDate = DateTime.Today,
                SubDivisionalOfficerDate = DateTime.Today
            };

            var pdfBytes = PDFGenerateService.GeneratePurchaseAccountForm();

            return File(pdfBytes, "application/pdf", "PurchaseAccount.pdf");
        }

        [HttpGet("generate-form")]
        public IActionResult GenerateSecurityDepositForm()
        {
            try
            {
                // Create dummy data matching the reference document
                var formData = new SecurityDepositFormData
                {
                    ContractorName = "Brothers Ltd",
                    WorkName = "Supply & Repairing worker and Supply & Repairing works and related service for Dhaka Mira B610014 Control Division Dhaka during the FY 2023-2024",
                    MemoNumber = "4584",
                    MemoDate = "27-11-2023",
                    ContractNumber = "116/EE/ECDDHA/2023-24",
                    ContractId = "871919",
                    WorkStartDate = "27-11-2023",
                    CompletionMemoNumber = "4584",
                    DepositAmount = "15,845568",
                    CashBookNumber = "458",
                    CashBookDate = "27-11-2023",
                    SecurityDepositAmount = "15,845568",
                    IsTimeExtended = "না",
                    AdditionalMaterials = "না",
                    MaterialValueDeduction = "না",
                    SubAssistantComment = "জামানতের টাকা ফেরত প্রদানের জন্য সুপারিশ সহ পেশ করা হইল।",
                    SubDivisionalComment = "জামানতের টাকা ফেরত প্রদানের নিমিত্ত সুপারিশ সহকারে প্রেরণ করা হইল।",
                    DivisionalAccountantComment = "নিরীক্ষিত।"
                };

                // Generate PDF
                byte[] pdfBytes =PDFGenerateService.GenerateSecurityDepositForm(formData);

                // Return PDF file
                return File(pdfBytes, "application/pdf", "Security_Deposit_Return_Form.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generating PDF: {ex.Message}");
            }
        }

        [HttpGet("pdf")]
        public IActionResult GeneratePurchaseAccountPdf()
        {
            var pdfBytes = PurchaseAccountPdfGenerator.GeneratePurchaseAccountForm();
            return File(pdfBytes, "application/pdf", "PurchaseAccountForMaterials.pdf");
        }
        public class PurchaseAccountData1
        {
            public string PANumber { get; set; }
            public string Date { get; set; }
            public string ContractNumber { get; set; }
            public string SupplierName { get; set; }
            public string WorkName { get; set; }
            public string ReceiptDate { get; set; }
            public string MBReference { get; set; }
        }

        // Material class to hold database data
        public class MaterialItem
        {
            public string Name { get; set; }
            public decimal UnitPrice { get; set; }
            public int Quantity { get; set; }
        }

        // Dummy form data for testing
        private static PurchaseAccountData1 GetDummyFormData()
        {
            return new PurchaseAccountData1
            {
                PANumber = "PA-2025-001",
                Date = "21/08/2025",
                ContractNumber = "CT-AUTO-2025-078",
                SupplierName = "Bangladesh Auto Parts Ltd., Dhanmondi, Dhaka",
                WorkName = "Supply of Tires, Rims and Batteries for Government Vehicle Fleet",
                ReceiptDate = "18/08/2025",
                MBReference = "MB-2025-Vol-12, Page 45-48"
            };
        }

        // Dummy data for testing - Automotive parts (Tires, Rims, Batteries)
        private static List<MaterialItem> GetDummyMaterials()
        {
            return new List<MaterialItem>
            {
                new MaterialItem { Name = "Car Tires - 195/65R15 Michelin", UnitPrice = 8500, Quantity = 4 },
                new MaterialItem { Name = "Truck Tires - 10.00R20 Bridgestone", UnitPrice = 25000, Quantity = 6 },
                new MaterialItem { Name = "Motorcycle Tires - 90/90-17 MRF", UnitPrice = 3200, Quantity = 8 },
                new MaterialItem { Name = "Car Battery - 65Ah Exide", UnitPrice = 12000, Quantity = 10 },
                new MaterialItem { Name = "Truck Battery - 150Ah Hamko", UnitPrice = 18500, Quantity = 5 },
                new MaterialItem { Name = "Motorcycle Battery - 12V 7Ah", UnitPrice = 2800, Quantity = 12 },
                new MaterialItem { Name = "Alloy Rims - 15 inch 4-hole", UnitPrice = 6500, Quantity = 8 },
                new MaterialItem { Name = "Steel Rims - 16 inch 5-hole", UnitPrice = 4200, Quantity = 12 },
                new MaterialItem { Name = "Motorcycle Rims - 17 inch Spoke", UnitPrice = 3800, Quantity = 6 },
                new MaterialItem { Name = "SUV Tires - 235/70R16 Yokohama", UnitPrice = 15000, Quantity = 4 },
                new MaterialItem { Name = "Bus Battery - 200Ah Heavy Duty", UnitPrice = 35000, Quantity = 3 },
                new MaterialItem { Name = "Truck Rims - 20 inch 10-hole", UnitPrice = 8500, Quantity = 8 },
                new MaterialItem { Name = "Rickshaw Tires - 4.00-8 Local", UnitPrice = 1200, Quantity = 20 },
                new MaterialItem { Name = "UPS Battery - 100Ah Deep Cycle", UnitPrice = 15500, Quantity = 6 },
                new MaterialItem { Name = "Bicycle Tires - 26x1.95 Kenda", UnitPrice = 450, Quantity = 15 }
            };
        }

        [HttpGet("generate-pa-account-with-dummy")]
        public IActionResult GeneratePAAccountWithDummyDataPDF()
        {
            try
            {
                var materials = GetDummyMaterials();
                var formData = GetDummyFormData();

                var pdfBytes = GeneratePurchaseAccountForm1(materials, formData);

                return File(pdfBytes, "application/pdf", "PurchaseAccount.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generating PDF: {ex.Message}");
            }
        }

        [HttpGet("generate-hand-receipt")]
        public IActionResult GenerateIndentForm(
               [FromQuery] string? indentNo = null,
               [FromQuery] DateTime? date = null,
               [FromQuery] string? issuedBy = null,
               [FromQuery] string? onIndentNo = null,
               [FromQuery] DateTime? indentDate = null)
        {
            try
            {
                // Create data object with query parameters
                var data = new IndentFormData
                {
                    IndentNo = indentNo,
                    Date = date ?? DateTime.Now,
                    IssuedBy = issuedBy,
                    OnIndentNo = onIndentNo,
                    IndentDate = indentDate ?? DateTime.Now,
                    Items = new List<IndentItem>
                {
                    new IndentItem
                    {
                        Description = "Tyres 195/55 R 16\nSuperior Quality, Foreign Made,\nManufactured within 01 (One) year\nfrom delivery date.",
                        HeadAccount = "",
                        Quantity = "03(Three) 03(Three)",
                        WorkName = ""
                    },
                    new IndentItem
                    {
                        Description = "Tyres 195/55 R 16\nSuperior Quality, Foreign Made,\nManufactured within 01 (One) year\nfrom delivery date.",
                        HeadAccount = "",
                        Quantity = "03(Three) 03(Three)",
                        WorkName = ""
                    },
                    new IndentItem
                    {
                        Description = "Tyres 195/55 R 16\nSuperior Quality, Foreign Made,\nManufactured within 01 (One) year\nfrom delivery date.",
                        HeadAccount = "",
                        Quantity = "03(Three) 03(Three)",
                        WorkName = ""
                    }
                }
                };

                // Generate the PDF
                var pdfBytes = HandReceiptPDF(data);

                // Return the PDF file
                return File(pdfBytes, "application/pdf", $"Form1_Indent_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generating PDF: {ex.Message}");
            }
        }


    }
}
