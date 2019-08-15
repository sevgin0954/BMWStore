using BMWStore.Common.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class Status : BaseEntity
    {
        [Required]
        [MaxLength(EntitiesConstants.TestDriveNameMaxLength)]
        [MinLength(EntitiesConstants.TestDriveNameMinLength)]
        public string Name { get; set; }

        public ICollection<TestDrive> TestDrives { get; set; } = new List<TestDrive>();
    }
}
