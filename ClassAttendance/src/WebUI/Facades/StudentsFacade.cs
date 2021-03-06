﻿using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Api;
using WebUI.Models.ViewModels.StudentModel;

namespace WebUI.Facades
{
    public class StudentsFacade
    {
        private readonly IStudentApi _studentApi;
        private readonly IGroupApi _groupApi;

        public StudentsFacade(IStudentApi studentApi, IGroupApi groupApi)
        {
            _studentApi = studentApi;
            _groupApi = groupApi;
        }

        public async Task<IEnumerable<StudentViewModel>> GetStudentListAsync()
        {
            var students = await _studentApi.GetAll();

            // Unable to do this using Select because i'm getting error of running second operation
            // Before first was completed
            var list = new List<StudentViewModel>();
            foreach (var student in students)
            {
                list.Add(new StudentViewModel
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Address = student.Address,
                    GroupName = await GetGroupNameAsync(student.Id),
                });
            }

            return list;
        }

        public async Task<StudentManageViewModel> GetViewModel()
        {
            var model = new StudentManageViewModel
            {
                Groups = await _groupApi.GetAll(),
                Student = new Student(),
            };

            return model;
        }

        public async Task<StudentManageViewModel> GetViewModel(int studentId)
        {
            var model = new StudentManageViewModel
            {
                Groups = await _groupApi.GetAll(),
                Student = await _studentApi.GetByIdAsync(studentId),
            };

            return model;
        }

        public async Task AddStudentAsync(Student student)
        {
            await _studentApi.AddAsync(student);
        }

        public async Task EditStudentAsync(Student student)
        {
            await _studentApi.UpdateAsync(student);
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            await _studentApi.DeleteAsync(studentId);
        }

        private async Task<string> GetGroupNameAsync(int studentId)
        {
            var group = await _studentApi.GetStudentGroupAsync(studentId);

            return group.Name;
        }
    }
}