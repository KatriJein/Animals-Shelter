import style from './ArticlesSearch.module.css';
import UsefulArticle from './UsefulArticle';
import ButtonBack from '../ButtonBack';

function ArticlesSearch(props) {
    const { articles, isSearch, heading, onBack } = props;

    return (
        <div className={style.mainContainer}>
            <ButtonBack className={style.buttonBack} onClick={onBack} />
            <div className={style.container}>
                <h2>Статьи по {isSearch ? "запросу" : "теме"} «{heading}»</h2>
                <div className={style.containerArticles}>
                    {articles.length > 0 ? (
                        articles.map((article) => (
                            <UsefulArticle key={article.id} heading={article.heading} text={article.text} />
                        ))
                    ) : (
                        <p className={style.notFound}>Статьи не найдены :(</p>
                    )}
                </div>
            </div>
        </div>
    );
}

export default ArticlesSearch;
