using System;
using System.Linq;

namespace Idis.Website
{
    public static class AutoTemplate
    {
        public static string AutoClass(string nameSpace, string className)
        {
            return $@"
            namespace {nameSpace
            }{{
                public class {className}
                {{
                    
                }}
            }}".AutoTrim();
        }

        public static string AutoModel(string nameSpace, string className)
        {
            return $@"
            namespace {nameSpace}
            {{
                public class {className}Model : : EntityBase
                {{
                    
                }}
            }}".AutoTrim();
        }

        public static string AutoRepository(string nameSpace, string className)
        {
            return $@"
            namespace {nameSpace}
            {{
                public class {className}Repository : RepositoryBase<{className}>, I{className}Repository
                {{
                    private readonly DataContext _context;
        
                    public {className}Repository(DataContext context) : base(context)
                    {{
                        _context = context;
                    }}
                }}
            }}".AutoTrim();
        }

        public static string AutoIRepository(string nameSpace, string className)
        {
            return $@"
            namespace {nameSpace}
            {{
                public interface I{className}Repository : IRepositoryBase<{className}>
                {{
        
                }}
            }}".AutoTrim();
        }

        public static string AutoService(string nameSpace, string className)
        {
            var fitArray = className.ToCharArray();
            fitArray[0] = char.ToLower(fitArray[0]);
            var lowerName = string.Join("", fitArray);

            return $@"
            namespace {nameSpace}
            {{
                public class {className}Service : ServiceBase<{className}Model, {className}>, I{className}Service
                {{
                    private readonly I{className}Repository _{lowerName}Repo;
                    public {className}Service(I{className}Repository {lowerName}Repo) : base({lowerName}Repo)
                    {{
                        _{lowerName}Repo = {lowerName}Repo;
                    }}
                }}
            }}".AutoTrim();
        }

        public static string AutoIService(string nameSpace, string className)
        {
            return $@"
            namespace {nameSpace}
            {{
                public interface I{className}Service : IServiceBase<{className}Model, {className}>
                {{
        
                }}
            }}".AutoTrim();
        }

        public static string AutoTrim(this string code)
        {
            string newline = Environment.NewLine;
            string[] line_array = code.Split(newline);

            var trimLen = line_array
                .Skip(1)
                .Min(s => s.Length - s.TrimStart().Length);

            return string.Join(newline, line_array
                .Select(line => line[Math.Min(line.Length, trimLen)..]));
        }
    }
}
