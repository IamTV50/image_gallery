import { useState, useEffect } from "react";
import ImageList from "./ImageList";

function App() {
    const [maxNumOfImages, setMaxNumOfImages] = useState(0); //number of images in db
    const [images, setImages] = useState([]);
    const [fetchOffset, setFetchOffset] = useState(0)
    const [loadingMoreImages, setLoadingMoreImages] = useState(false)

    const fetchUrl = `http://localhost:2222/api/images?start=${fetchOffset}`;

    const incrementFetchOffset = () => {
        if(parseInt(fetchOffset) != images.length){
            return;
        }
        setFetchOffset(parseInt(fetchOffset) + 6);
    } //number after 'parseInt(fetchOffset)' needs to be SAME as 'NumberOfImages' value in 'ImagesController.cs' (if not, some pictures may be skipped and not shown or shown multiple times)

    const fetchData = (url) => {
        setLoadingMoreImages(true);

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

            setLoadingMoreImages(false)
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

    const extendPageDiv = (maxNumOfImages > images.length) ? "" : "hidden"; //adds extra space at the bottom that allows user to scroll and trigger 'handleScroll' function

    return(
        <div>
            <h1>Image Gallery</h1>
            { images && <ImageList imagesData={images}/> }
            { loadingMoreImages ? <span id="loadigMsg" aria-busy="true">loading more images...</span> : "" }
            <div id="forceExtendPage" className={extendPageDiv}></div>
        </div>
    );
}

export default App;
