using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TalentSphere.Controllers;
using TalentSphere.DTOs;
using TalentSphere.Services.Interfaces;

namespace TalentSphere.Tests.Controllers
{
    // Tests for UserRolesController.GetById endpoint
    [TestFixture]
    public class UserRolesControllerTests
    {
        private Mock<IUserRoleService> _serviceMock;
        private UserRolesController _controller;

        [SetUp]
        public void Setup()
        {
            // Create a fake IUserRoleService
            _serviceMock = new Mock<IUserRoleService>();

            // Mapper and AuditLogHelper are not used by GetById, so pass null!
            _controller = new UserRolesController(
                _serviceMock.Object,
                mapper: null!,
                auditLogHelper: null!);
        }

        [Test]
        public async Task GetById_UserRoleExists_ReturnsOkResult()
        {
            // Arrange: mock returns a fake user-role
            var fakeUserRole = new UserRoleResponseDto { UserRoleId = 1 };
            _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(fakeUserRole);

            // Act
            var result = await _controller.GetById(1);

            // Assert: 200 OK
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetById_UserRoleDoesNotExist_ReturnsNotFound()
        {
            // Arrange: mock returns null
            _serviceMock.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((UserRoleResponseDto)null!);

            // Act
            var result = await _controller.GetById(99);

            // Assert: 404 Not Found
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
    }
}
