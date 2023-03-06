using RatingAPI.Core.Abstraction;

namespace RatingAPI.Infrastructure.Services;

public class TextAnalyzerService : ITextAnalyzer
{
    private static IEnumerable<string> RestrictedContent = new List<string>()
    {
        "banned_word1", "banned_word2", "banned_word3"
    };

    public TextAnalyzerService()
    {
    }
    
    public bool DoesTextContainRestrictedContent(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return false;
        }
        
        var words = text.Split(",.- ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        foreach (var word in words)
        {
            if (RestrictedContent.Contains(word)) 
                return true;
        }

        return false;
    }
}