using BMWStore.Common.Constants;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.TestDriveModels.BindingModels
{
    public class ScheduleTestDriveBindingModel : IMapTo<TestDrive>
    {
        [Required]
        public string CarId { get; set; }

        [Required]
        public DateTime ScheduleDate { get; set; }

        [MinLength(EntitiesConstants.CommentCommentMinLength)]
        [MaxLength(EntitiesConstants.CommentCommentMaxLength)]
        public string Comment { get; set; }
    }
}
