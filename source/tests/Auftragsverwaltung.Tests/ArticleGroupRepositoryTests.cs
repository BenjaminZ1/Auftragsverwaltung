using Auftragsverwaltung.Domain.ArticleGroup;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Infrastructure.ArticleGroup;
using Auftragsverwaltung.Infrastructure.Common;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var articleGroupRepository = new ArticleGroupRepository(dbContextFactoryFake);

            //act
            var result = await articleGroupRepository.Create(articleGroup);

            //assert
            result.Should().BeOfType(typeof(ResponseDto<ArticleGroup>));
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            await InstanceHelper.AddDbTestArticleGroup(_options);
            int articleGroupId = 1;

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var articleGroupRepository = new ArticleGroupRepository(dbContextFactoryFake);

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
            await InstanceHelper.AddDbTestArticleGroup(_options);

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var articleGroupRepository = new ArticleGroupRepository(dbContextFactoryFake);

            //act
            var result = await articleGroupRepository.GetAll();

            //assert
            var resultList = result.ToList();
            resultList.Should().BeOfType(typeof(List<ArticleGroup>));
            resultList.Count().Should().Be(1);
        }

        [Test]
        public async Task Delete_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            await InstanceHelper.AddDbTestArticleGroup(_options);
            int articleGroupId = 1;

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var articleGroupRepository = new ArticleGroupRepository(dbContextFactoryFake);

            //act
            var result = await articleGroupRepository.Delete(articleGroupId);

            //assert
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Update_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            await InstanceHelper.AddDbTestArticleGroup(_options);
            int articleGroupId = 1;
            string newArticleGroupName = "NewArticleGroupName";

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var articleGroupRepository = new ArticleGroupRepository(dbContextFactoryFake);

            var entity = articleGroupRepository.Get(articleGroupId);
            var articleGroup = entity.Result;
            articleGroup.Name = newArticleGroupName;

            //act
            var result = await articleGroupRepository.Update(articleGroupId, articleGroup);

            //assert
            result.Entity.Name.Should().BeEquivalentTo(newArticleGroupName);
            result.Flag.Should().BeTrue();
        }
    }
}
