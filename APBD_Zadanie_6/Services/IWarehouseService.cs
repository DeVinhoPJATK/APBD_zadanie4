﻿using APBD_Task_6.Models;

namespace Zadanie5.Services
{
    public interface IWarehouseService
    {
        public Task<int> AddProduct(ProductWarehouse productWarehouse);
    }
}
