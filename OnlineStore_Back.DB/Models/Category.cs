﻿
namespace OnlineStore_Back.DB.Models
{
    public class Category
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
    }
}