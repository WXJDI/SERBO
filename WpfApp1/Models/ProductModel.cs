﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public int Quantite { get; set; }
        public ProductModel()
        {

        }
        public ProductModel ( ProductModel pro )
        {
            Id = pro.Id;
            Name = pro.Name;
            Price = pro.Price;
            Quantite = pro.Quantite;
        }
    }
}
