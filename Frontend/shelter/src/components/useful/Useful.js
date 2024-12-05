import UsefulButton from './UsefulButton';
import style from './UsefulPage.module.css';
import UsefulArticle from './UsefulArticle';
import search from '../../img/search.svg';
import { categories } from '../../utils/animalInfo';

export default function Useful(props) {
    const { popularArticles, onSearch, onCategoryClick } = props;

    const handleSearchSubmit = (event) => {
        event.preventDefault();
        const query = event.target.searchInput.value.trim();
        if (query) {
            onSearch(query);
        }
    };

    return (
        <main className={style.mainContainer}>
            <div className={style.containerSearch}>
                <h2 className={style.h2}>Что Вас интересует?</h2>
                <form className={style.searchContainer} role="search" onSubmit={handleSearchSubmit}>
                    <div className={style.inputWrapper}>
                        <img loading="lazy" src={search} className={style.searchIcon} alt="" />
                        <input
                            id="searchInput"
                            type="search"
                            className={style.searchInput}
                            placeholder="Введите запрос..."
                            aria-label="Search query"
                        />
                    </div>
                    <button type="submit" className={style.searchButton}>
                        Найти
                    </button>
                </form>
                <p className={style.text}>или выберите подходящую категорию</p>
            </div>

            <div className={style.containerCategories}>
                {categories.map((category) => (
                    <UsefulButton
                        key={category.text}
                        text={category.text}
                        icon={category.icon}
                        color={category.color}
                        onClick={() => onCategoryClick(category.category)}
                    />
                ))}
            </div>

            <div className={style.containerPopularArticles}>
                <h2 className={style.h2}>Популярные статьи</h2>
                <div className={style.containerArticles}>
                    {popularArticles.map((article) => (
                        <UsefulArticle key={article.id} heading={article.title} text={article.description} />
                    ))}
                </div>
            </div>
        </main>
    );
}
