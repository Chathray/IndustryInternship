using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Idis.Infrastructure
{
    public class InternRepository : RepositoryBase<Intern>, IInternRepository
    {
        public InternRepository(DataContext context) : base(context)
        { }

        public dynamic GetInternInfo(int id)
        {
            //var result = _context.Interns
            //    .Where(intern => intern.InternId == id)
            //    .Select(intern => new
            //    {
            //        internid = intern.InternId,
            //        email = intern.Email,
            //        lastname = intern.LastName,
            //        firstname = intern.FirstName,
            //        gender = intern.Gender,
            //        birth = intern.DateOfBirth,
            //        phone = intern.Phone,
            //        type = intern.Type,
            //        duration = intern.Duration,
            //        organizationid = intern.OrganizationId,
            //        departmentid = intern.DepartmentId,
            //        trainingid = intern.TrainingId,
            //        avatar = intern.Avatar
            //    }).First();

            var result = _context.Database.GetDbConnection()
                .ExecuteScalar($"CALL GetInternInfo('{id}')");

            return result;
        }


        public dynamic GetInternDetail(int internId)
        {
            //var intern = GetOne(internId);

            //_context.Entry(intern).Reference(_ => _.Training).Load();
            //_context.Entry(intern).Reference(_ => _.Department).Load();
            //_context.Entry(intern).Reference(_ => _.Organization).Load();
            //_context.Entry(intern).Reference(_ => _.Mentor).Load();

            //var result = new
            //{
            //    internid = intern.InternId,
            //    email = intern.Email,
            //    fullname = $"{intern.FirstName} {intern.LastName}",
            //    gender = intern.Gender,
            //    birth = intern.DateOfBirth,
            //    phone = intern.Phone,
            //    address1 = intern.Address1,
            //    address2 = intern.Address2,
            //    type = intern.Type,
            //    duration = intern.Duration,
            //    organization = intern.Organization?.OrgName,
            //    department = intern.Department?.DepName,
            //    training = intern.Training?.TraName,
            //    mentor = $"{intern.Mentor?.FirstName} {intern.Mentor?.LastName}",
            //    joindate = intern.CreatedDate
            //};

            var result = _context.Database.GetDbConnection()
                .ExecuteScalar($"CALL GetInternDetail('{internId}')");

            return result;
        }


        public DataSet GetInternModelList(int page, int size, int sort, int search_on, string search_string)
        {
            string search_on_p = search_on switch
            {
                9 => $"MATCH (t1.firstname, t1.lastname, t1.email, t1.phone)" +
                     $"AGAINST('{search_string}' IN NATURAL LANGUAGE MODE)",

                1 => $"t1.InternId LIKE '{search_string}'",
                2 => $"t1.Email LIKE '{search_string}'",
                3 => $"t1.FirstName LIKE '{search_string}'",
                4 => $"t1.Phone LIKE '{search_string}'",
                5 => $"DepName LIKE '{search_string}'",
                6 => $"OrgName LIKE '{search_string}'",
                7 => $"TraName LIKE '{search_string}'",
                8 => $"t5.FirstName LIKE '{search_string}'",
                _ => "1",
            };

            string sort_p = sort switch
            {
                2 => "t1.Email",
                3 => "t1.FirstName",
                4 => "t1.Phone",
                5 => "DepName",
                6 => "OrgName",
                7 => "TraName",
                8 => "t5.FirstName",
                _ => "t1.InternId",
            };

            string p_type = $"{search_on_p} ORDER BY {sort_p} LIMIT {(page - 1) * size},{size}";

            return _context.Database.GetDbConnection()
                .ExecReaders($"CALL GetInternList({"\""}{p_type}{"\""})");
        }
        public DataSet GetInternModelList(int page, int size, int sort, int search_on, string search_string, int on_passed, int date_filter, string start_date, string end_date)
        {
            return _context.Database.GetDbConnection()
                .ExecReaders($"CALL GetInternListWithFilter({on_passed},{date_filter},'{start_date}','{end_date}',{(page - 1) * size},{size},{sort},{search_on},'{search_string}')");
        }

        public IList<Intern> GetInternByPage(int page, int size)
        {
            return _context.Interns
                .AsNoTracking()
                .Include(b => b.Organization)
                .Include(b => b.Department)
                .Include(b => b.Editor)
                .Include(b => b.Mentor)
                .Include(b => b.Training)
                .OrderBy(i => i.InternId)
                .ThenBy(i => i.FirstName)
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();
        }

        public IList<Training> GetJointTrainings(int internId)
        {
            List<Training> result = new();

            var list = _context.Database.GetDbConnection()
                 .ExecuteScalar($"CALL GetJointTrainings({internId})");

            if (list is not null)
            {
                string[] splited = list.ToString().Split(',');

                var list_id = splited.Distinct().AsList();

                foreach (var training_id in list_id)
                {
                    if (training_id == "" || training_id == "0") continue;
                    var step = _context.Trainings.Find(int.Parse(training_id));
                    result.Add(step);
                }
            }
            return result;
        }

        public dynamic GetWhitelist()
        {
            //var list = _context.Database.GetDbConnection()
            //     .ExecuteScalar("CALL GetWhitelist()").ToString();

            var list = _context.Interns
                .Select(intern => new
                {
                    iid = intern.InternId,
                    src = $"/img/avatar/{intern.Avatar}",
                    value = $"{intern.FirstName} {intern.LastName}"
                }).ToList();

            return list;
        }

        public Training GetTraining(int internId)
        {
            //var result = _context.Interns
            //    .Include(intern => intern.Training)
            //    .SingleOrDefault(i=>i.InternId == internId)
            //    .Training;

            var intern = GetOne(internId);
            _context.Entry(intern).Reference(_ => _.Training).Load();
            return intern.Training;
        }
    }
}
