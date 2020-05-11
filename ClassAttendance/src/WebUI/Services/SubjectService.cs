using BLL.Interfaces;
using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Services
{
    public class SubjectService
    {
        private readonly ISubjectService _subjectService;

        public SubjectService(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        public IEnumerable<Subject> GetSubjects() => _subjectService.GetAll();

        public async Task AddSubjectAsync(Subject subject) => await _subjectService.AddAsync(subject);

        public async Task UpdateSubjectAsync(Subject subject) => await _subjectService.UpdateAsync(subject);

        public async Task DeleteSubjectAsync(int id) => await _subjectService.DeleteAsync(id);

        public async Task<Subject> GetByIdAsync(int id) => await _subjectService.GetByIdAsync(id);
    }
}
