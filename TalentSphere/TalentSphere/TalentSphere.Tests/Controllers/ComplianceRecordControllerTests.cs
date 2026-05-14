using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TalentSphere.Controllers;
using TalentSphere.DTOs;
using TalentSphere.Services.Interfaces;

namespace TalentSphere.Tests.Controllers
{
    // Tests for ComplianceRecordController.GetComplianceRecordById endpoint
    [TestFixture]
    public class ComplianceRecordControllerTests
    {
        private Mock<IComplianceRecordService> _serviceMock;
        private ComplianceRecordController _controller;

        [SetUp]
        public void Setup()
        {
            // Create a fake IComplianceRecordService
            _serviceMock = new Mock<IComplianceRecordService>();

            // AuditLogHelper is not used by GetComplianceRecordById, so we pass null!
            _controller = new ComplianceRecordController(
                _serviceMock.Object,
                auditLogHelper: null!);
        }

        [Test]
        public async Task GetComplianceRecordById_RecordExists_ReturnsOkResult()
        {
            // Arrange: mock returns a fake compliance record
            var fakeRecord = new ComplianceRecordResponseDTO { ComplianceID = 1 };
            _serviceMock.Setup(s => s.GetComplianceRecordByIdAsync(1)).ReturnsAsync(fakeRecord);

            // Act
            var result = await _controller.GetComplianceRecordById(1);

            // Assert: 200 OK
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetComplianceRecordById_RecordDoesNotExist_ReturnsNotFound()
        {
            // Arrange: mock returns null
            _serviceMock.Setup(s => s.GetComplianceRecordByIdAsync(99)).ReturnsAsync((ComplianceRecordResponseDTO)null!);

            // Act
            var result = await _controller.GetComplianceRecordById(99);

            // Assert: 404 Not Found
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }
    }
}
