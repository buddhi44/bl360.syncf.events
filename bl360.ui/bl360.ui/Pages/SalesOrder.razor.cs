using bl360.domain;
using bl360.ui.Extensions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bl360.ui.Pages
{
    public partial  class SalesOrder
    {
        private BLUIElement formDefinition;
        private Order order;
        private BLUIElement findOrderUI;
        private BLUIElement getFromQuoteUI;
        private BLUIElement modalUIElement;
        private BLUIElement editUIElement;
        private IDictionary<string, EventCallback> _interactionLogic;
        private IDictionary<string, BLUIElement> _modalDefinitions;
        private IDictionary<string, IBLUIOperationHelper> _objectHelpers;
        long elementKey;

        protected override async Task OnInitializedAsync()
        {
            order = new();
            formDefinition = new();
            elementKey = 208236;
            if (elementKey > 10)
            {
                var formrequest = new ObjectFormRequest();
                formrequest.MenuKey = elementKey;
                formDefinition = await _navManger.GetMenuUIElement(formrequest);
            }
            if (formDefinition != null)
            {
                formDefinition.IsDebugMode = true;
                formDefinition.IsMustElements = formDefinition.Children.Where(x => x.IsMust).Select(i => i._internalElementName).ToList();
            }
        }
    }
}
