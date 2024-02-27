namespace api.Models;

public class ClientImageDto { //DTO -> data transfer object (template of data class that exposes ONLY data we want to send to the client)
	public string Id { get; set; }
	public string ImageName { get; set; }
	public byte[]? ImageData { get; set; } //in case there was an error with converting image to bytes

	public ClientImageDto(string id = "", string imgName = "", byte[]? imgData = null) {
		Id = id;
		ImageName = imgName;
		if (imgData != null) {
			ImageData = imgData;
		}
		else {
			ImageData = Array.Empty<byte>();
		}
	}
}