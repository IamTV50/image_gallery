import ImageCard from "./ImageCard";

function ImageList({ imagesData }){
	return(
		<div className="ImagesGrid">{
			imagesData.map(data => {
				//convert byte array to data url
				const imgFromBytesArray = `data:image/jpeg;base64,${data.dtoImg.imageData}`;
				const imageFullName = data.dtoImg.imageName;
				const imgExtension = (imageFullName.substring(imageFullName.length - 3, imageFullName.length))

				return(
					<ImageCard imageName={imageFullName}
							   imageDataUrl={imgFromBytesArray}
							   visible={1}
							   key={data.dtoImg.id}/> //key is NOT ImageCard prop. It's here so browser doesn't log error for missing unique key inside .map return
				)
			})
		}</div>
	);
}

export default ImageList;