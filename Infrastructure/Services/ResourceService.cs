using Application.Abstractions.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class ResourceService(
    IRepository<User> userRepository,
    IRepository<Organization> organizationRepository,
    IRepository<Resource> resourceRepository,
    IHttpContextAccessor httpContextAccessor
    ) : IResourceService
{
    public async Task CreateResourceAsync(Resource resource, CancellationToken cancellationToken)
    {
        var getUsersQuery = userRepository.GetQueryable().AsNoTracking();
        var volunteerEmail = GetUserEmailFromContext();

        var volunteer = await getUsersQuery.FirstOrDefaultAsync(u => u.Email == volunteerEmail, cancellationToken)
                       ?? throw new NotFoundException("Volunteer not found.");

        if (volunteer.Role == Role.Volunteer || volunteer.Role == Role.OrganisationOwner)
        {
            throw new ForbiddenException("You are not volunteer or owner of the organization");
        }

        resource.VolunteerId = volunteer.Id;


        await resourceRepository.AddAsync(resource, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var getUsersQuery = userRepository.GetQueryable().AsNoTracking();
        var volunteerEmail = GetUserEmailFromContext();

        var volunteer = await getUsersQuery.FirstOrDefaultAsync(u => u.Email == volunteerEmail, cancellationToken)
                       ?? throw new NotFoundException("Volunteer not found.");

        if (volunteer.Role == Role.Volunteer || volunteer.Role == Role.OrganisationOwner)
        {
            throw new ForbiddenException("You are not volunteer or owner of the organization");
        }

        var resource = await resourceRepository.GetAsync(id, cancellationToken)
                        ?? throw new NotFoundException("Resource not found.");

        resourceRepository.Delete(resource);
    }

    public async Task<ICollection<Resource>> GetOrganisationResourcesAsync(Guid organizationId, CancellationToken cancellationToken)
    {
        var organization = await organizationRepository.GetAsync(organizationId, cancellationToken)
                        ?? throw new NotFoundException("Organization not found.");

        var resources = await resourceRepository.GetQueryable()
                                .Where(r => r.Volunteer.OrganizationId == organizationId)
                                .AsNoTracking()
                                .ToListAsync(cancellationToken);

        return resources;
    }

    public async Task<ICollection<Resource>> GetResourcesAsync(CancellationToken cancellationToken)
    {
        var resources = await resourceRepository.GetQueryable()
                                .AsNoTracking()
                                .ToListAsync(cancellationToken);

        return resources;
    }

    public async Task UpdateResourceAsync(Resource resource, CancellationToken cancellationToken)
    {
        var getUsersQuery = userRepository.GetQueryable().AsNoTracking();
        var volunteerEmail = GetUserEmailFromContext();

        var volunteer = await getUsersQuery.FirstOrDefaultAsync(u => u.Email == volunteerEmail, cancellationToken)
                       ?? throw new NotFoundException("Volunteer not found.");

        if (volunteer.Role == Role.Volunteer || volunteer.Role == Role.OrganisationOwner)
        {
            throw new ForbiddenException("You are not volunteer or owner of the organization");
        }

        var entity = await resourceRepository.GetAsync(resource.Id, cancellationToken)
                        ?? throw new NotFoundException("Resource not found.");
        entity.Title = resource.Title; 
        entity.Description = resource.Description;
        entity.Type = resource.Type;
        entity.Address = resource.Address;
        entity.Phone = resource.Phone;

        resourceRepository.Update(entity);
    }

    private string GetUserEmailFromContext()
    {
        const string firebaseEmailClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        return httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == firebaseEmailClaim)!.Value;
    }
}
