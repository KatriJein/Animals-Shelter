import { useState } from "react";
import style from "./ListNews.module.css";

export default function ListPhotos(props) {
    const { photos } = props;

    const [modalData, setModalData] = useState({
        isOpen: false,
        imageSrc: "",
    });

    const openModal = (imageSrc) => {
        setModalData({ isOpen: true, imageSrc });
    };

    const closeModal = () => {
        setModalData({ isOpen: false, imageSrc: "" });
    };

    return (
        <>
            <ul className={style.listPhotos}>
                {photos.map((item, index) => (
                    <li key={index} className={style.containerPhoto}>
                        <img
                            src={item.link}
                            alt="фото"
                            onClick={() => openModal(item.link)}
                        />
                    </li>
                ))}
            </ul>

            {modalData.isOpen && (
                <div className={style.modalOverlay} onClick={closeModal}>
                    <div className={style.modalContent} onClick={(e) => e.stopPropagation()}>
                        <img src={modalData.imageSrc} alt="фото" className={style.modalImage} />
                        <button className={style.closeButton} onClick={closeModal}>
                            &times;
                        </button>
                    </div>
                </div>
            )}
        </>
    );
}
