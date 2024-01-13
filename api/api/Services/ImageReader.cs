namespace api.Services;

public static class ImageReader {
	public static byte[] ReadImageBytes(string? filePath) {
		if (string.IsNullOrEmpty(filePath)) {
			throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
		}

		try {
			// Read the image bytes from the file
			return File.ReadAllBytes(filePath);
		}
		catch (Exception ex) {
			throw new Exception("Error reading image file.", ex);
		}
	}
}
