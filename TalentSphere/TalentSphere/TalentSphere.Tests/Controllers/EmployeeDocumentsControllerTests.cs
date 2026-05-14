using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TalentSphere.Controllers;
using TalentSphere.DTOs;
using TalentSphere.Services.Interfaces;

namespace TalentSphere.Tests.Controllers
{
    // Tests for EmployeeDocumentsController.GetById endpoint
    [TestFixture]
    public class EmployeeDocumentsControllerTests
    {
        private Mock<IEmployeeDocumentService> _serviceMock;
        private EmployeeDocumentsController _controller;

        [SetUp]
        public void Setup()
        {
            // Create a fake IEmployeeDocumentService
            _serviceMock = new Mock<IEmployeeDocumentService>();

            // Mapper and AuditLogHelper are not used by GetById, so pass null!
            _controller = new EmployeeDocumentsController(
                _serviceMock.Object,
                mapper: null!,
                auditLogHelper: null!);
        }

        [Test]
        public async Task GetById_DocumentExists_ReturnsOkResult()
        {
            // Arrange: mock returns a fake document
            var fakeDoc = new EmployeeDocumentResponseDto { DocumentID = 1 };
            _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(fakeDoc);

            // Act
            var result = await _controller.GetById(1);

            // Assert: 200 OK
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetById_DocumentDoesNotExist_ReturnsNotFound()
        {
            // Arrange: mock returns null
            _serviceMock.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((EmployeeDocumentResponseDto)null!);

            // Act
            var result = await _controller.GetById(99);

            // Assert: 404 Not Found
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
    }
}
