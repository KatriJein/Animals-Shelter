import React, { useState } from 'react';
import styles from './Catalog.module.css';
import Filter from './Filter';
import Card from './card/Card';
import { Provider, useDispatch, useSelector } from 'react-redux';
import { isAgeInRange } from '../../utils/filter';

const pet = {
    name: "Слипнотик",
    breed: "корги",
    age: 2,
    gender: "мальчик",
}

const defaulFilters = {
    age: "",
    sex: "",
    size: "",
    wool: "",
    color: "",
    temper: "",
    healthCondition: "",
    livingCondition: "",
    receiptDate: ""
}

export default function Catalog() {
    const [filters, setFilters] = useState(defaulFilters);

    const animals = useSelector((state) => state.animals.animals);
    console.log(animals[0], filters);
    const handleFilterChange = (filterName, value) => {
        setFilters(prevFilters => ({
            ...prevFilters,
            [filterName]: value
        }));
    };

    const filteredAnimals = animals.filter(pet => {
        return Object.keys(filters).every(filterName => {
            if (filterName === 'age') {
                return isAgeInRange(pet.age, filters[filterName]); 
            }
            return filters[filterName] === "" || pet[filterName] === filters[filterName];
        });
    });

    return (
        <main className={styles.mainContainer}>
            <h2 className={styles.h2}>Каталог питомцев</h2>
            <Filter filters={filters} onFilterChange={handleFilterChange}/>
            <div className={styles.cardsList}>
                {filteredAnimals.map((pet) => (
                    <Card key={pet.id} pet={pet} />
                ))}
                {/* <Card pet={pet} />
                <Card pet={pet} />
                <Card pet={pet} />
                <Card pet={pet} />
                <Card pet={pet} /> */}

            </div>
        </main>

    )
}
