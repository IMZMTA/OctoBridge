using FluentValidation;
using OctoBridge.Domain.Enums;
using OctoBridge.Domain.Constants;
using OctoBridge.Application.CQRS.Abstractions;

namespace OctoBridge.Application.Features.Github.Queries.GetRepositories;

public class GetRepositoriesCommandValidator : BaseValidator<GetRepositoriesRequestDto>
{
    public GetRepositoriesCommandValidator()
    {

        RuleFor(x => x.OwnerName)
            .Must(name => !string.IsNullOrWhiteSpace(name))
            .When(x => x.OwnerType != RepoOwnerType.Authenticated)
            .WithMessage(Messages.OwnerNameRequired);

        RuleFor(x => x.PageSize)
            .GreaterThan(AppConstants.Zero)
            .LessThanOrEqualTo(GitHubConstants.MaxPageSize)
            .WithMessage(string.Format(Messages.PageSizeInvalid, GitHubConstants.MaxPageSize));

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(AppConstants.One)
                .WithMessage(Messages.PageNumberInvalid);

        RuleFor(x => x.Since)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .When(x => x.Since.HasValue)
            .WithMessage(Messages.SinceInvalid);

        RuleFor(x => x.Affiliations)
            .Must(a => a == null || a.Count != AppConstants.Zero)
            .WithMessage(Messages.AffiliationsEmpty);

        RuleFor(x => x.Affiliations)
            .Must(a => a == null || a.All(v => Enum.IsDefined(v)))
            .When(x => x.Affiliations != null)
            .WithMessage(Messages.InvalidAffiliation);

        RuleFor(x => x.Visibility)
            .IsInEnum()
            .When(x => x.Visibility.HasValue)
            .WithMessage(Messages.InvalidVisibility);

        RuleFor(x => x.Type)
            .IsInEnum()
            .When(x => x.Type.HasValue)
            .WithMessage(Messages.InvalidType);

        RuleFor(x => x.Sort)
            .IsInEnum()
            .WithMessage(Messages.InvalidSort);

        RuleFor(x => x.Direction)
            .IsInEnum()
            .WithMessage(Messages.InvalidDirection);

        RuleFor(x => x)
        .Must(x =>
        {
            bool hasType = x.Type.HasValue;
            bool hasVisibility = x.Visibility.HasValue && x.Visibility != RepoVisibility.All;
            bool hasAffiliation = x.Affiliations != null && x.Affiliations.Any();

            return !(hasType && (hasVisibility || hasAffiliation));
        })
        .WithMessage(Messages.InvalidTypeCombination);

    }
}