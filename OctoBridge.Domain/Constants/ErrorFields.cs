namespace OctoBridge.Domain.Constants;

public static class ErrorFields
{
    public const string General = "General";

    public const string RepositoryOwner = nameof(RepositoryOwner);
    public const string RepositoryName = nameof(RepositoryName);

    public const string Title = nameof(Title);
    public const string Body = nameof(Body);

    public const string Labels = nameof(Labels);
    public const string Assignees = nameof(Assignees);

    public const string Head = nameof(Head);
    public const string Base = nameof(Base);
    public const string HeadRepo = nameof(HeadRepo);

    public const string Issue = nameof(Issue);

    public const string PageSize = nameof(PageSize);
    public const string PageNumber = nameof(PageNumber);

    public const string Since = nameof(Since);
    public const string Until = nameof(Until);

    public const string Author = nameof(Author);
    public const string Committer = nameof(Committer);

    public const string Token = nameof(Token);
    public const string Email = nameof(Email);
    public const string Authentication = nameof(Authentication);
    public const string Authorization = nameof(Authorization);
    public const string PAT = nameof(PAT);
}