using FluentValidation;
using OctoBridge.Domain.Constants;
using OctoBridge.Application.CQRS.Abstractions;

namespace OctoBridge.Application.Features.Github.Queries.GetRepositoryIssuesList;

public class GetRepositoryIssuesListCommandValidator
    : BaseValidator<GetRepositoryIssuesListRequestDto>
{
    public GetRepositoryIssuesListCommandValidator()
    {
        RuleFor(x => x.RepositoryOwner)
            .NotEmpty()
            .WithMessage(Messages.RepositoryOwnerRequired);

        RuleFor(x => x.RepositoryName)
            .NotNull()
            .NotEmpty()
            .WithMessage(Messages.RepositoryNameRequired);

        RuleFor(x => x.PageSize)
            .NotNull()
            .GreaterThan(AppConstants.Zero)
            .LessThanOrEqualTo(GitHubConstants.MaxPageSize)
            .WithMessage(string.Format(Messages.PageSizeInvalid, GitHubConstants.MaxPageSize));

        RuleFor(x => x.PageNo)
            .NotNull()
            .GreaterThanOrEqualTo(AppConstants.One)
            .WithMessage(Messages.PageNumberInvalid);

        RuleFor(x => x.Since)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .When(x => x.Since.HasValue)
            .WithMessage(Messages.SinceInvalid);

        RuleFor(x => x.Sort)
            .IsInEnum()
            .WithMessage(Messages.InvalidSort);

        RuleFor(x => x.Direction)
            .IsInEnum()
            .WithMessage(Messages.InvalidDirection);

        RuleFor(x => x.State)
            .IsInEnum()
            .WithMessage(Messages.InvalidIssueState);

        RuleFor(x => x.Assignee)
            .Must(a => string.IsNullOrEmpty(a) || a == "*" || a == "none" || a.Length <= AppConstants.MaxUsernameLength)
            .WithMessage(Messages.InvalidAssignee);

        RuleForEach(x => x.Labels)
            .MaximumLength(AppConstants.MaxLabelNameLength)
            .When(x => x.Labels != null && x.Labels.Any())
            .WithMessage(string.Format(Messages.LabelTooLong, AppConstants.MaxLabelNameLength));

    }
}