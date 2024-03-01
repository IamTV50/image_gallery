import { useState, useEffect } from "react";
import ImageList from "./ImageList";

function App() {
    const [images, setImages] = useState(null);
    const [fetchOffset, setFetchOffset] = useState(0)
    const fetchUrl = `http://localhost:2222/api/images?start=${fetchOffset}`;

    const incrementFetchOffset = () => { setFetchOffset(parseInt(fetchUrl) + 3); } //number after 3 needs to be SAME as 'NumberOfImages' value in 'ImagesController.cs' (if not, some pictures may be skipped and not shown or shown multiple times)

    useEffect(() => {
        fetch(fetchUrl)
        .then((response) => response.json())
        .then((data) => {
            setImages(data);
        })
        .catch((error) => {
            console.error("Fetch error:", error);
        });

    },[fetchOffset]);

    return(
        <div>
            <h1>Image Gallery</h1>
            { images && <ImageList imagesData={images} /> }
        </div>
    );
}

export default App;
