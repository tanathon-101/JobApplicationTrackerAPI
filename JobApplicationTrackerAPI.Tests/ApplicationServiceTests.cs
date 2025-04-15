namespace JobApplicationTrackerAPI.Tests;

public class ApplicationServiceTests
{
    private readonly ApplicationService _service;
    private readonly Mock<IApplicationRepository> _mockRepo;

    public ApplicationServiceTests()
    {
        _mockRepo = new Mock<IApplicationRepository>();
        _service = new ApplicationService(_mockRepo.Object);
    }

    [Fact]
    public async Task CreateAsync_Should_Set_Pending_Status_And_Return_Id()
    {
        // Arrange
        var request = new CreateApplicationRequest
        {
            UserId = 1,
            CompanyId = 1,
            PositionId = 1,
            AppliedDate = DateTime.UtcNow
        };

        _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Application>())).ReturnsAsync(101);

        // Act
        var result = await _service.CreateAsync(request);

        // Assert
        result.Id.Should().Be(101);
        result.Status.Should().Be(ApplicationStatus.Pending);
    }
}