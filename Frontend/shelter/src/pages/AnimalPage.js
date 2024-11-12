import Header from "../components/header/Header";
import PageAnimal from "../components/pageAnimal/pageAnimal";
import { useParams } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { useState, useEffect } from 'react';

export default function AnimalPage() {
    const { id } = useParams();
    const [isLoading, setIsLoading] = useState(true);

    const animal = useSelector((state) =>
        state.animals.animals.find((pet) => pet.id === id)
    );

    useEffect(() => {

        if (animal) {
            setIsLoading(false);
        }
    }, [animal]);

    return (
        <>
            {isLoading ? (
                <p>Загрузка...</p>
            ) : animal ? (
                <PageAnimal pet={animal} />
            ) : (
                <p>Животное не найдено.</p>
            )}
        </>
    );
}
