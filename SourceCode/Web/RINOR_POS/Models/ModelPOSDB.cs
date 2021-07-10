namespace RINOR_POS.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelPOSDB : DbContext
    {
        public ModelPOSDB()
            : base("name=ModelPOSDB")
        {
        }

        public virtual DbSet<pos_bill_template> pos_bill_template { get; set; }
        public virtual DbSet<pos_bill_template_image> pos_bill_template_image { get; set; }
        public virtual DbSet<pos_brand_data> pos_brand_data { get; set; }
        public virtual DbSet<pos_city> pos_city { get; set; }
        public virtual DbSet<pos_computer_name> pos_computer_name { get; set; }
        public virtual DbSet<pos_computer_type> pos_computer_type { get; set; }
        public virtual DbSet<pos_config> pos_config { get; set; }
        public virtual DbSet<pos_creditcard_bank> pos_creditcard_bank { get; set; }
        public virtual DbSet<pos_creditcard_type> pos_creditcard_type { get; set; }
        public virtual DbSet<pos_currency> pos_currency { get; set; }
        public virtual DbSet<pos_employee> pos_employee { get; set; }
        public virtual DbSet<pos_initial_cash> pos_initial_cash { get; set; }
        public virtual DbSet<pos_licence_data> pos_licence_data { get; set; }
        public virtual DbSet<pos_menu_application> pos_menu_application { get; set; }
        public virtual DbSet<pos_merchant_data> pos_merchant_data { get; set; }
        public virtual DbSet<pos_module_application> pos_module_application { get; set; }
        public virtual DbSet<pos_order_complete> pos_order_complete { get; set; }
        public virtual DbSet<pos_order_detail_complete> pos_order_detail_complete { get; set; }
        public virtual DbSet<pos_order_hold> pos_order_hold { get; set; }
        public virtual DbSet<pos_order_no> pos_order_no { get; set; }
        public virtual DbSet<pos_order_payment_complete> pos_order_payment_complete { get; set; }
        public virtual DbSet<pos_order_payment_detail> pos_order_payment_detail { get; set; }
        public virtual DbSet<pos_order_print> pos_order_print { get; set; }
        public virtual DbSet<pos_payment_group> pos_payment_group { get; set; }
        public virtual DbSet<pos_payment_type> pos_payment_type { get; set; }
        public virtual DbSet<pos_price_group_shop> pos_price_group_shop { get; set; }
        public virtual DbSet<pos_printer_type> pos_printer_type { get; set; }
        public virtual DbSet<pos_printers> pos_printers { get; set; }
        public virtual DbSet<pos_product_combo> pos_product_combo { get; set; }
        public virtual DbSet<pos_product_combo_link> pos_product_combo_link { get; set; }
        public virtual DbSet<pos_product_dept> pos_product_dept { get; set; }
        public virtual DbSet<pos_product_group> pos_product_group { get; set; }
        public virtual DbSet<pos_product_price> pos_product_price { get; set; }
        public virtual DbSet<pos_product_price_date> pos_product_price_date { get; set; }
        public virtual DbSet<pos_product_price_group> pos_product_price_group { get; set; }
        public virtual DbSet<pos_product_salemode> pos_product_salemode { get; set; }
        public virtual DbSet<pos_product_size> pos_product_size { get; set; }
        public virtual DbSet<pos_product_type> pos_product_type { get; set; }
        public virtual DbSet<pos_product_vat> pos_product_vat { get; set; }
        public virtual DbSet<pos_product_vat_type> pos_product_vat_type { get; set; }
        public virtual DbSet<pos_products> pos_products { get; set; }
        public virtual DbSet<pos_program_property> pos_program_property { get; set; }
        public virtual DbSet<pos_promotion> pos_promotion { get; set; }
        public virtual DbSet<pos_promotion_buy_get> pos_promotion_buy_get { get; set; }
        public virtual DbSet<pos_promotion_package> pos_promotion_package { get; set; }
        public virtual DbSet<pos_promotion_payment> pos_promotion_payment { get; set; }
        public virtual DbSet<pos_promotion_product> pos_promotion_product { get; set; }
        public virtual DbSet<pos_promotion_shop_link> pos_promotion_shop_link { get; set; }
        public virtual DbSet<pos_promotion_type> pos_promotion_type { get; set; }
        public virtual DbSet<pos_province> pos_province { get; set; }
        public virtual DbSet<pos_reason_detail> pos_reason_detail { get; set; }
        public virtual DbSet<pos_reason_group> pos_reason_group { get; set; }
        public virtual DbSet<pos_receipt_template> pos_receipt_template { get; set; }
        public virtual DbSet<pos_receipt_template_image> pos_receipt_template_image { get; set; }
        public virtual DbSet<pos_referance_property> pos_referance_property { get; set; }
        public virtual DbSet<pos_region> pos_region { get; set; }
        public virtual DbSet<pos_role_menu> pos_role_menu { get; set; }
        public virtual DbSet<pos_sale_mode> pos_sale_mode { get; set; }
        public virtual DbSet<pos_session> pos_session { get; set; }
        public virtual DbSet<pos_shop_data> pos_shop_data { get; set; }
        public virtual DbSet<pos_shop_property> pos_shop_property { get; set; }
        public virtual DbSet<pos_sync_log> pos_sync_log { get; set; }
        public virtual DbSet<pos_temp_detail_order> pos_temp_detail_order { get; set; }
        public virtual DbSet<pos_temp_order> pos_temp_order { get; set; }
        public virtual DbSet<pos_temp_order_promo> pos_temp_order_promo { get; set; }
        public virtual DbSet<pos_temp_payment_detail> pos_temp_payment_detail { get; set; }
        public virtual DbSet<pos_todo_list> pos_todo_list { get; set; }
        public virtual DbSet<pos_user_application> pos_user_application { get; set; }
        public virtual DbSet<pos_user_role> pos_user_role { get; set; }
        public virtual DbSet<pos_kds_back_end> pos_kds_back_end { get; set; }
        public virtual DbSet<pos_queue_front_end> pos_queue_front_end { get; set; }
        public virtual DbSet<vw_bill_template> vw_bill_template { get; set; }
        public virtual DbSet<vw_employee> vw_employee { get; set; }
        public virtual DbSet<vw_menu_app_list> vw_menu_app_list { get; set; }
        public virtual DbSet<vw_order_complete_totaldiscount> vw_order_complete_totaldiscount { get; set; }
        public virtual DbSet<vw_orderdetail_byprinter> vw_orderdetail_byprinter { get; set; }
        public virtual DbSet<vw_pay_group_bydevice> vw_pay_group_bydevice { get; set; }
        public virtual DbSet<vw_payment_bydevice> vw_payment_bydevice { get; set; }
        public virtual DbSet<vw_price_group_shop> vw_price_group_shop { get; set; }
        public virtual DbSet<vw_printers_bysession> vw_printers_bysession { get; set; }
        public virtual DbSet<vw_product_combo_link> vw_product_combo_link { get; set; }
        public virtual DbSet<vw_product_combo_list> vw_product_combo_list { get; set; }
        public virtual DbSet<vw_product_dept> vw_product_dept { get; set; }
        public virtual DbSet<vw_product_group> vw_product_group { get; set; }
        public virtual DbSet<vw_product_price_date> vw_product_price_date { get; set; }
        public virtual DbSet<vw_promo_sp_package> vw_promo_sp_package { get; set; }
        public virtual DbSet<vw_promotion_buyget> vw_promotion_buyget { get; set; }
        public virtual DbSet<vw_promotion_package> vw_promotion_package { get; set; }
        public virtual DbSet<vw_promotion_package_combo> vw_promotion_package_combo { get; set; }
        public virtual DbSet<vw_promotion_product> vw_promotion_product { get; set; }
        public virtual DbSet<vw_promotion_shoplink> vw_promotion_shoplink { get; set; }
        public virtual DbSet<vw_receipt_template> vw_receipt_template { get; set; }
        public virtual DbSet<vw_report_hourly> vw_report_hourly { get; set; }
        public virtual DbSet<vw_role_menu> vw_role_menu { get; set; }
        public virtual DbSet<vw_salemode_byshop> vw_salemode_byshop { get; set; }
        public virtual DbSet<vw_user_register_role> vw_user_register_role { get; set; }
        public virtual DbSet<vw_user_role> vw_user_role { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<pos_bill_template>()
                .Property(e => e.Position)
                .IsUnicode(false);

            modelBuilder.Entity<pos_bill_template>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<pos_bill_template_image>()
                .Property(e => e.LogoHeaderFile)
                .IsUnicode(false);

            modelBuilder.Entity<pos_bill_template_image>()
                .Property(e => e.QRFooterText)
                .IsUnicode(false);

            modelBuilder.Entity<pos_brand_data>()
                .Property(e => e.BrandKey)
                .IsUnicode(false);

            modelBuilder.Entity<pos_brand_data>()
                .Property(e => e.BrandCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_brand_data>()
                .Property(e => e.BrandName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_brand_data>()
                .Property(e => e.IPAddress)
                .IsUnicode(false);

            modelBuilder.Entity<pos_city>()
                .Property(e => e.CityName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_computer_name>()
                .Property(e => e.ComputerName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_computer_name>()
                .Property(e => e.PayTypeList)
                .IsUnicode(false);

            modelBuilder.Entity<pos_computer_name>()
                .Property(e => e.SaleModeList)
                .IsUnicode(false);

            modelBuilder.Entity<pos_computer_name>()
                .Property(e => e.PrinterList)
                .IsUnicode(false);

            modelBuilder.Entity<pos_computer_name>()
                .Property(e => e.RegistrationNumber)
                .IsUnicode(false);

            modelBuilder.Entity<pos_computer_name>()
                .Property(e => e.DeviceCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_computer_name>()
                .Property(e => e.ReceiptHeader)
                .IsUnicode(false);

            modelBuilder.Entity<pos_computer_name>()
                .Property(e => e.FullTaxHeader)
                .IsUnicode(false);

            modelBuilder.Entity<pos_computer_name>()
                .Property(e => e.IPAddress)
                .IsUnicode(false);

            modelBuilder.Entity<pos_computer_type>()
                .Property(e => e.ComputerTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_config>()
                .Property(e => e.ConfigCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_config>()
                .Property(e => e.ConfigName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_config>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<pos_creditcard_bank>()
                .Property(e => e.CreditCardBank)
                .IsUnicode(false);

            modelBuilder.Entity<pos_creditcard_bank>()
                .Property(e => e.IconButton)
                .IsUnicode(false);

            modelBuilder.Entity<pos_creditcard_type>()
                .Property(e => e.CreditCardType)
                .IsUnicode(false);

            modelBuilder.Entity<pos_currency>()
                .Property(e => e.CurrencyCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_currency>()
                .Property(e => e.CurrencyName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_currency>()
                .Property(e => e.CurrencyCountry)
                .IsUnicode(false);

            modelBuilder.Entity<pos_employee>()
                .Property(e => e.employee_nik)
                .IsUnicode(false);

            modelBuilder.Entity<pos_employee>()
                .Property(e => e.employee_name)
                .IsUnicode(false);

            modelBuilder.Entity<pos_employee>()
                .Property(e => e.employee_email)
                .IsUnicode(false);

            modelBuilder.Entity<pos_employee>()
                .HasMany(e => e.pos_user_application)
                .WithRequired(e => e.pos_employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<pos_licence_data>()
                .Property(e => e.DeviceKey)
                .IsUnicode(false);

            modelBuilder.Entity<pos_menu_application>()
                .Property(e => e.menu_code)
                .IsUnicode(false);

            modelBuilder.Entity<pos_menu_application>()
                .Property(e => e.menu_name)
                .IsUnicode(false);

            modelBuilder.Entity<pos_menu_application>()
                .Property(e => e.menu_url)
                .IsUnicode(false);

            modelBuilder.Entity<pos_merchant_data>()
                .Property(e => e.MerchantKey)
                .IsUnicode(false);

            modelBuilder.Entity<pos_merchant_data>()
                .Property(e => e.MerchantCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_merchant_data>()
                .Property(e => e.MerchantName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_merchant_data>()
                .Property(e => e.IPaddress)
                .IsUnicode(false);

            modelBuilder.Entity<pos_merchant_data>()
                .Property(e => e.DatabaseName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_module_application>()
                .Property(e => e.module_code)
                .IsUnicode(false);

            modelBuilder.Entity<pos_module_application>()
                .Property(e => e.module_name)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_complete>()
                .Property(e => e.sessionId)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_complete>()
                .Property(e => e.SessionKey)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_complete>()
                .Property(e => e.computerID)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_complete>()
                .Property(e => e.PayPromoName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_complete>()
                .Property(e => e.AcceptedListPromotionID)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_complete>()
                .Property(e => e.CustomerName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_complete>()
                .Property(e => e.CustomerTableNo)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_complete>()
                .Property(e => e.BillDiscountType)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_complete>()
                .Property(e => e.Void_Reason)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_detail_complete>()
                .Property(e => e.sessionId)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_detail_complete>()
                .Property(e => e.SessionKey)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_detail_complete>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_detail_complete>()
                .Property(e => e.DiscountType)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_detail_complete>()
                .Property(e => e.ModifierText)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_detail_complete>()
                .Property(e => e.Reason)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_detail_complete>()
                .Property(e => e.RowPackageProduct)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_hold>()
                .Property(e => e.SessionID)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_hold>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_hold>()
                .Property(e => e.CustomerName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_no>()
                .Property(e => e.SessionId)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_payment_complete>()
                .Property(e => e.sessionId)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_payment_complete>()
                .Property(e => e.CashierName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_payment_complete>()
                .Property(e => e.ComputerID)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_payment_detail>()
                .Property(e => e.sessionId)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_payment_detail>()
                .Property(e => e.PayTypeCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_payment_detail>()
                .Property(e => e.PayTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_payment_detail>()
                .Property(e => e.CreditCardNo)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_payment_detail>()
                .Property(e => e.CreditCardHolderName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_print>()
                .Property(e => e.SessionID)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_print>()
                .Property(e => e.ComputerID)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_print>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_print>()
                .Property(e => e.PrinterName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_order_print>()
                .Property(e => e.PrinterDeviceName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_payment_group>()
                .Property(e => e.PayGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_payment_group>()
                .Property(e => e.IconButtonServer)
                .IsUnicode(false);

            modelBuilder.Entity<pos_payment_group>()
                .Property(e => e.IconButton)
                .IsUnicode(false);

            modelBuilder.Entity<pos_payment_type>()
                .Property(e => e.PayTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_payment_type>()
                .Property(e => e.PayTypeCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_payment_type>()
                .Property(e => e.DisplayName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_payment_type>()
                .Property(e => e.IconButtonServer)
                .IsUnicode(false);

            modelBuilder.Entity<pos_payment_type>()
                .Property(e => e.IconButton)
                .IsUnicode(false);

            modelBuilder.Entity<pos_payment_type>()
                .Property(e => e.EDCType)
                .IsUnicode(false);

            modelBuilder.Entity<pos_printer_type>()
                .Property(e => e.PrinterTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_printer_type>()
                .Property(e => e.PrintDesign)
                .IsUnicode(false);

            modelBuilder.Entity<pos_printers>()
                .Property(e => e.PrinterName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_printers>()
                .Property(e => e.PrinterDeviceName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_printers>()
                .Property(e => e.PrinterDeviceBackup)
                .IsUnicode(false);

            modelBuilder.Entity<pos_printers>()
                .Property(e => e.PrinterNameForPrint)
                .IsUnicode(false);

            modelBuilder.Entity<pos_product_combo>()
                .Property(e => e.ProductComboName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_product_dept>()
                .Property(e => e.ProductDeptCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_product_dept>()
                .Property(e => e.ProductDeptName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_product_group>()
                .Property(e => e.ProductGroupCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_product_group>()
                .Property(e => e.ProductGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_product_price>()
                .Property(e => e.PriceRemark)
                .IsUnicode(false);

            modelBuilder.Entity<pos_product_price_group>()
                .Property(e => e.ProductPriceName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_product_size>()
                .Property(e => e.Size)
                .IsUnicode(false);

            modelBuilder.Entity<pos_product_type>()
                .Property(e => e.ProductTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_product_vat>()
                .Property(e => e.ProductVATCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_product_vat>()
                .Property(e => e.VATDisplay)
                .IsUnicode(false);

            modelBuilder.Entity<pos_product_vat>()
                .Property(e => e.VATDesp)
                .IsUnicode(false);

            modelBuilder.Entity<pos_product_vat_type>()
                .Property(e => e.VATTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_products>()
                .Property(e => e.ProductCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_products>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_products>()
                .Property(e => e.ProductName1)
                .IsUnicode(false);

            modelBuilder.Entity<pos_products>()
                .Property(e => e.ProductName2)
                .IsUnicode(false);

            modelBuilder.Entity<pos_products>()
                .Property(e => e.ProductName3)
                .IsUnicode(false);

            modelBuilder.Entity<pos_products>()
                .Property(e => e.ProductName4)
                .IsUnicode(false);

            modelBuilder.Entity<pos_products>()
                .Property(e => e.ProductName5)
                .IsUnicode(false);

            modelBuilder.Entity<pos_products>()
                .Property(e => e.ProductPictureServer)
                .IsUnicode(false);

            modelBuilder.Entity<pos_products>()
                .Property(e => e.ProductPictureClient)
                .IsUnicode(false);

            modelBuilder.Entity<pos_products>()
                .Property(e => e.PrinterID)
                .IsUnicode(false);

            modelBuilder.Entity<pos_products>()
                .Property(e => e.PrintList)
                .IsUnicode(false);

            modelBuilder.Entity<pos_products>()
                .Property(e => e.KDSID)
                .IsUnicode(false);

            modelBuilder.Entity<pos_products>()
                .Property(e => e.KDSSummaryID)
                .IsUnicode(false);

            modelBuilder.Entity<pos_products>()
                .Property(e => e.EnableWeekly)
                .IsUnicode(false);

            modelBuilder.Entity<pos_products>()
                .Property(e => e.VATCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_products>()
                .Property(e => e.ProductUnitName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_products>()
                .Property(e => e.LimitDiscountAmount)
                .HasPrecision(12, 0);

            modelBuilder.Entity<pos_products>()
                .Property(e => e.LimitDiscountPercent)
                .HasPrecision(12, 0);

            modelBuilder.Entity<pos_products>()
                .Property(e => e.ProductDesp)
                .IsUnicode(false);

            modelBuilder.Entity<pos_program_property>()
                .Property(e => e.PropertyName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_program_property>()
                .Property(e => e.PropertyParam)
                .IsUnicode(false);

            modelBuilder.Entity<pos_program_property>()
                .Property(e => e.PropertyDesp)
                .IsUnicode(false);

            modelBuilder.Entity<pos_program_property>()
                .Property(e => e.DefaultTextValue)
                .IsUnicode(false);

            modelBuilder.Entity<pos_promotion>()
                .Property(e => e.PromotionCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_promotion>()
                .Property(e => e.PromotionName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_promotion>()
                .Property(e => e.EffectTo)
                .IsUnicode(false);

            modelBuilder.Entity<pos_promotion>()
                .Property(e => e.WeeklyCondition)
                .IsUnicode(false);

            modelBuilder.Entity<pos_promotion>()
                .Property(e => e.DayCondition)
                .IsUnicode(false);

            modelBuilder.Entity<pos_promotion_payment>()
                .Property(e => e.DiscountPercentage)
                .HasPrecision(16, 2);

            modelBuilder.Entity<pos_promotion_type>()
                .Property(e => e.PromotionType)
                .IsUnicode(false);

            modelBuilder.Entity<pos_province>()
                .Property(e => e.ProvinceName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_reason_detail>()
                .Property(e => e.ReasonText)
                .IsUnicode(false);

            modelBuilder.Entity<pos_reason_group>()
                .Property(e => e.ReasonGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_receipt_template>()
                .Property(e => e.Position)
                .IsUnicode(false);

            modelBuilder.Entity<pos_receipt_template>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<pos_receipt_template_image>()
                .Property(e => e.LogoHeaderFile)
                .IsUnicode(false);

            modelBuilder.Entity<pos_receipt_template_image>()
                .Property(e => e.QRFooterText)
                .IsUnicode(false);

            modelBuilder.Entity<pos_referance_property>()
                .Property(e => e.PropertyName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_referance_property>()
                .Property(e => e.PropertyParam)
                .IsUnicode(false);

            modelBuilder.Entity<pos_referance_property>()
                .Property(e => e.PropertyDesp)
                .IsUnicode(false);

            modelBuilder.Entity<pos_referance_property>()
                .Property(e => e.DefaultTextValue)
                .IsUnicode(false);

            modelBuilder.Entity<pos_region>()
                .Property(e => e.RegionName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_sale_mode>()
                .Property(e => e.SaleModeName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_sale_mode>()
                .Property(e => e.PrefixText)
                .IsUnicode(false);

            modelBuilder.Entity<pos_sale_mode>()
                .Property(e => e.PrefixTextPrinting)
                .IsUnicode(false);

            modelBuilder.Entity<pos_sale_mode>()
                .Property(e => e.PrefixQueue)
                .IsUnicode(false);

            modelBuilder.Entity<pos_sale_mode>()
                .Property(e => e.ReceiptHeaderText)
                .IsUnicode(false);

            modelBuilder.Entity<pos_sale_mode>()
                .Property(e => e.PayTypeList)
                .IsUnicode(false);

            modelBuilder.Entity<pos_sale_mode>()
                .Property(e => e.NOTinPayTypeList)
                .IsUnicode(false);

            modelBuilder.Entity<pos_sale_mode>()
                .Property(e => e.PrefixHeaderText)
                .IsUnicode(false);

            modelBuilder.Entity<pos_sale_mode>()
                .Property(e => e.AutoAddProduct)
                .IsUnicode(false);

            modelBuilder.Entity<pos_sale_mode>()
                .Property(e => e.KDSHeaderColor)
                .IsUnicode(false);

            modelBuilder.Entity<pos_sale_mode>()
                .Property(e => e.IconButtonServer)
                .IsUnicode(false);

            modelBuilder.Entity<pos_sale_mode>()
                .Property(e => e.IconButton)
                .IsUnicode(false);

            modelBuilder.Entity<pos_session>()
                .Property(e => e.SessionKey)
                .IsUnicode(false);

            modelBuilder.Entity<pos_session>()
                .Property(e => e.ComputerName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_session>()
                .Property(e => e.OpenStaff)
                .IsUnicode(false);

            modelBuilder.Entity<pos_session>()
                .Property(e => e.CloseStaff)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.ShopCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.ShopKey)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.DocumentShopCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.ShopName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.SCPercent)
                .HasPrecision(10, 4);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.VATCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.CompanyAddress1)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.CompanyAddress2)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.CompanyZipCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.CompanyTelephone)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.CompanyFax)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.CompanyCountry)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.CompanyTaxID)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.CompanyRegisterID)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.BranchName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.BranchNo)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.BranchNameInFullTaxReport)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.AccountingCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.TaxPOSID)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.DeliveryName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.DeliveryAddress1)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.DeliveryAddress2)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.DeliveryZipCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.DeliveryTelephone)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.DeliveryFax)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.IPAddress)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.Addtional)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.SLOC)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_data>()
                .Property(e => e.PTTShopCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_property>()
                .Property(e => e.PropertyName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_property>()
                .Property(e => e.PropertyParam)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_property>()
                .Property(e => e.PropertyDesp)
                .IsUnicode(false);

            modelBuilder.Entity<pos_shop_property>()
                .Property(e => e.DefaultTextValue)
                .IsUnicode(false);

            modelBuilder.Entity<pos_sync_log>()
                .Property(e => e.SyncBy)
                .IsUnicode(false);

            modelBuilder.Entity<pos_sync_log>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<pos_temp_detail_order>()
                .Property(e => e.sessionId)
                .IsUnicode(false);

            modelBuilder.Entity<pos_temp_detail_order>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_temp_detail_order>()
                .Property(e => e.DiscountType)
                .IsUnicode(false);

            modelBuilder.Entity<pos_temp_detail_order>()
                .Property(e => e.PromotionName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_temp_detail_order>()
                .Property(e => e.ModifierText)
                .IsUnicode(false);

            modelBuilder.Entity<pos_temp_detail_order>()
                .Property(e => e.RowPackageProduct)
                .IsUnicode(false);

            modelBuilder.Entity<pos_temp_order>()
                .Property(e => e.sessionId)
                .IsUnicode(false);

            modelBuilder.Entity<pos_temp_order>()
                .Property(e => e.computerID)
                .IsUnicode(false);

            modelBuilder.Entity<pos_temp_order>()
                .Property(e => e.AcceptedListPromotionID)
                .IsUnicode(false);

            modelBuilder.Entity<pos_temp_order>()
                .Property(e => e.CustomerName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_temp_order>()
                .Property(e => e.CustomerTableNo)
                .IsUnicode(false);

            modelBuilder.Entity<pos_temp_order>()
                .Property(e => e.BillDiscountType)
                .IsUnicode(false);

            modelBuilder.Entity<pos_temp_order_promo>()
                .Property(e => e.SessionID)
                .IsUnicode(false);

            modelBuilder.Entity<pos_temp_order_promo>()
                .Property(e => e.PromotionCode)
                .IsUnicode(false);

            modelBuilder.Entity<pos_temp_order_promo>()
                .Property(e => e.DiscountPrice)
                .HasPrecision(16, 2);

            modelBuilder.Entity<pos_temp_order_promo>()
                .Property(e => e.CountOn)
                .IsUnicode(false);

            modelBuilder.Entity<pos_temp_payment_detail>()
                .Property(e => e.SessionId)
                .IsUnicode(false);

            modelBuilder.Entity<pos_temp_payment_detail>()
                .Property(e => e.CreditCardNo)
                .IsUnicode(false);

            modelBuilder.Entity<pos_temp_payment_detail>()
                .Property(e => e.CreditCardHolderName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_todo_list>()
                .Property(e => e.ToDo)
                .IsUnicode(false);

            modelBuilder.Entity<pos_todo_list>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<pos_todo_list>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<pos_todo_list>()
                .Property(e => e.ImageServer)
                .IsUnicode(false);

            modelBuilder.Entity<pos_todo_list>()
                .Property(e => e.ImageClient)
                .IsUnicode(false);

            modelBuilder.Entity<pos_todo_list>()
                .Property(e => e.ActionPIC)
                .IsUnicode(false);

            modelBuilder.Entity<pos_user_application>()
                .Property(e => e.user_name)
                .IsUnicode(false);

            modelBuilder.Entity<pos_user_application>()
                .Property(e => e.user_password)
                .IsUnicode(false);

            modelBuilder.Entity<pos_user_role>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<pos_kds_back_end>()
                .Property(e => e.OrderNo)
                .IsUnicode(false);

            modelBuilder.Entity<pos_kds_back_end>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_kds_back_end>()
                .Property(e => e.Duration)
                .IsFixedLength();

            modelBuilder.Entity<pos_queue_front_end>()
                .Property(e => e.OrderNo)
                .IsUnicode(false);

            modelBuilder.Entity<pos_queue_front_end>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<vw_bill_template>()
                .Property(e => e.MasterShopName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_bill_template>()
                .Property(e => e.ShopName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_bill_template>()
                .Property(e => e.Position)
                .IsUnicode(false);

            modelBuilder.Entity<vw_bill_template>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<vw_employee>()
                .Property(e => e.employee_nik)
                .IsUnicode(false);

            modelBuilder.Entity<vw_employee>()
                .Property(e => e.employee_name)
                .IsUnicode(false);

            modelBuilder.Entity<vw_employee>()
                .Property(e => e.employee_email)
                .IsUnicode(false);

            modelBuilder.Entity<vw_employee>()
                .Property(e => e.ShopCode)
                .IsUnicode(false);

            modelBuilder.Entity<vw_employee>()
                .Property(e => e.ShopKey)
                .IsUnicode(false);

            modelBuilder.Entity<vw_employee>()
                .Property(e => e.ShopName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_employee>()
                .Property(e => e.BrandCode)
                .IsUnicode(false);

            modelBuilder.Entity<vw_employee>()
                .Property(e => e.BrandName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_employee>()
                .Property(e => e.MerchantCode)
                .IsUnicode(false);

            modelBuilder.Entity<vw_employee>()
                .Property(e => e.MerchantName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_menu_app_list>()
                .Property(e => e.module_code)
                .IsUnicode(false);

            modelBuilder.Entity<vw_menu_app_list>()
                .Property(e => e.module_name)
                .IsUnicode(false);

            modelBuilder.Entity<vw_menu_app_list>()
                .Property(e => e.menu_code)
                .IsUnicode(false);

            modelBuilder.Entity<vw_menu_app_list>()
                .Property(e => e.menu_name)
                .IsUnicode(false);

            modelBuilder.Entity<vw_menu_app_list>()
                .Property(e => e.menu_url)
                .IsUnicode(false);

            modelBuilder.Entity<vw_order_complete_totaldiscount>()
                .Property(e => e.sessionId)
                .IsUnicode(false);

            modelBuilder.Entity<vw_order_complete_totaldiscount>()
                .Property(e => e.SessionKey)
                .IsUnicode(false);

            modelBuilder.Entity<vw_order_complete_totaldiscount>()
                .Property(e => e.TotalDiscountManual)
                .HasPrecision(38, 2);

            modelBuilder.Entity<vw_order_complete_totaldiscount>()
                .Property(e => e.TotalPromotion)
                .HasPrecision(38, 2);

            modelBuilder.Entity<vw_order_complete_totaldiscount>()
                .Property(e => e.TotalPromoPayment)
                .HasPrecision(38, 2);

            modelBuilder.Entity<vw_orderdetail_byprinter>()
                .Property(e => e.sessionId)
                .IsUnicode(false);

            modelBuilder.Entity<vw_orderdetail_byprinter>()
                .Property(e => e.SessionKey)
                .IsUnicode(false);

            modelBuilder.Entity<vw_orderdetail_byprinter>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_orderdetail_byprinter>()
                .Property(e => e.DiscountType)
                .IsUnicode(false);

            modelBuilder.Entity<vw_orderdetail_byprinter>()
                .Property(e => e.ModifierText)
                .IsUnicode(false);

            modelBuilder.Entity<vw_orderdetail_byprinter>()
                .Property(e => e.PrintDesign)
                .IsUnicode(false);

            modelBuilder.Entity<vw_orderdetail_byprinter>()
                .Property(e => e.PrinterName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_pay_group_bydevice>()
                .Property(e => e.UniqID)
                .IsUnicode(false);

            modelBuilder.Entity<vw_pay_group_bydevice>()
                .Property(e => e.DeviceCode)
                .IsUnicode(false);

            modelBuilder.Entity<vw_pay_group_bydevice>()
                .Property(e => e.PayGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_payment_bydevice>()
                .Property(e => e.UniqId)
                .IsUnicode(false);

            modelBuilder.Entity<vw_payment_bydevice>()
                .Property(e => e.DeviceCode)
                .IsUnicode(false);

            modelBuilder.Entity<vw_payment_bydevice>()
                .Property(e => e.PayGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_payment_bydevice>()
                .Property(e => e.PayTypeID)
                .IsUnicode(false);

            modelBuilder.Entity<vw_payment_bydevice>()
                .Property(e => e.PayTypeCode)
                .IsUnicode(false);

            modelBuilder.Entity<vw_payment_bydevice>()
                .Property(e => e.PayTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_price_group_shop>()
                .Property(e => e.ShopCode)
                .IsUnicode(false);

            modelBuilder.Entity<vw_price_group_shop>()
                .Property(e => e.ShopName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_price_group_shop>()
                .Property(e => e.ProductPriceName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_printers_bysession>()
                .Property(e => e.PrintID)
                .IsUnicode(false);

            modelBuilder.Entity<vw_printers_bysession>()
                .Property(e => e.sessionId)
                .IsUnicode(false);

            modelBuilder.Entity<vw_printers_bysession>()
                .Property(e => e.SessionKey)
                .IsUnicode(false);

            modelBuilder.Entity<vw_printers_bysession>()
                .Property(e => e.PrintDesign)
                .IsUnicode(false);

            modelBuilder.Entity<vw_product_combo_link>()
                .Property(e => e.ShopName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_product_combo_link>()
                .Property(e => e.ProductGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_product_combo_link>()
                .Property(e => e.ProductComboName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_product_combo_link>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_product_combo_list>()
                .Property(e => e.ShopName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_product_combo_list>()
                .Property(e => e.ProductGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_product_combo_list>()
                .Property(e => e.ProductComboName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_product_dept>()
                .Property(e => e.ProductDeptCode)
                .IsUnicode(false);

            modelBuilder.Entity<vw_product_dept>()
                .Property(e => e.ProductDeptName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_product_group>()
                .Property(e => e.ProductGroupCode)
                .IsUnicode(false);

            modelBuilder.Entity<vw_product_group>()
                .Property(e => e.ProductGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_product_price_date>()
                .Property(e => e.ValidDate)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promo_sp_package>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_buyget>()
                .Property(e => e.SaleModeName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_buyget>()
                .Property(e => e.ShopName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_buyget>()
                .Property(e => e.ProductGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_buyget>()
                .Property(e => e.ProductDeptName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_buyget>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_buyget>()
                .Property(e => e.GetProductName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_package>()
                .Property(e => e.PromotionCode)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_package>()
                .Property(e => e.PromotionName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_package>()
                .Property(e => e.SaleModeName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_package>()
                .Property(e => e.ShopName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_package>()
                .Property(e => e.ProductGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_package>()
                .Property(e => e.ProductDeptName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_package>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_package>()
                .Property(e => e.ProductComboName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_package_combo>()
                .Property(e => e.PromotionCode)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_package_combo>()
                .Property(e => e.PromotionName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_package_combo>()
                .Property(e => e.SaleModeName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_package_combo>()
                .Property(e => e.ShopName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_package_combo>()
                .Property(e => e.ProductGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_package_combo>()
                .Property(e => e.ProductDeptName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_package_combo>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_package_combo>()
                .Property(e => e.ProductSize)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_product>()
                .Property(e => e.ProductGroupCode)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_product>()
                .Property(e => e.ProductGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_product>()
                .Property(e => e.ProductDeptCode)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_product>()
                .Property(e => e.ProductDeptName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_product>()
                .Property(e => e.ProductCode)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_product>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_product>()
                .Property(e => e.ProductName4)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_product>()
                .Property(e => e.ShopCode)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_product>()
                .Property(e => e.ShopName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_product>()
                .Property(e => e.SaleModeName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_product>()
                .Property(e => e.DiscountType)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_shoplink>()
                .Property(e => e.MasterShopName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_promotion_shoplink>()
                .Property(e => e.ShopName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_receipt_template>()
                .Property(e => e.MasterShopName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_receipt_template>()
                .Property(e => e.ShopName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_receipt_template>()
                .Property(e => e.Position)
                .IsUnicode(false);

            modelBuilder.Entity<vw_receipt_template>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<vw_report_hourly>()
                .Property(e => e.computerID)
                .IsUnicode(false);

            modelBuilder.Entity<vw_report_hourly>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_role_menu>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<vw_role_menu>()
                .Property(e => e.module_code)
                .IsUnicode(false);

            modelBuilder.Entity<vw_role_menu>()
                .Property(e => e.module_name)
                .IsUnicode(false);

            modelBuilder.Entity<vw_role_menu>()
                .Property(e => e.menu_code)
                .IsUnicode(false);

            modelBuilder.Entity<vw_role_menu>()
                .Property(e => e.menu_name)
                .IsUnicode(false);

            modelBuilder.Entity<vw_salemode_byshop>()
                .Property(e => e.SaleModeName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_salemode_byshop>()
                .Property(e => e.PrefixText)
                .IsUnicode(false);

            modelBuilder.Entity<vw_salemode_byshop>()
                .Property(e => e.PrefixTextPrinting)
                .IsUnicode(false);

            modelBuilder.Entity<vw_salemode_byshop>()
                .Property(e => e.PrefixQueue)
                .IsUnicode(false);

            modelBuilder.Entity<vw_salemode_byshop>()
                .Property(e => e.ReceiptHeaderText)
                .IsUnicode(false);

            modelBuilder.Entity<vw_salemode_byshop>()
                .Property(e => e.PayTypeList)
                .IsUnicode(false);

            modelBuilder.Entity<vw_salemode_byshop>()
                .Property(e => e.NOTinPayTypeList)
                .IsUnicode(false);

            modelBuilder.Entity<vw_salemode_byshop>()
                .Property(e => e.PrefixHeaderText)
                .IsUnicode(false);

            modelBuilder.Entity<vw_salemode_byshop>()
                .Property(e => e.AutoAddProduct)
                .IsUnicode(false);

            modelBuilder.Entity<vw_salemode_byshop>()
                .Property(e => e.KDSHeaderColor)
                .IsUnicode(false);

            modelBuilder.Entity<vw_salemode_byshop>()
                .Property(e => e.IconButton)
                .IsUnicode(false);

            modelBuilder.Entity<vw_user_register_role>()
                .Property(e => e.employee_nik)
                .IsUnicode(false);

            modelBuilder.Entity<vw_user_register_role>()
                .Property(e => e.employee_name)
                .IsUnicode(false);

            modelBuilder.Entity<vw_user_register_role>()
                .Property(e => e.employee_email)
                .IsUnicode(false);

            modelBuilder.Entity<vw_user_register_role>()
                .Property(e => e.user_name)
                .IsUnicode(false);

            modelBuilder.Entity<vw_user_role>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<vw_user_role>()
                .Property(e => e.MerchantName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_user_role>()
                .Property(e => e.BrandName)
                .IsUnicode(false);

            modelBuilder.Entity<vw_user_role>()
                .Property(e => e.ShopName)
                .IsUnicode(false);
        }
    }
}
