using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GWC_rest.Models;

namespace GWC_rest.Data
{
    public class ConversationContext : DbContext
    {
        public ConversationContext (DbContextOptions<ConversationContext> options)
            : base(options)
        {
        }

        public DbSet<GWC_rest.Models.Conversation> Conversation { get; set; }
    }
}
