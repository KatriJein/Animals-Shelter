import React, { useState } from 'react';
import styles from './FilterComponent.module.css';

export default function FilterComponent(props) {
    const { filter } = props;
    const [selectedFilter, setSelectedFilter] = useState("");

    const handleFilterChange = (event) => {
        setSelectedFilter(event.target.value);
    };

    return (
        <select value={selectedFilter} onChange={handleFilterChange} className={styles.container}>
            <option value="">{filter.placeholder}</option>
            {filter.options.map((filterKey) => (
                <option key={filterKey} value={filterKey}>
                    {filterKey}
                </option>
            ))}
        </select>
    );
};
