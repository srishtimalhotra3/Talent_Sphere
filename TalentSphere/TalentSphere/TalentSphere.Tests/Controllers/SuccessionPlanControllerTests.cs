using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TalentSphere.Controllers;
using TalentSphere.DTOs;
using TalentSphere.Services.Interfaces;

namespace TalentSphere.Tests.Controllers
{
    // Tests for SuccessionPlanController.GetById endpoint
    [TestFixture]
    public class SuccessionPlanControllerTests
    {
        private Mock<ISuccessionPlanService> _serviceMock;
        private SuccessionPlanController _controller;

        [SetUp]
        public void Setup()
        {
            // Create a fake ISuccessionPlanService
            _serviceMock = new Mock<ISuccessionPlanService>();

            // AuditLogHelper is not used by GetById, so pass null!
            _controller = new SuccessionPlanController(
                _serviceMock.Object,
                auditLogHelper: null!);
        }

        [Test]
        public async Task GetById_SuccessionPlanExists_ReturnsOkResult()
        {
            // Arrange: mock returns a fake plan
            var fakePlan = new SuccessionPlanResponseDTO { SuccessionID = 1 };
            _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(fakePlan);

            // Act
            var result = await _controller.GetById(1);

            // Assert: 200 OK
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetById_SuccessionPlanDoesNotExist_ReturnsNotFound()
        {
            // Arrange: mock returns null
            _serviceMock.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((SuccessionPlanResponseDTO?)null);

            // Act
            var result = await _controller.GetById(99);

            // Assert: 404 Not Found
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
    }
}
