namespace Idis.Infrastructure
{
    public class OrganizationRepository : RepositoryBase<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(DataContext context) : base(context)
        { }
    }
}
