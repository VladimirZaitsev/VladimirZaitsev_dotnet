using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Dtos;
using DAL.Interfaces;
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

        public GroupService(IStore<GroupDto> groups, IMapper mapper)
        {
            _groups = groups;
            _mapper = mapper;
        }

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

        public IEnumerable<Group> GetAll()
        {
            var dtos = _groups.GetAll().AsEnumerable();
            var models = _mapper.Map<IEnumerable<Group>>(dtos);

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

        public Group GetGroupByStudentId(int studentId)
        {
            var groupQuery = from grp in _groups.GetAll().ToList()
                             where grp.StudentIds.Contains(studentId)
                             select grp;

            var group = groupQuery.SingleOrDefault();

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
