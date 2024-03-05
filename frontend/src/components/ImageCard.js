function ImageCard({ imageName, imageDataUrl, visible }){
	const cardClasses = visible ? "ImageCard" : "ImageCard hidden";

	return(
		<div className={cardClasses}>
			<img alt={imageName} src={imageDataUrl}/>
		</div>
	);
}

export default ImageCard;