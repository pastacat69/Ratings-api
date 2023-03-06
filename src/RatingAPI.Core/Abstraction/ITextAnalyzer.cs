namespace RatingAPI.Core.Abstraction;

public interface ITextAnalyzer
{
    bool DoesTextContainRestrictedContent(string text);
}