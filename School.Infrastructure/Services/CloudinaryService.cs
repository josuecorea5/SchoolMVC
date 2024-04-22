using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace School.Infrastructure.Services
{
	public class CloudinaryService : ICloudinaryService
	{
		private readonly Cloudinary _cloudinary;
        public CloudinaryService(IConfiguration configuration)
        {
			var acc = new Account(
					configuration["Cloudinary:CloudName"],
					configuration["Cloudinary:ApiKey"],
					configuration["Cloudinary:ApiSecret"]
				);

			_cloudinary = new Cloudinary(acc);
        }
        public async Task<ImageUploadResult> AddPhoto(IFormFile file)
		{
			var uploadResult = new ImageUploadResult();

			if(file.Length > 0)
			{
				using var stream = file.OpenReadStream();
				var fileName = file.Name + DateTime.Now.ToString();
				var uploadParams = new ImageUploadParams
				{
					File = new FileDescription(fileName, stream),
				};

				uploadResult = await _cloudinary.UploadAsync(uploadParams);
			}

			return uploadResult;
		}

		public Task<DeletionResult> DeletePhoto(string image)
		{
			var fi = new FileInfo(image);
			var publicId = Path.GetFileNameWithoutExtension(fi.Name);
			var deleteParams = new DeletionParams(publicId);
			var result = _cloudinary.DestroyAsync(deleteParams);

			return result;
		}
	}
}
