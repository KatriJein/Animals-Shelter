import React from 'react';
import styles from './Filter.module.css';
import FilterComponent from './FilterComponent';
import { FilterOptions } from '../../filterOptions';

export default function Filter(props) {
    const { filters, onFilterChange } = props;
    const allFilters = { ...FilterOptions };

    const handleFilterChange = (filterName, value) => {
        onFilterChange(filterName, value);
    };

    return (
        <div className={styles.filterContainer}>
            <p className={styles.p}>Фильтры</p>
            <div className={styles.filter}>
                {Object.keys(allFilters).map((filter) => (
                    <FilterComponent
                        key={filter}
                        placeholder={allFilters[filter].placeholder}
                        options={allFilters[filter].options}
                        selected={filters[filter]}
                        onChange={(value) => handleFilterChange(filter, value)}
                        type={allFilters[filter].type} // передаем тип выбора (checkbox или radio)
                    />
                ))}
            </div>
        </div>
    );
}
