import React, { useState } from 'react';
import styles from './Catalog.module.css';
import Filter from './Filter';
import Card from './card/Card';
import { useSelector } from 'react-redux';
import { isAgeInRange } from '../../utils/filter';

const defaulFilters = {
    age: [],
    sex: "",
    size: [],
    wool: [],
    color: [],
    temperFeatures: [],
    healthCondition: [],
    livingCondition: [],
    receiptDate: ""
};

export default function Catalog() {
    const [filters, setFilters] = useState(defaulFilters);
    const animals = useSelector((state) => state.animals.animals);

    const handleFilterChange = (filterName, value) => {
        setFilters(prevFilters => ({
            ...prevFilters,
            [filterName]: value
        }));
    };

    console.log(filters)

    const filteredAnimals = animals.filter(pet => {
        return Object.keys(filters).every(filterName => {
            if (filterName === 'age') {
                return isAgeInRange(pet.age, filters[filterName]);
            }
    
            if (Array.isArray(pet[filterName])) {
                return filters[filterName].length === 0 || filters[filterName].every(f => pet[filterName].includes(f));
            }
    
            if (Array.isArray(filters[filterName])) {
                return filters[filterName].length === 0 || filters[filterName].includes(pet[filterName]);
            }

            return filters[filterName] === "" || pet[filterName] === filters[filterName];
        });
    });
    

    return (
        <main className={styles.mainContainer}>
            <h2 className={styles.h2}>Наши питомцы</h2>
            <div className={styles.container}>
                <Filter filters={filters} onFilterChange={handleFilterChange} />
                <div className={styles.cardsList}>
                    {filteredAnimals.map((pet) => (
                        <Card key={pet.id} pet={pet} />
                    ))}
                </div>
            </div>
        </main>
    );
}
