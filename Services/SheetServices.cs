using ArhivaBlanketa.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
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

        public List<Sheet> GetBySubject(string subjectName)
        {
            return _sheets.Find(sheet => sheet.SubjectName == subjectName).ToList();
        }
        

        public Sheet GetSheet(string id) =>
            _sheets.Find(sheet => sheet.Id == id).FirstOrDefault();

        public void Add(Sheet sheet)
        {
            
            sheet.Status = false;
            _sheets.InsertOne(sheet);
           
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
            foreach (Subject subj in subjects) { 
                subj.Sheets.Remove(id);
                _subject.ReplaceOne(sbj => sbj.Id == subj.Id, subj);
            }
            _sheets.DeleteOne(sheet => sheet.Id == id);
        }
    }
}
