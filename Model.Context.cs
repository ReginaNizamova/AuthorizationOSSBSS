﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AuthorizationOSSBSS
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AuthorizationEntities : DbContext
    {
        public AuthorizationEntities()
            : base("name=AuthorizationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DataConnectionSubscriber> DataConnectionSubscribers { get; set; }
        public virtual DbSet<EquipmentData> EquipmentDatas { get; set; }
        public virtual DbSet<IdentificationData> IdentificationDatas { get; set; }
        public virtual DbSet<InformationInstalledEquipment> InformationInstalledEquipments { get; set; }
        public virtual DbSet<InstallationPlaceInformation> InstallationPlaceInformations { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<TypesPort> TypesPorts { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}