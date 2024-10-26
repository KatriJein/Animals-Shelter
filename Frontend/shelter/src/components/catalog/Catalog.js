import React from 'react';
import styles from './Catalog.module.css';
import Filter from './Filter';
import Card from './card/Card';

const pet = {
    name: "Слипнотик",
    breed: "корги",
    age: 2,
    gender: "мальчик",
}

export default function Catalog() {

    return (
        <main className={styles.mainContainer}>
            <h2 className={styles.h2}>Каталог питомцев</h2>
            <Filter />
            <div className={styles.cardsList}>
                <Card pet={pet} />
                <Card pet={pet} />
                <Card pet={pet} />
                <Card pet={pet} />
                <Card pet={pet} />

            </div>
        </main>

    )
}
