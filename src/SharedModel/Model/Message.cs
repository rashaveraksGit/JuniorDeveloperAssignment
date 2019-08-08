using System;
using System.Collections.Generic;

namespace SharedModel.Model
{
    public class Message
    {
        public Message()
        {
            Basket = new List<int>();
        }
        public DateTime MessageDate { get; set; }
        public string FullName { get; set; }
        public string Emailaddress { get; set; }
        public List<int> Basket { get; set; }
    }
}