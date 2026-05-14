using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TalentSphere.Controllers;
using TalentSphere.DTOs;
using TalentSphere.Services.Interfaces;

namespace TalentSphere.Tests.Controllers
{
    // Tests for RolesController.GetById endpoint
    [TestFixture]
    public class RolesControllerTests
    {
        private Mock<IRoleService> _roleServiceMock;
        private RolesController _controller;

        [SetUp]
        public void Setup()
        {
            // Create a fake IRoleService
            _roleServiceMock = new Mock<IRoleService>();

            // AuditLogHelper is not used by GetById, so pass null!
            _controller = new RolesController(
                _roleServiceMock.Object,
                auditLogHelper: null!);
        }

        [Test]
        public async Task GetById_RoleExists_ReturnsOkResult()
        {
            // Arrange: mock returns a fake role
            var fakeRole = new RoleResponseDTO { RoleID = 1 };
            _roleServiceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(fakeRole);

            // Act
            var result = await _controller.GetById(1);

            // Assert: 200 OK
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetById_RoleDoesNotExist_ReturnsNotFound()
        {
            // Arrange: mock returns null
            _roleServiceMock.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((RoleResponseDTO)null!);

            // Act
            var result = await _controller.GetById(99);

            // Assert: 404 Not Found
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }
    }
}
