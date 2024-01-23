using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace bl360.domain
{
    public class Order : BaseEntity
    {
        public long OrderKey { get; set; } = 1;
        public string OrderNumber { get; set; } = "";
        public string OrderDocumentNumber { get; set; } = "";
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime OrderFinishDate { get; set; } = DateTime.Now;
        public DateTime DeliveryDate { get; set; } = DateTime.Now;
        public string? Description { get; set; } = "";
        public CodeBaseResponse OrderLocation { get; set; }
        public CodeBaseResponse OrderLocation2 { get; set; } = new CodeBaseResponse();
        public CodeBaseResponse OrderPaymentTerm { get; set; }
        public AddressResponse OrderCustomer { get; set; }
        public AddressResponse OrderRepAddress { get; set; }
        public AccountResponse OrderAccount { get; set; } = new AccountResponse();
        public CodeBaseResponse OrderType { get; set; } = new CodeBaseResponse();
        public CodeBaseResponse OrderCategory1 { get; set; }
        public CodeBaseResponse OrderCategory2 { get; set; }
        public CodeBaseResponse OrderCategory3 { get; set; }
        public CodeBaseResponse OrderStatus { get; set; }
        public ProjectResponse OrderProject { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
        public OrderItem SelectedOrderItem { get; set; }
        public OrderItem EditingLineItem { get; set; }
        public long FormObjectKey { get; set; } = 1;
        public decimal HeaderLevelDisountPrecentage { get; set; }
        public bool IsFromQuotation { get; set; }
        public int Cd1Ky { get; set; } = 1;
        public string Prefix { get; set; } = "";
        public CodeBaseResponse OrderPrefix { get; set; }
        public CodeBaseResponse AddressCategory1 { get; set; } = new CodeBaseResponse();
        public CodeBaseResponse AddressCategory2 { get; set; } = new CodeBaseResponse();
        public CodeBaseResponse AddressCategory3 { get; set; } = new CodeBaseResponse();
        public decimal MeterReading { get; set; }
        public AddressResponse EnteredUser { get; set; }
        public CodeBaseResponse BussinessUnit { get; set; }
        public AccountResponse Insurance { get; set; }
        public int FromOrderKey { get; set; } = 1;
        public IList<Base64Document> Base64Documents { get; set; }
        public string OrderYourReference { get; set; } = "";
        public string HeaderDescription { get; set; } = "";
        public AccountResponse BaringHeaderPrincipleAccount { get; set; }
        public decimal PrincipalPercentage { get; set; }
        public decimal PrincipalAmount { get; set; }
        public AccountResponse BaringHeaderCompanyAccount { get; set; }
        public decimal CompanyPercentage { get; set; }
        public decimal CompanyAmount { get; set; }
        public decimal CustomerPrecentage { get; set; }
        public decimal CustomerAmount { get; set; }
        public bool IsIRNEstimateOrder { get; set; }
        public bool IsSupplimentaryEstimateOrder { get; set; }
        public int OrderHeaderAccountKey1 { get; set; } = 1;
        public int OrderHeaderAccountKey2 { get; set; } = 1;
        public int OrderHeaderAccountKey3 { get; set; } = 1;
        public decimal InsurencePrecentage { get; set; }
        public decimal InsurenceAmount { get; set; }
        public decimal OwnerPrecentage { get; set; }
        public decimal OwnerAmount { get; set; }
        public bool isHold { get; set; } = false;
        public string? PrefixedOrderNumber { get; set; } = "";
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public AddressResponse Address2 { get; set; }
        public CodeBaseResponse OrderApproveState { get; set; } = new CodeBaseResponse();
        public bool IsTrnRateInclusiveTaxTyp1_VAT { get; set; }
        public bool CmbItmisCd06isDisInclVAT { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal SubTotal { get; set; }
        public decimal NetTotal { get; set; }
        public decimal GrossTotal { get; set; }
        public decimal HeaderDiscountAmount { get; set; }
        public decimal TransactionDiscountAmount { get; set; }

        public bool IsPromotionItem { get; set; }


        public string UUID { get; set; } = string.Empty;

        public string ReferenceLineUUID { get;set; }=string.Empty;
        public decimal HeaderDiscountPercentage { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool ShouldSelectShift { get; set; }
        public long WorkStationKy { get; set; } = 1;
        public CodeBaseResponse Shift { get; set; } = new CodeBaseResponse();
        public DateTime ShiftDate { get; set; } = DateTime.Today;
        public decimal Amount { get; set; }
        public Order()
        {
            OrderLocation = new CodeBaseResponse();
            OrderPaymentTerm = new CodeBaseResponse();
            OrderCustomer = new AddressResponse();
            OrderRepAddress = new AddressResponse();
            OrderDocumentNumber = "";
            OrderItems = new List<OrderItem>();
            OrderType = new CodeBaseResponse();
            OrderAccount = new AccountResponse();
            OrderDocumentNumber = Guid.NewGuid().ToString().Substring(0, 8);
            SelectedOrderItem = new OrderItem();
            EditingLineItem = new OrderItem();
            OrderCategory1 = new CodeBaseResponse();
            OrderCategory2 = new CodeBaseResponse();
            OrderCategory3 = new CodeBaseResponse();
            OrderProject = new ProjectResponse();
            OrderStatus = new CodeBaseResponse();
            OrderPrefix = new CodeBaseResponse();
            EnteredUser = new AddressResponse();
            Insurance = new AccountResponse();
            BussinessUnit = new CodeBaseResponse();
            DeliveryDate = DateTime.Now;
            Base64Documents = new List<Base64Document>();
            BaringHeaderPrincipleAccount = new AccountResponse();
            BaringHeaderCompanyAccount = new AccountResponse();
            AddressCategory1 = new CodeBaseResponse();
            AddressCategory2 = new CodeBaseResponse();
            AddressCategory3 = new CodeBaseResponse();
            Address2 = new AddressResponse();
            OrderApproveState = new CodeBaseResponse();
            OrderDate = DateTime.Now;
            DeliveryDate = DateTime.Now;
        }


        public OrderItem CreateOrderItem(ItemResponse transactionItem, CodeBaseResponse parentLocation)
        {
            OrderItem item = new OrderItem();
            item.OrderLineLocation = parentLocation;
            item.OrderType = this.OrderType;
            item.TransactionQuantity = 1;
            item.TransactionItem = transactionItem;
            item.AvailableStock = 10;
            item.TransactionRate = transactionItem.Rate;
            this.SelectedOrderItem = item;
            return item;
        }

        public OrderItem CreateOrderItem(OrderItem item)
        {

            item.OrderType = this.OrderType;
            item.TransactionQuantity = 1;
            this.SelectedOrderItem = item;
            return item;
        }

        public void Update(OrderItem itm, int index)
        {
            OrderItems[index].TransactionItem = itm.TransactionItem;
            OrderItems[index].TransactionQuantity = itm.TransactionQuantity;
            OrderItems[index].Rate = itm.Rate;
            OrderItems[index].DiscountPercentage = itm.DiscountPercentage;
            OrderItems[index].OrderType = itm.OrderType;
            OrderItems[index].AvailableStock = itm.AvailableStock;
            OrderItems[index].TransactionUnit = itm.TransactionUnit;

            if (itm.RequestedQuantity > 0)
            {
                OrderItems[index].RequestedQuantity = itm.RequestedQuantity;
                OrderItems[index].OrderLineLocation = itm.OrderLineLocation;
                OrderItems[index].IsTransfer = itm.IsTransfer;
                OrderItems[index].IsTransferConfirmed = itm.IsTransferConfirmed;
            }

        }

        #region old calculations
        public decimal GetOrderTotalWithDiscounts()
        {
            return OrderItems.Sum(x => x.GetLineTotalWithDiscount());
        }

        public decimal GetOrderTotalWithoutDiscounts()
        {
            return OrderItems.Sum(x => x.GetLineTotalWithoutDiscount());
        }

        public decimal GetOrderTotalWithTaxes()
        {
            return OrderItems.Sum(x => x.GetLineTotalWithTax());
        }


        public decimal GetOrderRateTotal()
        {
            return OrderItems.Sum(x => (x.IsActive == 1) ? x.TransactionRate : 0);

        }

        public decimal GetOrderDiscountTotal()
        {
            return OrderItems.Sum(d => d.GetLineDiscount());
        }

        public decimal GetQuantityTotal()
        {
            return OrderItems.Sum(q => (q.IsActive == 1) ? q.TransactionQuantity : 0);
        }

        public decimal GetRequestedQuantityTotal()
        {
            return OrderItems.Sum(q => (q.IsActive == 1) ? q.RequestedQuantity : 0);
        }

        public decimal GetTotalTaxType1()
        {
            return OrderItems.Sum(q => q.GetItemTaxType1());
        }

        public decimal GetTotalTaxType4()
        {
            return OrderItems.Sum(q => q.GetItemTaxType4());
        }

        public decimal GetTotalTaxType5()
        {
            return OrderItems.Sum(q => q.GetItemTaxType5());
        }
        public decimal GetCarMartOrderTotalWithTaxes()
        {
            return OrderItems.Sum(x => (x.IsActive == 1) ? x.GetCarMartLineTotalWithTax() : 0);
        }
        #endregion
        public void AddGridItems(OrderItem item)
        {
            OrderItems.Add(item);
            SelectedOrderItem = new();
        }

        public void CopyFrom(Order source)
        {
            source.CopyProperties(this);

        }

        #region new calculations
        public decimal GetOrderTotalWithDiscountsNew()//gross total
        {
            decimal amt = OrderItems.Where(x => x.IsActive == 1 && !x.TransactionItem.IsCompensationItem()).Sum(x => x.GetLineTotalWithDiscountNew());
            amt = Math.Max(amt, 0);

            return amt;
        }

        public decimal GetOrderTotalWithoutDiscountsNew()//sub total 
        {
            return OrderItems.Where(x => x.IsActive == 1).Sum(x => x.GetLineTotalWithoutDiscountNew());
        }

        public decimal GetTransactionRateTotalNew()
        {
            return OrderItems.Where(x => x.IsActive == 1).Sum(x => x.TransactionRate);
        }

        public decimal GetOrderNetLineTotal(decimal HdrDisAmt = 0, decimal SubTTLDis = 0)
        {
            return OrderItems.Where(x => x.IsActive == 1 && !x.TransactionItem.IsCompensationItem()).Sum(x => x.GetNetLineTotalNew(HdrDisAmt, SubTTLDis));
        }

        public decimal GetOrderDiscountTotalNew()
        {
            return OrderItems.Where(x => x.IsActive == 1).Sum(d => d.GetLineDiscountNew());
        }
        public decimal CalculateCBalancesNew()
        {
            // Amount6 = Amount1 + Amount3 - this.GetOrderTotalWithDiscountsNew();
            return 0;

        }

        public void CalculateTotalsNew()
        {
            TotalQuantity = GetQuantityTotal();

            foreach (var trnItm in OrderItems.Where(x => x.IsActive == 1 && !x.TransactionItem.IsCompensationItem()))
            {
                trnItm.GetNetLineTotalNew(0, 0, 0, IsTrnRateInclusiveTaxTyp1_VAT, CmbItmisCd06isDisInclVAT);
            }

            decimal tempGrossTotal = OrderItems.Where(x => x.IsActive == 1).Sum(d => (d.SubTotal - ( d.TransactionDiscountAmount+d.Discount2Amount)));
            foreach (var trnItm in OrderItems.Where(x => x.IsActive == 1 && !x.TransactionItem.IsCompensationItem()))
            {
                trnItm.GetNetLineTotalNew(HeaderDiscountAmount, HeaderLevelDisountPrecentage, tempGrossTotal, IsTrnRateInclusiveTaxTyp1_VAT, CmbItmisCd06isDisInclVAT);
            }

            if (HeaderLevelDisountPrecentage > 0)
            {
                HeaderDiscountAmount = tempGrossTotal * HeaderLevelDisountPrecentage / 100;
            }
            else
            {
                if (tempGrossTotal > 0)
                {
                    HeaderLevelDisountPrecentage = Math.Round(HeaderDiscountAmount * 100 / tempGrossTotal, 6);
                }

            }

            // TransactionDiscountAmount = OrderItems.Where(x => x.IsActive == 1).Sum(d => d.TransactionDiscountAmount);
            TransactionDiscountAmount = HeaderDiscountAmount;
            SubTotal = OrderItems.Where(x => x.IsActive == 1).Sum(d => d.SubTotal);
            GrossTotal = OrderItems.Where(x => x.IsActive == 1).Sum(d => d.GrossTotal);
            NetTotal = OrderItems.Where(x => x.IsActive == 1).Sum(d => d.NetTotal);
            Amount = NetTotal;
        }

        public void CalculateTotalsNewWithVaTInclusive()
        {
            TotalQuantity = GetQuantityTotal();

            //sub total
            TransactionDiscountAmount = OrderItems.Where(x => x.IsActive == 1).Sum(d => Math.Round((d.TransactionRate * d.DiscountPercentage / 100) * d.TransactionQuantity, 6));

            SubTotal = GetOrderTotalWithoutDiscountsNew();
            decimal DiffSubTTLDis = GetOrderTotalWithDiscountsNew();
            GrossTotal = GetOrderTotalWithDiscountsNew() - HeaderDiscountAmount;//gross total
            NetTotal = GetOrderNetLineTotal(HeaderDiscountAmount, DiffSubTTLDis);//net total & header discount portionate
        }

        public decimal GetVATInclusiveAmount(string property)
        {
            if (property.Equals("HdrDis"))
            {
                return OrderItems.Where(p => p.IsActive == 1 && !p.IsVoidItem() && !p.IsReturnItem).Sum(x => x.GetVatInclusiveLineAmount(property));
            }
            return OrderItems.Where(p => p.IsActive == 1 && !p.IsVoidItem()).Sum(x => x.GetVatInclusiveLineAmount(property));
        }
        #endregion
    }


    public class OrderItem : BaseEntity
    {
        public int FromOrderDetailKey { get; set; } = 1;
        public decimal TransactionRate { get; set; }
        public decimal Rate { get; set; }
        public CodeBaseResponse OrderLineLocation { get; set; }
        public int IsTransfer { get; set; }
        public int IsTransferConfirmed { get; set; }
        public decimal TransactionQuantity { get; set; }
        public decimal TransferQuantity { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TransactionDiscountAmount { get; set; }
        public ItemResponse TransactionItem { get; set; }
        public CodeBaseResponse OrderType { get; set; }
        public decimal AvailableStock { get; set; }
        public decimal LineNumber { get; set; }
        public decimal RequestedQuantity { get; set; }
        public decimal LineTotal { get; set; }
        public decimal LineTotalWithoutDiscount { get; set; }
        public decimal ItemTaxType1 { get; set; }
        public decimal ItemTaxType2 { get; set; }
        public decimal ItemTaxType3 { get; set; }
        public decimal ItemTaxType4 { get; set; }
        public decimal ItemTaxType5 { get; set; }
        public decimal ItemTaxType1Per { get; set; }
        public decimal ItemTaxType2Per { get; set; }
        public decimal ItemTaxType3Per { get; set; }
        public decimal ItemTaxType4Per { get; set; }
        public decimal ItemTaxType5Per { get; set; }
        public decimal ItemTaxType5Per2 { get; set; }
        public UnitResponse TransactionUnit { get; set; }
        public CodeBaseResponse BussinessUnit { get; set; }
        public bool IsInEditMode { get; set; }
        public bool IsJustAdded { get; set; }
        public bool IsItemLocked { get; set; }
        public bool IsAlwMinusQty { get; set; }
        public bool IsLineDiscountChanged { get; set; }
        public int OrderDetailsAccountKey1 { get; set; } = 1;
        public int OrderDetailsAccountKey2 { get; set; } = 1;
        public int OrderDetailsAccountKey3 { get; set; } = 1;
        public AccountResponse BaringPrinciple { get; set; }
        public decimal PrinciplePrecentage { get; set; }
        public decimal PrincipleAmount { get; set; }
        public AccountResponse BaringCompany { get; set; }
        public decimal CompanyPrecentage { get; set; }
        public decimal CompanyAmount { get; set; }
        public AccountResponse BaringCustomer { get; set; }
        public decimal CustomerPrecentage { get; set; }
        public decimal CustomerAmount { get; set; }
        public int IsSelected { get; set; }
        public AccountResponse Supplier { get; set; }//where to map
        public User EnteredUser { get; set; }
        public IList<AddressResponse> ResourceAddressList { get; set; }
        public AddressResponse ResourceAddress { get; set; }
        public string Description { get; set; } = "";
        public DateTime EnteredDate { get; set; }//where to map
        public bool IsServiceItem { get; set; }
        public bool IsMaterialItem { get; set; }
        public bool IsOtherService { get; set; }
        public bool IsNoteItem { get; set; }
        public decimal Time { get; set; }
        public decimal NetTotal { get; set; }
        public ItemResponse Packages { get; set; } // FOR INSURENCE
        public decimal VAT { get; set; }
        public decimal VATAmount { get; set; }
        public string Catergory { get; set; } = "";
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public DateTime RequiredDate { get; set; } = DateTime.Now;
        public CodeBaseResponse AnalysisType1 { get; set; }
        public CodeBaseResponse AnalysisType2 { get; set; }
        public CodeBaseResponse AnalysisType3 { get; set; }
        public CodeBaseResponse AnalysisType4 { get; set; }
        public decimal InsurencePrecentage { get; set; }
        public decimal InsurenceAmount { get; set; }
        public decimal OwnerPrecentage { get; set; }
        public decimal OwnerAmount { get; set; }
        public decimal ApprovedAmount { get; set; }
        public decimal OwnerTemporyDiscount { get; set; }
        public decimal OwnerTemporyAmount { get; set; }
        public decimal OwnerRateAfterDiscount { get; set; }
        public decimal InsuranceTemporyDiscount { get; set; }
        public decimal InsuranceTemporyAmount { get; set; }
        public decimal InsuranceRateAfterDiscount { get; set; }
        public int FromOrderDetKy { get; set; } = 1;
        public ProjectResponse OrderLineProject { get; set; }
        public int ProcessDetailsKey { get; set; } = 1;
        public int ObjectKey { get; set; } = 1;
        public decimal SubTotal { get; set; }

        //aggregates 
        public decimal LineDiscount { get { return GetLineDiscount(); } }
        public decimal ItemTaxType1Total { get { return GetItemTaxType1(); } }
        public decimal ItemTaxType4Total { get { return GetItemTaxType4(); } }
        public decimal LineTotalWithTax { get { return GetLineTotalWithTax(); } }

        public decimal ItemTaxType6Per { get; set; }
        public decimal ItemTaxType6 { get; set; }
        public bool IsRateInclusiveTaxType1 { get; set; }
        public bool CalculateTaxOnPreDiscountValue { get; set; }
        public decimal GrossTotal { get; set; }
        public int NumOfDecimalPoint { get; set; } = 6;
        public decimal HeaderDiscountPercentage { get; set; }
        public decimal HeaderDiscountAmount { get; set; }
        public bool IsReturnItem { get; set; }
        public bool IsReplacingItem { get; set; }

        public decimal Discount2Percentage { get; set; }
        public decimal Discount2Amount { get; set; }

        public string LineUUID { get; set; } = string.Empty;
        public string RefLineUUID { get; set; } = string.Empty;

        public bool IsPromotionItem { get; set; }

        public OrderItem()
        {
            OrderLineLocation = new CodeBaseResponse();
            TransactionItem = new ItemResponse();
            TransactionUnit = new UnitResponse();
            OrderType = new CodeBaseResponse();
            BussinessUnit = new CodeBaseResponse();
            Supplier = new AccountResponse();
            ResourceAddressList = new List<AddressResponse>();
            ResourceAddress = new AddressResponse();
            EnteredUser = new User();
            BaringCompany = new AccountResponse();
            BaringPrinciple = new AccountResponse();
            BaringCustomer = new AccountResponse();
            Packages = new ItemResponse();
            AnalysisType1 = new CodeBaseResponse();
            AnalysisType2 = new CodeBaseResponse();
            AnalysisType3 = new CodeBaseResponse();
            AnalysisType4 = new CodeBaseResponse();
            OrderLineProject = new ProjectResponse();
            LineUUID = Guid.NewGuid().ToString();

        }

        public bool IsVoidItem()
        {
            return IsApproved == 17;
        }

        #region old calculations

        public decimal GetLineDiscount()
        {
            if (this.IsActive == 1)
            {
                DiscountAmount = (this.TransactionRate * DiscountPercentage / 100) * TransactionQuantity;
            }
            else
            {
                DiscountAmount = 0;
            }

            return DiscountAmount;

        }

        public decimal GetDiscount2Amount()
        {
            if (this.IsActive == 1)
            {
                Discount2Amount = (GetLineTotalWithoutDiscount() - GetLineDiscount()) * Discount2Percentage / 100;
            }
            else
            {
                Discount2Amount = 0;
            }

            return Discount2Amount;
        }

        public decimal GetLineTotalWithDiscount()
        {
            return GetLineTotalWithoutDiscount() - (GetLineDiscount() + GetDiscount2Amount()) - HeaderDiscountAmount;
        }


        public decimal GetLineTotalWithTax()
        {
            return GetLineTotalWithDiscount() + GetItemTaxType1() + GetItemTaxType4() + GetItemTaxType5();
        }

        public decimal GetLineTotalWithoutDiscount()
        {
            if (this.IsActive == 1)
            {
                return TransactionQuantity * TransactionRate;
            }
            else
            {
                return 0;
            }


        }
        public void CalculateCarmartAccountBalance()
        {
            CustomerAmount = (this.CustomerPrecentage / 100) * GetBalanceAfterAddingItemTaxType5();
            CompanyAmount = GetBalanceAfterAddingItemTaxType5() - CustomerAmount;
            CompanyPrecentage = 100 - this.CustomerPrecentage;
        }
        public bool NeedToRequestFromAnotherLocation()
        {
            return this.AvailableStock < this.TransactionQuantity && IsItemLocked;
        }

        public decimal GetItemTaxType1()
        {
            if (this.IsActive == 1)
            {
                ItemTaxType1 = (GetLineTotalWithDiscount() * this.ItemTaxType1Per) / 100;
            }
            else
            {
                ItemTaxType1 = 0;
            }

            return ItemTaxType1;
        }

        public decimal GetItemTaxType4()
        {
            if (this.IsActive == 1)
            {
                ItemTaxType4 = (GetLineTotalWithDiscount() * this.ItemTaxType4Per) / 100;
            }
            else
            {
                ItemTaxType4 = 0;
            }


            return ItemTaxType4;
        }

        public decimal GetItemTaxType5()
        {
            if (this.IsActive == 1)
            {
                ItemTaxType5 = (GetLineTotalWithDiscount() * this.ItemTaxType5Per) / 100;
            }
            else
            {
                ItemTaxType5 = 0;
            }


            return ItemTaxType5;
        }

        public decimal GetLineCompanyAmount()
        {
            CompanyAmount = ((this.GetBalanceAfterAddingItemTaxType5() + this.GetVatItemTaxType1()) * CompanyPrecentage / 100);

            return CompanyAmount;
        }

        public decimal GetLinePrincipleAmount()
        {
            PrincipleAmount = ((this.GetBalanceAfterAddingItemTaxType5() + this.GetVatItemTaxType1()) * PrinciplePrecentage / 100);

            return PrincipleAmount;
        }

        public decimal GetLineCustomerAmount()
        {
            CustomerAmount = (GetBalanceAfterAddingItemTaxType5() + GetVatItemTaxType1()) - GetLineCompanyAmount() - GetLinePrincipleAmount();
            return CustomerAmount;
        }


        public void CalculateLineBalance()
        {
            this.GetLineTotalWithDiscount();
            this.GetLineCompanyAmount();
            this.GetLinePrincipleAmount();
            this.GetLineCustomerAmount();

            NetTotal = GetLineTotalWithTax();

            //SubTotal = GetLineTotalWithTax();
        }

        //CarMartWorkShop

        public void CarMartWorkShopCalculateLineBalance()
        {
            this.GetLineTotalWithDiscount();
            this.GetLineCompanyAmount();
            this.GetLinePrincipleAmount();
            this.GetLineCustomerAmount();
            SubTotal = GetBalanceAfterAddingItemTaxType5();
            NetTotal = GetBalanceAfterAddingItemTaxType5() + GetVatItemTaxType1();
        }

        // CarMartInsurance
        public void CarMartInsuranceCalculateLineBalance()
        {
            this.GetLineTotalWithDiscount();
            SubTotal = GetBalanceAfterAddingItemTaxType5();
            NetTotal = GetBalanceAfterAddingItemTaxType5() + GetVatItemTaxType1();
        }
        public decimal GetBalanceAfterAddingItemTaxType5()
        {
            return GetLineTotalWithDiscount() + GetItemTaxType5();
        }
        public decimal GetVatItemTaxType1()
        {
            if (this.IsActive == 1)
            {
                ItemTaxType1 = (GetBalanceAfterAddingItemTaxType5() * this.ItemTaxType1Per) / 100;
            }
            else
            {
                ItemTaxType1 = 0;
            }

            return ItemTaxType1;
        }

        public decimal GetCarMartLineTotalWithTax()
        {
            return GetBalanceAfterAddingItemTaxType5() + GetVatItemTaxType1();
        }
        //
        public void CopyFrom(OrderItem source)
        {
            source.CopyProperties(this);

        }

        public OrderItem  ProcessItemPromotion(decimal OfferQuantity)
        {

            return this;
        }

        #endregion

        #region new calculations

        public decimal GetLineDiscountNew()
        {
            return Math.Round((this.TransactionRate * DiscountPercentage / 100) * TransactionQuantity, NumOfDecimalPoint); ;
        }
        public decimal GetLineDiscountPercentageNew()
        {
            if (TransactionRate > 0)
            {
                DiscountPercentage = Math.Round(TransactionDiscountAmount * 100 / this.TransactionRate, NumOfDecimalPoint);
                return DiscountPercentage;
            }
            else
            {
                return 0;
            }

        }
        public decimal GetLineTotalWithoutDiscountNew()
        {
            return Math.Round(TransactionQuantity * TransactionRate, NumOfDecimalPoint);
        }

        public decimal GetItemTaxType1New()//vat - (subtotal-linedis-headerdis+sscl)*itmtaxtype1per
        {
            decimal itemTaxType1 = 0;
            if (this.IsActive == 1)
            {
                itemTaxType1 = Math.Round((GrossTotal + ItemTaxType5) * this.ItemTaxType1Per / 100, NumOfDecimalPoint);
            }
            else
            {
                itemTaxType1 = 0;
            }

            return itemTaxType1;
        }

        public decimal GetItemTaxType4New()//svat- (subtotal-linedis-headerdis)*itmtaxtype4per
        {
            decimal itemTaxType4 = 0;
            if (this.IsActive == 1)
            {
                itemTaxType4 = Math.Round((GrossTotal * this.ItemTaxType4Per) / 100, NumOfDecimalPoint);
            }
            else
            {
                itemTaxType4 = 0;
            }


            return itemTaxType4;
        }

        public decimal GetItemTaxType5New()//sscl- (subtotal-linedis-headerdis)*itmtaxtype5per1*itmtaxtype2per
        {
            decimal itemTaxType5 = 0;
            if (this.IsActive == 1)
            {
                itemTaxType5 = Math.Round((this.GrossTotal * this.ItemTaxType5Per2 * this.ItemTaxType5Per) / 100, NumOfDecimalPoint);

            }
            else
            {
                itemTaxType5 = 0;
            }


            return itemTaxType5;
        }

        public decimal GetLineTotalWithDiscountNew()
        {
            decimal lineTotal = GetLineTotalWithoutDiscountNew();
            decimal dis = GetLineDiscountNew();

            return lineTotal - dis;
        }

        public decimal GetNetLineTotalNew(decimal HdrDisAmt = 0, decimal HdrDisPer = 0, decimal SubTTLDis = 0, bool IsTrnRateInclusiveTaxTyp1_VAT = false, bool CmbItmisCd06isDisInclVAT = false)
        {
            SubTotal = GetSubTotWithoutVATCal(IsTrnRateInclusiveTaxTyp1_VAT);
            if (IsTrnRateInclusiveTaxTyp1_VAT && CmbItmisCd06isDisInclVAT)
            {
                if (DiscountPercentage > 0)
                {
                    TransactionDiscountAmount = GetDisAmtCal(DiscountPercentage, SubTotal);
                }
                else if (TransactionDiscountAmount > 0 && DiscountPercentage == 0)
                {
                    decimal newvattemp = (100 + ItemTaxType1Per) / 100;
                    TransactionDiscountAmount = TransactionDiscountAmount / newvattemp;
                    DiscountPercentage = GetDisPerCal(TransactionDiscountAmount, SubTotal);
                }
            }
            else if (TransactionDiscountAmount > 0 && DiscountPercentage == 0)
            {
                DiscountPercentage = GetDisPerCal(TransactionDiscountAmount, SubTotal);
            }
            else
            {
                TransactionDiscountAmount = GetDisAmtCal(DiscountPercentage, SubTotal);
                if (Discount2Percentage>0) 
                {
                    Discount2Amount = (SubTotal - TransactionDiscountAmount) * Discount2Percentage / 100;
                }
            }

            if (HdrDisAmt > 0 && HdrDisPer == 0)
            {
                HeaderDiscountAmount = CalculateHdrDisAmtDetail(HdrDisAmt, SubTTLDis);
            }
            else if (HdrDisAmt >= 0 && HdrDisPer > 0)
            {
                HeaderDiscountAmount = Math.Round((SubTotal - TransactionDiscountAmount-Discount2Amount) * HdrDisPer / 100, NumOfDecimalPoint);
            }
            else
            {
                HeaderDiscountAmount = 0;
            }

            GrossTotal = (SubTotal - TransactionDiscountAmount-Discount2Amount - HeaderDiscountAmount);
            ItemTaxType5 = GetItemTaxType5New();
            ItemTaxType1 = GetItemTaxType1New();
            ItemTaxType4 = GetItemTaxType4New();

            NetTotal = GrossTotal + ItemTaxType1 + ItemTaxType4 + ItemTaxType5;
            return NetTotal;
        }

        public void ReverseCalculation(decimal HdrDisAmt = 0, decimal HdrDisPer = 0, decimal SubTTLDis = 0, bool IsTrnRateInclusiveTaxTyp1_VAT = false, bool CmbItmisCd06isDisInclVAT = false)
        {
        }

        public decimal GetVATExclusiveDiscount(bool IsTrnRateInclusiveTaxTyp1_VAT, decimal DisPer)//used only for setvalue
        {
            decimal subTotWOVAT = GetSubTotWithoutVATCal(IsTrnRateInclusiveTaxTyp1_VAT);

            return GetDisAmtCal(DisPer, subTotWOVAT);

        }
        public decimal GetVATExclusiveDiscountPer(bool IsTrnRateInclusiveTaxTyp1_VAT, bool CmbItmisCd06isDisInclVAT, decimal DisAmt)//used only for setvalue
        {
            decimal subTotWOVAT = GetSubTotWithoutVATCal(IsTrnRateInclusiveTaxTyp1_VAT);
            decimal disper = 0;
            if (IsTrnRateInclusiveTaxTyp1_VAT && CmbItmisCd06isDisInclVAT)
            {
                decimal newvattemp = (100 + ItemTaxType1Per) / 100;
                DisAmt = DisAmt / newvattemp;
                disper = GetDisPerCal(DisAmt, subTotWOVAT);

            }
            else
            {
                disper = GetDisPerCal(DisAmt, subTotWOVAT);
            }

            return disper;

        }
        public void DiscountRounder()
        {
            if (DiscountAmount > 0) { }
            if (TransactionDiscountAmount % 1 != 0)
            {
                TransactionDiscountAmount = Math.Round(TransactionDiscountAmount);
                if (TransactionRate > 0)
                {
                    DiscountPercentage = Math.Round(TransactionDiscountAmount * 100 / this.TransactionRate, NumOfDecimalPoint);
                }
                else
                {
                    DiscountPercentage = 0;
                }
            }

        }
        private decimal GetSubTotWithoutVATCal(bool IsTrnRateInclusiveTaxTyp1_VAT)
        {
            decimal subtotlwithoutVATtemp = GetLineTotalWithoutDiscountNew();
            if (IsTrnRateInclusiveTaxTyp1_VAT)
            {
                subtotlwithoutVATtemp = Math.Round(subtotlwithoutVATtemp * 100 / (100 + ItemTaxType1Per), NumOfDecimalPoint);
            }
            return subtotlwithoutVATtemp;
        }
        private decimal GetDisAmtCal(decimal DisPer, decimal SubTotalWithoutVATInclusive)
        {
            decimal disamttemp = 0;
            disamttemp = Math.Round((SubTotalWithoutVATInclusive * DisPer) / (100), NumOfDecimalPoint);
            return disamttemp;
        }
        private decimal CalculateHdrDisAmtDetail(decimal HdrDisAmt, decimal SubTTLDis)
        {
            decimal headerDisAmt = 0;
            IsDirty = true;
            if (HdrDisAmt > 0)
            {
                if (IsActive == 1)
                {
                    var GrsDisAmt = SubTotal - TransactionDiscountAmount-Discount2Amount;
                    if (SubTTLDis > 0)
                    {
                        var tempDisAmt = (GrsDisAmt / SubTTLDis);
                        headerDisAmt = Math.Round(tempDisAmt * HdrDisAmt, NumOfDecimalPoint);
                        //GridLine.ItemTaxType1= (GridLine.ItemTaxType1Per / 100) * (GrsDisAmt - GridLine.HeaderDiscountAmount + GridLine.ItemTaxType4);
                    }

                }
            }
            else if (HdrDisAmt == 0)
            {
                if (IsActive == 1)
                {
                    var GrsDisAmt = SubTotal - TransactionDiscountAmount + ItemTaxType4;
                    headerDisAmt = 0;
                }
            }

            return headerDisAmt;
        }
        private decimal GetDisPerCal(decimal DisAmt, decimal SubTtlWOVAT)
        {
            decimal disper = 0;
            disper = Math.Round((DisAmt / SubTtlWOVAT) * 100, NumOfDecimalPoint);
            return disper;
        }

        public decimal GetVatInclusiveLineAmount(string property)
        {
            switch (property)
            {
                case "SubTotal": return Math.Round(TransactionRate * TransactionQuantity, 2);
                case "Dis": return Math.Round(TransactionDiscountAmount * (100 + ItemTaxType1Per) / 100, 2);
                case "HdrDis": return Math.Round(HeaderDiscountAmount * (100 + ItemTaxType1Per) / 100, 2);
                default: return 0;
            }
        }
        #endregion

    }

    public class OrderPresavingValidationResponse
    {
        public bool HasError { get; set; }
        public string Message { get; set; } = "";
        public bool IsLock { get; set; }
    }
    public class ItemRateRequest
    {
        public long ObjectKey { get; set; } = 1;

        public long ItemKey { get; set; } = 1;
        public DateTime EffectiveDate { get; set; }
        public long LocationKey { get; set; } = 1;
        public long TransactionTypeKey { get; set; } = 1;
        public long BussienssUnitKey { get; set; } = 1;
        public long ProjectKey { get; set; } = 1;
        public long AddressKey { get; set; } = 1;

        public long AccountKey { get; set; } = 1;
        public long PayementTermKey { get; set; } = 1;
        public long Code1Key { get; set; }
        public long Code2Key { get; set; }
        public decimal Rate { get; set; }

        public string ConditionCode { get; set; } = "";

        public long TransactionUnitKey { get; set; } = 1;
        public long ToLocationKey { get; set; } = 1;
        public long OrderCategory1Key { get; set; } = 1;


    }

    public class ItemRequestModel
    {
        public int ObjectKey { get; set; } = 1;
        public long TransactionTypeKey { get; set; } = 1;
        public long ItemKey { get; set; } = 1;
        public long LocationKey { get; set; } = 1;

        public long ProjectKey { get; set; } = 1;

        public long AddressKey { get; set; } = 1;
        public string ItemCode { get; set; } = "";
        public DateTime RequestDate { get; set; }

    }


    public class OrderSaveResponse
    {
        public string OrderNumber { get; set; } = "";
        public string Prefix { get; set; } = "";

        public long OrderKey { get; set; } = 1;
        public int MaterialRequsitionKey { get; set; } = 1;
        public int ServiceRequsitionKey { get; set; } = 1;

    }

    public class OrderOpenRequest
    {
        public long OrderKey { get; set; } = 1;//get quotation order ky
        public long BaseTypKy { get; set; } = 1;//get sales order ordertypky
        public long PrjKy { get; set; } = 1;// selected projected ky or else 1
        public long ObjKy { get; set; } = 1;
        public long TransactionKey { get; set; } = 1;
        public long EstimationKey { get; set; } = 1;
        public long MaterialRequsitionKey { get; set; } = 1;
        public long WorkOrderServiceRequsitionKey { get; set; } = 1;
        public long MaterialIssueKey { get; set; } = 1;
        public int AddressKy1 { get; set; } = 1;
    }

    public class OrderRecallDTO
    {
        public DateTime fromDate { get; set; } = DateTime.UtcNow;
        public DateTime toDate { get; set; } = DateTime.UtcNow;
        public int OrdTypKy { get; set; } = 1;
        public int ObjKy { get; set; } = 1;
    }
    public class OrderFindDto
    {
        public DateTime FromDate { get; set; } = DateTime.Now;
        public DateTime ToDate { get; set; } = DateTime.Now;
        public int OrderTypeKey { get; set; }
        public string OrderNo { get; set; } = "";
        public int FromOrderNumber { get; set; } = 1;
        public int ToOrderNumber { get; set; } = int.MaxValue;

        public string DocumentNumber { get; set; } = "";
        public string YourReference { get; set; } = "";

        public int LocationKey { get; set; } = 1;

        public int ProjectKey { get; set; } = 1;
        public int AccountKey { get; set; } = 1;

        public int AddressKey { get; set; } = 1;

        public int ApproveStatusKey { get; set; } = 1;
        public bool IsPrinterd { get; set; }

        public bool IsRecurence { get; set; }

        public int PrefixKey { get; set; } = 1;

        public int ObjectKey { get; set; } = 1;
        public CodeBaseResponse Location { get; set; }
        public CodeBaseResponse Prefix { get; set; }
        public bool IsWorkOrder { get; set; }
        public int VehicleAddressKey { get; set; } = 1;
        public CodeBaseResponse OrderStatus { get; set; }
        public string? NullableFromDateString { get; set; } = "";
        public string? NullableToDateString { get; set; } = "";
        public OrderFindDto()
        {
            Location = new CodeBaseResponse();
            Prefix = new CodeBaseResponse();
            OrderStatus = new CodeBaseResponse();
        }
    }

    public class OrderRecallResults
    {
        public int OrdKy { get; set; } = 1;
        public DateTime? OrdDt { get; set; }
        public int OrdNo { get; set; } = 1;
        public string rem { get; set; }
    }

    public class OrderFindResults
    {
        public int OrderKey { get; set; } = 1;

        public DateTime OrderDate { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public string Prefix { get; set; } = "";

        public string OrderNumber { get; set; } = "";

        public string DocumentNumber { get; set; } = "";

        public string YourReference { get; set; } = "";

        public string Description { get; set; } = "";
        public string CusSupId { get; set; } = "";
        public string CusSupName { get; set; } = "";
        public int ProjectKey { get; set; }
        public string ProjectName { get; set; } = "";
        public CodeBaseResponse ApproveState { get; set; }
        public AccountResponse Account { get; set; }
        public int RequestingObjectKey { get; set; }
        public string PreviewURL { get; set; } = "";
        public int EntUsrKy { get; set; }
        public int IsActive { get; set; }
        public CodeBaseResponse ApproveReason { get; set; }
        public CodeBaseResponse OrderCategory1 { get; set; }
        public CodeBaseResponse OrderCategory2 { get; set; }
        public CodeBaseResponse OrderStatus { get; set; }
        public OrderFindResults()
        {
            ApproveState = new CodeBaseResponse();
            Account = new AccountResponse();
            ApproveReason = new CodeBaseResponse();
            OrderCategory1 = new CodeBaseResponse();
            OrderCategory2 = new CodeBaseResponse();
            OrderStatus = new CodeBaseResponse();
        }
    }

    public class FindQuoteDTO
    {
        public DateTime FromDate { get; set; } = DateTime.Now;
        public DateTime ToDate { get; set; } = DateTime.Now;
        public int OrderTypeKey { get; set; }
        public int FromOrderNumber { get; set; } = 0;
        public int ToOrderNumber { get; set; } = int.MaxValue;

        public string DocumentNumber { get; set; } = "";
        public string YourReference { get; set; } = "";

        public int LocationKey { get; set; } = 1;

        public int ProjectKey { get; set; } = 1;
        public int AccountKey { get; set; } = 1;

        public int AddressKey { get; set; } = 1;

        public int ApproveStatusKey { get; set; } = 1;
        public bool IsPrinterd { get; set; }

        public bool IsRecurence { get; set; }

        public int PrefixKey { get; set; } = 1;

        public int ObjectKey { get; set; } = 1;
    }

    public class GetFromQuoatationDTO
    {
        public int OrdTypKy { get; set; }
        public int PreOrdTypKy { get; set; }
        public AddressResponse Supplier { get; set; }
        public AddressResponse AdvAnalysis { get; set; }
        public CodeBaseResponse Location { get; set; }
        public DateTime FromDate { get; set; } = new DateTime(1990, 1, 1);
        public DateTime ToDate { get; set; } = DateTime.Now;
        public ProjectResponse Project { get; set; }
        public string SoNo { get; set; } = "";
        public int OrdNo { get; set; }
        public CodeBaseResponse PreFix { get; set; }
        public long ObjKy { get; set; }

        public GetFromQuoatationDTO()
        {
            Supplier = new();
            AdvAnalysis = new();
            Location = new CodeBaseResponse();
            Project = new ProjectResponse();
            PreFix = new CodeBaseResponse();
        }

    }
    public class GetFromQuotResults
    {
        public int OrdKy { get; set; }
        public string OrdNo { get; set; } = "";
        public DateTime OrdDt { get; set; }
        public string DocNo { get; set; } = "";
        public int PrjId { get; set; }
        public string PrjNm { get; set; } = "";
        public string SupAccCd { get; set; } = "";
        public string SupAccNm { get; set; } = "";
        public string Prefix { get; set; } = "";
        public string LocCd { get; set; } = "";
    }

    public class OrderTranApprovestatus
    {
        public int IsAlwAdd { get; set; }
        public int isAlwUpdate { get; set; }
        public int IsAlwAcs { get; set; }
        public int IsAlwApr { get; set; }
        public int IsAlwDel { get; set; }
        public string Message { get; set; } = "";
    }

}
