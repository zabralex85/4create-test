using FileManager.Application.Common.Behaviours;
using FileManager.Application.Common.Interfaces;
using FileManager.Application.Files.Commands.UploadFile;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace FileManager.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<UploadFile>> _logger = null!;
    private Mock<IUser> _user = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<UploadFile>>();
        _user = new Mock<IUser>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _user.Setup(x => x.Id).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<UploadFile>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new UploadFile
        {
            FileName = "test.json",
            Content = "{}"
        }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<UploadFile>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new UploadFile {
            FileName = "test.json",
            Content = "{}"
        }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
