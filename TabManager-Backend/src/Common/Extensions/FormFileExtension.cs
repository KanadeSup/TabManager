using Microsoft.AspNetCore.Http;

namespace Common.Extensions
{
   public static class FormFileExtension
   {
      public static async Task<byte[]> GetBytes(this IFormFile formFile)
      {
         using var memoryStream = new MemoryStream();
         await formFile.CopyToAsync(memoryStream);
         return memoryStream.ToArray();
      }
   }
}