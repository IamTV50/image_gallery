using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagesController: ControllerBase {
	private const int NumberOfImages = 6;
	
	[HttpGet]
	public ActionResult<string> GetImages([FromQuery] int start = 0) {
		var dbService = new DbService();
		var tmpImgList = new List<Image>();

		tmpImgList = dbService.getImages(NumberOfImages, start); //sanitization happens in DbService

		if (tmpImgList == null) {
			return Ok("{}");
		}
		
		foreach (var img in tmpImgList) {
			img.ImageData = ImageReader.ReadImageBytes(img.FilePath);
		}

		//clean up / remove data that don't need to be send to client
		var clientReadyImgList = tmpImgList.Select(img => new {
			dtoImg = new ClientImageDto(img.Id, $"{img.ImageName}.{img.ImageType}", img.ImageData)
		}).ToList();
		
		// will be used later for caching images on client
		Response.Headers.Append("Cache-Control", "public, max-age=3600"); // Cache for 1 hour

		//also return number of images in db
		if (start == 0) {
			var numOfImagesInDb = dbService.countImages();
			var combinedResponse = new {
				numOfAllImages = numOfImagesInDb,
				images = clientReadyImgList
			};
			
			return Ok(combinedResponse);
		}
		
		return Ok(clientReadyImgList);
	}
}