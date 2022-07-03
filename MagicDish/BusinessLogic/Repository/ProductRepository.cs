﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class ProductRepository : IProductRepository
    {
        static List<Product> _products = new List<Product>
        {
            new Product(1,"pasta",UnitOfMeasure.grams),
            new Product(2,"tomato",UnitOfMeasure.pieces),
            new Product(3,"minced meat",UnitOfMeasure.grams),
        };
        public Task<List<Product>> GetAsync()
        {
            return Task.FromResult<List<Product>>(_products);
        }
    }
}
