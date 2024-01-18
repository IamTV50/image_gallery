import os
import mysql.connector # pip install mysql-connector-python 
import uuid
from datetime import datetime

IMAGES_FOLDER = '' #absolute path to folder containing the images
SQL_URL = 'localhost'
SQL_PORT = '3306'
SQL_UNAME = ''
SQL_PASSWD = ''
SQL_DB_NAME = 'image_gallery'
SQL_TABLE_NAME = 'images'

show_tables_query = f"SHOW TABLES LIKE '{SQL_TABLE_NAME}'"
create_table_query = f"CREATE TABLE {SQL_TABLE_NAME} (Id varchar(36) PRIMARY KEY, FilePath varchar(255) NOT NULL, ImageSize Int NOT NULL, ImageName varchar(70) NOT NULL, Type varchar(4) NOT NULL, AddedDate DATETIME NOT NULL);"

def getImageDetails(image):
	imageFullPath = f'{IMAGES_FOLDER}\\{image}'
	data = {
		'name': image.split('.')[0], 
		'fullPath': imageFullPath, 
		'size': os.stat(imageFullPath).st_size,
		'extension': image.split('.')[-1]
	}

	return(data)
	
def fillTable(db, cursor):
	for img in os.listdir(IMAGES_FOLDER):
		try:
			imgData = getImageDetails(img)
			id = str(uuid.uuid4())
			current_datetime = datetime.now()
			formatted_datetime = current_datetime.strftime("%Y-%m-%d %H:%M:%S") # Format the date and time as "dd/MM/yyyy-hh:mm"
			
			query = "INSERT INTO {} (Id, FilePath, ImageSize, ImageName, Type, AddedDate) VALUES (%s, %s, %s, %s, %s, %s)".format(SQL_TABLE_NAME)
			values = (id, imgData['fullPath'], imgData['size'], imgData['name'], imgData['extension'], formatted_datetime)
			cursor.execute(query, values)
			db.commit()

			print(f'"{img}" data was successfully written to db')
		except mysql.connector.Error as err:
			print(f"error: {err}")

def main():
	db = mysql.connector.connect(
		host = SQL_URL,
		user = SQL_UNAME,
		password = SQL_PASSWD,
		database = SQL_DB_NAME
	)

	cursor = db.cursor() # Creating an instance of 'cursor' class which is used to execute the 'SQL' statements in 'Python'

	cursor.execute(show_tables_query)
	result = cursor.fetchone()
	
	# table 'SQL_TABLE_NAME' does not exist
	if(result == None):
		try:
			cursor.execute(create_table_query)
			db.commit()

			print(f'table {SQL_TABLE_NAME} created successfully')
		except mysql.connector.Error as err:
			print(f"error: {err}")

	fillTable(db, cursor)
		
	cursor.close()
	db.close()

main()
