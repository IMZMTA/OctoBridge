using FluentValidation;
using OctoBridge.Domain.Enums;
using OctoBridge.Domain.Constants.Messages;
using OctoBridge.Domain.Constants.External;
using OctoBridge.Application.CQRS.Abstractions;

namespace OctoBridge.Application.Features.Github.Queries.GetRepositories;

public class GetRepositoriesCommandValidator : BaseValidator<GetRepositoriesRequestDto>
{
    public GetRepositoriesCommandValidator()
    {

        RuleFor(x => x.OwnerName)
            .Must(name => !string.IsNullOrWhiteSpace(name))
            .When(x => x.OwnerType != RepoOwnerType.Authenticated)
            .WithMessage(ValidationMessages.OwnerNameRequired);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(GitHubConstants.MaxPageSize)
            .WithMessage(string.Format(ValidationMessages.PageSizeInvalid, GitHubConstants.MaxPageSize));

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
                .WithMessage(ValidationMessages.PageNumberInvalid);

        RuleFor(x => x.Since)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .When(x => x.Since.HasValue)
            .WithMessage(ValidationMessages.SinceInvalid);

        RuleFor(x => x.Affiliations)
            .Must(a => a == null || a.Count != 0)
            .WithMessage(ValidationMessages.AffiliationsEmpty);

        RuleFor(x => x.Affiliations)
            .Must(a => a == null || a.All(v => Enum.IsDefined(v)))
            .When(x => x.Affiliations != null)
            .WithMessage(ValidationMessages.InvalidAffiliation);

        RuleFor(x => x.Visibility)
            .IsInEnum()
            .When(x => x.Visibility.HasValue)
            .WithMessage(ValidationMessages.InvalidVisibility);

        RuleFor(x => x.Type)
            .IsInEnum()
            .When(x => x.Type.HasValue)
            .WithMessage(ValidationMessages.InvalidType);

        RuleFor(x => x.Sort)
            .IsInEnum()
            .WithMessage(ValidationMessages.InvalidSort);

        RuleFor(x => x.Direction)
            .IsInEnum()
            .WithMessage(ValidationMessages.InvalidDirection);

        RuleFor(x => x)
        .Must(x =>
        {
            bool hasType = x.Type.HasValue;
            bool hasVisibility = x.Visibility.HasValue && x.Visibility != RepoVisibility.All;
            bool hasAffiliation = x.Affiliations != null && x.Affiliations.Any();

            return !(hasType && (hasVisibility || hasAffiliation));
        })
        .WithMessage(ValidationMessages.InvalidTypeCombination);

    }
}