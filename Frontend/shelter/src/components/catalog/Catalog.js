import React from 'react';
import styles from './Catalog.module.css';
import Filter from './Filter';
import Card from './card/Card';
import { Provider, useDispatch, useSelector } from 'react-redux';

const pet = {
    name: "Слипнотик",
    breed: "корги",
    age: 2,
    gender: "мальчик",
}

export default function Catalog() {

    const animals = useSelector((state) => state.animals.animals);

    return (
        <main className={styles.mainContainer}>
            <h2 className={styles.h2}>Каталог питомцев</h2>
            <Filter />
            <div className={styles.cardsList}>
                {animals.map((pet) => (
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
