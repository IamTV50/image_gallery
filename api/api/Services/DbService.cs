using System.Runtime.InteropServices.JavaScript;
using api.Models;
using MySql.Data.MySqlClient;

namespace api.Services;

public class DbService {
	private readonly string DbConnectionString = string.Empty; 

	public DbService() {
		string? server = Environment.GetEnvironmentVariable("SERVER_URL");
		string? port = Environment.GetEnvironmentVariable("PORT");
		string? dbName = Environment.GetEnvironmentVariable("DATABASE");
		string? user = Environment.GetEnvironmentVariable("USER");
		string? password = Environment.GetEnvironmentVariable("PASSWORD");
		
		DbConnectionString = $"Server={server};Port={port};Database={dbName};User={user};Password={password};";
	}

	public List<Image>? getImages(int numberOfImages, int start) {
		var query = "SELECT * FROM images ORDER BY AddedDate LIMIT @numberOfImages OFFSET @start;";
		var returnList = new List<Image>();

		try {
			using MySqlConnection connection = new MySqlConnection(DbConnectionString);
			connection.Open();

			using MySqlCommand command = new MySqlCommand(query, connection);
			command.Parameters.AddWithValue("@numberOfImages", numberOfImages);
			command.Parameters.AddWithValue("@start", start);
			
			using MySqlDataReader reader = command.ExecuteReader();

			while (reader.Read()) {
				var image = new Image();

				image.ImageData = null;
				image.Id = reader["Id"].ToString();
				image.FilePath = reader["FilePath"].ToString();
				image.ImageSize = reader["ImageSize"].ToString();
				image.ImageName = reader["ImageName"].ToString();
				
				//convert 'Type' from string to ImageExtension
				if (Enum.TryParse<ImageExtension>(reader["Type"].ToString(), out var imgType)) {
					image.ImageType = imgType;
				}
				else {
					image.ImageType = ImageExtension.other;
				}

				returnList.Add(image);
			}
			
			reader.Close();
			connection.Close();
		}
		catch (Exception ex) {
			return null;
		}

		return returnList;
	}

	public int countImages() { 
		int imgCount = 0;
		var query = "SELECT COUNT(id) FROM images;";

		try {
			using MySqlConnection connection = new MySqlConnection(DbConnectionString);
			connection.Open();
			using MySqlCommand command = new MySqlCommand(query, connection);
			using MySqlDataReader reader = command.ExecuteReader();
			reader.Read();

			imgCount = reader.GetInt32("COUNT(id)");

			reader.Close();
			connection.Close();
		}
		catch (Exception ex) {
			return 0;
		}

		return imgCount;
	}
}
