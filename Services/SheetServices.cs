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
        private readonly IMongoCollection<Subject> _subject;

        public SheetServices(ISheetDataBaseSettings settings)
        {
            
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _sheets = database.GetCollection<Sheet>("Sheet");
            _subject = database.GetCollection<Subject>("Subject");
        }

        public List<Sheet> Get() =>
            _sheets.Find(sheet => true).ToList();

        public Sheet GetSheet(string id) =>
            _sheets.Find(sheet => sheet.Id == id).FirstOrDefault();

        public void Add(string id, Sheet sheet)
        {
            Subject s = new Subject();
            s = _subject.Find(sub => sub.Id == id).FirstOrDefault();
            sheet.Status = false;
            _sheets.InsertOne(sheet);
            s.Sheets.Add(sheet.Id.ToString());
            _subject.ReplaceOne(sbj => sbj.Id == s.Id, s);
        }

        public void Update(Sheet sheet, Sheet sheetIn)
        {
            if (sheetIn.Id == null)
                sheetIn.Id = sheet.Id;

            _sheets.ReplaceOne(sht => sht.Id == sheet.Id, sheetIn);
        }


        public void Remove(string id)
        {
            List<Subject> subjects = _subject.Find(sheet => true).ToList();
            foreach (Subject subj in subjects)
                subj.Sheets.Remove(id);

            _sheets.DeleteOne(sheet => sheet.Id == id);
        }
    }
}
