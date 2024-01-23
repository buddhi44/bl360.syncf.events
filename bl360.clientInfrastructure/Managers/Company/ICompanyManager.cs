
using bl360.clientInfrastructure;
using bl360.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bluelotus360.Com.commonLib.Managers.Company
{
    public interface ICompanyManager:IManager
    {
        Task<IList<CompanyResponse>> GetUserCompanies();
        Task UpdateSelectedCompany(CompanyResponse response);

    }
}
