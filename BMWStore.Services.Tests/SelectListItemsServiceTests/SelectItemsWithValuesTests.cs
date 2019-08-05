using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using Xunit;

namespace BMWStore.Services.Tests.SelectListItemsServiceTests
{
    public class SelectItemsWithValuesTests : BaseSelectListItemsServiceTests, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public SelectItemsWithValuesTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public void WithItemAndValue_ShouldSelectItem()
        {
            var service = this.GetService();
            var selectListItems = new List<SelectListItem>();
            var item1 = this.AddSelectListItem(selectListItems);
            var item2 = this.AddSelectListItem(selectListItems);

            service.SelectItemsWithValues(selectListItems, item1.Value);

            Assert.Contains(selectListItems, i => i.Selected == true);
        }

        [Fact]
        public void WithItemAndIncorrectValue_ShouldNotSelectItem()
        {
            var service = this.GetService();
            var selectListItems = new List<SelectListItem>();
            var item1 = this.AddSelectListItem(selectListItems);
            var item2 = this.AddSelectListItem(selectListItems);

            service.SelectItemsWithValues(selectListItems, Guid.NewGuid().ToString());

            Assert.DoesNotContain(selectListItems, i => i.Selected == true);
        }

        private ISelectListItemsService GetService()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            return service;
        }

        private SelectListItem AddSelectListItem(List<SelectListItem> selectListItems)
        {
            var item = new SelectListItem() { Value = Guid.NewGuid().ToString() };
            selectListItems.Add(item);

            return item;
        }
    }
}
