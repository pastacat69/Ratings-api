using NUnit.Framework;
using RatingAPI.Infrastructure.Services;

namespace RatingsAPI.Tests.Unit;

public class TextAnalyzerTests
{
    public class DoesTextContainRestrictedContent
    {
        [TestCase("Some banned_word1")]
        [TestCase("Some banned_word3")]
        [TestCase("Some banned_word2")]
        [TestCase("Some lala banned_word3")]
        public void Should_ReturnTrue_When_RestrictedContentProvided(string text)
        {
            var analyzer = new TextAnalyzerService();
            var result = analyzer.DoesTextContainRestrictedContent(text);

            Assert.IsTrue(result);
        }
        
        [TestCase("No restricted content")]
        [TestCase("No banne words")]
        [TestCase("Happy")]
        [TestCase("Some lala")]
        public void Should_ReturnFalse_When_NoRestrictedContentProvided(string text)
        {
            var analyzer = new TextAnalyzerService();
            var result = analyzer.DoesTextContainRestrictedContent(text);

            Assert.IsFalse(result);
        }
    }
}