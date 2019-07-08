using System.Collections.Generic;

namespace BMWStore.Common.SortTypes
{
    public static class IconLinks
    {
        public static Dictionary<string, string> SortTypeIconLink { get; private set; } = new Dictionary<string, string>()
        {
            { "Name", "<i class=\"far fa-credit-card\"></i>" },
            { "Orders", "<i class=\"fas fa-donate\"></i>" },
            { "LockoutStatus", "<i class=\"fas fa-ban\"></i>" },
            { "Email", "<i class=\"fas fa-envelope\"></i>" },
            { "Ascending", "<i class=\"fas fa-arrow-up\"></i>" },
            { "Descending", "<i class=\"fas fa-arrow-down\"></i>" }
        };
    }
}
