import styles from './Filter.module.css';
import FilterComponent from './FilterComponent';

export default function Filter() {
    const filters = [{ placeholder: "Возраст", options: ["<1 год", "<2 года", "<3 года"] }, { placeholder: "Пол", options: ["Мужской", "Женский"] }, { placeholder: "Размер", options: ["Маленький", "Средний", "Большой"] }, { placeholder: "Темперамент", options: ["Ласковый", "Нормальный", "Грустный"] }, { placeholder: "Другие фильтры", options: ["Кошка", "Собака", "Птица"] }];
    return (
        <div className={styles.filterContainer}>
            <div className={styles.logoContainer}>
                <button className={styles.buttonLogo}><img src="" alt="фильтр собаки" className={styles.logo} /></button>
                <button className={styles.buttonLogo}><img src="" alt="фильтр кошки" className={styles.logo} /></button>
            </div>
            <div>
                <p className={styles.p}>Фильтры</p>
                <ul className={styles.ul}>
                    {filters.map((filter) => (
                        <li key={filter.placeholder}><FilterComponent filter={filter} /></li>))}
                </ul>
            </div>
        </div>
    );
}