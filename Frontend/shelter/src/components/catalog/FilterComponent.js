import React, { useState } from 'react';
import styles from './FilterComponent.module.css';

export default function FilterComponent(props) {
    const { placeholder, options, selected, onChange } = props;
    // const [selectedFilter, setSelectedFilter] = useState("");

    // const handleFilterChange = (event) => {
    //     setSelectedFilter(event.target.value);
    // };

    return (
        <select value={selected} onChange={(e) => onChange(e.target.value)} className={styles.container}>
            <option value="">{placeholder}</option>
            {Object.keys(options).map((filterKey) => (
                <option key={filterKey} value={filterKey}>
                    {options[filterKey]}
                </option>
            ))}
        </select>
    );
};
