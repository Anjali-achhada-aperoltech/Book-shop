using Microsoft.AspNetCore.Http;

namespace Book.Reposittiory
{
    public class UtilityRepo
    {
   //     private IWebHostEnviroment _env;
   //     private IHttpContextAccessor _contextAccessor;

   //     public UtilityRepo(IHttpContextAccessor contextAccessor, IWebHostEnvironment env)
   //     {
   //         _contextAccessor = contextAccessor;
   //         _env = env;
   //     }

   //     public Task DeleteImage(string ContainerName, string dbPath)
   //     {
   //         if (string.IsNullOrEmpty(dbPath))
   //         {
   //             return Task.CompletedTask;
   //         }
   //         var filename = Path.GetFileName(dbPath);
   //         var CompletePath = Path.Combine(_env.WebRootPath, ContainerName, filename);
   //         if (File.Exists(CompletePath))
   //         {
   //             File.Delete(CompletePath);
   //         }
   //         return Task.CompletedTask;
   //     }

   //     public async Task<string> EditImage(string ContainerName, IFormFile file, string dbPath)
   //     {
   //         await DeleteImage(ContainerName, dbPath);
   //         return await SaveImage(ContainerName, file);
   //     }
   //     //https://localhost:7271/containername/


   //     public async Task<string> SaveImage(string ContainerName, IFormFile file)
   //     {
   //         var extension = Path.GetExtension(file.FileName);
   //         var filename = $"{Guid.NewGuid()}{extension}";
   //         string folder = Path.Combine(_env.WebRootPath, ContainerName);

   //         if (!Directory.Exists(folder))
   //         {
   //             Directory.CreateDirectory(folder)
   //;
   //         }
   //         string filepath = Path.Combine(folder, filename);
   //         using (var Memorystreem = new MemoryStream())
   //         {
   //             await file.CopyToAsync(Memorystreem);
   //             var content = Memorystreem.ToArray();
   //             await File.WriteAllBytesAsync(filepath, content);
   //         }

   //         var basePath = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}";

   //         var CompletePath = Path.Combine(basePath, ContainerName, filename).Replace('\\', '/');

   //         return CompletePath;
   //     }
    }
}
