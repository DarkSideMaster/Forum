﻿using Forum.Data;
using Forum.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum.Services
{
    public class ForumService : IForums
    {

        private readonly ApplicationDbContext _context;

        public ForumService(ApplicationDbContext context) 
        {
            _context = context;

        }

        public Task Create(Forums forum)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int forumId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Forums> GetAll()
        {
            return _context.Forums.Include( forum=>forum.Posts);
        }

        public IEnumerable<ApplicationUser> GetAllActiveUsers()
        {
            throw new NotImplementedException();
        }

        public Forums GetbyId(int Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumDescription(int forumId, string newDescription)
        {
            throw new NotImplementedException();
        }
    }
}