using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Auftragsverwaltung.Tests
{
    [TestFixture]
    class CustomerValidatorTests
    {

        [Test]
        public void Validate_WhenOk_ShouldHaveNoError()
        {
            var customerDtoStub = InstanceHelper.GenerateCustomerDtoServiceTestData();
        }
    }
}
