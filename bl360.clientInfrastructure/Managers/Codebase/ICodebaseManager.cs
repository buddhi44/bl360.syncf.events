using bl360.domain;
using bluelotus360.Com.commonLib.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bl360.clientInfrastructure.Managers.Codebase
{
    public interface ICodebaseManager:IManager
    {
        Task<IList<CodeBaseResponse>> GetCodesByConditionCode(CodeBaseResponse record);
        Task<CodeBaseResponse> GetCodesByConditionCodeAndOurCode(CodeBaseResponse record);
    }
}
