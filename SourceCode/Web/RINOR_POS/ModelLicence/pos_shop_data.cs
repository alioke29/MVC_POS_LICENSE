namespace RINOR_POS.ModelLicence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_shop_data
    {
        [Key]
        public int ShopID { get; set; }

        public int BrandID { get; set; }

        public int MerchantID { get; set; }

        [StringLength(10)]
        public string ShopCode { get; set; }

        [StringLength(50)]
        public string ShopKey { get; set; }

        [StringLength(10)]
        public string DocumentShopCode { get; set; }

        [StringLength(50)]
        public string ShopName { get; set; }

        public bool? IsShop { get; set; }

        public bool? IsInv { get; set; }

        public bool? HasSC { get; set; }

        public bool? IsSCBeforeDisc { get; set; }

        public decimal? SCPercent { get; set; }

        [StringLength(10)]
        public string VATCode { get; set; }

        public int? VATType { get; set; }

        public bool? MasterShop { get; set; }

        public int? MasterShopLink { get; set; }

        public bool? ShowInReport { get; set; }

        public int? ShopTypeID { get; set; }

        public int? ShopCatID1 { get; set; }

        public int? ShopCatID2 { get; set; }

        public int? ShopCatID3 { get; set; }

        public int? ShopCatID4 { get; set; }

        public int? ShopCatID5 { get; set; }

        public int? ShopCatID6 { get; set; }

        public int? ShopCatID7 { get; set; }

        public int? ShopCatID8 { get; set; }

        public int? ShopCatID9 { get; set; }

        public int? ShopCatID10 { get; set; }

        public TimeSpan? OpenHour { get; set; }

        public TimeSpan? CloseHour { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; }

        [StringLength(255)]
        public string CompanyAddress1 { get; set; }

        [StringLength(255)]
        public string CompanyAddress2 { get; set; }

        public int? CompanyProvince { get; set; }

        public int? CompanyCity { get; set; }

        public int? CompanyRegion { get; set; }

        public int? DisplayCompanyProvinceLangID { get; set; }

        [StringLength(50)]
        public string CompanyZipCode { get; set; }

        [StringLength(50)]
        public string CompanyTelephone { get; set; }

        [StringLength(50)]
        public string CompanyFax { get; set; }

        [StringLength(3)]
        public string CompanyCountry { get; set; }

        [StringLength(50)]
        public string CompanyTaxID { get; set; }

        [StringLength(50)]
        public string CompanyRegisterID { get; set; }

        [StringLength(255)]
        public string BranchName { get; set; }

        [StringLength(50)]
        public string BranchNo { get; set; }

        public int? CompanyRegisterLocation { get; set; }

        [StringLength(255)]
        public string BranchNameInFullTaxReport { get; set; }

        [StringLength(255)]
        public string AccountingCode { get; set; }

        [StringLength(50)]
        public string TaxPOSID { get; set; }

        [StringLength(100)]
        public string DeliveryName { get; set; }

        [StringLength(255)]
        public string DeliveryAddress1 { get; set; }

        [StringLength(255)]
        public string DeliveryAddress2 { get; set; }

        public int? DeliveryProvince { get; set; }

        public int? DeliveryCity { get; set; }

        public int? DeliveryRegion { get; set; }

        [StringLength(50)]
        public string DeliveryZipCode { get; set; }

        [StringLength(50)]
        public string DeliveryTelephone { get; set; }

        [StringLength(50)]
        public string DeliveryFax { get; set; }

        [StringLength(50)]
        public string IPAddress { get; set; }

        [StringLength(100)]
        public string Addtional { get; set; }

        public int? ProductLevelOrder { get; set; }

        [StringLength(50)]
        public string SLOC { get; set; }

        [StringLength(20)]
        public string PTTShopCode { get; set; }

        public DateTime? StartSaleDate { get; set; }

        public int? StartMonth { get; set; }

        public int? StartYear { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
