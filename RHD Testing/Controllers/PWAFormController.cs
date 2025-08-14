using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RHD_Testing.Data;
using RHD_Testing.Services;

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

        [HttpGet("pdf")]
        public IActionResult GeneratePurchaseAccountPdf()
        {
            var pdfBytes = PurchaseAccountPdfGenerator.GeneratePurchaseAccountForm();
            return File(pdfBytes, "application/pdf", "PurchaseAccountForMaterials.pdf");
        }
    }
}
