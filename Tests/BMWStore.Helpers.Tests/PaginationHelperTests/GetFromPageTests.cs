using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BMWStore.Helpers.Tests.PaginationHelperTests
{
    public class GetFromPageTests
    {
        [Fact]
        public void WithoutEntities_ShouldReturnEmptyCollection()
        {
            var entities = new List<TestEntity>().AsQueryable();
            var pageNumber = 1;
            var pageSize = 10;

            var result = entities.GetFromPage(pageNumber, pageSize);

            Assert.Empty(result);
        }

        [Fact]
        public void WithEntitiesAndBiggerPageNumber_ShouldReturnEmptyCollection()
        {
            var entities = new List<TestEntity>
            {
                new TestEntity()
            }.AsQueryable();
            var pageNumber = 2;
            var pageSize = 10;

            var result = entities.GetFromPage(pageNumber, pageSize);

            Assert.Empty(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WithEntitesAndSmallerPageNumber_ShouldReturnEntitiesFromFirstPage(int pageNumber)
        {
            var entities = new List<TestEntity>
            {
                new TestEntity()
            }.AsQueryable();
            var pageSize = 10;

            var result = entities.GetFromPage(pageNumber, pageSize);

            Assert.Single(result);
        }

        [Fact]
        public void WithEntitiesAndSecondPageNumber_ShouldReturnEntitiesFromSecondPage()
        {
            var entities = new List<TestEntity>
            {
                new TestEntity(),
                new TestEntity(),
                new TestEntity()
            }.AsQueryable();
            var pageNumber = 2;
            var pageSize = 2;

            var result = entities.GetFromPage(pageNumber, pageSize);

            Assert.Single(result);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(3, 0)]
        public void WithEntitiesAndSmallerThanOnePageSize_ShouldSetPageSizeToOne(int pageNumber, int pageSize)
        {
            var entities = new List<TestEntity>
            {
                new TestEntity(),
                new TestEntity(),
                new TestEntity()
            }.AsQueryable();

            var result = entities.GetFromPage(pageNumber, pageSize);

            Assert.Single(result);
        }
    }

    class TestEntity
    {

    }
}