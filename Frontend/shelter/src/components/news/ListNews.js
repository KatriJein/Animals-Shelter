import style from "./ListNews.module.css"

export default function ListNews(props) {
    const { news } = props;

    if (!news || news.length === 0) {
        return <p className={style.error}>Новости не найдены :(</p>;
    }

    return (
        <ul className={style.ul}>
            {news.map((item) => (
                <li key={item.id} className={style.containerItem}>
                    <img src={item.mainImageSrc} alt={item.title} className={style.img} />
                    <div className={style.containerNew}>
                        <div className={style.containerText}>
                            <span className={style.tag}>Новые поступления</span>
                            <h2>{item.title}</h2>
                            <p className={style.description}>{item.description}</p>
                        </div>
                        <p className={style.date}>{item.lastUpdatedAt}</p>
                    </div>
                </li>
            ))}
        </ul>
    )
}