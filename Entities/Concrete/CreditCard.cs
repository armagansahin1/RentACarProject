using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class CreditCard:IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public int CCV { get; set; }
        public int ExpYear { get; set; }
        public int ExpMonth { get; set; }
        public Int64 CardNumber { get; set; }
    }
}
