namespace FileManager.Application.Files.Commands.UploadFile;

public class UploadFileCommandValidator : AbstractValidator<UploadFile>
{
    public UploadFileCommandValidator()
    {
        RuleFor(v => v.FileName)
            .MaximumLength(200)
            .NotEmpty();
    }
}
