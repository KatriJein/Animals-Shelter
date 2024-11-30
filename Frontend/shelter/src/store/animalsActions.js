import { createAsyncThunk } from '@reduxjs/toolkit';

const API_URL = process.env.REACT_APP_API_URL;

export const fetchAnimals = createAsyncThunk(
    'animals/fetchAnimals',
    async () => {
        const response = await fetch(`${API_URL}/animals/all`);
        const data = await response.json();
        return data; 
    }
);

export const addAnimal = createAsyncThunk(
    'animals/addAnimal',
    async (newAnimal) => {
        const response = await fetch(`${API_URL}/animals`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(newAnimal),
        });
        const data = await response.json();
        return data; 
    }
);

export const updateAnimal = createAsyncThunk(
    'animals/updateAnimal',
    async ({ id, updatedData }) => {
        const response = await fetch(`${API_URL}/animals/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(updatedData),
        });
        const data = await response.json();
        return data; 
    }
);

export const deleteAnimal = createAsyncThunk(
    'animals/deleteAnimal',
    async (id) => {
        await fetch(`${API_URL}/animals/${id}`, {
            method: 'DELETE',
        });
        return id; 
    }
);