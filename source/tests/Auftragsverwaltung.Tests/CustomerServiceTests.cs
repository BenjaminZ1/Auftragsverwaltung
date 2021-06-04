using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using Auftragsverwaltung.Infrastructure.Customer;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Auftragsverwaltung.Repository.Tests
{
    [TestFixture]
    class CustomerServiceTests
    {
        private List<Customer> _customerTestData;

        [SetUp]
        public void GenerateTestData()
        {
            _customerTestData = InstanceHelper.GenerateCustomerServiceTestData();
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int id = 1;
            var customerStub = _customerTestData[0];
            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            A.CallTo(() => customerRepositoryFake.Get(id)).Returns(customerStub);

            var customerService = new CustomerService(customerRepositoryFake);
            var expectedResult = new CustomerDto(customerStub);

            //act
            var result = await customerService.Get(id);

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Get_WhenOk_GetCalledOnce()
        {
            //arrange
            int id = 1;
            var customerStub = _customerTestData[0];
            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            A.CallTo(() => customerRepositoryFake.Get(id)).Returns(customerStub);

            var customerService = new CustomerService(customerRepositoryFake);
            var expectedResult = new CustomerDto(customerStub);

            //act
            var result = await customerService.Get(id);

            //assert
            A.CallTo(() => customerRepositoryFake.Get(id)).MustHaveHappenedOnceExactly();
        }

    }
}
