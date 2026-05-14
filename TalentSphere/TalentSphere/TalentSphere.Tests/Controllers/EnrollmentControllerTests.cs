using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TalentSphere.Controllers;
using TalentSphere.DTOs;
using TalentSphere.Services.Interfaces;

namespace TalentSphere.Tests.Controllers
{
    // Tests for EnrollmentController.GetById endpoint
    [TestFixture]
    public class EnrollmentControllerTests
    {
        private Mock<IEnrollmentService> _enrollmentServiceMock;
        private EnrollmentController _controller;

        [SetUp]
        public void Setup()
        {
            // Create a fake IEnrollmentService
            _enrollmentServiceMock = new Mock<IEnrollmentService>();

            // AuditLogHelper is not used by GetById, so we pass null!
            _controller = new EnrollmentController(
                _enrollmentServiceMock.Object,
                auditLogHelper: null!);
        }

        [Test]
        public async Task GetById_EnrollmentExists_ReturnsOkResult()
        {
            // Arrange: mock returns a fake enrollment
            var fakeEnrollment = new EnrollmentResponseDTO { EnrollmentID = 1 };
            _enrollmentServiceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(fakeEnrollment);

            // Act
            var result = await _controller.GetById(1);

            // Assert: 200 OK
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetById_EnrollmentDoesNotExist_ReturnsNotFound()
        {
            // Arrange: mock returns null
            _enrollmentServiceMock.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((EnrollmentResponseDTO?)null);

            // Act
            var result = await _controller.GetById(99);

            // Assert: 404 Not Found
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }
    }
}
