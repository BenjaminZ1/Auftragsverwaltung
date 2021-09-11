using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Infrastructure.Common;
using FluentAssertions;
using NUnit.Framework;

namespace Auftragsverwaltung.Tests
{
    [TestFixture]
    class AppDbContextFactoryTests
    {
        [Test]
        public void CreateDbContext_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var appDbContextFactory = new AppDbContextFactory();

            //act
            var result = appDbContextFactory.CreateDbContext(new []{"test"});

            //assert
            result.Should().BeOfType(typeof(AppDbContext));
        }
    }
}
