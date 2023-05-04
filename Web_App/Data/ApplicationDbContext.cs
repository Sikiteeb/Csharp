using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAppCrypto.Data
{
    
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RsaKey> RsaKeys { get; set; }
        public DbSet<KeyExchange> KeyExchange { get; set; }
        public DbSet<Caesar> Caesars { get; set; }
    }
}