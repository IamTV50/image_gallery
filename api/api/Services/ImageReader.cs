namespace api.Services;

public static class ImageReader {
	public static byte[]? ReadImageBytes(string? filePath) {
		if (string.IsNullOrEmpty(filePath)) {
			return null;
		}

		try {
			// Read the image bytes from the file
			return File.ReadAllBytes(filePath);
		}
		catch (Exception ex) {
			return null;
		}
	}
}
