using Dapper.Contrib.Extensions;
using System;

namespace Server.Model
{
    [Table("Persons")]
    public class Person
    {
        public int Id { get; set; }
        public int FIO_Id { get; set; }
        public int Adress_Id { get; set; }
        public DateTime Birthday { get; set; }
    }
}
