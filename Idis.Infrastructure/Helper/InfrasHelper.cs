using System.IO;

namespace Idis.Infrastructure.Helper
{
    public static class InfrasHelper
    {
        public static byte[] ConvertStreamToBytes(this Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
