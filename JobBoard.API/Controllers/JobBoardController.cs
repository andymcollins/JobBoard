using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using JobBoard.API.Models;

namespace JobBoard.API.Controllers
{
#pragma warning disable CS1591
    [ApiController]
    [Route("api/v1/[controller]")]
    public class JobBoardController : ControllerBase
    {
        protected readonly ILogger Logger;
        protected readonly JobBoardDbContext DbContext;

        public JobBoardController(ILogger<JobBoardController> logger, JobBoardDbContext dbContext)
        {
            Logger = logger;
            DbContext = dbContext;
        }
#pragma warning restore CS1591

        // GET
        // api/v1/JobBoard/Job

        /// <summary>
        /// Retrieves Jobs
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="location">Location</param>
        /// <returns>A response with Jobs </returns>
        /// <response code="200">Returns current Jobs</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("Jobs")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetJobsAsync(int pageSize = 10, int pageNumber = 1, string location = null)
        {
            var response = new PagedResponse<Job>();

            try
            {
                // Get the "proposed" query from repository
                var query = DbContext.GetJobs();

                // Set paging values
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;

                // Get the total rows
                response.ItemsCount = await query.CountAsync();

                // Get the specific page from database
                response.Model = await query.Paging(pageSize, pageNumber).ToListAsync();

                response.Message = string.Format("Page {0} of {1}, Total Number of Jobs: {2}.", pageNumber, response.PageCount, response.ItemsCount);
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error" + ex;
            }
            return response.ToHttpResponse();
        }

        // GET
        // api/v1/JobBoard/Job/5

        /// <summary>
        /// Retrieves a Job by ID
        /// </summary>
        /// <param name="id">Job id</param>
        /// <returns>A response with Job</returns>
        /// <response code="200">Returns the Job</response>
        /// <response code="404">If job does not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("Job/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetJobAsync(int id)
        {
            var response = new SingleResponse<Job>();

            try
            {
                // Get the Job by id
                response.Model = await DbContext.GetJobsAsync(new Job(id));
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error " + ex;
            }
            return response.ToHttpResponse();
        }

        // POST
        // api/v1/JobBoard/Job/

        /// <summary>
        /// Creates a new Job
        /// </summary>
        /// <param name="request">Request model</param>
        /// <returns>A response with new Job</returns>
        /// <response code="201">A response as creation of Job</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("Job")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostJobAsync([FromBody]PostJobRequest request)
        {
            var response = new SingleResponse<Job>();

            try
            {
                var existingEntity = await DbContext
                    .GetJobsByJobTitleAsync(new Job { JobTitle = request.JobTitle });

                if (existingEntity != null)
                    ModelState.AddModelError("JobTitle", "Job already exists");

                if (!ModelState.IsValid)
                    return BadRequest();

                // Create entity from request model
                var entity = request.ToEntity();

                // Add entity to repository
                DbContext.Add(entity);

                // Save entity in database
                await DbContext.SaveChangesAsync();

                // Set the entity to response model
                response.Model = entity;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PostJobAsync), ex);
            }

            return response.ToHttpCreatedResponse();
        }
    }
}
