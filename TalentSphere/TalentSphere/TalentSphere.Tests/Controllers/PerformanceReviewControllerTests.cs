using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TalentSphere.Controllers;
using TalentSphere.DTOs.PerformanceReview;
using TalentSphere.Services.Interfaces;

namespace TalentSphere.Tests.Controllers
{
    // Tests for PerformanceReviewController.GetById endpoint
    [TestFixture]
    public class PerformanceReviewControllerTests
    {
        private Mock<IPerformanceReviewService> _serviceMock;
        private PerformanceReviewController _controller;

        [SetUp]
        public void Setup()
        {
            // Create a fake IPerformanceReviewService
            _serviceMock = new Mock<IPerformanceReviewService>();

            // IEmployeeService and AuditLogHelper are not used by GetById, so pass null!
            _controller = new PerformanceReviewController(
                _serviceMock.Object,
                employeeService: null!,
                auditLogHelper: null!);
        }

        [Test]
        public async Task GetById_ReviewExists_ReturnsOkResult()
        {
            // Arrange: mock returns a fake review
            var fakeReview = new PerformanceReviewDTO { ReviewID = 1 };
            _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(fakeReview);

            // Act
            var result = await _controller.GetById(1);

            // Assert: 200 OK (result.Result is OkObjectResult because action returns ActionResult<T>)
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetById_ReviewDoesNotExist_ReturnsNotFound()
        {
            // Arrange: mock returns null
            _serviceMock.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((PerformanceReviewDTO?)null);

            // Act
            var result = await _controller.GetById(99);

            // Assert: 404 Not Found
            Assert.That(result.Result, Is.InstanceOf<NotFoundObjectResult>());
        }
    }
}
