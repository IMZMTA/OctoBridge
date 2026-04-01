using FluentValidation;
using OctoBridge.Domain.Constants;
using OctoBridge.Domain.Constants.App;
using OctoBridge.Domain.Constants.External;
using OctoBridge.Domain.Constants.Messages;
using OctoBridge.Application.CQRS.Abstractions;

namespace OctoBridge.Application.Features.Github.Queries.GetRepositoryIssuesList;

public class GetRepositoryIssuesListCommandValidator
    : BaseValidator<GetRepositoryIssuesListRequestDto>
{
    public GetRepositoryIssuesListCommandValidator()
    {
        RuleFor(x => x.RepositoryOwner)
            .NotNull()
            .NotEmpty()
            .WithMessage(string.Format(ValidationMessages.Required,ErrorFields.RepositoryOwner));

        RuleFor(x => x.RepositoryName)
            .NotNull()
            .NotEmpty()
            .WithMessage(string.Format(ValidationMessages.Required,ErrorFields.RepositoryName));

        RuleFor(x => x.PageSize)
            .NotNull()
            .GreaterThan(0)
            .LessThanOrEqualTo(GitHubConstants.MaxPageSize)
            .WithMessage(string.Format(ValidationMessages.PageSizeInvalid, GitHubConstants.MaxPageSize));

        RuleFor(x => x.PageNo)
            .NotNull()
            .GreaterThanOrEqualTo(1)
            .WithMessage(ValidationMessages.PageNumberInvalid);

        RuleFor(x => x.Since)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .When(x => x.Since.HasValue)
            .WithMessage(ValidationMessages.SinceInvalid);

        RuleFor(x => x.Sort)
            .IsInEnum()
            .WithMessage(ValidationMessages.InvalidSort);

        RuleFor(x => x.Direction)
            .IsInEnum()
            .WithMessage(ValidationMessages.InvalidDirection);

        RuleFor(x => x.State)
            .IsInEnum()
            .WithMessage(ValidationMessages.InvalidIssueState);

        RuleFor(x => x.Assignee)
            .Must(a => string.IsNullOrEmpty(a) || a == "*" || a == "none" || a.Length <= AppLimits.MaxUsernameLength)
            .WithMessage(ValidationMessages.InvalidAssignee);

        RuleForEach(x => x.Labels)
            .MaximumLength(AppLimits.MaxLabelNameLength)
            .When(x => x.Labels != null && x.Labels.Any())
            .WithMessage(string.Format(ValidationMessages.TooLong,ErrorFields.Labels, AppLimits.MaxLabelNameLength));

    }
}