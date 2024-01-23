
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bl360.clientInfrastructure;
using bl360.domain;


namespace bl360.clientInfrastructure.Managers.ComboDataManager
{
    public interface IComboDataManager : IManager
    {
        Task<IList<CodeBaseResponse>> GetCodeBaseResponses(ComboRequestDTO requestDTO);

        Task<IList<AddressResponse>> GetAddressResponses(ComboRequestDTO requestDTO);
        Task<IList<User>> GetUserResponses(ComboRequestDTO requestDTO);

        Task<IList<ItemResponse>> GetItemResponses(ComboRequestDTO requestDTO);

        Task<IList<UnitResponse>> GetItemUnits(ComboRequestDTO requestDTO);

        Task<IList<ItemCodeResponse>> GetItemByItemCode(ItemRequestModel itemRequest);


        
    }
}
