using Auftragsverwaltung.Domain.Article;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Infrastructure.Article;
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
    public class ArticleRepositoryTests
    {
        private DbContextOptions<AppDbContext> _options;

        [OneTimeSetUp]
        public void BuidDbContext()
        {
            _options = InstanceHelper.AppDbContext_BuildDbContext();
        }

        [SetUp]
        public void ResetDb()
        {
            InstanceHelper.ResetDb(_options);
        }

        [Test]
        public async Task Create_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var articleTestData = InstanceHelper.GenerateArticleTestData();
            var article = articleTestData[0];

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

            var articleRepository = new ArticleRepository(serviceScopeFactoryFake);

            //act
            var result = await articleRepository.Create(article);

            //assert
            result.Should().BeOfType(typeof(ResponseDto<Article>));
            result.Entity.ArticleGroup.ArticleGroupId.Should().Be(article.ArticleGroupId);
            result.Entity.ArticleGroupId.Should().Be(article.ArticleGroupId);
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Create_WhenExceptionIsThrown_ReturnsCorrectResult()
        {
            //arrange
            var articleTestData = InstanceHelper.GenerateArticleTestData();
            var article = articleTestData[0];
            article.ArticleId = 1;

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

            var articleRepository = new ArticleRepository(serviceScopeFactoryFake);

            //act
            var result = await articleRepository.Create(article);

            //assert
            result.Should().BeOfType(typeof(ResponseDto<Article>));
            result.Flag.Should().BeFalse();
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int articleId = 1;

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

            var articleRepository = new ArticleRepository(serviceScopeFactoryFake);

            //act
            var result = await articleRepository.Get(articleId);

            //assert
            result.Should().BeOfType(typeof(Article));
            result.ArticleId.Should().Be(articleId);
            result.ArticleGroup.Should().NotBeNull();
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

            var articleRepository = new ArticleRepository(serviceScopeFactoryFake);

            //act
            var result = await articleRepository.GetAll();

            //assert
            var resultList = result.ToList();
            resultList.Should().BeOfType(typeof(List<Article>));
            resultList.Count().Should().Be(2);
        }

        [Test]
        public async Task Delete_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int articleId = 1;

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

            var articleRepository = new ArticleRepository(serviceScopeFactoryFake);

            //act
            var result = await articleRepository.Delete(articleId);

            //assert
            result.Should().BeOfType(typeof(ResponseDto<Article>));
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Delete_WhenExceptionIsThrown_ReturnsCorrectResult()
        {
            //arrange
            int articleId = 5;

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

            var articleRepository = new ArticleRepository(serviceScopeFactoryFake);

            //act
            var result = await articleRepository.Delete(articleId);

            //assert
            result.Should().BeOfType(typeof(ResponseDto<Article>));
            result.Flag.Should().BeFalse();
        }

        [Test]
        public async Task Update_WhenOK_ReturnsCorrectResult()
        {
            //arrange
            int articleId = 1;
            string newArticleDescription = "NewName";

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

            var articleRepository = new ArticleRepository(serviceScopeFactoryFake);

            var entity = articleRepository.Get(articleId);
            var article = entity.Result;
            article.Description = newArticleDescription;

            //act
            var result = await articleRepository.Update(article);

            //assert
            result.Entity.Description.Should().BeEquivalentTo(newArticleDescription);
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Update_WhenExceptionIsThrown_ReturnsCorrectResult()
        {
            //arrange
            int articleId = 1;
            string newArticleDescription = "NewName";

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

            var articleRepository = new ArticleRepository(serviceScopeFactoryFake);

            var entity = articleRepository.Get(articleId);
            var article = entity.Result;
            article.Description = newArticleDescription;
            article.ArticleId = 3;

            //act
            var result = await articleRepository.Update(article);

            //assert
            result.Should().BeOfType(typeof(ResponseDto<Article>));
            result.Flag.Should().BeFalse();
        }

        [Test]
        public async Task Delete_WhenInOrder_ReturnsCorrectResult()
        {
            //arrange
            int articleId = 1;

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

            var articleRepository = new ArticleRepository(serviceScopeFactoryFake);

            //act
            var result = await articleRepository.Delete(articleId);

            //assert
            result.Flag.Should().BeTrue();
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

            var articleRepository = new ArticleRepository(serviceScopeFactoryFake);

            //act
            var result = await articleRepository.Search("Zahnbürste");

            //assert
            var resultList = result.ToList();
            resultList.Should().BeOfType(typeof(List<Article>));
            resultList.Count().Should().Be(1);
        }
    }
}
