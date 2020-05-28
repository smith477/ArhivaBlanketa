using ArhivaBlanketa.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArhivaBlanketa.Services
{
    public class SheetServices
    {
        private readonly IMongoCollection<Sheet> _sheets;
        private readonly SubjectServices _subject;

        public SheetServices(ISheetDataBaseSettings settings, SubjectServices subject)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _sheets = database.GetCollection<Sheet>("Sheet");
            _subject = subject;
        }

        public List<Sheet> Get() =>
            _sheets.Find(sheet => true).ToList();

        public Sheet GetSheet(string id) =>
            _sheets.Find(sheet => sheet.Id == id).FirstOrDefault();

        public void Add(string id, Sheet sheet)
        {
            _subject.AddImg(id, sheet.Id);
            _sheets.InsertOne(sheet);
        }

        public void Remove(string id) =>
            _sheets.DeleteOne(sheet => sheet.Id == id);
    }
}
