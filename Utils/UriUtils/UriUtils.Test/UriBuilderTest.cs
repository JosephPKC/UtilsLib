using FluentAssertions;

namespace UriUtils.Test
{
    public class UriBuilderTest
    {
        #region BuildUri
        [Fact]
        public void BuildUri_NoBaseUri_ReturnNull()
        {
            UriBuilder builder = new();

            string? actual = builder.BuildUri();

            actual.Should().BeNull();
        }

        [Fact]
        public void BuildUri_IsHttps_ReturnUriWithHttps()
        {
            string baseUri = "base";
            bool isHttps = true;

            UriBuilder builder = new();
            builder.BaseUri(baseUri).Https(isHttps);

            string expected = "https://base";
            string? actual = builder.BuildUri();

            actual.Should().Be(expected);
        }

        [Fact]
        public void BuildUri_IsNotHttps_ReturnUriWithHttp()
        {
            string baseUri = "base";
            bool isHttps = false;

            UriBuilder builder = new();
            builder.BaseUri(baseUri).Https(isHttps);

            string expected = "http://base";
            string? actual = builder.BuildUri();

            actual.Should().Be(expected);
        }

        [Fact]
        public void BuildUri_HasVersion_ReturnUriWithVersion()
        {
            string baseUri = "base";
            string version = "v1";

            UriBuilder builder = new();
            builder.BaseUri(baseUri).Version(version);

            string expected = "https://base/v1";
            string? actual = builder.BuildUri();

            actual.Should().Be(expected);
        }

        [Fact]
        public void BuildUri_HasNoVersion_ReturnUriWithoutVersion()
        {
            string baseUri = "base";
            string version = string.Empty;

            UriBuilder builder = new();
            builder.BaseUri(baseUri).Version(version);

            string expected = "https://base";
            string? actual = builder.BuildUri();

            actual.Should().Be(expected);
        }

        [Fact]
        public void BuildUri_HasValidEndpointAndId_ReturnUriWithEndpointAndId()
        {
            string baseUri = "base";
            string endpoint = "endpoint";
            string id = "1";

            UriBuilder builder = new();
            builder.BaseUri(baseUri).Endpoint(endpoint, id);

            string expected = "https://base/endpoint/1";
            string? actual = builder.BuildUri();

            actual.Should().Be(expected);
        }

        [Fact]
        public void BuildUri_HasValidIdOnly_ReturnUriWithIdOnly()
        {
            string baseUri = "base";
            string endpoint = string.Empty;
            string id = "1";

            UriBuilder builder = new();
            builder.BaseUri(baseUri).Endpoint(endpoint, id);

            string expected = "https://base/1";
            string? actual = builder.BuildUri();

            actual.Should().Be(expected);
        }

        [Fact]
        public void BuildUri_HasValidEndpointOnly_ReturnUriWithEndpointOnly()
        {
            string baseUri = "base";
            string endpoint = "endpoint";
            string id = string.Empty;

            UriBuilder builder = new();
            builder.BaseUri(baseUri).Endpoint(endpoint, id);

            string expected = "https://base/endpoint";
            string? actual = builder.BuildUri();

            actual.Should().Be(expected);
        }

        [Fact]
        public void BuildUri_HasNoValidEndpointOrId_ReturnUriWithoutEndpointAndId()
        {
            string baseUri = "base";
            string endpoint = string.Empty;
            string id = string.Empty;

            UriBuilder builder = new();
            builder.BaseUri(baseUri).Endpoint(endpoint, id);

            string expected = "https://base";
            string? actual = builder.BuildUri();

            actual.Should().Be(expected);
        }

        [Fact]
        public void BuildUri_HasMultipleEndpointsAndIds_ReturnUriWithAllEndpointsAndIds()
        {
            string baseUri = "base";
            Dictionary<string, string> endpoints = new() { { "endpoint1", "1" }, { "endpoint2", "2" } };

            UriBuilder builder = new();
            builder.BaseUri(baseUri).Endpoint(endpoints);

            string expected = "https://base/endpoint1/1/endpoint2/2";
            string? actual = builder.BuildUri();

            actual.Should().Be(expected);
        }

        [Fact]
        public void BuildUri_HasValidQueryKeyAndVal_ReturnUriWithQueryParam()
        {
            string baseUri = "base";
            string query = "query";
            string val = "val";

            UriBuilder builder = new();
            builder.BaseUri(baseUri).QueryParam(query, val);

            string expected = "https://base?query=val";
            string? actual = builder.BuildUri();

            actual.Should().Be(expected);
        }

        [Fact]
        public void BuildUri_HasValidKeyOnly_ReturnUriWithoutQueryParam()
        {
            string baseUri = "base";
            string query = "query";
            string val = string.Empty;

            UriBuilder builder = new();
            builder.BaseUri(baseUri).QueryParam(query, val);

            string expected = "https://base";
            string? actual = builder.BuildUri();

            actual.Should().Be(expected);
        }

        [Fact]
        public void BuildUri_HasValidValOnly_ReturnUriWithoutQueryParam()
        {
            string baseUri = "base";
            string query = string.Empty;
            string val = "val";

            UriBuilder builder = new();
            builder.BaseUri(baseUri).QueryParam(query, val);

            string expected = "https://base";
            string? actual = builder.BuildUri();

            actual.Should().Be(expected);
        }

        [Fact]
        public void BuildUri_HasNoValidKeyOrVal_ReturnUriWithoutQueryParam()
        {
            string baseUri = "base";
            string query = string.Empty;
            string val = string.Empty;

            UriBuilder builder = new();
            builder.BaseUri(baseUri).QueryParam(query, val);

            string expected = "https://base";
            string? actual = builder.BuildUri();

            actual.Should().Be(expected);
        }

        [Fact]
        public void BuildUri_HasMultipleQueryParams_ReturnUriWithAllQueryParams()
        {
            string baseUri = "base";
            Dictionary<string, string> queryParams = new() { { "query1", "1" }, { "query2", "2" } };

            UriBuilder builder = new();
            builder.BaseUri(baseUri).QueryParam(queryParams);

            string expected = "https://base?query1=1&query2=2";
            string? actual = builder.BuildUri();

            actual.Should().Be(expected);
        }
        #endregion

    }
}
