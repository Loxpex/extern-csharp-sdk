﻿using System;
using FluentAssertions;
using Kontur.Extern.Client.Models.Common;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.OtherTests
{
    [TestFixture]
    internal class LinkShould
    {
        private Link link;
        private Uri href;
        private string rel;

        [OneTimeSetUp]
        public void SetUp()
        {
            rel = "index";
            href = new Uri("http://htmlbook.ru/");
            link = new Link(href, rel);
        }

        [Test]
        public void BeNotEqual_ToDifferentLinks()
        {
            link.Should().NotBeNull();
            link.Should().NotBe(new Link(href, "difference"));
            link.Should().NotBe(new Link(new Uri("http://htmlbook.ru/html"), rel));
            link.Should().NotBe(new Link(href, rel, "extra name"));
        }

        [Test]
        public void Throw_WhenConstructedWithBadParameters()
        {
            Assert.Throws<ArgumentNullException>(() => link = new Link(href, null));
            Assert.Throws<ArgumentNullException>(() => link = new Link(null, rel));
        }

        [Test]
        public void BeEqual_ToSameLinks()
        {
            var similarLink = new Link(link.Href, link.Rel, link.Profile, link.Title, link.Profile, link.Templated);
            link.Should().Be(similarLink);
        }

        [Test]
        public void BeEqual_ToSelf()
        {
            var sameLink = link;
            link.Should().Be(sameLink);
        }

        [TestCase("NAME", "TITLE", "PROFILE", true)]
        [TestCase("NAME", "TITLE", "PROFILE", false)]
        [TestCase("NAME", "TITLE", null, false)]
        [TestCase("NAME", null, null, false)]
        [TestCase(null, null, null, false)]
        public void StringifyCorrectly(string name, string title, string profile, bool templated)
        {
            var testLink = new Link(new Uri("http://htmlbook.ru/"), "index", name, title, profile, templated);
            var testString = testLink.ToString();
            testString.Should().ContainAll("<", "/>", "href=", "rel=");
            if (templated)
                testString.Should().Contain("templated=\"true\" ");
            if (name != null)
                testString.Should().Contain($"name=\"{name}\" ");
            if (title != null)
                testString.Should().Contain($"title=\"{title}\" ");
            if (profile != null)
                testString.Should().Contain($"profile=\"{profile}\" ");
        }
    }
}