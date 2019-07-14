using System.Collections.Generic;

namespace BMWStore.Common.SortTypes
{
    public static class IconLinks
    {
        public static Dictionary<string, string> SortTypeIconLink { get; private set; } = new Dictionary<string, string>()
        {
            { "Ascending", "<i class=\"fas fa-arrow-up\"></i>" },
            { "Condition", "<i class=\"fas fa-car\"></i>" },
            { "Descending", "<i class=\"fas fa-arrow-down\"></i>" },
            { "Email", "<i class=\"fas fa-envelope\"></i>" },
            { "LockoutStatus", "<i class=\"fas fa-ban\"></i>" },
            { "Mileage", "<i class=\"fas fa-road\"></i>" },
            { "Name", "<i class=\"far fa-credit-card\"></i>" },
            { "Orders", "<i class=\"fas fa-donate\"></i>" },
            { "Price", "<i class=\"far fa-money-bill-alt\"></i>" },
            { "Warranty", "<i class=\"fas fa-wrench\"></i>" },
            { "Year", "<i class=\"far fa-calendar-alt\"></i>" }
        };
    }
}
