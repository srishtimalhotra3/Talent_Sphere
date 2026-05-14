using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TalentSphere.Controllers;
using TalentSphere.DTOs.CareerPlan;
using TalentSphere.Services.Interfaces;

namespace TalentSphere.Tests.Controllers
{
    // Tests for CareerPlanController.GetById endpoint
    [TestFixture]
    public class CareerPlanControllerTests
    {
        private Mock<ICareerPlanService> _serviceMock;
        private CareerPlanController _controller;

        [SetUp]
        public void Setup()
        {
            // Create a fake ICareerPlanService
            _serviceMock = new Mock<ICareerPlanService>();

            // AuditLogHelper is not used by GetById, so we pass null!
            _controller = new CareerPlanController(
                _serviceMock.Object,
                auditLogHelper: null!);
        }

        [Test]
        public async Task GetById_PlanExists_ReturnsOkResult()
        {
            // Arrange: mock returns a fake career plan
            var fakePlan = new CareerPlanResponseDTO { PlanID = 1 };
            _serviceMock.Setup(s => s.GetPlanByIdAsync(1)).ReturnsAsync(fakePlan);

            // Act
            var result = await _controller.GetById(1);

            // Assert: 200 OK
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetById_PlanDoesNotExist_ReturnsNotFound()
        {
            // Arrange: mock returns null
            _serviceMock.Setup(s => s.GetPlanByIdAsync(99)).ReturnsAsync((CareerPlanResponseDTO?)null);

            // Act
            var result = await _controller.GetById(99);

            // Assert: 404 Not Found
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }
    }
}
