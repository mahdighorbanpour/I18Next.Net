using I18Next.Net.Plugins;
using NUnit.Framework;

namespace I18Next.Net.Tests.Plugins;

[TestFixture]
public class DefaultPluralResolverFixture
{
    [TestCase(JsonFormat.Version1, ExpectedResult = "")]
    [TestCase(JsonFormat.Version2, ExpectedResult = "")]
    [TestCase(JsonFormat.Version3, ExpectedResult = "")]
    [TestCase(JsonFormat.Version4, ExpectedResult = "_one")]
    public string GetPluralSuffix_OneInEnglish_ShouldReturnEmptyWhenUsingSimpleSuffix(JsonFormat jsonFormatVersion)
    {
        var pluralResolver = new DefaultPluralResolver()
        {
            JsonFormatVersion = jsonFormatVersion,
            UseSimplePluralSuffixIfPossible = true
        };

        return pluralResolver.GetPluralSuffix("en", 1);
    }

    [TestCase(JsonFormat.Version1, ExpectedResult = "")]
    [TestCase(JsonFormat.Version2, ExpectedResult = "_1")]
    [TestCase(JsonFormat.Version3, ExpectedResult = "_0")]
    [TestCase(JsonFormat.Version4, ExpectedResult = "_one")]
    public string GetPluralSuffix_OneInEnglish_ShouldReturnNumberWhenNotUsingSimpleSuffix(JsonFormat jsonFormatVersion)
    {
        var pluralResolver = new DefaultPluralResolver()
        {
            JsonFormatVersion = jsonFormatVersion,
            UseSimplePluralSuffixIfPossible = false
        };

        return pluralResolver.GetPluralSuffix("en", 1);
    }

    [TestCase(JsonFormat.Version1, ExpectedResult = "_plural")]
    [TestCase(JsonFormat.Version2, ExpectedResult = "_plural")]
    [TestCase(JsonFormat.Version3, ExpectedResult = "_plural")]
    [TestCase(JsonFormat.Version4, ExpectedResult = "_other")]
    public string GetPluralSuffix_TwoInEnglish_ShouldReturnPluralWhenUsingSimpleSuffix(JsonFormat jsonFormatVersion)
    {
        var pluralResolver = new DefaultPluralResolver()
        {
            JsonFormatVersion = jsonFormatVersion,
            UseSimplePluralSuffixIfPossible = true
        };

        return pluralResolver.GetPluralSuffix("en", 2);
    }

    [TestCase(JsonFormat.Version1, 2, ExpectedResult = "_plural")]
    [TestCase(JsonFormat.Version2, 2, ExpectedResult = "_plural")]
    [TestCase(JsonFormat.Version3, 2, ExpectedResult = "_plural")]
    [TestCase(JsonFormat.Version4, 0, ExpectedResult = "_other")]
    [TestCase(JsonFormat.Version4, 1, ExpectedResult = "_one")]
    [TestCase(JsonFormat.Version4, 2, ExpectedResult = "_other")]
    [TestCase(JsonFormat.Version4, 3, ExpectedResult = "_other")]
    [TestCase(JsonFormat.Version4, 10, ExpectedResult = "_other")]
    [TestCase(JsonFormat.Version4, 11, ExpectedResult = "_other")]
    [TestCase(JsonFormat.Version4, 100, ExpectedResult = "_other")]
    public string GetPluralSuffix_English_ShouldReturnPluralWhenUsingSimpleSuffix(JsonFormat jsonFormatVersion, int count)
    {
        var pluralResolver = new DefaultPluralResolver()
        {
            JsonFormatVersion = jsonFormatVersion,
            UseSimplePluralSuffixIfPossible = true
        };

        return pluralResolver.GetPluralSuffix("en", count);
    }

    [TestCase(JsonFormat.Version1, ExpectedResult = "_2")]
    [TestCase(JsonFormat.Version2, ExpectedResult = "_2")]
    [TestCase(JsonFormat.Version3, ExpectedResult = "_1")]
    [TestCase(JsonFormat.Version4, ExpectedResult = "_two")]
    public string GetPluralSuffix_TwoInEnglish_ShouldReturnNumberWhenNotUsingSimpleSuffix(JsonFormat jsonFormatVersion)
    {
        var pluralResolver = new DefaultPluralResolver()
        {
            JsonFormatVersion = jsonFormatVersion,
            UseSimplePluralSuffixIfPossible = false
        };

        return pluralResolver.GetPluralSuffix("en", 2);
    }

    [TestCase(JsonFormat.Version1, ExpectedResult = "")]
    [TestCase(JsonFormat.Version2, ExpectedResult = "")]
    [TestCase(JsonFormat.Version3, ExpectedResult = "_0")]
    [TestCase(JsonFormat.Version4, ExpectedResult = "_one")]
    public string GetPluralSuffix_OneInJapanese_ShouldReturnNumber(JsonFormat jsonFormatVersion)
    {
        var pluralResolver = new DefaultPluralResolver()
        {
            JsonFormatVersion = jsonFormatVersion,
        };

        return pluralResolver.GetPluralSuffix("ja", 1);
    }

    [TestCase(JsonFormat.Version1, ExpectedResult = "")]
    [TestCase(JsonFormat.Version2, ExpectedResult = "")]
    [TestCase(JsonFormat.Version3, ExpectedResult = "_0")]
    [TestCase(JsonFormat.Version4, ExpectedResult = "_one")]
    public string GetPluralSuffix_TwoInJapanese_ShouldReturnNumber(JsonFormat jsonFormatVersion)
    {
        var pluralResolver = new DefaultPluralResolver()
        {
            JsonFormatVersion = jsonFormatVersion,
        };

        return pluralResolver.GetPluralSuffix("ja", 2);
    }


    [TestCase(JsonFormat.Version1, 2, ExpectedResult = "_2")]
    [TestCase(JsonFormat.Version2, 2, ExpectedResult = "_2")]
    [TestCase(JsonFormat.Version3, 2, ExpectedResult = "_2")]
    [TestCase(JsonFormat.Version4, 0, ExpectedResult = "_zero")]
    [TestCase(JsonFormat.Version4, 1, ExpectedResult = "_one")]
    [TestCase(JsonFormat.Version4, 2, ExpectedResult = "_two")]
    [TestCase(JsonFormat.Version4, 3, ExpectedResult = "_few")]
    [TestCase(JsonFormat.Version4, 10, ExpectedResult = "_few")]
    [TestCase(JsonFormat.Version4, 11, ExpectedResult = "_many")]
    [TestCase(JsonFormat.Version4, 100, ExpectedResult = "_other")]
    public string GetPluralSuffix_Arabic_ShouldReturn_Correct(JsonFormat jsonFormatVersion, int count)
    {
        var pluralResolver = new DefaultPluralResolver()
        {
            JsonFormatVersion = jsonFormatVersion,
        };

        return pluralResolver.GetPluralSuffix("ar", count);
    }
}
