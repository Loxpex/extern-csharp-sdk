using System;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Api.Client.End2EndTests.Client.TestAbstractions;
using Kontur.Extern.Api.Client.End2EndTests.TestEnvironment;
using Kontur.Extern.Api.Client.Exceptions;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Api.Client.End2EndTests.Client
{
    public class DocflowPathsExtensions_Tests : GeneratedAccountTests
    {
        public DocflowPathsExtensions_Tests(ITestOutputHelper output, IsolatedAccountEnvironment environment)
            : base(output, environment)
        {
        }

        [Fact]
        public async Task Get_should_fail_when_try_to_read_non_existent_docflow()
        {
            Func<Task> func = async () => await Context.Docflows.GetDocflow(AccountId, Guid.NewGuid());

            await func.Should().ThrowAsync<ApiException>();
        }

        [Fact]
        public async Task TryGet_should_return_null_when_try_to_read_non_existent_docflow()
        {
            var docflow = await Context.Docflows.GetDocflowOrNull(AccountId, Guid.NewGuid());

            docflow.Should().BeNull();
        }
    }
}