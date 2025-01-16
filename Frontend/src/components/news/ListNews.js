import style from "./ListNews.module.css";
import { findTag } from '../../utils/animalInfo';
import { formatDate } from "../../utils/utils";
import { useState } from "react";

export default function ListNews(props) {
    const { news } = props;

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

    if (!news || news.length === 0) {
        return <p className={style.error}>Новости не найдены :(</p>;
    }

    return (<>
        <ul className={style.ul}>
            {news.map((item) => (
                <li key={item.id} className={style.containerItem}>
                    <img src={item.mainImageSrc} alt={item.title} className={style.img} onClick={() => openModal(item.mainImageSrc)} />
                    <div className={style.containerNew}>
                        <div className={style.containerText}>
                            <span className={style.tag} style={{ backgroundColor: findTag(item.tag).color }}>{findTag(item.tag).title}</span>
                            <h2 className={style.title}>{item.title}</h2>
                            <p className={style.description}>{item.description}</p>
                        </div>
                        <p className={style.date}>{formatDate(item.lastUpdatedAt)}</p>
                    </div>
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
    )
}