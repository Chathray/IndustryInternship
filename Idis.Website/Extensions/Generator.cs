using System.IO;

namespace Idis.Website
{
    public class Generator
    {
        public static void AutoClass(string parentPath, string nameSpace, string ext, string[] classList)
        {
            foreach (var className in classList)
            {
                var content = AutoTemplate.AutoClass(nameSpace, className);
                File.WriteAllText(parentPath + "\\" + className + ext, content);
            }
        }

        public static void AutoModel(string parentPath, string nameSpace, string ext, string[] classList)
        {
            foreach (var className in classList)
            {
                var content = AutoTemplate.AutoModel(nameSpace, className);
                File.WriteAllText(parentPath + "\\" + className + "Model" + ext, content);
            }
        }

        public static void AutoIRepository(string parentPath, string nameSpace, string ext, string[] classList)
        {
            foreach (var className in classList)
            {
                var content = AutoTemplate.AutoIRepository(nameSpace, className);
                File.WriteAllText(parentPath + "\\I" + className + "Repository" + ext, content);
            }
        }

        public static void AutoRepository(string parentPath, string nameSpace, string ext, string[] classList)
        {
            foreach (var className in classList)
            {
                var content = AutoTemplate.AutoRepository(nameSpace, className);
                File.WriteAllText(parentPath + "\\" + className + "Repository" + ext, content);
            }
        }

        public static void AutoIService(string parentPath, string nameSpace, string ext, string[] classList)
        {
            foreach (var className in classList)
            {
                var content = AutoTemplate.AutoIService(nameSpace, className);
                File.WriteAllText(parentPath + "\\I" + className + "Service" + ext, content);
            }
        }

        public static void AutoService(string parentPath, string nameSpace, string ext, string[] classList)
        {
            foreach (var className in classList)
            {
                var content = AutoTemplate.AutoService(nameSpace, className);
                File.WriteAllText(parentPath + "\\" + className + "Service" + ext, content);
            }
        }
    }
}
