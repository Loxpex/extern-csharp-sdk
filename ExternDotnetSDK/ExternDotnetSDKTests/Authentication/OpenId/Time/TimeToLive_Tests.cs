using System;
using FluentAssertions;
using FluentAssertions.Extensions;
using Kontur.Extern.Client.Authentication.OpenId.Time;
using NSubstitute;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.Authentication.OpenId.Time
{
    [TestFixture]
    internal class TimeToLive_Tests
    {
        [TestFixture]
        internal class Ctor
        {
            [Test]
            public void Should_fail_when_given_the_empty_interval()
            {
                Action action = () => _ = new TimeToLive(default, Substitute.For<IStopwatch>());

                action.Should().Throw<ArgumentException>();
            }
        }

        [TestFixture]
        internal class HasPassed
        {
            [Test]
            public void Should_indicate_that_TTL_has_not_been_passed_yet()
            {
                var stopwatch = Substitute.For<IStopwatch>();
                var timeToLive = new TimeToLive(1.Seconds(), stopwatch);

                timeToLive.HasExpired.Should().BeFalse();
            }
            
            [Test]
            public void Should_indicate_that_TTL_has_been_passed()
            {
                var stopwatch = Substitute.For<IStopwatch>();
                stopwatch.Elapsed.Returns(11.Seconds());
                var timeToLive = new TimeToLive(10.Seconds(), stopwatch);

                timeToLive.HasExpired.Should().BeTrue();
            }
        }

        [TestFixture]
        internal class WillExpireAfter
        {
            [Test]
            public void Should_indicate_that_TTL_will_not_pass_after_specified_interval()
            {
                var stopwatch = Substitute.For<IStopwatch>();
                stopwatch.Elapsed.Returns(1.Seconds());
                var timeToLive = new TimeToLive(10.Seconds(), stopwatch);

                timeToLive.WillExpireAfter(8.Seconds()).Should().BeFalse();
            }
            
            [Test]
            public void Should_indicate_that_TTL_will_pass_after_specified_interval()
            {
                var stopwatch = Substitute.For<IStopwatch>();
                stopwatch.Elapsed.Returns(1.Seconds());
                var timeToLive = new TimeToLive(10.Seconds(), stopwatch);

                timeToLive.WillExpireAfter(9.Seconds()).Should().BeTrue();
            }
            
            [Test]
            public void Should_indicate_that_TTL_will_pass_after_the_given_interval_when_the_time_is_not_elapsed_yet()
            {
                var stopwatch = Substitute.For<IStopwatch>();
                var timeToLive = new TimeToLive(10.Seconds(), stopwatch);

                timeToLive.WillExpireAfter(10.Seconds()).Should().BeTrue();
            }
            
            [Test]
            public void Should_indicate_that_TTL_will_pass_after_the_given_interval_when_elapsed_only_part_of_the_TTL()
            {
                var stopwatch = Substitute.For<IStopwatch>();
                stopwatch.Elapsed.Returns(1.Seconds());
                var timeToLive = new TimeToLive(10.Seconds(), stopwatch);

                timeToLive.WillExpireAfter(10.Seconds()).Should().BeTrue();
            }
        }

        [TestFixture]
        internal class TryCreateActive
        {
            [Test]
            public void Should_successfully_create_TTL_when_it_is_not_expired()
            {
                var stopwatch = Substitute.For<IStopwatch>();
                stopwatch.Elapsed.Returns(0.5.Seconds());
                
                var success = TimeToLive.TryCreateActive(1.Seconds(), stopwatch, out var timeToLive);

                success.Should().BeTrue();
                timeToLive.HasExpired.Should().BeFalse();
            }

            [Test]
            public void Should_return_error_when_the_given_interval_has_been_passed_already()
            {
                var stopwatch = Substitute.For<IStopwatch>();
                stopwatch.Elapsed.Returns(1.Seconds());
                
                var success = TimeToLive.TryCreateActive(1.Seconds(), stopwatch, out var timeToLive);

                success.Should().BeFalse();
                timeToLive.Should().BeNull();
            }
        }
    }
}