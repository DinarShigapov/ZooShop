﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cashier.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ZooShopDBEntities1 : DbContext
    {
        public ZooShopDBEntities1()
            : base("name=ZooShopDBEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Animal> Animal { get; set; }
        public virtual DbSet<BonusCard> BonusCard { get; set; }
        public virtual DbSet<CompositionOfSupply> CompositionOfSupply { get; set; }
        public virtual DbSet<Delivery> Delivery { get; set; }
        public virtual DbSet<DeliveryStatus> DeliveryStatus { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        public virtual DbSet<Provider> Provider { get; set; }
        public virtual DbSet<Sale> Sale { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<Supply> Supply { get; set; }
    }
}
