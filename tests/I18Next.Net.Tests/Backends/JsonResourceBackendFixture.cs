﻿using FluentAssertions;
using I18Next.Net.Backends;
using I18Next.Net.TranslationTrees;
using NUnit.Framework;
using System.Threading.Tasks;

namespace I18Next.Net.Tests.Backends;

[TestFixture]
public class JsonResourceBackendFixture
{
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _backend = new JsonResourceBackend("TestFiles", typeof(JsonResourceBackendFixture).Assembly);
        _tree = await _backend.LoadNamespaceAsync("en-US", "testResource");
    }

    private JsonResourceBackend _backend;
    private ITranslationTree _tree;

    [Test]
    public async Task LoadNamespaceAsync_ExtractLanguagePart_ShouldProvideTranslationsForOnlyTheLanguagePart()
    {
        var tree = await _backend.LoadNamespaceAsync("de-DE", "testResource");

        tree.Should().NotBeNull();

        _tree.GetValue("Value1", null).Should().Be("Translated value 1");
        _tree.GetValue("Value2", null).Should().Be("Translated value 2");
        _tree.GetValue("Value3", null).Should().Be("  Translated value 3   ");
        _tree.GetValue("Value4", null).Should().Be("Translated value 4");
        _tree.GetValue("Value5", null).Should().Be("Translated value 5");
        _tree.GetValue("Value6", null).Should().Be("Translated value 6");
    }

    [Test]
    public void LoadNamespaceAsync_NestedKeys_ShouldProvideCorrectTranslations()
    {
        _tree.Should().NotBeNull();

        _tree.GetValue("SectionA.Value1", null).Should().Be("Translated value 1");
        _tree.GetValue("SectionA.Value2", null).Should().Be("Translated value 2");
        _tree.GetValue("SectionA.Value3", null).Should().Be("  Translated value 3   ");
        _tree.GetValue("SectionA.Value4", null).Should().Be("Translated value 4");
        _tree.GetValue("SectionA.Value5", null).Should().Be("Translated value 5");
        _tree.GetValue("SectionA.Value6", null).Should().Be("Translated value 6");

        _tree.GetValue("SectionB.SubSectionA.Value1", null).Should().Be("Translated value 1");
        _tree.GetValue("SectionB.SubSectionA.Value2", null).Should().Be("Translated value 2");
        _tree.GetValue("SectionB.SubSectionA.Value3", null).Should().Be("  Translated value 3   ");
        _tree.GetValue("SectionB.SubSectionA.Value4", null).Should().Be("Translated value 4");
        _tree.GetValue("SectionB.SubSectionA.Value5", null).Should().Be("Translated value 5");
        _tree.GetValue("SectionB.SubSectionA.Value6", null).Should().Be("Translated value 6");
    }

    [Test]
    public void LoadNamespaceAsync_RootKeys_ShouldProvideCorrectTranslations()
    {
        _tree.Should().NotBeNull();

        _tree.GetValue("Value1", null).Should().Be("Translated value 1");
        _tree.GetValue("Value2", null).Should().Be("Translated value 2");
        _tree.GetValue("Value3", null).Should().Be("  Translated value 3   ");
        _tree.GetValue("Value4", null).Should().Be("Translated value 4");
        _tree.GetValue("Value5", null).Should().Be("Translated value 5");
        _tree.GetValue("Value6", null).Should().Be("Translated value 6");
    }
}
