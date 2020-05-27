using BLL.Interfaces;
using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Api;

namespace WebUI.Facades
{
    public class SubjectsFacade
    {
        private readonly ISubjectApi _subjectApi;

        public SubjectsFacade(ISubjectApi subjectApi)
        {
            _subjectApi = subjectApi;
        }

        public async Task<IEnumerable<Subject>> GetSubjects() => await _subjectApi.GetAll();

        public async Task AddSubjectAsync(Subject subject) => await _subjectApi.AddAsync(subject);

        public async Task UpdateSubjectAsync(Subject subject) => await _subjectApi.UpdateAsync(subject);

        public async Task DeleteSubjectAsync(int id) => await _subjectApi.DeleteAsync(id);

        public async Task<Subject> GetByIdAsync(int id) => await _subjectApi.GetByIdAsync(id);
    }
}
