using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using MyApp.Controllers;
using MyApp.Models;
using MyApp.Services;

namespace MyApp.Tests.Controllers
{
    [TestFixture]
    public class OrderDetailsControllerTests
    {
        private Mock<IOrderDetailsService> _orderDetailsServiceMock;
        private Mock<IExportService> _exportServiceMock;
        private OrderDetailsController _controller;

        [SetUp]
        public void Setup()
        {
            _orderDetailsServiceMock = new Mock<IOrderDetailsService>();
            _exportServiceMock = new Mock<IExportService>();
            _controller = new OrderDetailsController(_orderDetailsServiceMock.Object, _exportServiceMock.Object);
        }

        #region GetOrderDetails
        [Test]
        public void GetOrderDetails_OrderExists_ReturnsOkResult()
        {
            // Arrange
            int orderId = 1;
            var orderDetail = new OrderDetail { OrderId = orderId };
            _orderDetailsServiceMock.Setup(s => s.GetOrderDetails(orderId)).Returns(orderDetail);
            
            // Act
            var result = _controller.GetOrderDetails(orderId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(orderDetail, okResult.Value);
        }

        [Test]
        public void GetOrderDetails_OrderDoesNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            int orderId = 1;
            _orderDetailsServiceMock.Setup(s => s.GetOrderDetails(orderId)).Returns((OrderDetail)null);
            
            // Act
            var result = _controller.GetOrderDetails(orderId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult.Value);
        }

        [Test]
        public void GetOrderDetails_ServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            int orderId = 1;
            _orderDetailsServiceMock.Setup(s => s.GetOrderDetails(orderId)).Throws(new Exception("Service error"));
            
            // Act
            var result = _controller.GetOrderDetails(orderId);

            // Assert
            var statusResult = result as ObjectResult;
            Assert.IsNotNull(statusResult);
            Assert.AreEqual(500, statusResult.StatusCode);
        }
        #endregion

        #region UpdateSection
        [Test]
        public void UpdateSection_ValidSection_ReturnsOkResult()
        {
            // Arrange
            int orderId = 1;
            string section = "orderitems";
            var updatedData = new { Data = "Test Data" };
            _orderDetailsServiceMock.Setup(s => s.UpdateSection(orderId, section)).Returns(updatedData);

            // Act
            var result = _controller.UpdateSection(orderId, section);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(updatedData, okResult.Value);
        }

        [Test]
        public void UpdateSection_ServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            int orderId = 1;
            string section = "orderitems";
            _orderDetailsServiceMock.Setup(s => s.UpdateSection(orderId, section)).Throws(new Exception("Update error"));

            // Act
            var result = _controller.UpdateSection(orderId, section);

            // Assert
            var statusResult = result as ObjectResult;
            Assert.IsNotNull(statusResult);
            Assert.AreEqual(500, statusResult.StatusCode);
        }
        #endregion

        #region ExportSectionToExcel
        [Test]
        public void ExportSectionToExcel_ValidExport_ReturnsFileResult()
        {
            // Arrange
            int orderId = 1;
            string section = "orderitems";
            var excelFile = new ExcelFile
            {
                Content = new byte[] { 1, 2, 3 },
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                FileName = "Export_orderitems_1.xlsx"
            };
            _exportServiceMock.Setup(s => s.ExportSectionToExcel(orderId, section)).Returns(excelFile);

            // Act
            var result = _controller.ExportSectionToExcel(orderId, section);

            // Assert
            Assert.IsInstanceOf<FileContentResult>(result);
            var fileResult = result as FileContentResult;
            Assert.AreEqual(excelFile.Content, fileResult.FileContents);
            Assert.AreEqual(excelFile.ContentType, fileResult.ContentType);
            Assert.AreEqual(excelFile.FileName, fileResult.FileDownloadName);
        }

        [Test]
        public void ExportSectionToExcel_NullExcelFile_ReturnsInternalServerError()
        {
            // Arrange
            int orderId = 1;
            string section = "orderitems";
            _exportServiceMock.Setup(s => s.ExportSectionToExcel(orderId, section)).Returns((ExcelFile)null);

            // Act
            var result = _controller.ExportSectionToExcel(orderId, section);

            // Assert
            var statusResult = result as ObjectResult;
            Assert.IsNotNull(statusResult);
            Assert.AreEqual(500, statusResult.StatusCode);
        }

        [Test]
        public void ExportSectionToExcel_ServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            int orderId = 1;
            string section = "orderitems";
            _exportServiceMock.Setup(s => s.ExportSectionToExcel(orderId, section)).Throws(new Exception("Export error"));

            // Act
            var result = _controller.ExportSectionToExcel(orderId, section);

            // Assert
            var statusResult = result as ObjectResult;
            Assert.IsNotNull(statusResult);
            Assert.AreEqual(500, statusResult.StatusCode);
        }
        #endregion

        #region SaveUserPreferences
        [Test]
        public void SaveUserPreferences_ValidPreferences_ReturnsOkResult()
        {
            // Arrange
            int orderId = 1;
            var preferences = new UserPreferences { PreferencesJson = "{\"filter\": true}" };
            
            // Act
            var result = _controller.SaveUserPreferences(orderId, preferences);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);
        }

        [Test]
        public void SaveUserPreferences_ServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            int orderId = 1;
            var preferences = new UserPreferences { PreferencesJson = "{\"filter\": true}" };
            _orderDetailsServiceMock.Setup(s => s.SaveUserPreferences(orderId, preferences)).Throws(new Exception("Save error"));
            
            // Act
            var result = _controller.SaveUserPreferences(orderId, preferences);

            // Assert
            var statusResult = result as ObjectResult;
            Assert.IsNotNull(statusResult);
            Assert.AreEqual(500, statusResult.StatusCode);
        }
        #endregion

        #region GetUserPreferences
        [Test]
        public void GetUserPreferences_ValidPreferences_ReturnsOkResult()
        {
            // Arrange
            int orderId = 1;
            var preferences = new UserPreferences { PreferencesJson = "{\"columns\": [100,200]}" };
            _orderDetailsServiceMock.Setup(s => s.GetUserPreferences(orderId)).Returns(preferences);

            // Act
            var result = _controller.GetUserPreferences(orderId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(preferences, okResult.Value);
        }

        [Test]
        public void GetUserPreferences_ServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            int orderId = 1;
            _orderDetailsServiceMock.Setup(s => s.GetUserPreferences(orderId)).Throws(new Exception("Preferences error"));

            // Act
            var result = _controller.GetUserPreferences(orderId);

            // Assert
            var statusResult = result as ObjectResult;
            Assert.IsNotNull(statusResult);
            Assert.AreEqual(500, statusResult.StatusCode);
        }
        #endregion
    }
}
