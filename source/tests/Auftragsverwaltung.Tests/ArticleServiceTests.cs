using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.Domain.Article;
using Auftragsverwaltung.Domain.Common;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
