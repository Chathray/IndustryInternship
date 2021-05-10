using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Idis.WebApi
{
    public class ModelSample
    {
        public static RegisterRequest RegisterRequest => new()
        {
            FirstName = "Thach",
            LastName = "Jelly",
            Email = "Thach@admin",
            Password = "zxzxzx",
        };

        public static PasswordUpdateRequest PasswordUpdateRequest => new()
        {
            CurrentPassword = "zazaza",
            NewPassword = "zxzxzx",
        };

        public static AuthenticationRequest AuthenticationRequest => new()
        {
            Email = "admin@x",
            Password = "zazaza",
            Remember = true,
        };

        public static CreateEventRequest EventRequest => new()
        {
            CreatedBy = 1,
            GestsField = null,
            Title = "Leo núi cuối tuần",
            Deadline = "10/05/2021 - 11/05/2021",
            RepeatField = "weekdays",
            Type = "fullcalendar-custom-event-holidays",
            EventLocationLabel = "Núi Vũng Chua",
            EventDescriptionLabel = "Xin hãy đi đúng giờ",
            Image = "/img/_event.svg"
        };

        public static CreateInternRequest CreateInternRequest => new()
        {
            FirstName = "Người",
            LastName = "Máy",
            Email = "anna@chatbot",
            Phone = "094315544",
            DateOfBirth = "20/07/1998",
            Gender = "Unspecified",
            Address1 = "Thôn 11, Xã Eakhal, Eahleo, ĐakLak",
            Address2 = "94, Cần Vương, Quy Nhơn, Bình Định",
            DepartmentId = 1,
            OrganizationId = 1,
            MentorId = 1,
            TrainingId = 1,
            Type = "Full time",
            Duration = "22/02/2021 - 16/05/2021",
            Avatar = null,
            UpdatedBy = 1
        };


        public static CreateQuestionRequest CreateQuestionRequest => new()
        {
            QuestionId = 1,
            Group = "Recruit",
            InData = "TMA là viết tắt của từ gì?",
            OutData = "Thân Mật Á!"
        };


        public static Application.TrainingModel TrainingModel => new()
        {
           TrainingId = 100,
           CreatedBy = 2,
           TraData = "{'ops':[{'insert':'s\n'}]}",
           TraName = "ASP.NET Core 5.0 MVC"
        };
    }
}