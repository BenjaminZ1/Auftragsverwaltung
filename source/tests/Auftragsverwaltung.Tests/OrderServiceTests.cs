using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.Domain.ArticleGroup;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Order;
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
    class OrderServiceTests
    {
        private List<OrderDto> _orderDtoTestData;
        private List<Order> _orderTestData;
        private IMapper _mapper;

        [SetUp]
        public void GenerateTestData()
        {
            _orderDtoTestData = InstanceHelper.GenerateOrderDtoServiceTestData();
            _orderTestData = InstanceHelper.GenerateOrderServiceTestData();
            _mapper = InstanceHelper.GetMapper();
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int id = 1;
            var orderStub = _orderTestData[0];
            var orderRepositoryFake = A.Fake<IOrderRepository>();
            A.CallTo(() => orderRepositoryFake.Get(id)).Returns(orderStub);

            var orderService = new OrderService(orderRepositoryFake, InstanceHelper.GetMapper());
            var expectedResult = _mapper.Map<OrderDto>(orderStub);

            //act
            var result = await orderService.Get(id);

            //assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Get_WhenOk_GetsCalledOnce()
        {
            //arrange
            int id = 1;
            var orderStub = _orderTestData[0];
            var orderRepositoryFake = A.Fake<IOrderRepository>();
            A.CallTo(() => orderRepositoryFake.Get(id)).Returns(orderStub);

            var orderService = new OrderService(orderRepositoryFake, InstanceHelper.GetMapper());
            var expectedResult = _mapper.Map<OrderDto>(orderStub);

            //act
            var result = await orderService.Get(id);

            //assert
            A.CallTo(() => orderRepositoryFake.Get(id)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task GetAll_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var orderStubs = _orderTestData;
            var orderRepositoryFake = A.Fake<IOrderRepository>();
            A.CallTo(() => orderRepositoryFake.GetAll()).Returns(orderStubs);

            var orderService = new OrderService(orderRepositoryFake, InstanceHelper.GetMapper());
            var expectedResult = orderStubs.Select(o => _mapper.Map<OrderDto>(o));

            //act
            var result = await orderService.GetAll();

            //assers
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task GetAll_WhenOk_GetsCalledOnce()
        {
            //arrange
            var orderStubs = _orderTestData;
            var orderRepositoryFake = A.Fake<IOrderRepository>();
            A.CallTo(() => orderRepositoryFake.GetAll()).Returns(orderStubs);

            var orderService = new OrderService(orderRepositoryFake, InstanceHelper.GetMapper());
            var expectedResult = orderStubs.Select(o => _mapper.Map<OrderDto>(o));

            //act
            var result = await orderService.GetAll();

            //assers
            A.CallTo(() => orderRepositoryFake.GetAll()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Create_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var orderStub = _orderTestData[0];
            var orderDtoStub = _orderDtoTestData[0];
            var responseDto = new ResponseDto<Order>()
            {
                Entity = orderStub
            };

            var orderRepositoryFake = A.Fake<IOrderRepository>();
            A.CallTo(() => orderRepositoryFake.Create(A<Order>.Ignored)).Returns(responseDto);

            var orderService = new OrderService(orderRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await orderService.Create(orderDtoStub);

            //assert
            result.Should().BeOfType(typeof(OrderDto));
            result.Response.Entity.Should().BeNull();
            result.OrderId.Should().Be(orderStub.OrderId);
        }

        [Test]
        public async Task Create_WhenOk_GetsCalledOnce()
        {
            //arrange
            var orderStub = _orderTestData[0];
            var orderDtoStub = _orderDtoTestData[0];
            var responseDto = new ResponseDto<Order>()
            {
                Entity = orderStub
            };

            var orderRepositoryFake = A.Fake<IOrderRepository>();
            A.CallTo(() => orderRepositoryFake.Create(A<Order>.Ignored)).Returns(responseDto);

            var orderService = new OrderService(orderRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await orderService.Create(orderDtoStub);

            //assert
            A.CallTo(() => orderRepositoryFake.Create(A<Order>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Update_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var orderStub = _orderTestData[0];
            var orderDtoStub = _orderDtoTestData[0];

            var changedOrderStub = orderStub;
            var changedOrderDtoStub = orderDtoStub;
            changedOrderStub.Date = new DateTime(2017, 6, 15);
            changedOrderDtoStub.Date = new DateTime(2017, 6, 15);

            var responseDto = new ResponseDto<Order>()
            {
                Entity = changedOrderStub
            };

            var orderRepositoryFake = A.Fake<IOrderRepository>();
            A.CallTo(() => orderRepositoryFake.Update(A<Order>.Ignored)).Returns(responseDto);

            var orderService = new OrderService(orderRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await orderService.Update(changedOrderDtoStub);

            //assert
            result.Should().BeOfType(typeof(OrderDto));
            result.Response.Entity.Should().BeNull();
            result.OrderId.Should().Be(orderStub.OrderId);
            result.Date.Should().Be(changedOrderStub.Date);
        }

        [Test]
        public async Task Update_WhenOk_GetsCalledOnce()
        {
            //arrange
            var orderStub = _orderTestData[0];
            var orderDtoStub = _orderDtoTestData[0];

            var changedOrderStub = orderStub;
            var changedOrderDtoStub = orderDtoStub;
            changedOrderStub.Date = new DateTime(2017, 6, 15);
            changedOrderDtoStub.Date = new DateTime(2017, 6, 15);

            var responseDto = new ResponseDto<Order>()
            {
                Entity = changedOrderStub
            };

            var orderRepositoryFake = A.Fake<IOrderRepository>();
            A.CallTo(() => orderRepositoryFake.Update(A<Order>.Ignored)).Returns(responseDto);

            var orderService = new OrderService(orderRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await orderService.Update(changedOrderDtoStub);

            //assert
            A.CallTo(() => orderRepositoryFake.Update(A<Order>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Delete_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int id = 1;
            var orderStub = _orderTestData[0];
            var responseDto = new ResponseDto<Order>()
            {
                Entity = orderStub
            };

            var orderRepositoryFake = A.Fake<IOrderRepository>();
            A.CallTo(() => orderRepositoryFake.Delete(id)).Returns(responseDto);

            var orderService = new OrderService(orderRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await orderService.Delete(id);

            //assert
            result.Should().BeOfType(typeof(OrderDto));
            result.Response.Entity.Should().BeNull();
            result.OrderId.Should().Be(orderStub.OrderId);
        }

        [Test]
        public async Task Delete_WhenOk_GetsCalledOnce()
        {
            //arrange
            int id = 1;
            var orderStub = _orderTestData[0];
            var responseDto = new ResponseDto<Order>()
            {
                Entity = orderStub
            };

            var orderRepositoryFake = A.Fake<IOrderRepository>();
            A.CallTo(() => orderRepositoryFake.Delete(id)).Returns(responseDto);

            var orderService = new OrderService(orderRepositoryFake, InstanceHelper.GetMapper());

            //act
            var result = await orderService.Delete(id);

            //assert
            A.CallTo(() => orderRepositoryFake.Delete(id)).MustHaveHappenedOnceExactly();
        }
    }
}
