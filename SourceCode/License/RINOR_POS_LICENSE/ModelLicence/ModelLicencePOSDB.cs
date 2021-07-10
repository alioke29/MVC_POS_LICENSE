namespace RINOR_POS.ModelLicence
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelLicencePOSDB : DbContext
    {
        public ModelLicencePOSDB()
            : base("name=ModelLicencePOSDB")
        {
        }

        public virtual DbSet<GCS_LICENSE_DEV> GCS_LICENSE_DEV { get; set; }
        public virtual DbSet<pos_brand_data> pos_brand_data { get; set; }
        public virtual DbSet<pos_city> pos_city { get; set; }
        public virtual DbSet<pos_computer_name> pos_computer_name { get; set; }
        public virtual DbSet<pos_computer_type> pos_computer_type { get; set; }
        public virtual DbSet<pos_currency> pos_currency { get; set; }
        public virtual DbSet<pos_employee> pos_employee { get; set; }
        public virtual DbSet<POS_Licence_Type> POS_Licence_Type { get; set; }
        public virtual DbSet<pos_menu_application> pos_menu_application { get; set; }
        public virtual DbSet<pos_merchant_data> pos_merchant_data { get; set; }
        public virtual DbSet<pos_module_application> pos_module_application { get; set; }
        public virtual DbSet<pos_province> pos_province { get; set; }
        public virtual DbSet<pos_region> pos_region { get; set; }
        public virtual DbSet<pos_role_menu> pos_role_menu { get; set; }
        public virtual DbSet<pos_shop_data> pos_shop_data { get; set; }
        public virtual DbSet<pos_user_application> pos_user_application { get; set; }
        public virtual DbSet<pos_user_role> pos_user_role { get; set; }
        public virtual DbSet<vw_employee> vw_employee { get; set; }
        public virtual DbSet<vw_menu_app_list> vw_menu_app_list { get; set; }
        public virtual DbSet<vw_pendingkey> vw_pendingkey { get; set; }
        public virtual DbSet<vw_role_menu> vw_role_menu { get; set; }
        public virtual DbSet<vw_user_role> vw_user_role { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GCS_LICENSE_DEV>()
                .Property(e => e.MerchantKey)
                .IsUnicode(false);

            modelBuilder.Entity<GCS_LICENSE_DEV>()
                .Property(e => e.BrandKey)
                .IsUnicode(false);

            modelBuilder.Entity<GCS_LICENSE_DEV>()
                .Property(e => e.ShopKey)
                .IsUnicode(false);

            modelBuilder.Entity<GCS_LICENSE_DEV>()
                .Property(e => e.DeviceKey)
                .IsUnicode(false);

            modelBuilder.Entity<GCS_LICENSE_DEV>()
                .Property(e => e.LicenceKey)
                .IsUnicode(false);

            modelBuilder.Entity<GCS_LICENSE_DEV>()
                .Property(e => e.BuyerEmail)
                .IsUnicode(false);

            modelBuilder.Entity<GCS_LICENSE_DEV>()
                .Property(e => e.BuyerMobileNo)
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

            modelBuilder.Entity<POS_Licence_Type>()
                .Property(e => e.LicenceName)
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

            modelBuilder.Entity<pos_province>()
                .Property(e => e.ProvinceName)
                .IsUnicode(false);

            modelBuilder.Entity<pos_region>()
                .Property(e => e.RegionName)
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

            modelBuilder.Entity<pos_user_application>()
                .Property(e => e.user_name)
                .IsUnicode(false);

            modelBuilder.Entity<pos_user_application>()
                .Property(e => e.user_password)
                .IsUnicode(false);

            modelBuilder.Entity<pos_user_role>()
                .Property(e => e.Role)
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

            modelBuilder.Entity<vw_pendingkey>()
                .Property(e => e.Desctription)
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
