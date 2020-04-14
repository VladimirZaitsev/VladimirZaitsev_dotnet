using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.DTO;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GroupService : IGroupService
    {
        private readonly IStore<GroupDto> _groups;
        private readonly IMapper _mapper;

        public async Task<int> AddAsync(Group item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (string.IsNullOrEmpty(item.Name))
            {
                throw new ArgumentException("Group name can't be empty");
            }

            var dto = _mapper.Map<GroupDto>(item);
            await _groups.AddAsync(dto);
            return dto.Id;
        }

        public async Task DeleteAsync(int id)
        {
            await _groups.DeleteAsync(id);
        }

        public IAsyncEnumerable<Group> GetAll()
        {
            var dtos = _groups.GetAll().AsAsyncEnumerable();
            var models = _mapper.Map<IAsyncEnumerable<Group>>(dtos);

            return models;
        }

        public async Task<Group> GetByIdAsync(int id)
        {
            var group = await _groups.GetByIdAsync(id);

            if (group == null)
            {
                throw new ArgumentException("Group not found");
            }

            var model = _mapper.Map<Group>(group);
            return model;
        }

        public async Task<IAsyncEnumerable<Group>> GetGroupsByLecturerIdAsync(int lecturerId)
        {
            var groups = await _groups.GetAll()
                .Where(group => group.LecturerId == lecturerId)
                .ToListAsync();

            var hasRecords = groups.Any();

            if (!hasRecords)
            {
                throw new ArgumentException("Groups are not found");
            }

            var models = _mapper.Map<IAsyncEnumerable<Group>>(groups);

            return models;
        }

        public async Task<Group> GetGroupByStudentIdAsync(int studentId)
        {
            var groupQuery = from grp in _groups.GetAll()
                             where grp.StudentIds.Contains(studentId)
                             select grp;

            var group = await groupQuery.SingleOrDefaultAsync();

            if (group == null)
            {
                throw new ArgumentException("Group not found");
            }

            var model = _mapper.Map<Group>(group);

            return model;
        }

        public async Task UpdateAsync(Group item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (string.IsNullOrEmpty(item.Name))
            {
                throw new ArgumentException("Group name can't be empty");
            }

            var dto = _mapper.Map<GroupDto>(item);
            await _groups.UpdateAsync(dto);
        }
    }
}
