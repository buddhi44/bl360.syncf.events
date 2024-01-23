
using bl360.clientInfrastructure;
using bl360.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bl360.clientInfrastructure.Managers.NavMenuManager
{

    public interface INavMenuManager:IManager
    {

        Task<BLUIElement> GetMenuUIElement(ObjectFormRequest request);
    }
}
