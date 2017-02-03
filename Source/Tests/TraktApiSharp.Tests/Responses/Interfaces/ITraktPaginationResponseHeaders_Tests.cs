﻿namespace TraktApiSharp.Tests.Responses.Interfaces
{
    using FluentAssertions;
    using System.Linq;
    using TraktApiSharp.Experimental.Responses.Interfaces.Base;
    using TraktApiSharp.Tests.Traits;
    using Xunit;

    [Category("Responses.Interfaces")]
    public class ITraktPaginationResponseHeaders_Tests
    {
        [Fact]
        public void Test_ITraktPaginationResponseHeaders_Is_Interface()
        {
            typeof(ITraktPaginationResponseHeaders).IsInterface.Should().BeTrue();
        }

        [Fact]
        public void Test_ITraktPaginationResponseHeaders_Has_Page_Property()
        {
            var userCountPropertyInfo = typeof(ITraktPaginationResponseHeaders).GetProperties()
                                                                               .Where(p => p.Name == "Page")
                                                                               .FirstOrDefault();

            userCountPropertyInfo.CanRead.Should().BeTrue();
            userCountPropertyInfo.CanWrite.Should().BeTrue();
            userCountPropertyInfo.PropertyType.Should().Be(typeof(int?));
        }

        [Fact]
        public void Test_ITraktPaginationResponseHeaders_Has_Limit_Property()
        {
            var userCountPropertyInfo = typeof(ITraktPaginationResponseHeaders).GetProperties()
                                                                               .Where(p => p.Name == "Limit")
                                                                               .FirstOrDefault();

            userCountPropertyInfo.CanRead.Should().BeTrue();
            userCountPropertyInfo.CanWrite.Should().BeTrue();
            userCountPropertyInfo.PropertyType.Should().Be(typeof(int?));
        }

        [Fact]
        public void Test_ITraktPaginationResponseHeaders_Has_PageCount_Property()
        {
            var userCountPropertyInfo = typeof(ITraktPaginationResponseHeaders).GetProperties()
                                                                               .Where(p => p.Name == "PageCount")
                                                                               .FirstOrDefault();

            userCountPropertyInfo.CanRead.Should().BeTrue();
            userCountPropertyInfo.CanWrite.Should().BeTrue();
            userCountPropertyInfo.PropertyType.Should().Be(typeof(int?));
        }

        [Fact]
        public void Test_ITraktPaginationResponseHeaders_Has_ItemCount_Property()
        {
            var userCountPropertyInfo = typeof(ITraktPaginationResponseHeaders).GetProperties()
                                                                               .Where(p => p.Name == "ItemCount")
                                                                               .FirstOrDefault();

            userCountPropertyInfo.CanRead.Should().BeTrue();
            userCountPropertyInfo.CanWrite.Should().BeTrue();
            userCountPropertyInfo.PropertyType.Should().Be(typeof(int?));
        }
    }
}
