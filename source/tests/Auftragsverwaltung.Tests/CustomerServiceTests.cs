using System.Collections;
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
using Auftragsverwaltung.Application.Validators;
using FluentValidation;
using FluentValidation.Results;
using NUnit.Framework.Internal;

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
            var customerValidatorFake = A.Fake<AbstractValidator<CustomerDto>>();
            var customerSerializerFake = A.Fake<ISerializer<CustomerDto>>();

            A.CallTo(() => customerRepositoryFake.Get(id)).Returns(customerStub);

            var customerService = new CustomerService(customerRepositoryFake, InstanceHelper.GetMapper(),
                customerValidatorFake, customerSerializerFake);
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
            var customerDtoStub = _customerDtoTestData[0];

            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            var mapperFake = A.Fake<IMapper>();
            var customerValidatorFake = A.Fake<AbstractValidator<CustomerDto>>();
            var customerSerializerFake = A.Fake<ISerializer<CustomerDto>>();

            A.CallTo(() => customerRepositoryFake.Get(id)).Returns(customerStub);
            A.CallTo(() => mapperFake.Map<CustomerDto>(customerStub)).Returns(customerDtoStub);

            var customerService = new CustomerService(customerRepositoryFake, mapperFake,
                customerValidatorFake, customerSerializerFake);

            //act
            var result = await customerService.Get(id);

            //assert
            A.CallTo(() => customerRepositoryFake.Get(id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => mapperFake.Map<CustomerDto>(customerStub)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task GetAll_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var customerStubs = _customerTestData;
            var customerDtoStubs = _customerDtoTestData;

            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            var customerValidatorFake = A.Fake<AbstractValidator<CustomerDto>>();
            var customerSerializerFake = A.Fake<ISerializer<CustomerDto>>();

            A.CallTo(() => customerRepositoryFake.GetAll()).Returns(customerStubs);


            var customerService = new CustomerService(customerRepositoryFake, InstanceHelper.GetMapper(),
                customerValidatorFake, customerSerializerFake);
            var expectedResult = customerStubs.Select(x => _mapper.Map<CustomerDto>(x));

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
            var mapperFake = A.Fake<IMapper>();
            var customerValidatorFake = A.Fake<AbstractValidator<CustomerDto>>();
            var customerSerializerFake = A.Fake<ISerializer<CustomerDto>>();

            A.CallTo(() => customerRepositoryFake.GetAll()).Returns(customerStubs);

            var customerService = new CustomerService(customerRepositoryFake, mapperFake,
                customerValidatorFake, customerSerializerFake);

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
            var validationResultStub = new ValidationResult(new List<ValidationFailure>());
            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            var mapperFake = A.Fake<IMapper>();
            var customerValidatorFake = A.Fake<IValidator<CustomerDto>>();
            var customerSerializerFake = A.Fake<ISerializer<CustomerDto>>();

            A.CallTo(() => customerRepositoryFake.Create(A<Customer>.Ignored)).Returns(responseDto);
            A.CallTo(() => customerValidatorFake.Validate(customerDtoStub)).Returns(validationResultStub);


            var customerService = new CustomerService(customerRepositoryFake, mapperFake,
                customerValidatorFake, customerSerializerFake);

            //act
            var result = await customerService.Create(customerDtoStub);

            //assert
            A.CallTo(() => customerRepositoryFake.Create(A<Customer>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => customerValidatorFake.Validate(customerDtoStub)).MustHaveHappenedOnceExactly();
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

            var validationResultStub = new ValidationResult(new List<ValidationFailure>());
            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            var customerValidatorFake = A.Fake<IValidator<CustomerDto>>();
            var customerSerializerFake = A.Fake<ISerializer<CustomerDto>>();

            A.CallTo(() => customerRepositoryFake.Create(A<Customer>.Ignored)).Returns(responseDto);
            A.CallTo(() => customerValidatorFake.Validate(customerDtoStub)).Returns(validationResultStub);

            var customerService = new CustomerService(customerRepositoryFake, InstanceHelper.GetMapper(),
                customerValidatorFake, customerSerializerFake);

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

            var validationResultStub = new ValidationResult(new List<ValidationFailure>());
            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            var customerValidatorFake = A.Fake<IValidator<CustomerDto>>();
            var customerSerializerFake = A.Fake<ISerializer<CustomerDto>>();

            A.CallTo(() => customerRepositoryFake.Update(A<Customer>.Ignored)).Returns(responseDto);
            A.CallTo(() => customerValidatorFake.Validate(customerDtoStub)).Returns(validationResultStub);

            var customerService = new CustomerService(customerRepositoryFake, InstanceHelper.GetMapper(),
                customerValidatorFake, customerSerializerFake);

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

            var validationResultStub = new ValidationResult(new List<ValidationFailure>());
            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            var mapperFake = A.Fake<IMapper>();
            var customerValidatorFake = A.Fake<IValidator<CustomerDto>>();
            var customerSerializerFake = A.Fake<ISerializer<CustomerDto>>();

            A.CallTo(() => customerRepositoryFake.Update(A<Customer>.Ignored)).Returns(responseDto);
            A.CallTo(() => customerValidatorFake.Validate(customerDtoStub)).Returns(validationResultStub);

            var customerService = new CustomerService(customerRepositoryFake, mapperFake,
                customerValidatorFake, customerSerializerFake);

            //act
            var result = await customerService.Update(changedCustomerDtoStub);

            //assert
            A.CallTo(() => customerRepositoryFake.Update(A<Customer>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => customerValidatorFake.Validate(customerDtoStub)).MustHaveHappenedOnceExactly();
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
            var customerValidatorFake = A.Fake<IValidator<CustomerDto>>();
            var customerSerializerFake = A.Fake<ISerializer<CustomerDto>>();

            A.CallTo(() => customerRepositoryFake.Delete(id)).Returns(responseDto);

            var customerService = new CustomerService(customerRepositoryFake, InstanceHelper.GetMapper(),
                customerValidatorFake, customerSerializerFake);

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
            var customerValidatorFake = A.Fake<IValidator<CustomerDto>>();
            var customerSerializerFake = A.Fake<ISerializer<CustomerDto>>();

            A.CallTo(() => customerRepositoryFake.Delete(id)).Returns(responseDto);

            var customerService = new CustomerService(customerRepositoryFake, InstanceHelper.GetMapper(),
                customerValidatorFake, customerSerializerFake);

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
            var mapperFake = A.Fake<IMapper>();
            var customerValidatorFake = A.Fake<IValidator<CustomerDto>>();
            var customerSerializerFake = A.Fake<ISerializer<CustomerDto>>();

            var customerService = new CustomerService(customerRepositoryFake, mapperFake,
                customerValidatorFake, customerSerializerFake);

            //act
            await customerService.Serialize(customerDtoStub, testPath);

            //assert
            A.CallTo(() => customerSerializerFake.Serialize(customerDtoStub, testPath)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Deserialize_WhenOk_GetsCalledOnce()
        {
            //arrange
            string testPath = @"C:\temp\Auftragsverwaltung\test.xml";

            var customerRepositoryFake = A.Fake<IAppRepository<Customer>>();
            var mapperFake = A.Fake<IMapper>();
            var customerValidatorFake = A.Fake<IValidator<CustomerDto>>();
            var customerSerializerFake = A.Fake<ISerializer<CustomerDto>>();

            var customerService = new CustomerService(customerRepositoryFake, mapperFake,
                customerValidatorFake, customerSerializerFake);

            //act
            await customerService.Deserialize(testPath);

            //assert
            A.CallTo(() => customerSerializerFake.Deserialize(testPath)).MustHaveHappenedOnceExactly();
        }
    }
}
