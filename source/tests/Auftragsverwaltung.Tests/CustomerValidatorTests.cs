using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Application.Validators;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace Auftragsverwaltung.Tests
{
    [TestFixture]
    class CustomerValidatorTests
    {
        private  CustomerValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CustomerValidator();
        }

        [Test]
        public void Validate_WhenOk_ShouldHaveNoError()
        {
            //arrange
            var customerDtos = InstanceHelper.GenerateCustomerDtoServiceTestData();
            var customerDtoStub = customerDtos[0];
            customerDtoStub.Password = InstanceHelper.GetSecureString();

            //act
            var result = _validator.TestValidate(customerDtoStub);

            //assert
            result.ShouldNotHaveValidationErrorFor(c => c.CustomerNumber);
            result.ShouldNotHaveValidationErrorFor(c => c.Email);
            result.ShouldNotHaveValidationErrorFor(c => c.Website);
            result.ShouldNotHaveValidationErrorFor(c => c.Password);
        }

        [Test]
        public void Validate_WhenNotOk_ShouldHaveError()
        {
            //arrange
            var customerDtos = InstanceHelper.GenerateCustomerDtoServiceTestData();
            var customerDtoStub = customerDtos[0];
            customerDtoStub.CustomerNumber = "notValid";
            customerDtoStub.Email = "notValid";
            customerDtoStub.Website = "notValid";
            customerDtoStub.Password = InstanceHelper.GetNotValidSecureString();

            //act
            var result = _validator.TestValidate(customerDtoStub);

            //assert
            result.ShouldHaveValidationErrorFor(c => c.CustomerNumber);
            result.ShouldHaveValidationErrorFor(c => c.Email);
            result.ShouldHaveValidationErrorFor(c => c.Website);
            result.ShouldHaveValidationErrorFor(c => c.Password);
        }
    }
}
