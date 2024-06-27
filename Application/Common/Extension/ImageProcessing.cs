
using Microsoft.AspNetCore.Http;

namespace Application.Common.Extension
{
    public static class ImageProcessing
    {
        public static string UploadImage(this IFormFile file, string folder, string? defualt = null)
        {
            if (file is not null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Gallery", folder);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string filePath = Path.Combine(folderPath, fileName);
                var image = Image.Load(file.OpenReadStream());
                var save = image.SaveAsWebpAsync(filePath);
                if (save.IsCompleted)
                {
                    return fileName;
                }
            }
            if (!string.IsNullOrEmpty(defualt))
            {
                return defualt;
            }
            return "notFound.jpg";
        }
        public static Result RemoveImage(this string fileName, string folder, string? defualt = null)
        {
            if (fileName == "defaultAvatar.jpg" || fileName == "notFound.jpg")
            {
                return Result.Success();
            }
            if (!string.IsNullOrEmpty(defualt) && fileName == defualt)
            {
                return Result.Success();
            }

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "Gallery", folder);

            string path = Path.Combine(folderPath, fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
                return Result.Success();
            }
            else
            {
                return Result.Fail(FailMessage.ImagePathNotFound);
            }
        }
    }
}
