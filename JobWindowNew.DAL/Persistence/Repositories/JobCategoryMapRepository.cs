﻿using JobWindowNew.Domain.IRepositories;
using JobWindowNew.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace JobWindowNew.DAL.Persistence.Repositories
{
    public class JobCategoryMapRepository : IJobCategoryMapRepository
    {
        private readonly ApplicationDbContext _context;

        public JobCategoryMapRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(JobCategoryMap map)
        {
            _context.JobCategoryMaps.Add(map);
        }

        public IList<int> GetCategoriesForJob(long jobId)
        {
            return _context.JobCategoryMaps.Where(j => j.JobId == jobId)
                .Select(j => j.CategoryId).ToList();
        }

        public string GetFirstCategoryTypeForJob(long jobId)
        {
            return _context.JobCategoryMaps.Where(j => j.JobId == jobId)
                .Select(c => c.Category.CategoryName).FirstOrDefault();
        }

        public IQueryable<JobCategoryMap> GetJobsForEverGreenReport()
        {
            return _context.JobCategoryMaps
                .Where(j => j.Job.IsEverGreen)
                .GroupBy(m => m.Job.Id).SelectMany(gr => gr.Take(1))
                .Include(m => m.Job)
                .Include(m => m.Job.Country)
                .Include(m => m.Job.State)
                .Include(m => m.Job.EmploymentType)
                .Include(m => m.Job.JobBoard)
                .Include(m => m.Job.SalaryType)
                .Include(m => m.Category);
        }

        public void Delete(long jobId)
        {
            _context.JobCategoryMaps
                .RemoveRange(_context.JobCategoryMaps
                    .Where(j => j.JobId == jobId));
        }

        public void Update(JobCategoryMap map)
        {
            _context.Entry(map).State = EntityState.Modified;
        }

        public IQueryable<JobCategoryMap> GetJobsForActiveReport()
        {
            return _context.JobCategoryMaps
                .Where(m => m.Job.ExpirationDate > DateTime.Now)
                .GroupBy(m => m.Job.Id).SelectMany(gr => gr.Take(1))
                .Include(m => m.Job)
                .Include(m => m.Job.Country)
                .Include(m => m.Job.State)
                .Include(m => m.Job.EmploymentType)
                .Include(m => m.Job.JobBoard)
                .Include(m => m.Job.SalaryType)
                .Include(m => m.Category).Where(j => j.Job.IsEverGreen == false);
        }


        public IQueryable<JobCategoryMap> GetJobsForInActiveReport(int podId)
        {
            return _context.JobCategoryMaps
                .Where(j => j.Job.SchedulingPod == podId)
                .Where(j => j.Job.IsEverGreen == false)
                .Where(j => j.Job.ExpirationDate < DateTime.Now)
                .GroupBy(m => m.Job.Id).SelectMany(gr => gr.Take(1))
                .Include(m => m.Job)
                .Include(m => m.Job.Country)
                .Include(m => m.Job.State)
                .Include(m => m.Job.EmploymentType)
                .Include(m => m.Job.JobBoard)
                .Include(m => m.Job.SalaryType)
                .Include(m => m.Category)
                .OrderBy(m => m.Job.SchedulingPod)
                .ThenBy(m => m.Job.JobBoard.JobBoardName)
                .ThenBy(m => m.Job.City)
                .ThenBy(m => m.Category.CategoryName)
                .ThenBy(m => m.Job.ExpirationDate)
                .ThenByDescending(j => j.Job.ApsCl)
                .ThenByDescending(j => j.Job.Bob)
                .ThenByDescending(j => j.Job.Intvs2)
                .ThenByDescending(j => j.Job.Intvs);
        }
    }
}
