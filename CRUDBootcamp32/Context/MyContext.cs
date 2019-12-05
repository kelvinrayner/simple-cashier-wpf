﻿using CRUDBootcamp32.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDBootcamp32.Context
{
    class MyContext : DbContext
    {
        public MyContext() : base("MyContext") { }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionItem> TransactionItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
