using Dapper.Contrib.Extensions;
using System;

namespace Server.Model
{
    [Table("Terms")]
    public class Term
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ToComplete { get; set; }
    }
}
