using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.Domain.Article;
using Auftragsverwaltung.Domain.Common;
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
    class ArticleServiceTests
    {
        private List<ArticleDto> _articleDtoTestData;
        private List<Article> _articleTestData;
        private IMapper _mapper;

        [SetUp]
        public void GenerateTestData()
        {
            _articleDtoTestData = InstanceHelper.GenerateArticleDtoServiceTestData();
            _articleTestData = InstanceHelper.GenerateArticleServiceTestData();
            _mapper = InstanceHelper.GetMapper();
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int id = 1;
            var articleStub = _articleTestData[0];
            var articleRepositoryFake = A.Fake<IAppRepository<Article>>();
            A.CallTo(() => articleRepositoryFake.Get(id)).Returns(articleStub);

            var articleService = new ArticleService(articleRepositoryFake, InstanceHelper.GetMapper());
            var expectedResult = _mapper.Map<ArticleDto>(articleStub);

            //act
            var result = await articleService.Get(id);

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Get_WhenOk_GetsCalledOnce()
        {
            //arrange
            int id = 1;
            var articleStub = _articleTestData[0];
            var articleRepositoryFake = A.Fake<IAppRepository<Article>>();
            A.CallTo(() => articleRepositoryFake.Get(id)).Returns(articleStub);

            var articleService = new ArticleService(articleRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await articleService.Get(id);

            //assert
            A.CallTo(() => articleRepositoryFake.Get(id)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task GetAll_WhenOk_ReturnsCorrectResult()
        {
            var articleStubs = _articleTestData;
            var articleRepositoryFake = A.Fake<IAppRepository<Article>>();
            A.CallTo(() => articleRepositoryFake.GetAll()).Returns(articleStubs);

            var articleService = new ArticleService(articleRepositoryFake, InstanceHelper.GetMapper());
            var expectedResult = articleStubs.Select(a => _mapper.Map<ArticleDto>(a));

            //act
            var result = await articleService.GetAll();

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task GetAll_WhenOk_GetsCalledOnce()
        {
            //arrange
            var articleStubs = _articleTestData;
            var articleRepositoryFake = A.Fake<IAppRepository<Article>>();
            A.CallTo(() => articleRepositoryFake.GetAll()).Returns(articleStubs);

            var articleService = new ArticleService(articleRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await articleService.GetAll();

            //assert
            A.CallTo(() => articleRepositoryFake.GetAll()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Create_WhenOk_GetsCalledOnce()
        {
            //arrange
            var articleStub = _articleTestData[0];
            var articleDtoStub = _articleDtoTestData[0];
            var responseDto = new ResponseDto<Article>()
            {
                Entity = articleStub
            };
            var articleRepositoryFake = A.Fake<IAppRepository<Article>>();
            A.CallTo(() => articleRepositoryFake.Create(A<Article>.Ignored)).Returns(responseDto);

            var articleService = new ArticleService(articleRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await articleService.Create(articleDtoStub);

            //assert
            A.CallTo(() => articleRepositoryFake.Create(A<Article>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Create_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var articleStub = _articleTestData[0];
            var articleDtoStub = _articleDtoTestData[0];
            var responseDto = new ResponseDto<Article>()
            {
                Entity = articleStub
            };
            var articleRepositoryFake = A.Fake<IAppRepository<Article>>();
            A.CallTo(() => articleRepositoryFake.Create(A<Article>.Ignored)).Returns(responseDto);

            var articleService = new ArticleService(articleRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await articleService.Create(articleDtoStub);

            //assert
            result.Should().BeOfType(typeof(ArticleDto));
            result.Response.Entity.Should().BeNull();
            result.ArticleId.Should().Be(articleStub.ArticleId);
        }

        [Test]
        public async Task Update_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var articleStub = _articleTestData[0];
            var articleDtoStub = _articleDtoTestData[0];

            var changedArticleStub = articleStub;
            var changedArticleDtoStub = articleDtoStub;
            changedArticleStub.Description = "Milch";
            changedArticleDtoStub.Description = "Milch";

            var responseDto = new ResponseDto<Article>()
            {
                Entity = changedArticleStub
            };

            var articleRepositoryFake = A.Fake<IAppRepository<Article>>();
            A.CallTo(() => articleRepositoryFake.Update(A<Article>.Ignored)).Returns(responseDto);

            var articleService = new ArticleService(articleRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await articleService.Update(changedArticleDtoStub);

            //assert
            result.Should().BeOfType(typeof(ArticleDto));
            result.Response.Entity.Should().BeNull();
            result.ArticleId.Should().Be(articleStub.ArticleId);
            result.Description.Should().Be(changedArticleStub.Description);
        }

        [Test]
        public async Task Update_WhenOk_GetsCalledOnce()
        {
            //arrange
            var articleStub = _articleTestData[0];
            var articleDtoStub = _articleDtoTestData[0];

            var changedArticleStub = articleStub;
            var changedArticleDtoStub = articleDtoStub;
            changedArticleStub.Description = "Milch";
            changedArticleDtoStub.Description = "Milch";

            var responseDto = new ResponseDto<Article>()
            {
                Entity = changedArticleStub
            };

            var articleRepositoryFake = A.Fake<IAppRepository<Article>>();
            A.CallTo(() => articleRepositoryFake.Update(A<Article>.Ignored)).Returns(responseDto);

            var articleService = new ArticleService(articleRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await articleService.Update(changedArticleDtoStub);

            //assert
            A.CallTo(() => articleRepositoryFake.Update(A<Article>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Delete_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int id = 1;
            var articleStub = _articleTestData[0];
            var responseDto = new ResponseDto<Article>()
            {
                Entity = articleStub
            };
            var articleRepositoryFake = A.Fake<IAppRepository<Article>>();
            A.CallTo(() => articleRepositoryFake.Delete(id)).Returns(responseDto);

            var articleService = new ArticleService(articleRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await articleService.Delete(id);

            //assert
            result.Should().BeOfType(typeof(ArticleDto));
            result.Response.Entity.Should().BeNull();
            result.ArticleId.Should().Be(articleStub.ArticleId);
        }

        [Test]
        public async Task Delete_WhenOk_GetsCalledOnce()
        {
            //arrange
            int id = 1;
            var articleStub = _articleTestData[0];
            var responseDto = new ResponseDto<Article>()
            {
                Entity = articleStub
            };
            var articleRepositoryFake = A.Fake<IAppRepository<Article>>();
            A.CallTo(() => articleRepositoryFake.Delete(id)).Returns(responseDto);

            var articleService = new ArticleService(articleRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await articleService.Delete(id);

            //assert
            A.CallTo(() => articleRepositoryFake.Delete(id)).MustHaveHappenedOnceExactly();
        }
    }
}
