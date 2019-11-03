using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Zek.Model
{
    public class License
    {
        public int Id { get; set; }

        public string LicenseCode { get; set; }

        public string SerialCode { get; set; }

        #region Evaluation
        public short MaxUsageDays { get; set; }
        //public bool HasMaxUsageDays { get; }

        public short MaxUniqueUsageDays { get; set; }
        //public bool HasMaxUniqueUsageDays { get; }

        public short MaxExecutions { get; set; }
        //public bool HasMaxExecutions { get; }

        public short MaxInstances { get; set; }
        //public bool HasMaxInstances { get; }

        public short MaxCumulativeRuntime { get; set; }
        //public bool HasMaxCumulativeRuntime { get; }

        public short MaxRuntime { get; set; }
        //public bool HasMaxRuntime { get; }

        public bool EnableTamperChecking { get; set; }
        #endregion

        #region Hardware Locking
        //public byte[] MachineCode { get; set; }
        public string MachineCodeAsString { get; set; }
        public bool UseHashedMachineCode { get; set; }
        #endregion

        #region License Service
        public short MaxActivations { get; set; }
        //public bool HasMaxActivations { get; }

        public bool ActivationsAreFloating { get; set; }

        public TimeSpan FloatingLeasePeriod { get; set; }
        //public bool HasFloatingLeasePeriod { get; }

        public int FloatingHeartBeatInterval { get; set; }
        //public bool HasFloatingHeartBeatInterval { get; }



        //public byte[] LicenseServerMachineCode { get; set; }
        public string LicenseServerMachineCodeAsString { get; set; }
        //public bool HasLicenseServerMachineCode { get; }

        public bool NotifyServiceOnValidation { get; set; }
        public bool VerifyLocalTimeWithService { get; set; }

        #endregion

        #region .Net Specific
        //public AssemblyName HostAssemblyName { get; set; }
        public string HostAssemblyName { get; set; }
        //public bool HasHostAssemblyName { get; }

        public bool PerformHostAssemblyStrongNameVerification { get; set; }
        public bool PerformCryptoLicensingModuleStrongNameVerification { get; set; }


        public bool ValidAtRunTime { get; set; }
        public bool ValidAtDesignTime { get; set; }
        public bool HasSeparateRuntimeLicense { get; set; }
        public string ExplicitRunTimeLicenseCode { get; set; }
        public string AllowedDomains { get; set; }
        public bool HasAllowedDomains { get { return !string.IsNullOrWhiteSpace(AllowedDomains); } }
        #endregion

        #region Miscellanous
        public string UserData { get; set; }
        public bool HasUserData { get { return !string.IsNullOrWhiteSpace(UserData); } }

        public short NumberOfUsers { get; set; }
        public bool HasNumberOfUsers { get { return NumberOfUsers > 0; } }

        public bool DetectDateRollback { get; set; }

        public bool EnableAntiDebuggerProtection { get; set; }
        public DateTime DateExpires { get; set; }
        public DateTime DateGenerated { get; set; }
        public DateTime DateAdded { get; set; }
        public bool DisallowInRemoteSession { get; set; }

        public string ProjectName { get; set; }
        public string KeyName { get; set; }
        public string ProfileName { get; set; }

        #endregion









        public string ActivationContext { get; set; }


        public string AssemblyStoragePath { get; set; }
        public short CurrentCumulativeRuntime { get; set; }
        public short CurrentExecutions { get; set; }
        //public short CurrentRuntime { get; }
        public short CurrentUniqueUsageDays { get; set; }
        public short CurrentUsageDays { get; set; }

        public DateTime DateLastUsed { get; set; }

        public LicenseFeatures Features { get; set; }
        public string FileStoragePath { get; set; }

        
        //public bool HasDateExpires { get; }
        //public bool HasFeatures { get; }


        //public bool HasLeaseExpires { get; }

        //public bool HasMachineCode { get; }


        //public Assembly HostAssembly { get; set; }

        public DateTime LeaseExpires { get; set; }

        public string LicenseServiceSettingsFilePath { get; set; }
        public string LicenseServiceUrl { get; set; }

        public string RegistryStoragePath { get; set; }
        //public short RemainingCumulativeRuntime { get; }
        //public short RemainingExecutions { get; }
        //public short RemainingRuntime { get; }
        //public short RemainingUniqueUsageDays { get; }
        //public short RemainingUsageDays { get; }
        public ServiceParamEncryptionMode ServiceParamEncryptionMode { get; set; }
        //public virtual LicenseStatus Status { get; }
        public LicenseStorageMode StorageMode { get; set; }
        


        public string ValidationKey { get; set; }




    }

    public enum LicenseStorageMode
    {
        None,
        ToFile,
        ToRegistry,
        FromAssembly
    }

    public enum LicenseFeatures
    {
        DoesNotMatter = 0,
        Feature1 = 1,
        Feature10 = 0x200,
        Feature11 = 0x400,
        Feature12 = 0x800,
        Feature13 = 0x1000,
        Feature14 = 0x2000,
        Feature15 = 0x4000,
        Feature16 = 0x8000,
        Feature17 = 0x10000,
        Feature18 = 0x20000,
        Feature19 = 0x40000,
        Feature2 = 2,
        Feature20 = 0x80000,
        Feature21 = 0x100000,
        Feature22 = 0x200000,
        Feature23 = 0x400000,
        Feature24 = 0x800000,
        Feature25 = 0x1000000,
        Feature26 = 0x2000000,
        Feature27 = 0x4000000,
        Feature28 = 0x8000000,
        Feature29 = 0x10000000,
        Feature3 = 4,
        Feature30 = 0x20000000,
        Feature31 = 0x40000000,
        Feature4 = 8,
        Feature5 = 0x10,
        Feature6 = 0x20,
        Feature7 = 0x40,
        Feature8 = 0x80,
        Feature9 = 0x100
    }

    public enum ServiceParamEncryptionMode
    {
        RSA,
        Compatible,
        None
    }

    public class LicenseEntityConfiguration : EntityTypeConfiguration<License>
    {
        public LicenseEntityConfiguration()
        {
            ToTable("T_License", "Licensing");
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Property(t => t.HotelId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Hotel.T_HotelRoomType_HotelId_RoomTypeId") { IsUnique = true, Order = 1 }));
            //HasRequired(t => t.Hotel).WithMany().HasForeignKey(t => t.HotelId).WillCascadeOnDelete(false);

            //Property(t => t.RoomTypeId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Hotel.T_HotelRoomType_HotelId_RoomTypeId") { IsUnique = true, Order = 2 }));
            //HasRequired(t => t.RoomType).WithMany().HasForeignKey(t => t.RoomTypeId).WillCascadeOnDelete(false);



            Property(t => t.LicenseCode).IsUnicode(false).HasMaxLength(2500);
            Property(t => t.MachineCodeAsString).IsUnicode(false).HasMaxLength(200);
            Property(t => t.HostAssemblyName).IsUnicode(false).HasMaxLength(200);
            Property(t => t.ExplicitRunTimeLicenseCode).IsUnicode(false).HasMaxLength(2500);
            Property(t => t.AllowedDomains).IsUnicode(false).HasMaxLength(300);
            Property(t => t.UserData).IsUnicode(false).HasMaxLength(2500);


            Property(t => t.ProjectName).IsUnicode(false).HasMaxLength(200);
            Property(t => t.KeyName).IsUnicode(false).HasMaxLength(200);
            Property(t => t.ProfileName).IsUnicode(false).HasMaxLength(200);
            Property(t => t.SerialCode).IsUnicode(false).HasMaxLength(50);
            

            
            Property(t => t.DateExpires).IsRequired().HasPrecision(0).HasColumnType("datetime2");
            Property(t => t.DateGenerated).IsRequired().HasPrecision(0).HasColumnType("datetime2");
            Property(t => t.DateLastUsed).IsRequired().HasPrecision(0).HasColumnType("datetime2");

            Property(t => t.FloatingLeasePeriod).IsRequired().HasPrecision(0).HasColumnType("time");


            //Property(t => t.IsDeleted).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_HotelType_IsDeleted")));

            //HasRequired(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId).WillCascadeOnDelete(false);
            //Property(t => t.CreatorId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_HotelType_CreatorId")));
            //Property(t => t.CreateDate).IsRequired().HasPrecision(0).HasColumnType("datetime2");


            //HasOptional(t => t.Modifier).WithMany().HasForeignKey(t => t.ModifierId).WillCascadeOnDelete(false);
            //Property(t => t.ModifierId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_HotelType_ModifierId")));
            //Property(t => t.ModifidDate).HasPrecision(0).HasColumnType("datetime2");
        }
    }
}
