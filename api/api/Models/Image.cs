namespace api.Models;

public class Image {
	public string Id { get; set; }
	public string? FilePath { get; set; }
	public ImageExtension ImageType { get; set; } //only type, ex.: jpg
	public string? ImageSize { get; set; }
	public string? ImageName { get; set; } //full name, ex.: dog.jpg
	public byte[]? ImageData { get; set; } //image converted to byte[]
}