﻿using Book.Domain.Migrations;
using Book.Domain.Models;
using Book_Shop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Domain.Context
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {

        }
        public BookDbContext()
        {

        }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<BookLanguage> booklanguage { get; set; }
        public DbSet<Category> Categories { get; set; }
        
        public DbSet<BookItems> BookItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> orderDetails { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookLanguage>()
                .HasKey(bl => bl.Id);  

            base.OnModelCreating(modelBuilder);
        }



    }
}