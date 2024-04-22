using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace School.Infrastructure.Services
{
	public interface ICloudinaryService
	{
		Task<ImageUploadResult> AddPhoto(IFormFile file);
		Task<DeletionResult> DeletePhoto(string publicId);
	}
}
