using MyApp.Models;

namespace MyApp.Services
{
    public interface IExportService
    {
        ExcelFile ExportSectionToExcel(int orderId, string section);
    }
}
