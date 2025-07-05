using AutoMapper;
using BugTracker.Models;
using BugTracker.DTOs;

public class IssueProfile : Profile
{
    public IssueProfile()
    {
        CreateMap<Issue, IssueDTO>().ReverseMap();
    }
}
