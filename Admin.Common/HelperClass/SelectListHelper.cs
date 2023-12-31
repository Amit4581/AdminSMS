using Admin.Contract.Model.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Admin.Common.HelperClass
{
    public static class SelectListHelper
    {
        public static List<SelectListItem> ConvertToSelectList<T>(IEnumerable<T> models, Func<T, string> valueSelector, Func<T, string> textSelector, string defaultOption = null)
        {
            var selectListItems = new List<SelectListItem>();

            if (!string.IsNullOrEmpty(defaultOption))
            {
                selectListItems.Add(new SelectListItem { Value = "", Text = defaultOption });
            }

            selectListItems.AddRange(models.Select(model => new SelectListItem
            {
                Value = valueSelector(model),
                Text = textSelector(model)
            }));

            return selectListItems;
        }

    }
}
