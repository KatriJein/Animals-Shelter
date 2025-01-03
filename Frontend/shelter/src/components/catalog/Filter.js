import React from 'react';
import styles from './Filter.module.css';
import FilterComponent from './FilterComponent';
import { FilterOptions } from '../../filterOptions';

export default function Filter(props) {
    const { filters, onFilterChange, onResetFilters } = props;
    const allFilters = { ...FilterOptions };

    const handleFilterChange = (filterName, value) => {
        onFilterChange(filterName, value);
    };

    const hasActiveFilters = Object.values(filters).some(
        (value) => (Array.isArray(value) && value.length > 0) || (!Array.isArray(value) && value !== "")
    );

    return (
        <div className={styles.filterContainer}>
            <div className={styles.filterHeader}>
                <p className={styles.p}>Фильтры</p>
                {hasActiveFilters && (
                    <button className={styles.clearButton} onClick={onResetFilters}>
                        Очистить
                    </button>
                )}
            </div>
            <div className={styles.filter}>
                {Object.keys(allFilters).map((filter) => (
                    <FilterComponent
                        key={filter}
                        placeholder={allFilters[filter].placeholder}
                        options={allFilters[filter].options}
                        selected={filters[filter]}
                        onChange={(value) => handleFilterChange(filter, value)}
                        type={allFilters[filter].type}
                    />
                ))}
            </div>
        </div>
    );
}
