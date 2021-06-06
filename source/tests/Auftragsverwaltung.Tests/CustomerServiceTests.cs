using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Auftragsverwaltung.Tests
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

            //act
            var result = await customerService.Get(id);

            //assert
            A.CallTo(() => customerRepositoryFake.Get(id)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task GetAll_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var customerStubs = _customerTestData;
            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            A.CallTo(() => customerRepositoryFake.GetAll()).Returns(customerStubs);

            var customerService = new CustomerService(customerRepositoryFake);
            var expectedResult = customerStubs.Select(c => new CustomerDto(c));

            //act
            var result = await customerService.GetAll();

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task GetAll_WhenOk_GetsCalledOnce()
        {
            //arrange
            var customerStubs = _customerTestData;
            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            A.CallTo(() => customerRepositoryFake.GetAll()).Returns(customerStubs);

            var customerService = new CustomerService(customerRepositoryFake);
            

            //act
            var result = await customerService.GetAll();

            //assert
            A.CallTo(() => customerRepositoryFake.GetAll()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Create_WhenOk_GetsCalledOnce()
        {
            //arrange
            var customerStub = _customerTestData[0];
            var responseDto = new ResponseDto<Customer>()
            {
                Entity = customerStub
            };
            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            A.CallTo(() => customerRepositoryFake.Create(customerStub)).Returns(responseDto);

            var customerService = new CustomerService(customerRepositoryFake);


            //act
            var result = await customerService.Create(customerStub);

            //assert
            A.CallTo(() => customerRepositoryFake.Create(customerStub)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Create_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var customerStub = _customerTestData[0];
            var responseDto = new ResponseDto<Customer>()
            {
                Entity = customerStub
            };
            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            A.CallTo(() => customerRepositoryFake.Create(customerStub)).Returns(responseDto);

            var customerService = new CustomerService(customerRepositoryFake);


            //act
            var result = await customerService.Create(customerStub);

            //assert
            result.Should().BeOfType(typeof(CustomerDto));
            result.Response.Entity.Should().BeNull();
            result.CustomerId.Should().Be(customerStub.CustomerId);
        }

        [Test]
        public async Task Update_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int id = 1;
            var customerStub = _customerTestData[0];
            var changedCustomerStub = customerStub;
            changedCustomerStub.Firstname = "Rudolf";
            var responseDto = new ResponseDto<Customer>()
            {
                Entity = changedCustomerStub
            };
            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            A.CallTo(() => customerRepositoryFake.Update(id, changedCustomerStub)).Returns(responseDto);

            var customerService = new CustomerService(customerRepositoryFake);


            //act
            var result = await customerService.Update(id, changedCustomerStub);

            //assert
            result.Should().BeOfType(typeof(CustomerDto));
            result.Response.Entity.Should().BeNull();
            result.CustomerId.Should().Be(customerStub.CustomerId);
            result.Firstname.Should().Be(changedCustomerStub.Firstname);
        }

        [Test]
        public async Task Update_WhenOk_GetsCalledOnce()
        {
            //arrange
            int id = 1;
            var customerStub = _customerTestData[0];
            var changedCustomerStub = customerStub;
            changedCustomerStub.Firstname = "Rudolf";
            var responseDto = new ResponseDto<Customer>()
            {
                Entity = changedCustomerStub
            };
            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            A.CallTo(() => customerRepositoryFake.Update(id, changedCustomerStub)).Returns(responseDto);

            var customerService = new CustomerService(customerRepositoryFake);


            //act
            var result = await customerService.Update(id, changedCustomerStub);

            //assert
            A.CallTo(() => customerRepositoryFake.Update(id, changedCustomerStub)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Delete_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int id = 1;
            var customerStub = _customerTestData[0];
            var responseDto = new ResponseDto<Customer>()
            {
                Entity = customerStub
            };
            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            A.CallTo(() => customerRepositoryFake.Delete(id)).Returns(responseDto);

            var customerService = new CustomerService(customerRepositoryFake);

            //act
            var result = await customerService.Delete(id);

            //assert
            result.Should().BeOfType(typeof(CustomerDto));
            result.Response.Entity.Should().BeNull();
            result.CustomerId.Should().Be(customerStub.CustomerId);
        }

        [Test]
        public async Task Delete_WhenOk_GetsCalledOnce()
        {
            //arrange
            int id = 1;
            var customerStub = _customerTestData[0];
            var responseDto = new ResponseDto<Customer>()
            {
                Entity = customerStub
            };
            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            A.CallTo(() => customerRepositoryFake.Delete(id)).Returns(responseDto);

            var customerService = new CustomerService(customerRepositoryFake);

            //act
            var result = await customerService.Delete(id);

            //assert
            A.CallTo(() => customerRepositoryFake.Delete(id)).MustHaveHappenedOnceExactly();
        }
    }
}
