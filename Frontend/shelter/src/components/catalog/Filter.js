import styles from './Filter.module.css';
import FilterComponent from './FilterComponent';
import { FilterOptions } from '../../filterOptions';

export default function Filter(props) {
    const {filters, onFilterChange} = props;
    const allFilters = { ...FilterOptions };
    const handleFilterChange = (filterName, value) => {
        onFilterChange(filterName, value);
    };
    // const filters = [{ placeholder: "Возраст", options: ["<1 год", "<2 года", "<3 года"] }, { placeholder: "Пол", options: ["Мужской", "Женский"] }, { placeholder: "Размер", options: ["Маленький", "Средний", "Большой"] }, { placeholder: "Темперамент", options: ["Ласковый", "Нормальный", "Грустный"] }, { placeholder: "Другие фильтры", options: ["Кошка", "Собака", "Птица"] }];
    return (
        <div className={styles.filterContainer}>
            <div className={styles.logoContainer}>
                <button className={styles.buttonLogo}><img src="" alt="фильтр собаки" className={styles.logo} /></button>
                <button className={styles.buttonLogo}><img src="" alt="фильтр кошки" className={styles.logo} /></button>
            </div>
            <div>
                <p className={styles.p}>Фильтры</p>
                <div className={styles.filter}>
                {Object.keys(allFilters).map((filter) => (
                    <FilterComponent key={filter} placeholder={allFilters[filter].placeholder} options={allFilters[filter].options} selected={filters[filter]} onChange={(value) => handleFilterChange(filter, value)}/>
                ))}
                </div>
            </div>
        </div>
    );
}