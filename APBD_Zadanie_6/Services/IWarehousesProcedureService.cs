using APBD_Task_6.Models;

namespace Zadanie5.Services;

public interface IWarehousesProcedureService
{
    Task<int> AddProductToWarehouseAsync(ProductWarehouse productWarehouse);
}
