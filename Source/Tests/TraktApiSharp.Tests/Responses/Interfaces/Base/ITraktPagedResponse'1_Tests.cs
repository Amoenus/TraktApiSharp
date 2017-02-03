﻿namespace TraktApiSharp.Tests.Responses.Interfaces.Base
{
    using FluentAssertions;
    using TraktApiSharp.Experimental.Responses.Interfaces.Base;
    using TraktApiSharp.Tests.Traits;
    using Xunit;

    [Category("Responses.Interfaces.Base")]
    public class ITraktPagedResponse_1_Tests
    {
        [Fact]
        public void Test_ITraktPagedResponse_1_Is_Interface()
        {
            typeof(ITraktPagedResponse<>).IsInterface.Should().BeTrue();
        }

        [Fact]
        public void Test_ITraktPagedResponse_1_Has_GenericTypeParameter()
        {
            typeof(ITraktPagedResponse<>).ContainsGenericParameters.Should().BeTrue();
            typeof(ITraktPagedResponse<int>).GenericTypeArguments.Should().NotBeEmpty().And.HaveCount(1);
        }

        [Fact]
        public void Test_ITraktPagedResponse_1_Inherits_ITraktListResponse_1_Interface()
        {
            typeof(ITraktPagedResponse<int>).GetInterfaces().Should().Contain(typeof(ITraktListResponse<int>));
        }

        [Fact]
        public void Test_ITraktPagedResponse_1_Inherits_ITraktPagedResponseHeaders_Interface()
        {
            typeof(ITraktPagedResponse<>).GetInterfaces().Should().Contain(typeof(ITraktPagedResponseHeaders));
        }
    }
}
