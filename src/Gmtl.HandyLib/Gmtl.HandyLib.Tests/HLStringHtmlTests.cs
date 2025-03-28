// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLStringHtmlTests.cs" project="Gmtl.HandyLib.Tests" date="2015-10-22 20:24">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using Gmtl.HandyLib;
using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class HLStringHtmlTests
{
    [Test]
    public void FilterHtml_RemovesAllTags_WhenAllowedTagsIsEmpty()
    {
        string input = "<b>Hello</b> <i>World</i>!";
        string result = input.FilterHtml(new List<string>(), new List<string>());
        Assert.AreEqual("<b>Hello</b> <i>World</i>!", result);
    }

    [Test]
    public void FilterHtml_OnlyKeepsAllowedTags()
    {
        string input = "<b>Hello</b> <i>World</i>!";
        string result = input.FilterHtml(new List<string> { "b" }, new List<string>());
        Assert.AreEqual("<b>Hello</b> World!", result);
    }

    [Test]
    public void FilterHtml_RemovesAttributes_WhenAllowedAttributesIsEmpty()
    {
        string input = "<a href=\"#\" class=\"btn\">Click</a>";
        string result = input.FilterHtml(new List<string> { "a" }, new List<string>());
        Assert.AreEqual("<a>Click</a>", result);
    }

    [Test]
    public void FilterHtml_KeepsAllowedAttributes()
    {
        string input = "<a href=\"#\" class=\"btn\">Click</a>";
        string result = input.FilterHtml(new List<string> { "a" }, new List<string> { "href" });
        Assert.AreEqual("<a href=\"#\">Click</a>", result);
    }

    [Test]
    public void FilterHtml_IgnoresCaseInTags()
    {
        string input = "<B>Hello</B> <I>World</I>!";
        string result = input.FilterHtml(new List<string> { "b" }, new List<string>());
        Assert.AreEqual("<b>Hello</b> World!", result);
    }

    [Test]
    public void FilterHtml_KeepsNestedTags()
    {
        string input = "<div><p><b>Bold</b> and <i>Italic</i></p></div>";
        string result = input.FilterHtml(new List<string> { "p", "b" }, new List<string>());
        Assert.AreEqual("<p><b>Bold</b> and Italic</p>", result);
    }

    [Test]
    public void FilterHtml_KeepsNestedTagsWithAttributes()
    {
        string input = "<div class=\"container\"><p id=\"text\"><b style=\"color:red;\">Bold</b></p></div>";
        string result = input.FilterHtml(new List<string> { "p", "b" }, new List<string> { "id", "style" });
        Assert.AreEqual("<p id=\"text\"><b style=\"color:red;\">Bold</b></p>", result);
    }

    [Test]
    public void FilterHtml_RemovesDisallowedNestedTags()
    {
        string input = "<div><span>Hello <b>World</b></span></div>";
        string result = input.FilterHtml(new List<string> { "b" }, new List<string>());
        Assert.AreEqual("Hello <b>World</b>", result);
    }

    [Test]
    public void FilterHtml_RemovesAttributesFromNestedTags()
    {
        string input = "<div class=\"container\"><p id=\"text\"><b style=\"color:red;\">Bold</b></p></div>";
        string result = input.FilterHtml(new List<string> { "p", "b" }, new List<string>());
        Assert.AreEqual("<p><b>Bold</b></p>", result);
    }
}