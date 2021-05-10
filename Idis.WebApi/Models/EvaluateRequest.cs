using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Idis.WebApi
{
    public class EvaluateRequest
    {
        public int InternId { get; set; }
        public float TechnicalSkill { get; set; }
        public float SoftSkill { get; set; }
        public float Attitude { get; set; }
    }
}
