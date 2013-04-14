using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Basic_Auditing.Models
{
    public class Audit
    {
        public Guid AuditID { get; set; }
        public string IPAddress { get; set; }
        public string UserName { get; set; }
        public string URLAccessed { get; set; }
        public DateTime TimeAccessed { get; set; }

        public Audit()
        {
        }
    }

    public class AuditingContext : DbContext
    {
        public DbSet<Audit> AuditRecords { get; set; }
    }

    public class PostingModel
    {
        public string PropertyA { get; set; }
        public string PropertyB { get; set; }

        public PostingModel()
        {

        }
    }
}