using Idis.Infrastructure;

namespace Idis.Application
{
    public class OrganizationService : ServiceBase<OrganizationModel, Organization>, IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepo;
        public OrganizationService(IOrganizationRepository organizationRepo) : base(organizationRepo)
        {
            _organizationRepo = organizationRepo;
        }


    }
}
