import { useState } from "react";
import style from "./Gallery.module.css";
import arrowLeft from "../../img/arrow_left.svg";
import arrowRight from "../../img/arrow_right.svg";

const imagesPerRow = 4;

export default function Gallery({ mainImageSrc, imagesSrc }) {
    const [activeImage, setActiveImage] = useState(mainImageSrc);
    const [startIndex, setStartIndex] = useState(0);
    const [isModalOpen, setIsModalOpen] = useState(false);
    
    const images = [mainImageSrc, ...imagesSrc];
    const visibleThumbnails = images.slice(startIndex, startIndex + 4);
    const hasNext = startIndex + imagesPerRow < images.length;

    const handleImageClick = (src) => setActiveImage(src);

    const handleNext = () => {
        if (startIndex + 4 < images.length) {
            setStartIndex(startIndex + 1);
        }
    };

    const handlePrev = () => {
        if (startIndex > 0) {
            setStartIndex(startIndex - 1);
        }
    };

    const openModal = () => setIsModalOpen(true);
    const closeModal = () => setIsModalOpen(false);

    return (
        <div className={style.mainContainer}>
            <img
                src={activeImage}
                alt="Главное фото"
                className={style.mainImg}
                onClick={openModal}
            />

            <div className={style.sliderContainer}>
                <button
                    onClick={handlePrev}
                    className={`${style.navButton} ${startIndex === 0 ? style.hidden : ''}`}>
                    <img src={arrowLeft} alt="Влево" />
                </button>

                <div className={style.containerImgs}>
                    {visibleThumbnails.map((src, index) => (
                        <img
                            key={index}
                            src={src}
                            alt={`Миниатюра ${index}`}
                            onClick={() => handleImageClick(src)}
                            className={`${style.img} ${src === activeImage ? style.active : ""}`}
                        />
                    ))}
                </div>

                <button
                    onClick={handleNext}
                    className={`${style.navButton} ${!hasNext ? style.hidden : ''}`}>
                    <img src={arrowRight} alt="Вправо" />
                </button>
            </div>

            {isModalOpen && (
                <div className={style.modal} onClick={closeModal}>
                    <img src={activeImage} alt="Полный размер фотографии" className={style.modalImage} />
                </div>
            )}
        </div>
    );
}
