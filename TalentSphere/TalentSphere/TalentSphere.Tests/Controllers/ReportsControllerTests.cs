using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TalentSphere.Controllers;
using TalentSphere.Models;
using TalentSphere.Services.Interfaces;

namespace TalentSphere.Tests.Controllers
{
    // Tests for ReportsController.GetById endpoint
    [TestFixture]
    public class ReportsControllerTests
    {
        private Mock<IReportService> _reportServiceMock;
        private ReportsController _controller;

        [SetUp]
        public void Setup()
        {
            // Create a fake IReportService
            _reportServiceMock = new Mock<IReportService>();
            _controller = new ReportsController(_reportServiceMock.Object);
        }

        [Test]
        public async Task GetById_ReportExists_ReturnsOkResult()
        {
            // Arrange: mock returns a fake report
            var fakeReport = new Report { ReportID = 1 };
            _reportServiceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(fakeReport);

            // Act
            var result = await _controller.GetById(1);

            // Assert: 200 OK
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetById_ReportDoesNotExist_ReturnsNotFound()
        {
            // Arrange: mock returns null
            _reportServiceMock.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((Report?)null);

            // Act
            var result = await _controller.GetById(99);

            // Assert: 404 Not Found
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }
    }
}
