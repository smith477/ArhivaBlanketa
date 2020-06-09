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
    public class SubjectController : Controller
    {
        private readonly SubjectServices _subjectService;

        public SubjectController(SubjectServices subject)
        {
            _subjectService = subject;
        }

        [HttpGet]
        public ActionResult<List<Subject>> Get() =>
            _subjectService.Get();
        [HttpGet("{id:length(24)}", Name = "GetSubject")]
        public ActionResult<Subject> Get(string id)
        {
            var subject = _subjectService.GetSubject(id);

            if (subject == null)
            {
                return NotFound();
            }

            return subject;
        }

        [HttpPost]
        public ActionResult<Subject> Create([FromBody] Subject subject)
        {
            _subjectService.Create(subject);

            return CreatedAtRoute("GetSubject", new { id = subject.Id.ToString() }, subject);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, [FromBody] Subject subjectIn)
        {
            var subject = _subjectService.GetSubject(id);

            if (subject == null)
            {
                return NotFound();
            }
            _subjectService.Update(subject, subjectIn);

            return NoContent();
        }

        [HttpGet("{major}")]
        public ActionResult<List<Subject>> FilterByMajor(string major) =>
            _subjectService.FilterByMajor(major);

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var subject = _subjectService.GetSubject(id);

            if (subject == null)
            {
                return NotFound();
            }

            _subjectService.Remove(subject.Id);

            return NoContent();
        }
    }
}

