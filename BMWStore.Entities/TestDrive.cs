using BMWStore.Common.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class TestDrive
    {
        public string Id { get; set; }

        [Required]
        public string CarId { get; set; }
        public BaseCar Car { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        public string StatusId { get; set; }
        public Status Status { get; set; }

        [Required]
        public DateTime ScheduleDate { get; set; }

        [MinLength(EntitiesConstants.CommentCommentMinLength)]
        [MaxLength(EntitiesConstants.CommentCommentMaxLength)]
        public string Comment { get; set; }
    }
}