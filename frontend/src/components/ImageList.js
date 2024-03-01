function ImageList({ imagesData }){
	return(
		<div className="ImagesGrid">{
			imagesData.map(data => {
				//convert byte array to data url
				const imgFromBytesArray = `data:image/jpeg;base64,${data.dtoImg.imageData}`;

				return(
					<div className="ImageCard" key={data.dtoImg.id}>
						<img alt={data.dtoImg.imageName} src={imgFromBytesArray} />
						<p className="Image">{data.dtoImg.imageName}</p>
					</div>

				)
			})
		}</div>
	);
}

export default ImageList;