import ListNews from "./ListNews";
import ListPhotos from "./ListPhotos";
import style from "./News.module.css";
import { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchNews, selectNews, selectNewsLoading, selectNewsError, fetchPhotos, selectPhotos, selectPhotosLoading } from '../../store/newsSlice';

export default function News() {
    const [state, setState] = useState('news');
    const dispatch = useDispatch();

    const news = useSelector(selectNews);
    const loadingNews = useSelector(selectNewsLoading);
    const loadingPhotos = useSelector(selectPhotosLoading);
    const error = useSelector(selectNewsError);
    const photos = useSelector(selectPhotos);

    useEffect(() => {
        dispatch(fetchNews());
        dispatch(fetchPhotos());
    }, [dispatch]);

    return (
        <div className={style.mainContainer}>
            <div className={style.containerButtons}>
                <button
                    className={`${style.button} ${state === 'news' ? style.active : ''}`}
                    onClick={() => setState('news')}
                >
                    Новости
                </button>
                <button
                    className={`${style.button} ${state === 'photos' ? style.active : ''}`}
                    onClick={() => setState('photos')}
                >
                    Фотографии
                </button>
            </div>
            <div>
                {state === 'news' ? (
                    loadingNews ? (
                        <p className={style.error}>Загрузка...</p>
                    ) : error ? (
                        <p className={style.error}>Произошла ошибка: {error}</p>
                    ) : (
                        <ListNews news={news} />
                    )
                ) : (
                    loadingPhotos ? (
                        <p className={style.error}>Загрузка...</p>
                    ) : error ? (
                        <p className={style.error}>Произошла ошибка: {error}</p>
                    ) : (
                        <ListPhotos photos={photos} />
                    )
                )}
            </div>
        </div>
    );
}
