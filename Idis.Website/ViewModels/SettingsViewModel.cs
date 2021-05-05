using Idis.Application;
using System.Collections.Generic;

namespace Idis.Website
{
    public class SettingsViewModel
    {
        public IList<DepartmentModel> Departments { get; set; }

        public string HeaderPhoto { get; set; }
        public string Avatar { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int Department { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string Role { get; set; }


        public string ComeFrom { get; set; }      //  
        public string JoinedDate { get; set; }
        public bool AvatarVisibility { get; set; }
    }
}
