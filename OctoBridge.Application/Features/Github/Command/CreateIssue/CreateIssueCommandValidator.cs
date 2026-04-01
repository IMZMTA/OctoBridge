using FluentValidation;
using OctoBridge.Domain.Constants;
using OctoBridge.Domain.Constants.App;
using OctoBridge.Domain.Constants.Messages;
using OctoBridge.Application.CQRS.Abstractions;

namespace OctoBridge.Application.Features.Github.Command.CreateIssue;

public class CreateIssueCommandValidator : BaseValidator<CreateIssueRequestDto>
{
    public CreateIssueCommandValidator()
    {

        RuleFor(x => x.RepositoryOwner)
            .NotNull()
            .NotEmpty()
            .WithMessage(string.Format(ValidationMessages.Required,ErrorFields.RepositoryOwner));

        RuleFor(x => x.RepositoryName)
            .NotNull()
            .NotEmpty()
            .WithMessage(string.Format(ValidationMessages.Required,ErrorFields.RepositoryName));

        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty()
            .WithMessage(string.Format(ValidationMessages.Required,ErrorFields.Title))
            .MaximumLength(AppLimits.MaxIssueTitleLength)
            .WithMessage(string.Format(ValidationMessages.TooLong, ErrorFields.Title, AppLimits.MaxIssueTitleLength));

        RuleForEach(x => x.Labels)
            .NotNull()
            .WithMessage(ValidationMessages.Required)
            .MaximumLength(AppLimits.MaxLabelNameLength)
            .WithMessage(string.Format(ValidationMessages.TooLong, ErrorFields.Labels, AppLimits.MaxLabelNameLength));

        RuleForEach(x => x.Assignees)
            .NotNull()
            .WithMessage(ValidationMessages.Required)
            .MaximumLength(AppLimits.MaxUsernameLength)
            .WithMessage(string.Format(ValidationMessages.TooLong, ErrorFields.Assignees, AppLimits.MaxUsernameLength));

        RuleFor(x => x.Body)
            .NotEmpty()
            .WithMessage(ValidationMessages.Required)
            .MaximumLength(AppLimits.MaxIssueBodyLength)
            .When(x => !string.IsNullOrEmpty(x.Body))
            .WithMessage(string.Format(ValidationMessages.TooLong, ErrorFields.Body, AppLimits.MaxIssueBodyLength));

    }
}