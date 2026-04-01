using FluentValidation;
using OctoBridge.Domain.Constants;
using OctoBridge.Domain.Constants.App;
using OctoBridge.Domain.Constants.Messages;
using OctoBridge.Domain.Constants.External;
using OctoBridge.Application.CQRS.Abstractions;

namespace OctoBridge.Application.Features.Github.Queries.GetCommits;

public class GetCommitsCommandValidator : BaseValidator<GetCommitsRequestDto>
{
    public GetCommitsCommandValidator()
    {
        var now = DateTime.UtcNow;
        var epoch = new DateTime(1970, 1, 1);
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
            .GreaterThanOrEqualTo(epoch)
            .LessThanOrEqualTo(now)
            .When(x => x.Since.HasValue)
            .WithMessage(
                ValidationMessages.SinceInvalid);

        // Until
        RuleFor(x => x.Until)
            .GreaterThanOrEqualTo(epoch)
            .LessThanOrEqualTo(now)
            .When(x => x.Until.HasValue)
            .WithMessage( ValidationMessages.UntilInvalid);

        // Since <= Until
        RuleFor(x => x)
            .Must(x => !x.Since.HasValue || !x.Until.HasValue || x.Since <= x.Until)
            .WithMessage(string.Format(
                ValidationMessages.InvalidDateRange,
                ErrorFields.Since,
                ErrorFields.Until));

        // Author
        RuleFor(x => x.Author)
            .MaximumLength(AppLimits.MaxUsernameLength)
            .When(x => !string.IsNullOrWhiteSpace(x.Author))
            .WithMessage(string.Format(
                ValidationMessages.TooLong,
                ErrorFields.Author,
                AppLimits.MaxUsernameLength));

        // Committer
        RuleFor(x => x.Committer)
            .MaximumLength(AppLimits.MaxUsernameLength)
            .When(x => !string.IsNullOrWhiteSpace(x.Committer))
            .WithMessage(string.Format(
                ValidationMessages.TooLong,
                ErrorFields.Committer,
                AppLimits.MaxUsernameLength));

    }
}