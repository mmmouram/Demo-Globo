using MyApp.Repositories;
using MyApp.Models;
using System;
using System.IO;
using ClosedXML.Excel;

namespace MyApp.Services
{
    public class ExportService : IExportService
    {
        private readonly IOrderDetailsRepository _orderDetailsRepository;

        public ExportService(IOrderDetailsRepository orderDetailsRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
        }

        public ExcelFile ExportSectionToExcel(int orderId, string section)
        {
            // Get data based on the requested section
            object data = null;
            switch (section.ToLower())
            {
                case "orderitems":
                    data = _orderDetailsRepository.GetOrderItems(orderId);
                    break;
                case "invoices":
                    data = _orderDetailsRepository.GetInvoices(orderId);
                    break;
                case "observations":
                    data = _orderDetailsRepository.GetObservations(orderId);
                    break;
                case "blocks":
                    data = _orderDetailsRepository.GetBlocks(orderId);
                    break;
                default:
                    throw new ArgumentException("Invalid section for export");
            }

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Data");
                    // For demonstration, writing a sample header and data row
                    worksheet.Cell(1, 1).Value = "Sample Header";
                    worksheet.Cell(2, 1).Value = "Sample Data";

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        return new ExcelFile
                        {
                            Content = stream.ToArray(),
                            ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            FileName = $"Export_{section}_{orderId}.xlsx"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                // Error logging would occur here
                throw new Exception("Error generating Excel file: " + ex.Message);
            }
        }
    }
}
