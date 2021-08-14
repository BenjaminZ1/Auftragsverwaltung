using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auftragsverwaltung.Domain.Article;
using Auftragsverwaltung.Domain.ArticleGroup;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Infrastructure.Article;
using Auftragsverwaltung.Infrastructure.Common;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

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
        public async Task Create_WhenNew_ReturnsCorrectResult()
        {
            //arrange
            var articleTestData = InstanceHelper.GetArticleTestData();
            var article = articleTestData[0];
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var articleRepository = new ArticleRepository(dbContextFactoryFake);

            //act
            var result = await articleRepository.Create(article);

            //assert
            result.Should().BeOfType(typeof(ResponseDto<Article>));
            result.Entity.ArticleGroup.ArticleGroupId.Should().Be(article.ArticleGroupId);
            result.Entity.ArticleGroupId.Should().Be(article.ArticleGroupId);
            result.Flag.Should().BeTrue();
        }


        [Test]
        public async Task CreateWithParentArticleGroup_WhenNewArticleGroupWithExistingParentArticleGroup_ReturnsCorrectResult()
        {
            await InstanceHelper.AddDbTestArticles(_options);

            //arrange
            var articleTestData = InstanceHelper.GetArticleTestData();
            var article = articleTestData[0];
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var articleRepository = new ArticleRepository(dbContextFactoryFake);

            //act
            var result = await articleRepository.Create(article);

            //assert
            result.Should().BeOfType(typeof(ResponseDto<Article>));
            result.Entity.ArticleGroup.Name.Should().Be(article.ArticleGroup.Name);
            result.Entity.ArticleGroup.ParentArticleGroup.Should().BeNull();
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            await InstanceHelper.AddDbTestArticles(_options);
            int articleId = 1;

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var articleRepository = new ArticleRepository(dbContextFactoryFake);

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
            await InstanceHelper.AddDbTestArticles(_options);

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var articleRepository = new ArticleRepository(dbContextFactoryFake);

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
            await InstanceHelper.AddDbTestArticles(_options);
            int articleId = 1;

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var articleRepository = new ArticleRepository(dbContextFactoryFake);

            //act
            var result = await articleRepository.Delete(articleId);

            //assert
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Update_WhenOK_ReturnsCorrectResult()
        {
            //arrange
            await InstanceHelper.AddDbTestArticles(_options);
            int articleId = 1;
            string newArticleDescription = "NewName";

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var articleRepository = new ArticleRepository(dbContextFactoryFake);

            var entity = articleRepository.Get(articleId);
            var article = entity.Result;
            article.Description = newArticleDescription;

            //act
            var result = await articleRepository.Update(article);

            //assert
            result.Entity.Description.Should().BeEquivalentTo(newArticleDescription);
            result.Flag.Should().BeTrue();
        }

        [Ignore("behaviour not defined")]
        [Test]
        public async Task Delete_WhenInOrder_ReturnsCorrectResult()
        {
            //arrange
            await InstanceHelper.AddDbTestOrder(_options);
            int articleId = 1;

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var articleRepository = new ArticleRepository(dbContextFactoryFake);

            //act
            var result = await articleRepository.Delete(articleId);

            //assert
            result.Flag.Should().BeTrue();
        }
    }
}
