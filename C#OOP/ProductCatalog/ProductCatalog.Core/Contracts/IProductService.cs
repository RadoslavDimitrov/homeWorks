﻿using ProductCatalog.Infrastructure.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.Core.Contracts
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();

        void Save(Product product);
    }
}
