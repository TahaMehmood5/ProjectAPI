﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DCAEntities : DbContext
    {
        public DCAEntities()
            : base("name=DCAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Appeal> Appeals { get; set; }
        public virtual DbSet<AssignCommittee> AssignCommittees { get; set; }
        public virtual DbSet<AssignPunishment> AssignPunishments { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Case> Cases { get; set; }
        public virtual DbSet<Committee> Committees { get; set; }
        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<P_Type> P_Type { get; set; }
        public virtual DbSet<Punishment> Punishments { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<venue> venues { get; set; }
    }
}