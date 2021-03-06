using Auftragsverwaltung.Domain.ArticleGroup;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Infrastructure.ArticleGroup;
using Auftragsverwaltung.Infrastructure.Common;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Tests
{
    [TestFixture]
    public class ArticleGroupRepositoryTests
    {
        private DbContextOptions<AppDbContext> _options;

        [OneTimeSetUp]
        public void BuildDbContext()
        {
            _options = InstanceHelper.AppDbContext_BuildDbContext();
        }

        [SetUp]
        public void ResetDb()
        {
            InstanceHelper.ResetDb(_options);
        }

        [Test]
        public async Task Create_WhenNew_ReturnsCorrectResult()
        {
            //arrange
            var articleGroup = new ArticleGroup()
            {
                Name = "TestArticleGroupName"
            };

            var serviceProviderFake = A.Fake<IServiceProvider>();
            A.CallTo(() => serviceProviderFake.GetService(typeof(AppDbContext)))
                .Returns(new AppDbContext(_options));

            var serviceScopeFake = A.Fake<IServiceScope>();
            A.CallTo(() => serviceScopeFake.ServiceProvider)
                .Returns(serviceProviderFake);

            var serviceScopeFactoryFake = A.Fake<IServiceScopeFactory>();
            A.CallTo(() => serviceScopeFactoryFake.CreateScope())
                .Returns(serviceScopeFake);

            A.CallTo(() => serviceProviderFake.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactoryFake);

            var articleGroupRepository = new ArticleGroupRepository(serviceScopeFactoryFake);

            //act
            var result = await articleGroupRepository.Create(articleGroup);

            //assert
            result.Should().BeOfType(typeof(ResponseDto<ArticleGroup>));
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Create_WhenExceptionIsThrown_ReturnsCorrectResult()
        {
            //arrange
            var articleGroup = new ArticleGroup()
            {
                ArticleGroupId = 1,
                Name = "TestArticleGroupName"
            };

            var serviceProviderFake = A.Fake<IServiceProvider>();
            A.CallTo(() => serviceProviderFake.GetService(typeof(AppDbContext)))
                .Returns(new AppDbContext(_options));

            var serviceScopeFake = A.Fake<IServiceScope>();
            A.CallTo(() => serviceScopeFake.ServiceProvider)
                .Returns(serviceProviderFake);

            var serviceScopeFactoryFake = A.Fake<IServiceScopeFactory>();
            A.CallTo(() => serviceScopeFactoryFake.CreateScope())
                .Returns(serviceScopeFake);

            A.CallTo(() => serviceProviderFake.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactoryFake);

            var articleGroupRepository = new ArticleGroupRepository(serviceScopeFactoryFake);

            //act
            var result = await articleGroupRepository.Create(articleGroup);

            //assert
            result.Should().BeOfType(typeof(ResponseDto<ArticleGroup>));
            result.Flag.Should().BeFalse();
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int articleGroupId = 1;

            var serviceProviderFake = A.Fake<IServiceProvider>();
            A.CallTo(() => serviceProviderFake.GetService(typeof(AppDbContext)))
                .Returns(new AppDbContext(_options));

            var serviceScopeFake = A.Fake<IServiceScope>();
            A.CallTo(() => serviceScopeFake.ServiceProvider)
                .Returns(serviceProviderFake);

            var serviceScopeFactoryFake = A.Fake<IServiceScopeFactory>();
            A.CallTo(() => serviceScopeFactoryFake.CreateScope())
                .Returns(serviceScopeFake);

            A.CallTo(() => serviceProviderFake.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactoryFake);

            var articleGroupRepository = new ArticleGroupRepository(serviceScopeFactoryFake);

            //act
            var result = await articleGroupRepository.Get(articleGroupId);

            //assert
            result.Should().BeOfType(typeof(ArticleGroup));
            result.ArticleGroupId.Should().Be(articleGroupId);
        }

        [Test]
        public async Task GetAll_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var serviceProviderFake = A.Fake<IServiceProvider>();
            A.CallTo(() => serviceProviderFake.GetService(typeof(AppDbContext)))
                .Returns(new AppDbContext(_options));

            var serviceScopeFake = A.Fake<IServiceScope>();
            A.CallTo(() => serviceScopeFake.ServiceProvider)
                .Returns(serviceProviderFake);

            var serviceScopeFactoryFake = A.Fake<IServiceScopeFactory>();
            A.CallTo(() => serviceScopeFactoryFake.CreateScope())
                .Returns(serviceScopeFake);

            A.CallTo(() => serviceProviderFake.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactoryFake);

            var articleGroupRepository = new ArticleGroupRepository(serviceScopeFactoryFake);

            //act
            var result = await articleGroupRepository.GetAll();

            //assert
            var resultList = result.ToList();
            resultList.Should().BeOfType(typeof(List<ArticleGroup>));
            resultList.Count().Should().Be(3);
        }

        [Test]
        public async Task Delete_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int articleGroupId = 2;

            var serviceProviderFake = A.Fake<IServiceProvider>();
            A.CallTo(() => serviceProviderFake.GetService(typeof(AppDbContext)))
                .Returns(new AppDbContext(_options));

            var serviceScopeFake = A.Fake<IServiceScope>();
            A.CallTo(() => serviceScopeFake.ServiceProvider)
                .Returns(serviceProviderFake);

            var serviceScopeFactoryFake = A.Fake<IServiceScopeFactory>();
            A.CallTo(() => serviceScopeFactoryFake.CreateScope())
                .Returns(serviceScopeFake);

            A.CallTo(() => serviceProviderFake.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactoryFake);

            var articleGroupRepository = new ArticleGroupRepository(serviceScopeFactoryFake);

            //act
            var result = await articleGroupRepository.Delete(articleGroupId);

            //assert
            result.Flag.Should().BeTrue();
        }


        [Test]
        public async Task Delete_WhenExceptionIsThrown_ReturnsCorrectResult()
        {
            //arrange
            int articleGroupId = 15;

            var serviceProviderFake = A.Fake<IServiceProvider>();
            A.CallTo(() => serviceProviderFake.GetService(typeof(AppDbContext)))
                .Returns(new AppDbContext(_options));

            var serviceScopeFake = A.Fake<IServiceScope>();
            A.CallTo(() => serviceScopeFake.ServiceProvider)
                .Returns(serviceProviderFake);

            var serviceScopeFactoryFake = A.Fake<IServiceScopeFactory>();
            A.CallTo(() => serviceScopeFactoryFake.CreateScope())
                .Returns(serviceScopeFake);

            A.CallTo(() => serviceProviderFake.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactoryFake);

            var articleGroupRepository = new ArticleGroupRepository(serviceScopeFactoryFake);

            //act
            var result = await articleGroupRepository.Delete(articleGroupId);

            //assert
            result.Should().BeOfType(typeof(ResponseDto<ArticleGroup>));
            result.Flag.Should().BeFalse();
        }

        [Test]
        public async Task Update_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int articleGroupId = 1;
            string newArticleGroupName = "NewArticleGroupName";

            var serviceProviderFake = A.Fake<IServiceProvider>();
            A.CallTo(() => serviceProviderFake.GetService(typeof(AppDbContext)))
                .Returns(new AppDbContext(_options));

            var serviceScopeFake = A.Fake<IServiceScope>();
            A.CallTo(() => serviceScopeFake.ServiceProvider)
                .Returns(serviceProviderFake);

            var serviceScopeFactoryFake = A.Fake<IServiceScopeFactory>();
            A.CallTo(() => serviceScopeFactoryFake.CreateScope())
                .Returns(serviceScopeFake);

            A.CallTo(() => serviceProviderFake.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactoryFake);

            var articleGroupRepository = new ArticleGroupRepository(serviceScopeFactoryFake);

            var entity = articleGroupRepository.Get(articleGroupId);
            var articleGroup = entity.Result;
            articleGroup.Name = newArticleGroupName;

            //act
            var result = await articleGroupRepository.Update(articleGroup);

            //assert
            result.Entity.Name.Should().BeEquivalentTo(newArticleGroupName);
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Update_WhenExceptionIsThrown_ReturnsCorrectResult()
        {
            //arrange
            int articleGroupId = 1;
            string newArticleGroupName = "NewArticleGroupName";

            var serviceProviderFake = A.Fake<IServiceProvider>();
            A.CallTo(() => serviceProviderFake.GetService(typeof(AppDbContext)))
                .Returns(new AppDbContext(_options));

            var serviceScopeFake = A.Fake<IServiceScope>();
            A.CallTo(() => serviceScopeFake.ServiceProvider)
                .Returns(serviceProviderFake);

            var serviceScopeFactoryFake = A.Fake<IServiceScopeFactory>();
            A.CallTo(() => serviceScopeFactoryFake.CreateScope())
                .Returns(serviceScopeFake);

            A.CallTo(() => serviceProviderFake.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactoryFake);

            var articleGroupRepository = new ArticleGroupRepository(serviceScopeFactoryFake);

            var entity = articleGroupRepository.Get(articleGroupId);
            var articleGroup = entity.Result;
            articleGroup.Name = newArticleGroupName;
            articleGroup.ArticleGroupId = 15;

            //act
            var result = await articleGroupRepository.Update(articleGroup);

            //assert
            result.Should().BeOfType(typeof(ResponseDto<ArticleGroup>));
            result.Flag.Should().BeFalse();
        }

        [Test]
        public async Task Search_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var serviceProviderFake = A.Fake<IServiceProvider>();
            A.CallTo(() => serviceProviderFake.GetService(typeof(AppDbContext)))
                .Returns(new AppDbContext(_options));

            var serviceScopeFake = A.Fake<IServiceScope>();
            A.CallTo(() => serviceScopeFake.ServiceProvider)
                .Returns(serviceProviderFake);

            var serviceScopeFactoryFake = A.Fake<IServiceScopeFactory>();
            A.CallTo(() => serviceScopeFactoryFake.CreateScope())
                .Returns(serviceScopeFake);

            A.CallTo(() => serviceProviderFake.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactoryFake);

            var articleGroupRepository = new ArticleGroupRepository(serviceScopeFactoryFake);

            //act
            var result = await articleGroupRepository.Search("Pflegeprodukte");

            //assert
            var resultList = result.ToList();
            resultList.Should().BeOfType(typeof(List<ArticleGroup>));
            resultList.Count().Should().Be(1);
        }
    }
}
