using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using TalentSphere.Controllers;
using TalentSphere.Models;
using TalentSphere.Services.Interfaces;

namespace TalentSphere.Tests.Controllers
{
    // Tests for SelectionsController.GetSelectionById endpoint
    [TestFixture]
    public class SelectionsControllerTests
    {
        private Mock<ISelectionService> _selectionServiceMock;
        private SelectionsController _controller;

        [SetUp]
        public void Setup()
        {
            // Create a fake ISelectionService
            _selectionServiceMock = new Mock<ISelectionService>();

            // Use NullLogger for logging; AuditLogHelper isn't used by GetSelectionById, so pass null!
            _controller = new SelectionsController(
                _selectionServiceMock.Object,
                NullLogger<SelectionsController>.Instance,
                auditLogHelper: null!);
        }

        [Test]
        public async Task GetSelectionById_SelectionExists_ReturnsOkResult()
        {
            // Arrange: mock returns a fake selection
            var fakeSelection = new Selection { SelectionID = 1 };
            _selectionServiceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(fakeSelection);

            // Act
            var result = await _controller.GetSelectionById(1);

            // Assert: 200 OK
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetSelectionById_SelectionDoesNotExist_ReturnsNotFound()
        {
            // Arrange: mock returns null
            _selectionServiceMock.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((Selection?)null);

            // Act
            var result = await _controller.GetSelectionById(99);

            // Assert: 404 Not Found
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }
    }
}
