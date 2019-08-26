using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using Xunit;

namespace BMWStore.Helpers.Tests.SelectListItemHelperTests
{
    public class SelectItemsWithValuesTests
    {
        [Fact]
        public void WithItemAndValue_ShouldSelectItem()
        {
            var selectListItems = new List<SelectListItem>();
            var item1 = this.AddSelectListItem(selectListItems);
            var item2 = this.AddSelectListItem(selectListItems);

            SelectListItemHelper.SelectItemsWithValues(selectListItems, item1.Value);

            Assert.Contains(selectListItems, i => i.Selected == true);
        }

        [Fact]
        public void WithItemAndIncorrectValue_ShouldNotSelectItem()
        {
            var selectListItems = new List<SelectListItem>();
            var item1 = this.AddSelectListItem(selectListItems);
            var item2 = this.AddSelectListItem(selectListItems);

            SelectListItemHelper.SelectItemsWithValues(selectListItems, Guid.NewGuid().ToString());

            Assert.DoesNotContain(selectListItems, i => i.Selected == true);
        }

        private SelectListItem AddSelectListItem(List<SelectListItem> selectListItems)
        {
            var item = new SelectListItem() { Value = Guid.NewGuid().ToString() };
            selectListItems.Add(item);

            return item;
        }
    }
}
