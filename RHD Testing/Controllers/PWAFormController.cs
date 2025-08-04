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
    }
}
