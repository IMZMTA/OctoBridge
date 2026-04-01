using FluentValidation;
using OctoBridge.Domain.Constants;
using OctoBridge.Domain.Constants.App;
using OctoBridge.Domain.Constants.Messages;
using OctoBridge.Application.CQRS.Abstractions;

namespace OctoBridge.Application.Features.Github.Command.CreatePullRequest;

public class CreatePullRequestCommandValidator : BaseValidator<CreatePullRequestRequestDto>
{
    public CreatePullRequestCommandValidator()
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
            .MaximumLength(AppLimits.MaxIssueTitleLength)
            .WithMessage(string.Format(ValidationMessages.TooLong,ErrorFields.Title, AppLimits.MaxIssueTitleLength))
            .When(x => !string.IsNullOrWhiteSpace(x.Title));
        
        RuleFor(x => x.Title)
            .NotEmpty()
            .When(x => !x.Issue.HasValue)
            .WithMessage(ValidationMessages.TitleRequiredWhenNoIssue);
        
        RuleFor(x => x.Issue)
            .GreaterThan(0)
            .When(x => x.Issue.HasValue)
            .WithMessage(string.Format(ValidationMessages.MustBePositive,ErrorFields.Issue));

        RuleFor(x => x)
            .Must(x => ( x.Issue.HasValue && string.IsNullOrWhiteSpace(x.Title)) ||
                (!x.Issue.HasValue && !string.IsNullOrWhiteSpace(x.Title)))
            .WithMessage(ValidationMessages.TitleRequiredWhenNoIssue);

        RuleFor(x => x.Body)
            .MaximumLength(AppLimits.MaxIssueBodyLength)
            .WithMessage(string.Format(ValidationMessages.TooLong,ErrorFields.Body, AppLimits.MaxIssueBodyLength));


        RuleFor(x => x.Head)
            .NotEmpty()
            .WithMessage(string.Format(ValidationMessages.Required,ErrorFields.Head))
            .MaximumLength(AppLimits.MaxBranchNameLength)
            .Matches(RegexConstants.GitHubHeadBranch)
            .WithMessage(ValidationMessages.HeadInvalid);

        RuleFor(x => x.Base)
            .NotEmpty()
            .WithMessage(string.Format(ValidationMessages.Required,ErrorFields.Base))
            .MaximumLength(AppLimits.MaxBranchNameLength)
            .Matches(RegexConstants.GitHubBaseBranch)
            .WithMessage(ValidationMessages.BaseInvalid);
        
        
        RuleFor(x => x.HeadRepo)
            .NotEmpty()
            .When(x => !string.IsNullOrEmpty(x.Head) && x.Head.Contains(":"))
            .WithMessage(ValidationMessages.HeadRepoRequired);

    }
}