
// using Microsoft.AspNetCore.Http;

// namespace Business.Helpers
// {
//     public static class FileHelper
//     {
//         private static readonly string AssetsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Images");

//         /// <summary>
//         /// Saves an uploaded file into /assets/images/{userId}/{issueId}/ and returns the relative path.
//         /// </summary>
//         public static async Task<string> SaveFileAsync(IFormFile file, int userId, int issueId)
//         {
//             if (file == null || file.Length == 0)
//                 throw new ArgumentException("File is empty or null.", nameof(file));

//             string userIssueFolder = Path.Combine(AssetsFolder, userId.ToString(), issueId.ToString());

//             if (!Directory.Exists(userIssueFolder))
//                 Directory.CreateDirectory(userIssueFolder);

//             string fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
//             string filePath = Path.Combine(userIssueFolder, fileName);

//             await using (var stream = new FileStream(filePath, FileMode.Create))
//             {
//                 await file.CopyToAsync(stream);
//             }

//             return $"/Assets/Images/{userId}/{issueId}/{fileName}";
//         }
//     }
// }


using Microsoft.AspNetCore.Http;

namespace Business.Helpers
{
    public static class FileHelper
    {
        private static readonly string AssetsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Images");

        /// <summary>
        /// Saves an uploaded file into /Assets/Images/{yyyy-MM-dd}/ and returns the relative path.
        /// </summary>
        public static async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty or null.", nameof(file));

            // Create folder with current date
            string dateFolder = DateTime.UtcNow.ToString("yyyy-MM-dd");
            string saveFolder = Path.Combine(AssetsFolder, dateFolder);

            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            // Use GUID as filename, keep original extension
            string fileExtension = Path.GetExtension(file.FileName); // .jpg, .png, etc.
            string fileName = $"{Guid.NewGuid()}{fileExtension}";

            string filePath = Path.Combine(saveFolder, fileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return relative path for DB
            return $"/Assets/Images/{dateFolder}/{fileName}";
        }
    }
}
