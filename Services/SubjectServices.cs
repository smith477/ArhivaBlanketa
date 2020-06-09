using ArhivaBlanketa.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArhivaBlanketa.Services
{
    public class SubjectServices
    {
        private readonly IMongoCollection<Subject> _subjects;

        public SubjectServices(ISheetDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _subjects = database.GetCollection<Subject>("Subject");
        }

        public List<Subject> Get() =>
            _subjects.Find(subject => true).ToList();

        public Subject GetSubject(string id) =>
            _subjects.Find(subject => subject.Id == id).FirstOrDefault();

        public Subject GetByName(string name) =>
            _subjects.Find(subject => subject.SubjectName == name).FirstOrDefault();

        public Subject Create(Subject subject)
        {
            _subjects.InsertOne(subject);

            return subject;
        }

        public void Update(Subject subject, Subject subjectIn)
        {
            if (subjectIn.Id == null)
                subjectIn.Id = subject.Id;

            _subjects.ReplaceOne(sbj => sbj.Id == subject.Id, subjectIn);
        }

        public List<Subject> FilterByMajor(string major) =>
            _subjects.Find(subject => subject.Major == major).ToList();

        public void AddImg(string subjectId, string sheetId)
        {
            Subject s = _subjects.Find(sub => sub.Id == subjectId).FirstOrDefault();
            s.Sheets.Add(sheetId);
            _subjects.ReplaceOne(sbj => sbj.Id == s.Id, s);
        }

        public void Remove(string id) =>
            _subjects.DeleteOne(subject => subject.Id == id);

    }
}
