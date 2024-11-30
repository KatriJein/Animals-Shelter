import UsefulButton from './UsefulButton';
import style from './UsefulPage.module.css';
import UsefulArticle from './UsefulArticle';
import search from '../../img/search.svg';
import feedingIcon from '../../img/feeding.svg';
import healthIcon from '../../img/health.svg';
import careIcon from '../../img/care.svg';
import behaviourIcon from '../../img/behaviour.svg';
import trainingIcon from '../../img/training.svg';

const categories = [{ text: 'Кормление', icon: feedingIcon, color: '#9498E0' }, { text: 'Дрессировка', icon: trainingIcon, color: '#986C73' }, { text: 'Здоровье', icon: healthIcon, color: '#505B86' }, { text: 'Уход', icon: careIcon, color: '#CC969D' }, { text: 'Поведение', icon: behaviourIcon, color: '#8E4A64' }];

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
                        onClick={() => onCategoryClick(category.text)}
                    />
                ))}
            </div>

            <div className={style.containerPopularArticles}>
                <h2 className={style.h2}>Популярные статьи</h2>
                <div className={style.containerArticles}>
                    {popularArticles.map((article) => (
                        <UsefulArticle key={article.id} heading={article.heading} text={article.text} />
                    ))}
                </div>
            </div>
        </main>
    );
}
