﻿using TimeTracker.Shared.Models.Project;

namespace TimeTracker.Client.Services.ProjectService
{
    public interface IProjectService
    {
        event Action? OnChange;
        public List<ProjectResponse> Projects { get; set; }
        Task LoadAllProjects(); 
        Task<ProjectResponse> GetProjectById(int id);
        Task CreateProject(ProjectRequest request);
        Task UpdateProject(int id, ProjectRequest request);
        Task DeleteProject(int id);

    }
}
