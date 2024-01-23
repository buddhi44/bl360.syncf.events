
using bl360.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bl360.ui.Extensions
{
	public interface IBLUIOperationHelper
	{
		void ResetToInitialValue();

		void UpdateVisibility(bool IsVisible);

		void ToggleEditable(bool IsEditable);

		Task Refresh();

		Task SetValue(object value);
		Task FocusComponentAsync();

		BLUIElement LinkedUIObject { get; }
	}
	public interface IBLServerDependentComponent
	{
		Task FetchData(bool useLocalstorage = false);

		Task SetDataSource(object DataSource);

	}
}
