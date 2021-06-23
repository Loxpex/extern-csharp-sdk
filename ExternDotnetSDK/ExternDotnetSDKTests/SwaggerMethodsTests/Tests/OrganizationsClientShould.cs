﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Kontur.Extern.Client.Models.Organizations;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class OrganizationsClientShould : AllTestsShould
    {
        protected Organization Organization;

        [OneTimeSetUp]
        public override async Task SetUp()
        {
            await base.SetUp().ConfigureAwait(false);
            Organization = (await Client.Organizations.GetAllOrganizationsAsync(Account.Id).ConfigureAwait(false)).Organizations[0];
        }

        [TestCase]
        [TestCase("0606257678")]
        [TestCase("0606257678", "671145475")]
        public void GetOrganizations_WithValidParameters(string inn = null, string kpp = null, int skip = 0, int take = 1000)
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Organizations.GetAllOrganizationsAsync(Account.Id, inn, kpp, skip, take).ConfigureAwait(false));
        }

        [TestCase(null, null, 0, 0)]
        [TestCase(null, null, -1)]
        [TestCase(null, "not a kpp")]
        [TestCase("not an inn")]
        public void GetNoOrganizations_WithBadParameters(string inn = null, string kpp = null, int skip = 0, int take = 1000)
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Organizations.GetAllOrganizationsAsync(Account.Id, inn, kpp, skip, take).ConfigureAwait(false));
        }

        [Test]
        public void GetNoOrganizations_WithNonexistentAccount()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Organizations.GetAllOrganizationsAsync(Guid.Empty).ConfigureAwait(false));
        }

        [Test]
        public void GetAnOrganization_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Organizations.GetOrganizationAsync(Account.Id, Organization.Id).ConfigureAwait(false));
        }

        [Test]
        public void FailToGetAnOrganization_WithBadParameters()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Organizations.GetOrganizationAsync(Account.Id, Guid.Empty).ConfigureAwait(false));
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Organizations.GetOrganizationAsync(Guid.Empty, Organization.Id).ConfigureAwait(false));
        }

        [Test]
        public void UpdateOrganizationName()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Organizations.UpdateOrganizationAsync(
                    Account.Id,
                    Organization.Id,
                    Organization.General.Name).ConfigureAwait(false));
        }

        [Test]
        public void FailToCreateNewOrganization_WithoutAccess()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Organizations.CreateOrganizationAsync(
                    Account.Id,
                    "9194113617",
                    "335544394",
                    "Good name").ConfigureAwait(false));
        }

        [Test]
        public void FailToDeleteOrganization_WithBadAccountId()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Organizations.DeleteOrganizationAsync(Guid.Empty, Organization.Id).ConfigureAwait(false));
        }

        [Test]
        public void FailToDeleteOrganization_WithBadOrganizationId()
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Organizations.DeleteOrganizationAsync(Account.Id, Guid.Empty).ConfigureAwait(false));
        }
    }
}