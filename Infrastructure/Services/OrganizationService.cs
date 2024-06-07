using Application.Abstractions.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class OrganizationService(
    IRepository<Organization> organizationRepository,
    IRepository<User> userRepository,
    IRepository<OrganizationApplication> organizationApplicationRepository,
    IHttpContextAccessor httpContextAccessor
    ) : IOrganizationService
{
    public async Task AcceptMemberAsync(Guid volunteerId, Guid organizationId, CancellationToken cancellationToken)
    {
        var getUsersQuery = userRepository.GetQueryable();
        var getApplicationsQuery = organizationApplicationRepository.GetQueryable();

        var ownerEmail = GetUserEmailFromContext();
        var owner = await getUsersQuery.FirstOrDefaultAsync(u => u.Email == ownerEmail, cancellationToken)
                       ?? throw new NotFoundException("Owner not found.");

        var application = await getApplicationsQuery.FirstOrDefaultAsync(a => a.VolunteerId == volunteerId && a.OrganisationId == organizationId, cancellationToken)
                       ?? throw new NotFoundException("Application not found.");

        if (owner.OrganizationId == organizationId)
        {
            var member = await getUsersQuery.FirstOrDefaultAsync(u => u.Id == volunteerId, cancellationToken)
                ?? throw new NotFoundException("Volunteer not found."); 

            application.IsAccepted = true;
            member.OrganizationId = organizationId;
            member.Role = Role.Volunteer;
            organizationApplicationRepository.Update(application);
            userRepository.Update(member);
        }
        else
        {
            throw new ForbiddenException("You are not an owner of the organization");
        }
    }

    public async Task CreateOrganizationAsync(string name, CancellationToken cancellationToken)
    {
        var getUsersQuery = userRepository.GetQueryable();

        var ownerEmail = GetUserEmailFromContext();
        var owner = await getUsersQuery.FirstOrDefaultAsync(u => u.Email == ownerEmail, cancellationToken)
                       ?? throw new NotFoundException("Owner not found.");

        if (await organizationRepository.GetQueryable().AnyAsync(o => o.Name == name, cancellationToken))
        {
            throw new ConflictException("Organization is already exists.");
        }

        owner.Role = Role.OrganisationOwner;
        var organization = new Organization { Name = name };

        userRepository.Update(owner);
        await organizationRepository.AddAsync(organization, cancellationToken);
    }

    public async Task JoinOrganizationAsync(Guid organizationId, CancellationToken cancellationToken)
    {
        var getUsersQuery = userRepository.GetQueryable().AsNoTracking();
        var getOrganizationQuery = organizationRepository.GetQueryable().AsNoTracking();

        var volunteerEmail = GetUserEmailFromContext();
        var volunteer = await getUsersQuery.FirstOrDefaultAsync(u => u.Email == volunteerEmail, cancellationToken)
                       ?? throw new NotFoundException("Volunteer not found.");

        var organization = await getOrganizationQuery.FirstOrDefaultAsync(o => o.Id == organizationId, cancellationToken)
                        ?? throw new NotFoundException("Organization not found.");

        var application = new OrganizationApplication { OrganisationId = organizationId, VolunteerId = volunteer.Id };

        await organizationApplicationRepository.AddAsync(application, cancellationToken);
    }

    public async Task KickUserAsync(Guid volunteerId, Guid organizationId, CancellationToken cancellationToken)
    {
        var getUsersQuery = userRepository.GetQueryable();
        var getOrganizationsQuery = organizationRepository.GetQueryable();

        var ownerEmail = GetUserEmailFromContext();
        var owner = await getUsersQuery.FirstOrDefaultAsync(u => u.Email == ownerEmail, cancellationToken)
                       ?? throw new NotFoundException("Owner not found.");
        var volunteer = await getUsersQuery.FirstOrDefaultAsync(u => u.Id == volunteerId, cancellationToken)
               ?? throw new NotFoundException("Volunteer not found.");
        var organization = await getOrganizationsQuery.FirstOrDefaultAsync(o => o.Id == organizationId, cancellationToken)
               ?? throw new NotFoundException("Organization not found.");

        if (owner.OrganizationId == organizationId && owner.Role == Role.OrganisationOwner)
        {
            volunteer.OrganizationId = null;
            userRepository.Update(volunteer);
        }
        else
        {
            throw new ForbiddenException("You are not an owner of this organization.");
        }
    }

    public async Task LeaveOrganizationAsync(Guid organizationId, CancellationToken cancellationToken)
    {
        var getUsersQuery = userRepository.GetQueryable();
        var volunteerEmail = GetUserEmailFromContext();
        var volunteer = await getUsersQuery.FirstOrDefaultAsync(u => u.Email == volunteerEmail, cancellationToken)
                       ?? throw new NotFoundException("Volunteer not found.");

        if(volunteer.OrganizationId == organizationId && volunteer.Role == Role.Volunteer)
        {
            volunteer.OrganizationId = null;
            userRepository.Update(volunteer);
        }
        else
        {
            throw new ForbiddenException("You are not a volunteer of this organization.");
        }
    }

    private string GetUserEmailFromContext()
    {
        const string firebaseEmailClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        return httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == firebaseEmailClaim)!.Value;
    }
}
