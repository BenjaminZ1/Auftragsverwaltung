using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Auftragsverwaltung.Infrastructure.Article
{
    class ArticleDbContext : BaseDbContext
    {
        public ArticleDbContext(DbContextOptions<ArticleDbContext> options) : base(options) { }
        public DbSet<Domain.Article> Articles { get; set; }
        public DbSet<Domain.ArticleGroup> ArticleGroups { get; set; }
        public DbSet<Domain.Position> Positions { get; set; }
    }
}
