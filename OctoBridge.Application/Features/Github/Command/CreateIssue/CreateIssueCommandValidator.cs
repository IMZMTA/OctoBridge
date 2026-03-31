using FluentValidation;
using OctoBridge.Domain.Constants;
using OctoBridge.Application.CQRS.Abstractions;

namespace OctoBridge.Application.Features.Github.Command.CreateIssue;

public class CreateIssueCommandValidator : BaseValidator<CreateIssueRequestDto>
{
    public CreateIssueCommandValidator()
    {

        RuleFor(x => x.RepositoryOwner)
            .NotNull()
            .NotEmpty()
            .WithMessage(Messages.RepositoryOwnerRequired);

        RuleFor(x => x.RepositoryName)
            .NotNull()
            .NotEmpty()
            .WithMessage(Messages.RepositoryNameRequired);

        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty()
            .WithMessage(Messages.IssueTitleRequired)
            .MaximumLength(AppConstants.MaxIssueTitleLength)
            .WithMessage(string.Format(Messages.IssueTitleTooLong, AppConstants.MaxIssueTitleLength));

        RuleForEach(x => x.Labels)
            .NotNull()
            .WithMessage(Messages.LabelNull)
            .MaximumLength(AppConstants.MaxLabelNameLength)
            .WithMessage(string.Format(Messages.LabelTooLong, AppConstants.MaxLabelNameLength));

        RuleForEach(x => x.Assignees)
            .NotNull()
            .WithMessage(Messages.AssigneeNull)
            .MaximumLength(AppConstants.MaxUsernameLength)
            .WithMessage(string.Format(Messages.AssigneeTooLong, AppConstants.MaxUsernameLength));

        RuleFor(x => x.Body)
            .NotEmpty()
            .WithMessage(Messages.IssueBodyNull)
            .MaximumLength(AppConstants.MaxIssueBodyLength)
            .When(x => !string.IsNullOrEmpty(x.Body))
            .WithMessage(string.Format(Messages.IssueBodyTooLong, AppConstants.MaxIssueBodyLength))
;

    }
}