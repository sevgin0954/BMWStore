using BMWStore.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.SearchModels.BindingModels
{
	public class SearchBindingModel
	{
		[Required]
		[MaxLength(EntitiesConstants.SearchKeyWordsMaxLength)]
		[MinLength(EntitiesConstants.SearchKeyWordsMinLength)]
		public string KeyWords { get; set; }

		public int PageNumber { get; set; } = 1;
	}
}
