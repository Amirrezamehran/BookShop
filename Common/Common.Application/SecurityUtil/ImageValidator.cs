using Microsoft.AspNetCore.Http;
using System.Drawing;
//using static System.Net.Mime.MediaTypeNames;

namespace Common.Application.SecurityUtil
{
 
    public static class ImageValidator
    {
        public static bool IsImage(this IFormFile? file)
        {
            if (file == null) return false;
            try
            {
                var img = Image.FromStream(file.OpenReadStream());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}