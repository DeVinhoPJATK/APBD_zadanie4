using APBD_Task_6.Models;
using Microsoft.AspNetCore.Mvc;
using Zadanie5.Services;

namespace Zadanie5.Controllers;

[ApiController]
[Route("api/warehouses2")]
public class WarehousesProcedureController : ControllerBase
{
    private readonly IWarehousesProcedureService _warehousesProcedureService;

    public WarehousesProcedureController(IWarehousesProcedureService warehousesProcedureService)
    {
        _warehousesProcedureService = warehousesProcedureService;
    }

    [HttpPost]
    public async Task<IActionResult> AddProcedureToWarehouseAsync(ProductWarehouse productWarehouse)
    {
        int idProductWarehouse = await _warehousesProcedureService.AddProductToWarehouseAsync(productWarehouse);
        return Ok(idProductWarehouse);
    }
}
