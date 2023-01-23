﻿using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShooping.CartAPI.Data
{
    public class ProductVO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; } = string.Empty;
        public string? CategoryName { get; set; }
        public string? ImageURL { get; set; }
    }
}