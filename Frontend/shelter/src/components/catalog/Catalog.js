import React, { useState, useEffect } from 'react';
import styles from './Catalog.module.css';
import Filter from './Filter';
import Card from './card/Card';
import { useSelector } from 'react-redux';
import { isAgeInRange } from '../../utils/filter';
import { isPetIdInArray } from '../../utils/utils';
import { selectAnimals, selectAnimalsLoading, selectAnimalsError } from '../../store/animalsSlice';
import { selectUser, selectloadingFavourites, selectUserError } from '../../store/userSlice';
import { fetchAnimals } from '../../store/animalsActions';
import { fetchFavourites } from '../../store/userSlice';
import { useDispatch } from 'react-redux';
import { setFilter, resetFilters, selectFilters } from '../../store/filtersSlice';

const defaultFilters = {
    age: [],
    sex: "",
    size: [],
    wool: [],
    color: [],
    temperFeatures: [],
    healthConditions: [],
    livingCondition: [],
    receiptDate: ""
};

export default function Catalog() {
    const filters = useSelector(selectFilters);
    console.log(filters);
    const dispatch = useDispatch();

    const animals = useSelector(selectAnimals);
    const user = useSelector(selectUser);

    const loadingFavourites = useSelector(selectloadingFavourites);
    const loadingAnimals = useSelector(selectAnimalsLoading);

    const errorAnimals = useSelector(selectAnimalsError);
    const errorUser = useSelector(selectUserError);

    useEffect(() => {
        dispatch(fetchAnimals());
        if (user.isAuthenticated) {
            dispatch(fetchFavourites(user.id));
        }
    }, [dispatch, user.isAuthenticated]);

    const handleFilterChange = (filterName, value) => {
        dispatch(setFilter({ filterName, value }));
    };

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
                    {loadingAnimals || loadingFavourites ? (
                        <p className={styles.error}>Загрузка...</p>
                    ) : errorAnimals || errorUser ? (
                        <p className={styles.error}>Произошла ошибка</p>
                    ) : (filteredAnimals.map((pet) => (
                        <Card key={pet.id} pet={pet} isAuthenticated={user.isAuthenticated} isFavourite={isPetIdInArray(user.favourites, pet.id)} />
                    )))}
                </div>
            </div>
        </main>
    );
}
