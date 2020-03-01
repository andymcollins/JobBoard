using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.API.Models
{
#pragma warning disable CS1591
    public static class WideWorldImportersDbContextExtensions
    {
        public static IQueryable<Job> GetJobs(this JobBoardDbContext dbContext, int pageSize = 10, int pageNumber = 1, string location = null)
        {
            // Get query from DbSet
            var query = dbContext.Job.AsQueryable();

            // Filter by: 'Location'
            if (!string.IsNullOrEmpty(location))
                query = query.Where(item => item.Location == location);
            return query;
        }

        public static async Task<Job> GetJobsAsync(this JobBoardDbContext dbContext, Job entity)
            => await dbContext.Job.FirstOrDefaultAsync(item => item.JobID == entity.JobID);

        public static async Task<Job> GetJobsByJobTitleAsync(this JobBoardDbContext dbContext, Job entity)
            => await dbContext.Job.FirstOrDefaultAsync(item => item.JobTitle == entity.JobTitle);
    }

    public static class IQueryableExtensions
    {
        public static IQueryable<TModel> Paging<TModel>(this IQueryable<TModel> query, int pageSize = 0, int pageNumber = 0) where TModel : class
            => pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;
    }
#pragma warning restore CS1591
}
