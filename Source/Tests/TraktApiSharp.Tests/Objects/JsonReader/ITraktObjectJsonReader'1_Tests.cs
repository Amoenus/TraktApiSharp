﻿namespace TraktApiSharp.Tests.Objects.JsonReader
{
    using FluentAssertions;
    using Newtonsoft.Json;
    using System.Linq;
    using Traits;
    using TraktApiSharp.Objects.JsonReader;
    using Xunit;

    [Category("Objects.JsonReader")]
    public class ITraktObjectJsonReader_1_Tests
    {
        [Fact]
        public void Test_ITraktObjectJsonReader_1_Is_Interface()
        {
            typeof(ITraktObjectJsonReader<>).IsInterface.Should().BeTrue();
        }

        [Fact]
        public void Test_ITraktObjectJsonReader_1_Has_ReadObject_From_Json_Method()
        {
            var methodInfo = typeof(ITraktObjectJsonReader<object>).GetMethods()
                .Where(m => m.Name == "ReadObject" && m.GetParameters().Length == 1)
                .FirstOrDefault(m => m.GetParameters()[0].ParameterType == typeof(string));

            methodInfo.ReturnType.Should().Be(typeof(object));
            methodInfo.GetParameters().Should().NotBeEmpty().And.HaveCount(1);

            var parameterInfo = methodInfo.GetParameters().First();

            parameterInfo.Should().NotBeNull();
            parameterInfo.ParameterType.Should().Be(typeof(string));
            parameterInfo.Name.Should().Be("json");
        }

        [Fact]
        public void Test_ITraktObjectJsonReader_1_Has_ReadObject_From_JsonReader_Method()
        {
            var methodInfo = typeof(ITraktObjectJsonReader<object>).GetMethods()
                .Where(m => m.Name == "ReadObject" && m.GetParameters().Length == 1)
                .FirstOrDefault(m => m.GetParameters()[0].ParameterType == typeof(JsonTextReader));

            methodInfo.ReturnType.Should().Be(typeof(object));
            methodInfo.GetParameters().Should().NotBeEmpty().And.HaveCount(1);

            var parameterInfo = methodInfo.GetParameters().First();

            parameterInfo.Should().NotBeNull();
            parameterInfo.ParameterType.Should().Be(typeof(JsonTextReader));
            parameterInfo.Name.Should().Be("jsonReader");
        }
    }
}
