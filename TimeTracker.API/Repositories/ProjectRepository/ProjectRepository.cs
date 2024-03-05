namespace TimeTracker.API.Repositories.ProjectRepository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _dataContext;
        private readonly IUserContextService _userContextService;

        public ProjectRepository(DataContext dataContext, IUserContextService userContextService)
        {
            _dataContext = dataContext;
            _userContextService = userContextService;
        }

        public async Task<List<Project>> GetAllProjects()
        {
            var userId = _userContextService.GetUserId();
            if (userId == null)
                return new List<Project>();

            return await _dataContext.Projects
                .Where(p => !p.IsDeleted && p.Users.Any(u => u.Id == userId)).ToListAsync();
        }

        public async Task<Project?> GetProjectById(int id)
        {
            var userId = _userContextService.GetUserId();
            if (userId == null)
                return null;

            var project = await _dataContext.Projects
                .FirstOrDefaultAsync(
                    p => !p.IsDeleted 
                    && p.Id == id 
                    && p.Users.Any(u => u.Id == userId));

            return project;
        }

        public async Task<List<Project>> CreateProject(Project project)
        {
            var user = await _userContextService.GetUserAsync();
            if (user == null)
                throw new EntityNotFoundException("User account was not found.");

            project.Users.Add(user);

            _dataContext.Projects.Add(project);
            await _dataContext.SaveChangesAsync();

            return await GetAllProjects();
        }

        public async Task<List<Project>> UpdateProject(int id, Project project)
        {
            var userId = _userContextService.GetUserId();
            if (userId == null)
                throw new EntityNotFoundException("User account was not found.");

            var dbProject = await _dataContext.Projects
                .FirstOrDefaultAsync(
                    p => !p.IsDeleted
                    && p.Id == id
                    && p.Users.Any(u => u.Id == userId));

            if (dbProject is null)
                throw new EntityNotFoundException("Project was not found.");

            if (project.ProjectDetails != null && dbProject.ProjectDetails != null)
            {
                dbProject.ProjectDetails.Description = project.ProjectDetails.Description;
                dbProject.ProjectDetails.StartDate = project.ProjectDetails.StartDate;
                dbProject.ProjectDetails.EndDate = project.ProjectDetails.EndDate;
            }
            else if (project.ProjectDetails != null && dbProject.ProjectDetails == null)
            {
                dbProject.ProjectDetails = new ProjectDetails
                {
                    Description = project.ProjectDetails.Description,
                    StartDate = project.ProjectDetails.StartDate,
                    EndDate = project.ProjectDetails.EndDate,
                    Project = project
                };
            }

            dbProject.Name = project.Name;
            dbProject.DateUpdated = DateTime.Now;

            await _dataContext.SaveChangesAsync();

            return await GetAllProjects();
        }

        public async Task<List<Project>?> DeleteProject(int id)
        {
            var userId = _userContextService.GetUserId();
            if (userId == null)
                return null;

            var dbProject = await _dataContext.Projects
                .FirstOrDefaultAsync(
                   p => !p.IsDeleted
                   && p.Id == id
                   && p.Users.Any(u => u.Id == userId));

            if (dbProject is null)
                return null;

            dbProject.IsDeleted = true;
            dbProject.DateDeleted = DateTime.Now;

            await _dataContext.SaveChangesAsync();

            return await GetAllProjects();
        }
    }
}
