﻿namespace TraktApiSharp.Tests.Requests.Shows
{
    using FluentAssertions;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Traits;
    using TraktApiSharp.Enums;
    using TraktApiSharp.Requests.Base;
    using TraktApiSharp.Requests.Parameters;
    using TraktApiSharp.Requests.Shows;
    using Xunit;

    [Category("Requests.Shows.Lists")]
    public class ATraktShowsMostPWCRequest_1_Tests
    {
        internal class TraktShowsMostPWCRequestMock : ATraktShowsMostPWCRequest<int>
        {
            public override string UriTemplate { get { throw new NotImplementedException(); } }

            public override void Validate()
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void Test_ATraktShowsMostPWCRequest_1_Is_Abstract()
        {
            typeof(ATraktShowsMostPWCRequest<>).IsAbstract.Should().BeTrue();
        }

        [Fact]
        public void Test_ATraktShowsMostPWCRequest_1_Has_GenericTypeParameter()
        {
            typeof(ATraktShowsMostPWCRequest<>).ContainsGenericParameters.Should().BeTrue();
            typeof(ATraktShowsMostPWCRequest<int>).GenericTypeArguments.Should().NotBeEmpty().And.HaveCount(1);
        }

        [Fact]
        public void Test_ATraktShowsMostPWCRequest_1_Inherits_ATraktShowsRequest_1()
        {
            typeof(ATraktShowsMostPWCRequest<int>).IsSubclassOf(typeof(ATraktShowsRequest<int>)).Should().BeTrue();
        }

        [Fact]
        public void Test_ATraktShowsMostPWCRequest_1_Has_Period_Property()
        {
            var propertyInfo = typeof(ATraktShowsMostPWCRequest<>)
                    .GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.Name == "Period")
                    .FirstOrDefault();

            propertyInfo.CanRead.Should().BeTrue();
            propertyInfo.CanWrite.Should().BeTrue();
            propertyInfo.PropertyType.Should().Be(typeof(TraktTimePeriod));
        }

        [Fact]
        public void Test_ATraktShowsMostPWCRequest_1_Has_AuthorizationRequirement_NotRequired()
        {
            var requestMock = new TraktShowsMostPWCRequestMock();
            requestMock.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.NotRequired);
        }

        [Theory, ClassData(typeof(TraktShowsMostPWCRequestMock_TestData))]
        public void Test_ATraktShowsMostPWCRequest_1_Returns_Valid_UriPathParameters(IDictionary<string, object> values,
                                                                                     IDictionary<string, object> expected)
        {
            values.Should().NotBeNull().And.HaveCount(expected.Count);

            if (expected.Count > 0)
                values.Should().Contain(expected);
        }

        public class TraktShowsMostPWCRequestMock_TestData : IEnumerable<object[]>
        {
            private static readonly TraktExtendedInfo _extendedInfo = new TraktExtendedInfo { Full = true };
            private static readonly TraktShowFilter _filter = new TraktShowFilter().WithYears(2005, 2016);
            private static readonly TraktTimePeriod _timePeriod = TraktTimePeriod.Monthly;
            private const int _page = 5;
            private const int _limit = 20;

            private static readonly TraktShowsMostPWCRequestMock _request1 = new TraktShowsMostPWCRequestMock();

            private static readonly TraktShowsMostPWCRequestMock _request2 = new TraktShowsMostPWCRequestMock
            {
                ExtendedInfo = _extendedInfo
            };

            private static readonly TraktShowsMostPWCRequestMock _request3 = new TraktShowsMostPWCRequestMock
            {
                Filter = _filter
            };

            private static readonly TraktShowsMostPWCRequestMock _request4 = new TraktShowsMostPWCRequestMock
            {
                Period = _timePeriod
            };

            private static readonly TraktShowsMostPWCRequestMock _request5 = new TraktShowsMostPWCRequestMock
            {
                Page = _page
            };

            private static readonly TraktShowsMostPWCRequestMock _request6 = new TraktShowsMostPWCRequestMock
            {
                Limit = _limit
            };

            private static readonly TraktShowsMostPWCRequestMock _request7 = new TraktShowsMostPWCRequestMock
            {
                ExtendedInfo = _extendedInfo,
                Filter = _filter
            };

            private static readonly TraktShowsMostPWCRequestMock _request8 = new TraktShowsMostPWCRequestMock
            {
                ExtendedInfo = _extendedInfo,
                Period = _timePeriod
            };

            private static readonly TraktShowsMostPWCRequestMock _request9 = new TraktShowsMostPWCRequestMock
            {
                ExtendedInfo = _extendedInfo,
                Page = _page
            };

            private static readonly TraktShowsMostPWCRequestMock _request10 = new TraktShowsMostPWCRequestMock
            {
                ExtendedInfo = _extendedInfo,
                Limit = _limit
            };

            private static readonly TraktShowsMostPWCRequestMock _request11 = new TraktShowsMostPWCRequestMock
            {
                ExtendedInfo = _extendedInfo,
                Page = _page,
                Limit = _limit
            };

            private static readonly TraktShowsMostPWCRequestMock _request12 = new TraktShowsMostPWCRequestMock
            {
                Filter = _filter,
                Period = _timePeriod
            };

            private static readonly TraktShowsMostPWCRequestMock _request13 = new TraktShowsMostPWCRequestMock
            {
                Filter = _filter,
                Page = _page
            };

            private static readonly TraktShowsMostPWCRequestMock _request14 = new TraktShowsMostPWCRequestMock
            {
                Filter = _filter,
                Limit = _limit
            };

            private static readonly TraktShowsMostPWCRequestMock _request15 = new TraktShowsMostPWCRequestMock
            {
                Filter = _filter,
                Page = _page,
                Limit = _limit
            };

            private static readonly TraktShowsMostPWCRequestMock _request16 = new TraktShowsMostPWCRequestMock
            {
                Period = _timePeriod,
                Page = _page
            };

            private static readonly TraktShowsMostPWCRequestMock _request17 = new TraktShowsMostPWCRequestMock
            {
                Period = _timePeriod,
                Limit = _limit
            };

            private static readonly TraktShowsMostPWCRequestMock _request18 = new TraktShowsMostPWCRequestMock
            {
                Period = _timePeriod,
                Page = _page,
                Limit = _limit
            };

            private static readonly TraktShowsMostPWCRequestMock _request19 = new TraktShowsMostPWCRequestMock
            {
                Page = _page,
                Limit = _limit
            };

            private static readonly TraktShowsMostPWCRequestMock _request20 = new TraktShowsMostPWCRequestMock
            {
                ExtendedInfo = _extendedInfo,
                Filter = _filter,
                Period = _timePeriod,
                Page = _page,
                Limit = _limit
            };

            private static readonly List<object[]> _data = new List<object[]>();

            public TraktShowsMostPWCRequestMock_TestData()
            {
                SetupPathParamters();
            }

            private void SetupPathParamters()
            {
                var strExtendedInfo = _extendedInfo.ToString();
                var filterParameters = _filter.GetParameters();
                var strTimePeriod = _timePeriod.UriName;
                var strPage = _page.ToString();
                var strLimit = _limit.ToString();

                _data.Add(new object[] { _request1.GetUriPathParameters(), new Dictionary<string, object>() });

                _data.Add(new object[] { _request2.GetUriPathParameters(), new Dictionary<string, object>
                    {
                        ["extended"] = strExtendedInfo
                    }});

                _data.Add(new object[] { _request3.GetUriPathParameters(), new Dictionary<string, object>(filterParameters) });

                _data.Add(new object[] { _request4.GetUriPathParameters(), new Dictionary<string, object>
                    {
                        ["period"] = strTimePeriod
                    }});

                _data.Add(new object[] { _request5.GetUriPathParameters(), new Dictionary<string, object>
                    {
                        ["page"] = strPage
                    }});

                _data.Add(new object[] { _request6.GetUriPathParameters(), new Dictionary<string, object>
                    {
                        ["limit"] = strLimit
                    }});

                _data.Add(new object[] { _request7.GetUriPathParameters(), new Dictionary<string, object>(filterParameters)
                    {
                        ["extended"] = strExtendedInfo
                    }});

                _data.Add(new object[] { _request8.GetUriPathParameters(), new Dictionary<string, object>
                    {
                        ["extended"] = strExtendedInfo,
                        ["period"] = strTimePeriod
                    }});

                _data.Add(new object[] { _request9.GetUriPathParameters(), new Dictionary<string, object>
                    {
                        ["extended"] = strExtendedInfo,
                        ["page"] = strPage
                    }});

                _data.Add(new object[] { _request10.GetUriPathParameters(), new Dictionary<string, object>
                    {
                        ["extended"] = strExtendedInfo,
                        ["limit"] = strLimit
                    }});

                _data.Add(new object[] { _request11.GetUriPathParameters(), new Dictionary<string, object>
                    {
                        ["extended"] = strExtendedInfo,
                        ["page"] = strPage,
                        ["limit"] = strLimit
                    }});

                _data.Add(new object[] { _request12.GetUriPathParameters(), new Dictionary<string, object>(filterParameters)
                    {
                        ["period"] = strTimePeriod
                    }});

                _data.Add(new object[] { _request13.GetUriPathParameters(), new Dictionary<string, object>(filterParameters)
                    {
                        ["page"] = strPage
                    }});

                _data.Add(new object[] { _request14.GetUriPathParameters(), new Dictionary<string, object>(filterParameters)
                    {
                        ["limit"] = strLimit
                    }});

                _data.Add(new object[] { _request15.GetUriPathParameters(), new Dictionary<string, object>(filterParameters)
                    {
                        ["page"] = strPage,
                        ["limit"] = strLimit
                    }});

                _data.Add(new object[] { _request16.GetUriPathParameters(), new Dictionary<string, object>
                    {
                        ["period"] = strTimePeriod,
                        ["page"] = strPage
                    }});

                _data.Add(new object[] { _request17.GetUriPathParameters(), new Dictionary<string, object>
                    {
                        ["period"] = strTimePeriod,
                        ["limit"] = strLimit
                    }});

                _data.Add(new object[] { _request18.GetUriPathParameters(), new Dictionary<string, object>
                    {
                        ["period"] = strTimePeriod,
                        ["page"] = strPage,
                        ["limit"] = strLimit
                    }});

                _data.Add(new object[] { _request19.GetUriPathParameters(), new Dictionary<string, object>
                    {
                        ["page"] = strPage,
                        ["limit"] = strLimit
                    }});

                _data.Add(new object[] { _request20.GetUriPathParameters(), new Dictionary<string, object>(filterParameters)
                    {
                        ["extended"] = strExtendedInfo,
                        ["period"] = strTimePeriod,
                        ["page"] = strPage,
                        ["limit"] = strLimit
                    }});
            }

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
