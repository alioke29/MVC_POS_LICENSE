namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_product_price
    {
        [Key]
        public int ProductPriceId { get; set; }

        public int ShopID { get; set; }

        public int ProductId { get; set; }

        public int SaleMode { get; set; }

        public int ProductPriceGroupId { get; set; }

        public int ProductPriceDateId { get; set; }

        public int ProductPrice { get; set; }

        public decimal? PrepaidPrice { get; set; }

        public bool? MainPrice { get; set; }

        [StringLength(100)]
        public string PriceRemark { get; set; }

        public bool? AddingFromBranch { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
