﻿using Microsoft.EntityFrameworkCore;
using Model.Entity;

namespace Model.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        {

        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
