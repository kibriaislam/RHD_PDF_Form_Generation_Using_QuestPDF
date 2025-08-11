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
                WorkName = "",
                CashBookVoucherNo = "",
                Date = DateTime.Now,
                WorkItems = new List<WorkItem>()
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
