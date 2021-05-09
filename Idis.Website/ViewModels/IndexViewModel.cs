using System;
using System.Data;
using System.IO;

namespace Idis.Website
{
    public class IndexViewModel
    {
        public PaginationLogic Pager { get; set; }
        public DataSet Interns { get; set; }

        public IndexViewModel() { }
        public IndexViewModel(PaginationLogic pager, DataSet interns)
        {
            Pager = pager;
            Interns = interns;
        }

        #region Intern Property

        public string Avatar { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Duration { get; set; }
        public string Type { get; set; }
        public int DepartmentId { get; set; }
        public int OrganizationId { get; set; }
        public int TrainingId { get; set; }

        #endregion End Intern Property

        public string Uptime { get; internal set; }


        public string CheckPageActive(int page)
        {
            return page == Pager.CurrentPage ? "active" : "";
        }

        public string GetAvatarHtml(DataRow X)
        {
            var img_src = "/img/avatar/" + X["Avatar"];
            var tooltip = X["InternId"] + ": " + X["CreatedDate"];

            if (File.Exists(Environment.CurrentDirectory + "/wwwroot" + img_src))
            {
                return @$"<div class='avatar avatar-sm avatar-circle' data-toggle='tooltip' data-placement='top' title='{tooltip}'>
                            <img class='avatar-img' src='{img_src}' alt='Image Description'>
                         </div>";
            }
            else
            {
                return @$"<div class='avatar avatar-sm avatar-circle avatar-soft-dark' data-toggle='tooltip' data-placement='top' title='{tooltip}'>
                            <span class='avatar-initials'>{X["FullName"].ToString()[0]}</span>
                         </div>";
            }
        }
    }
}