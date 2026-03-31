using FluentValidation;
using OctoBridge.Domain.Constants;
using OctoBridge.Application.CQRS.Abstractions;

namespace OctoBridge.Application.Features.Github.Queries.GetCommits;

public class GetCommitsCommandValidator : BaseValidator<GetCommitsRequestDto>
{
    public GetCommitsCommandValidator()
    {
        var now = DateTime.UtcNow;
        RuleFor(x => x.RepositoryOwner)
            .NotEmpty()
            .WithMessage(Messages.RepositoryOwnerRequired);

        RuleFor(x => x.RepositoryName)
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
            .GreaterThanOrEqualTo(new DateTime(1970, 1, 1))
            .LessThanOrEqualTo(now)
            .When(x => x.Since.HasValue)
            .WithMessage(Messages.SinceInvalid);

        RuleFor(x => x.Until)
            .GreaterThanOrEqualTo(new DateTime(1970, 1, 1))
            .LessThanOrEqualTo(now)
            .When(x => x.Until.HasValue)
            .WithMessage(Messages.UntilInvalid);

        RuleFor(x => x)
            .Must(x => !x.Since.HasValue || !x.Until.HasValue || x.Since <= x.Until)
            .WithMessage(Messages.SinceUntilInvalid);
        
        RuleFor(x => x.Author)
            .MaximumLength(AppConstants.MaxUsernameLength)
            .When(x => !string.IsNullOrEmpty(x.Author))
            .WithMessage(string.Format(Messages.AuthorTooLong, AppConstants.MaxUsernameLength));

        RuleFor(x => x.Committer)
            .MaximumLength(AppConstants.MaxUsernameLength)
            .When(x => !string.IsNullOrEmpty(x.Committer))
            .WithMessage(string.Format(Messages.CommitterTooLong, AppConstants.MaxUsernameLength))  ;

    }
}