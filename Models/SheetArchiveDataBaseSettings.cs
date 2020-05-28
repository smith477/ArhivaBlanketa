using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArhivaBlanketa.Models
{
    public class SheetArchiveDataBaseSettings : ISheetDataBaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ISheetDataBaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
