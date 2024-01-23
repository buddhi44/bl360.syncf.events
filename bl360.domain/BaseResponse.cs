using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bl360.domain
{
    public class BaseResponse
    {
        public bool IsDefault { get; set; }

        public bool IsMust { get; set; }

        public static bool IsValidData(BaseResponse response)
        {
            if (response == null)
            {
                return false;
            }

            if (response.GetType() == typeof(CodeBaseResponse))
            {
                return (response as CodeBaseResponse).CodeKey > 10;
            }


            if (response.GetType() == typeof(ItemResponse))
            {
                return (response as ItemResponse).ItemKey > 10;
            }


            if (response.GetType() == typeof(AddressResponse))
            {
                return (response as AddressResponse).AddressKey > 10;
            }


            if (response.GetType() == typeof(AccountResponse))
            {
                return (response as AccountResponse).AccountKey > 10;
            }

            if (response.GetType() == typeof(UnitResponse))
            {
                return (response as UnitResponse).UnitKey > 10;
            }


            return false;

        }
        public static long GetKeyValue(BaseResponse response)
        {
            if (response == null)
            {
                return 1;
            }

            if (response is CodeBaseResponse)
            {
                return (response as CodeBaseResponse).CodeKey;
            }
            if (response is AddressResponse)
            {
                return (response as AddressResponse).AddressKey;
            }
            if (response is AccountResponse)
            {
                return (response as AccountResponse).AccountKey;
            }
            if (response is UnitResponse)
            {
                return (response as UnitResponse).UnitKey;
            }
            if (response is ItemResponse)
            {
                return (response as ItemResponse).ItemKey;
            }

            if (response is ProjectResponse)
            {
                return (response as ProjectResponse).ProjectKey;
            }


            return 1;
        }


        public IDictionary<string, object> AddtionalData { get; set; }

        public BaseResponse()
        {
            AddtionalData = new Dictionary<string, object>();
        }

        public string? Base64ImageDocument { get; set; } = "";

    }
    public class ComboRequestDTO
    {
        public long RequestingElementKey { get; set; } = 1;
        public string RequestingURL { get; set; } = "";
        public IDictionary<string, object> AddtionalData { get; set; } = new Dictionary<string, object>();

        public ComboRequestDTO()
        {
            AddtionalData = new Dictionary<string, object>();

        }

        public string SearchQuery { get; set; } = "";

        public bool CancelRead { get; set; }
        public int EntityKey { get; set; } = 1;
        public long PreviousKey { get; set; } = 1;
    }

    public class CodeBaseResponse : BaseResponse
    {
        private object value1;
        private object value2;
        private object value3;

        public CodeBaseResponse(object value1, object value2, object value3)
        {
            this.value1 = value1;
            this.value2 = value2;
            this.value3 = value3;
        }

        public CodeBaseResponse()
        {
            this.CodeKey = 1;
        }
        public CodeBaseResponse(int CodeKey)
        {
            this.CodeKey = CodeKey;
        }
        public int CodeKey { get; set; } = 1;
        //public string CodeName { get; set; } = "";
        public string ConditionCode { get; set; } = "";
        public string CodeName { get; set; } = "-";
        public string OurCode { get; set; } = "";


        public override string ToString()
        {
            if (CodeKey == 1)
            {
                return "-";
            }
            if (CodeName != null)
            {
                return this.CodeName;
            }
            return "-";

        }

        public string Code { get; set; } = "";

        public int Count { get; set; } = 0;

        public void SeperateServiceType()
        {
            if (CodeKey == 443470)
            {
                AddtionalData["IsKiloWash"] = true;
                AddtionalData["IsCommon"] = false;
            }
            else if (CodeKey == 1 || CodeKey == 431996 || CodeKey == 432173 || CodeKey == 435650)
            {
                AddtionalData["IsCommon"] = true;
                AddtionalData["IsKiloWash"] = false;
            }
            else
            {
                AddtionalData["IsCommon"] = false;
                AddtionalData["IsKiloWash"] = false;
            }
        }

    }
    public class AddressResponse : BaseResponse
    {
        public long AddressKey { get; set; } = 1;

        public string AddressName { get; set; } = "-";

        public override string ToString()
        {
            if (AddressKey == 1)
            {
                return "-";
            }
            if (AddressName != null)
            {
                return this.AddressName;
            }
            return "-";
        }


        public string AddressID { get; set; } = "";
        public string AddressSName { get; set; } = "";
        public string Alias { get; set; } = "";

    }

    public class ItemResponse : BaseResponse
    {
        public long ItemKey { get; set; } = 1;
        public string ItemName { get; set; } = "-";

        public decimal Rate { get; set; } = 0;
        public int LineNumber { get; set; } = 0;
        public string ItemCode { get; set; } = "";

        public string ItemNameOnly { get; set; } = "";  // Added

        public string ItemCodeOnly { get; set; } = "";  // Added
        public override string ToString()
        {
            if (ItemKey == 1)
            {
                return "-";

            }
            if (ItemName != null)
            {
                return ItemName;
            }
            return "-";
        }

        public CodeBaseResponse ItemCategory1 { get; set; } = new CodeBaseResponse();
        public CodeBaseResponse ItemCategory2 { get; set; } = new CodeBaseResponse();

        public CodeBaseResponse ItemType { get; set; } = new CodeBaseResponse();

        public ItemResponse()
        {
            ItemType = new CodeBaseResponse();
        }


        public bool IsCompensationItem()
        {


            return BaseResponse.IsValidData(ItemType) && ItemType.Code.Equals("CMPMS", StringComparison.InvariantCultureIgnoreCase);

        }

        public bool IsServiceItem()
        {


            return (BaseResponse.IsValidData(ItemType) && ItemType.Code.Equals("ServiceItem", StringComparison.InvariantCultureIgnoreCase)) || (ItemType != null && ItemType.OurCode.Equals("SERVICE", StringComparison.InvariantCultureIgnoreCase));

        }

        public bool IsWeightItem()
        {
            return BaseResponse.IsValidData(ItemType) && ItemType.Code.Equals("WI", StringComparison.InvariantCultureIgnoreCase);
        }

        public bool HasUptoTenWeight()
        {
            return !string.IsNullOrEmpty(ItemCodeOnly) && ItemCodeOnly.Equals("WGT-003", StringComparison.InvariantCultureIgnoreCase);
        }
        public bool IsAnyWeight()
        {
            return !string.IsNullOrEmpty(ItemCodeOnly) && ItemCodeOnly.Equals("WGT-004", StringComparison.InvariantCultureIgnoreCase);
        }

        public bool IsTangibleItem()
        {
            var v = !(IsCompensationItem() || IsServiceItem());
            return v;
        }

    }

    public class ItemCodeResponse : ItemResponse
    {
        public int UnitKey { get; set; } = 1;

        public string UnitName { get; set; } = "";

        public string ItemCodeName { get; set; } = "";

        public decimal TransactionRate { get; set; }

        public decimal DiscountPercentage { get; set; }

        public decimal VAT { get; set; }

        public decimal NBT { get; set; }

        public decimal WHT { get; set; }

        public decimal SVAT { get; set; }

    }


    public class UnitResponse : BaseResponse
    {
        public long UnitKey { get; set; } = 1;

        public string UnitName { get; set; } = "-";

        public override string ToString()
        {

            return this.UnitName;
        }



    }


    public class UIInterectionArgs<T> : EventArgs
    {
        public string ObjectPath { get; set; }

        public string Caller { get; set; }

        public T DataObject { get; set; }

        public object sender { get; set; }

        public bool CancelChange { get; set; }
        public bool OverrideValue { get; set; }

        public T OverriddenValue { get; set; }
        public BLUIElement InitiatorObject { get; set; }

        public bool DelegateExecuted { get; set; }


        public EventArgs e { get; set; }




    }

    public class AccountResponse : BaseResponse
    {
        public long AccountKey { get; set; } = 1;

        public string AccountName { get; set; } = "-";
        public string AccountCode { get; set; } = "";

        public override string ToString()
        {
            if (AccountKey == 1)
            {
                return "-";
            }
            if (AccountName != null)
            {
                return this.AccountName;
            }
            return "-";

        }
    }


    public class ProjectResponse : BaseResponse
    {
        public long ProjectKey { get; set; } = 1;

        public string ProjectName { get; set; } = "-";

        public string ProjectId { get; set; } = "-";

        public override string ToString()
        {
            return this.ProjectName;
        }
        public DateTime ProjectStartDate { get; set; } = DateTime.Now;
        public DateTime ProjectEndDate { get; set; } = DateTime.Now;
        public DateTime ExpiryDate { get; set; } = DateTime.Now;
        public string Alias { get; set; } = "";

    }
}
