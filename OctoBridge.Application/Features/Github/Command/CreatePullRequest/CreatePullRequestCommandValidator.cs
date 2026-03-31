using FluentValidation;
using OctoBridge.Domain.Constants;
using OctoBridge.Application.CQRS.Abstractions;

namespace OctoBridge.Application.Features.Github.Command.CreatePullRequest;

public class CreatePullRequestCommandValidator : BaseValidator<CreatePullRequestRequestDto>
{
    public CreatePullRequestCommandValidator()
    {

        RuleFor(x => x.RepositoryOwner)
            .NotEmpty()
            .WithMessage(Messages.RepositoryOwnerRequired);

        RuleFor(x => x.RepositoryName)
            .NotEmpty()
            .WithMessage(Messages.RepositoryNameRequired);

        RuleFor(x => x.Title)
            .MaximumLength(AppConstants.MaxIssueTitleLength)
            .WithMessage(string.Format(Messages.TitleTooLong, AppConstants.MaxIssueTitleLength))
            .When(x => !string.IsNullOrWhiteSpace(x.Title));
        
        RuleFor(x => x.Title)
            .NotEmpty()
            .When(x => !x.Issue.HasValue)
            .WithMessage(Messages.TitleRequiredWhenNoIssue);
        
        RuleFor(x => x.Issue)
            .GreaterThan(AppConstants.Zero)
            .When(x => x.Issue.HasValue)
            .WithMessage(Messages.IssueMustBePositive);

        RuleFor(x => x)
            .Must(x => ( x.Issue.HasValue && string.IsNullOrWhiteSpace(x.Title)) ||
                (!x.Issue.HasValue && !string.IsNullOrWhiteSpace(x.Title)))
            .WithMessage(Messages.EitherTitleOrIssue);

        RuleFor(x => x.Body)
            .MaximumLength(AppConstants.MaxIssueBodyLength)
            .WithMessage(string.Format(Messages.BodyTooLong, AppConstants.MaxIssueBodyLength));


        RuleFor(x => x.Head)
            .NotEmpty()
            .WithMessage(Messages.HeadRequired)
            .MaximumLength(AppConstants.MaxBranchNameLength)
            .Matches(RegexConstants.GitHubHeadBranch)
            .WithMessage(Messages.HeadInvalid);

        RuleFor(x => x.Base)
            .NotEmpty()
            .WithMessage(Messages.BaseRequired)
            .MaximumLength(AppConstants.MaxBranchNameLength)
            .Matches(RegexConstants.GitHubBaseBranch)
            .WithMessage(Messages.BaseInvalid);
        
        
        RuleFor(x => x.HeadRepo)
            .NotEmpty()
            .When(x => !string.IsNullOrEmpty(x.Head) && x.Head.Contains(":"))
            .WithMessage(Messages.HeadRepoRequired);

    }
}