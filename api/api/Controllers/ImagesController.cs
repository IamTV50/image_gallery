﻿using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagesController: ControllerBase {
	[HttpGet]
	public ActionResult<string> GetImages() {
		var dbService = new DbService();
		var tmpImgList = new List<Image>();

		tmpImgList = dbService.getImages();

		if (tmpImgList == null) {
			return Ok("{}");
		}
		
		foreach (var img in tmpImgList) {
			try {
				img.ImageData = ImageReader.ReadImageBytes(img.FilePath);
			}
			catch (Exception ex) {
				// TODO - log the error
			}
		}

		//clean up / remove data that don't need to be send to client
		var clientReadyImgList = tmpImgList.Select(img => new {
			dtoImg = new ClientImageDto(img.Id, img.ImageName, img.ImageData)
		}).ToList();
		
		// will be used later for caching images on client
		Response.Headers.Append("Cache-Control", "public, max-age=3600"); // Cache for 1 hour
		
		return Ok(clientReadyImgList);
	}
}