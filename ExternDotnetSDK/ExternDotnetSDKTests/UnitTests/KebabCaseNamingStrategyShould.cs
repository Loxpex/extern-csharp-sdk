﻿using System.Linq;
using ExternDotnetSDK.Accounts;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ExternDotnetSDKTests.UnitTests
{
    [TestFixture]
    internal class KebabCaseNamingStrategyShould
    {
        [Test]
        public void SerializeClassesCorrectly()
        {
            var serialized = JsonConvert.SerializeObject(new AccountList());
            Assert.False(serialized.Any(char.IsUpper));
            serialized.Should().Contain("-");
        }
    }
}