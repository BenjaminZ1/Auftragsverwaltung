using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.Domain.ArticleGroup;
using Auftragsverwaltung.Domain.Common;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Tests
{
    [TestFixture]
    class ArticleGroupServiceTests
    {
        private List<ArticleGroupDto> _articleGroupDtoTestData;
        private List<ArticleGroup> _articleGroupTestData;
        private IMapper _mapper;

        [SetUp]
        public void GenerateTestData()
        {
            _articleGroupDtoTestData = InstanceHelper.GenerateArticleGroupDtoServiceTestData();
            _articleGroupTestData = InstanceHelper.GenerateArticleGroupServiceTestData();
            _mapper = InstanceHelper.GetMapper();
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int id = 1;
            var articleGroupStub = _articleGroupTestData[0];
            var articleGroupRepositoryFake = A.Fake<IArticleGroupRepository>();
            A.CallTo(() => articleGroupRepositoryFake.Get(id)).Returns(articleGroupStub);

            var articleGroupService = new ArticleGroupService(articleGroupRepositoryFake, InstanceHelper.GetMapper());
            var expectedResult = _mapper.Map<ArticleGroupDto>(articleGroupStub);

            //act
            var result = await articleGroupService.Get(id);

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Get_WhenOk_GetsCalledOnce()
        {
            //arrange
            int id = 1;
            var articleGroupStub = _articleGroupTestData[0];
            var articleGroupRepositoryFake = A.Fake<IArticleGroupRepository>();
            A.CallTo(() => articleGroupRepositoryFake.Get(id)).Returns(articleGroupStub);

            var articleGroupService = new ArticleGroupService(articleGroupRepositoryFake, InstanceHelper.GetMapper());
            var expectedResult = _mapper.Map<ArticleGroupDto>(articleGroupStub);

            //act
            var result = await articleGroupService.Get(id);

            //assert
            A.CallTo(() => articleGroupRepositoryFake.Get(id)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task GetAll_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var articleGroupStub = _articleGroupTestData;
            var articleGroupRepositoryFake = A.Fake<IArticleGroupRepository>();
            A.CallTo(() => articleGroupRepositoryFake.GetAll()).Returns(articleGroupStub);

            var articleGroupService = new ArticleGroupService(articleGroupRepositoryFake, InstanceHelper.GetMapper());
            var expectedResult = articleGroupStub.Select(a => _mapper.Map<ArticleGroupDto>(a));

            //act
            var result = await articleGroupService.GetAll();

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task GetAll_WhenOk_GetsCalledOnce()
        {
            //arrange
            var articleGroupStub = _articleGroupTestData;
            var articleGroupRepositoryFake = A.Fake<IArticleGroupRepository>();
            A.CallTo(() => articleGroupRepositoryFake.GetAll()).Returns(articleGroupStub);

            var articleGroupService = new ArticleGroupService(articleGroupRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await articleGroupService.GetAll();

            //assert
            A.CallTo(() => articleGroupRepositoryFake.GetAll()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Create_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var articleGroupStub = _articleGroupTestData[0];
            var articleGroupDtoStub = _articleGroupDtoTestData[0];
            var responseDto = new ResponseDto<ArticleGroup>()
            {
                Entity = articleGroupStub
            };
            var articleGroupRepositoryFake = A.Fake<IArticleGroupRepository>();
            A.CallTo(() => articleGroupRepositoryFake.Create(A<ArticleGroup>.Ignored)).Returns(responseDto);

            var articleGroupService = new ArticleGroupService(articleGroupRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await articleGroupService.Create(articleGroupDtoStub);

            //assert
            result.Should().BeOfType(typeof(ArticleGroupDto));
            result.Response.Entity.Should().BeNull();
            result.ArticleGroupId.Should().Be(articleGroupStub.ArticleGroupId);
        }

        [Test]
        public async Task Create_WhenOk_GetsCalledOnce()
        {
            //arrange
            var articleGroupStub = _articleGroupTestData[0];
            var articleGroupDtoStub = _articleGroupDtoTestData[0];
            var responseDto = new ResponseDto<ArticleGroup>()
            {
                Entity = articleGroupStub
            };
            var articleGroupRepositoryFake = A.Fake<IArticleGroupRepository>();
            A.CallTo(() => articleGroupRepositoryFake.Create(A<ArticleGroup>.Ignored)).Returns(responseDto);

            var articleGroupService = new ArticleGroupService(articleGroupRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await articleGroupService.Create(articleGroupDtoStub);

            //assert
            A.CallTo(() => articleGroupRepositoryFake.Create(A<ArticleGroup>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Update_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var articleGroupStub = _articleGroupTestData[0];
            var articleGroupDtoStub = _articleGroupDtoTestData[0];

            var changedArticleGroupStub = articleGroupStub;
            var changedArticleGroupDtoStub = articleGroupDtoStub;
            changedArticleGroupStub.Name = "Obst";
            changedArticleGroupDtoStub.Name = "Obst";

            var responseDto = new ResponseDto<ArticleGroup>()
            {
                Entity = changedArticleGroupStub
            };

            var articleGroupRepositoryFake = A.Fake<IArticleGroupRepository>();
            A.CallTo(() => articleGroupRepositoryFake.Update(A<ArticleGroup>.Ignored)).Returns(responseDto);

            var articleGroupService = new ArticleGroupService(articleGroupRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await articleGroupService.Update(changedArticleGroupDtoStub);

            //assert
            result.Should().BeOfType(typeof(ArticleGroupDto));
            result.Response.Entity.Should().BeNull();
            result.ArticleGroupId.Should().Be(articleGroupStub.ArticleGroupId);
            result.Name.Should().Be(changedArticleGroupStub.Name);
        }

        [Test]
        public async Task Update_WhenOk_GetsCalledOnce()
        {
            //arrange
            var articleGroupStub = _articleGroupTestData[0];
            var articleGroupDtoStub = _articleGroupDtoTestData[0];

            var changedArticleGroupStub = articleGroupStub;
            var changedArticleGroupDtoStub = articleGroupDtoStub;
            changedArticleGroupStub.Name = "Obst";
            changedArticleGroupDtoStub.Name = "Obst";

            var responseDto = new ResponseDto<ArticleGroup>()
            {
                Entity = changedArticleGroupStub
            };

            var articleGroupRepositoryFake = A.Fake<IArticleGroupRepository>();
            A.CallTo(() => articleGroupRepositoryFake.Update(A<ArticleGroup>.Ignored)).Returns(responseDto);

            var articleGroupService = new ArticleGroupService(articleGroupRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await articleGroupService.Update(changedArticleGroupDtoStub);

            //assert
            A.CallTo(() => articleGroupRepositoryFake.Update(A<ArticleGroup>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Delete_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int id = 1;
            var articleGroupStub = _articleGroupTestData[0];
            var responseDto = new ResponseDto<ArticleGroup>()
            {
                Entity = articleGroupStub
            };

            var articleRepositoryFake = A.Fake<IArticleGroupRepository>();
            A.CallTo(() => articleRepositoryFake.Delete(id)).Returns(responseDto);

            var articleGroupService = new ArticleGroupService(articleRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await articleGroupService.Delete(id);

            //assert
            result.Should().BeOfType(typeof(ArticleGroupDto));
            result.Response.Entity.Should().BeNull();
            result.ArticleGroupId.Should().Be(articleGroupStub.ArticleGroupId);
        }

        [Test]
        public async Task Delete_WhenOk_GetsCalledOnce()
        {
            //arrange
            int id = 1;
            var articleGroupStub = _articleGroupTestData[0];
            var responseDto = new ResponseDto<ArticleGroup>()
            {
                Entity = articleGroupStub
            };

            var articleRepositoryFake = A.Fake<IArticleGroupRepository>();
            A.CallTo(() => articleRepositoryFake.Delete(id)).Returns(responseDto);

            var articleGroupService = new ArticleGroupService(articleRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await articleGroupService.Delete(id);

            //assert
            A.CallTo(() => articleRepositoryFake.Delete(id)).MustHaveHappenedOnceExactly();
        }

    }
}
