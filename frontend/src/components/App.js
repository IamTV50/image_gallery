import { useState, useEffect } from "react";
import ImageList from "./ImageList";

function App() {
    const [maxNumOfImages, setMaxNumOfImages] = useState(0); //number of images in db
    const [images, setImages] = useState([]);
    const [fetchOffset, setFetchOffset] = useState(0)

    const fetchUrl = `http://localhost:2222/api/images?start=${fetchOffset}`;

    const incrementFetchOffset = () => {
        if(parseInt(fetchOffset) != images.length){
            console.log("increment failed:", fetchOffset, images.length)
            return;
        }
        setFetchOffset(parseInt(fetchOffset) + 6);
    } //number after 'parseInt(fetchOffset)' needs to be SAME as 'NumberOfImages' value in 'ImagesController.cs' (if not, some pictures may be skipped and not shown or shown multiple times)

    const fetchData = (url) => {
        fetch(url)
        .then((response) => response.json())
        .then((data) => {
            if(data.hasOwnProperty("numOfAllImages")){
                setImages(images.concat(data.images));
                setMaxNumOfImages(data.numOfAllImages);
            }
            else{
                setImages(images.concat(data));
            }
        })
        .catch((error) => {
            console.error("Fetch error:", error);
        })
    }

    //check if user has scrolled to bottom of the page
    const handleScroll = () => {
        const scrollableHeight = document.documentElement.scrollHeight - window.innerHeight;
        if (window.scrollY >= scrollableHeight){
            incrementFetchOffset();
        }
    };

    useEffect(() => {
        fetchData(fetchUrl);

        window.addEventListener("scroll", handleScroll);

        // Clean up the event listener when the component is unmounted
        return () => {
            window.removeEventListener("scroll", handleScroll);
        };
    },[fetchOffset]);

    return(
        <div>
            <h1>Image Gallery</h1>
            {images && <ImageList imagesData={images}/>}
        </div>
    );
}

export default App;
