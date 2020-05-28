using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArhivaBlanketa.Models;
using ArhivaBlanketa.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ArhivaBlanketa.Controllers
{
    [Route("api/[controller]")]
    public class SheetsController : Controller
    {
        public readonly SheetServices _sheetServices;
        public SheetsController(SheetServices sheetServices)
        {
            _sheetServices = sheetServices;
        }

        [HttpGet]
        public ActionResult<List<Sheet>> Get() =>
            _sheetServices.Get();

        [HttpGet("{id:length(24)}")]
        public ActionResult<Sheet> Get(string id)
        {
            var sheet = _sheetServices.GetSheet(id);

            if (sheet == null)
            {
                return NotFound();
            }

            return sheet;
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, [FromBody] Sheet sheetIn)
        {
            _sheetServices.Add(id, sheetIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var sheet = _sheetServices.GetSheet(id);

            if (sheet == null)
            {
                return NotFound();
            }

            _sheetServices.Remove(sheet.Id);

            return NoContent();
        }
    }
}
