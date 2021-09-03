using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Tests
{
    [TestFixture]
    class CustomerServiceTests
    {
        private List<CustomerDto> _customerDtoTestData;
        private List<Customer> _customerTestData;
        private IMapper _mapper;

        [SetUp]
        public void GenerateTestData()
        {
            _customerDtoTestData = InstanceHelper.GenerateCustomerDtoServiceTestData();
            _customerTestData = InstanceHelper.GenerateCustomerServiceTestData();
            _mapper = InstanceHelper.GetMapper();
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int id = 1;
            var customerStub = _customerTestData[0];
            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            A.CallTo(() => customerRepositoryFake.Get(id)).Returns(customerStub);

            var customerService = new CustomerService(customerRepositoryFake, InstanceHelper.GetMapper(),
                InstanceHelper.GetCustomerValidator(), InstanceHelper.GetCustomerSerializer());
            var expectedResult = _mapper.Map<CustomerDto>(customerStub);

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

            var customerService = new CustomerService(customerRepositoryFake, InstanceHelper.GetMapper(),
                InstanceHelper.GetCustomerValidator(), InstanceHelper.GetCustomerSerializer());

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

            var customerService = new CustomerService(customerRepositoryFake, InstanceHelper.GetMapper(),
                InstanceHelper.GetCustomerValidator(), InstanceHelper.GetCustomerSerializer());
            var expectedResult = customerStubs.Select(c => _mapper.Map<CustomerDto>(c));

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

            var customerService = new CustomerService(customerRepositoryFake, InstanceHelper.GetMapper(),
                InstanceHelper.GetCustomerValidator(), InstanceHelper.GetCustomerSerializer());

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
            var customerDtoStub = _customerDtoTestData[0];
            var responseDto = new ResponseDto<Customer>()
            {
                Entity = customerStub
            };
            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            A.CallTo(() => customerRepositoryFake.Create(A<Customer>.Ignored)).Returns(responseDto);

            var customerService = new CustomerService(customerRepositoryFake, InstanceHelper.GetMapper(),
                InstanceHelper.GetCustomerValidator(), InstanceHelper.GetCustomerSerializer());

            //act
            var result = await customerService.Create(customerDtoStub);

            //assert
            A.CallTo(() => customerRepositoryFake.Create(A<Customer>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Create_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var customerStub = _customerTestData[0];
            var customerDtoStub = _customerDtoTestData[0];
            var responseDto = new ResponseDto<Customer>()
            {
                Entity = customerStub
            };
            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            A.CallTo(() => customerRepositoryFake.Create(A<Customer>.Ignored)).Returns(responseDto);

            var customerService = new CustomerService(customerRepositoryFake, InstanceHelper.GetMapper(),
                InstanceHelper.GetCustomerValidator(), InstanceHelper.GetCustomerSerializer());

            //act
            var result = await customerService.Create(customerDtoStub);

            //assert
            result.Should().BeOfType(typeof(CustomerDto));
            result.Response.Entity.Should().BeNull();
            result.CustomerId.Should().Be(customerStub.CustomerId);
        }

        [Test]
        public async Task Update_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var customerStub = _customerTestData[0];
            var customerDtoStub = _customerDtoTestData[0];

            var changedCustomerStub = customerStub;
            var changedCustomerDtoStub = customerDtoStub;
            changedCustomerStub.Firstname = "Rudolf";
            changedCustomerDtoStub.Firstname = "Rudolf";

            var responseDto = new ResponseDto<Customer>()
            {
                Entity = changedCustomerStub
            };

            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            A.CallTo(() => customerRepositoryFake.Update(A<Customer>.Ignored)).Returns(responseDto);

            var customerService = new CustomerService(customerRepositoryFake, InstanceHelper.GetMapper(),
                InstanceHelper.GetCustomerValidator(), InstanceHelper.GetCustomerSerializer());


            //act
            var result = await customerService.Update(changedCustomerDtoStub);

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
            var customerStub = _customerTestData[0];
            var customerDtoStub = _customerDtoTestData[0];

            var changedCustomerStub = customerStub;
            var changedCustomerDtoStub = customerDtoStub;
            changedCustomerStub.Firstname = "Rudolf";
            changedCustomerDtoStub.Firstname = "Rudolf";

            var responseDto = new ResponseDto<Customer>()
            {
                Entity = changedCustomerStub
            };

            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            A.CallTo(() => customerRepositoryFake.Update(A<Customer>.Ignored)).Returns(responseDto);

            var customerService = new CustomerService(customerRepositoryFake, InstanceHelper.GetMapper(),
                InstanceHelper.GetCustomerValidator(), InstanceHelper.GetCustomerSerializer());


            //act
            var result = await customerService.Update(changedCustomerDtoStub);

            //assert
            A.CallTo(() => customerRepositoryFake.Update(A<Customer>.Ignored)).MustHaveHappenedOnceExactly();
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

            var customerService = new CustomerService(customerRepositoryFake, InstanceHelper.GetMapper(),
                InstanceHelper.GetCustomerValidator(), InstanceHelper.GetCustomerSerializer());

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

            var customerService = new CustomerService(customerRepositoryFake, InstanceHelper.GetMapper(),
                InstanceHelper.GetCustomerValidator(), InstanceHelper.GetCustomerSerializer());

            //act
            var result = await customerService.Delete(id);

            //assert
            A.CallTo(() => customerRepositoryFake.Delete(id)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Serialize_WhenOk_GetsCalledOnce()
        {
            //arrange
            string testPath = @"C:\temp\Auftragsverwaltung\test.xml";
            var customerDtoStub = _customerDtoTestData[0];

            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();

            var customerService = new CustomerService(customerRepositoryFake, InstanceHelper.GetMapper(),
                InstanceHelper.GetCustomerValidator(), InstanceHelper.GetCustomerSerializer());

            //act
            await customerService.Serialize(customerDtoStub, testPath);

            //assert
        }
    }
}
