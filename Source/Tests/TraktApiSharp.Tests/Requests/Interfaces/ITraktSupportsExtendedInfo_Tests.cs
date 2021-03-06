﻿namespace TraktApiSharp.Tests.Requests.Interfaces
{
    using FluentAssertions;
    using System.Linq;
    using Traits;
    using TraktApiSharp.Requests.Interfaces;
    using TraktApiSharp.Requests.Parameters;
    using Xunit;

    [Category("Requests.Interfaces")]
    public class ITraktSupportsExtendedInfo_Tests
    {
        [Fact]
        public void Test_ITraktSupportsExtendedInfo_Is_Interface()
        {
            typeof(ITraktSupportsExtendedInfo).IsInterface.Should().BeTrue();
        }

        [Fact]
        public void Test_ITraktSupportsExtendedInfo_Has_ExtendedInfo_Property()
        {
            var propertyInfo = typeof(ITraktSupportsExtendedInfo).GetProperties()
                                                                 .Where(p => p.Name == "ExtendedInfo")
                                                                 .FirstOrDefault();

            propertyInfo.CanRead.Should().BeTrue();
            propertyInfo.CanWrite.Should().BeTrue();
            propertyInfo.PropertyType.Should().Be(typeof(TraktExtendedInfo));
        }
    }
}
