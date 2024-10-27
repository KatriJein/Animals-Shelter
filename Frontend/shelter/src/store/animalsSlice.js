import { createSlice } from '@reduxjs/toolkit';
import { fetchAnimals, addAnimal, updateAnimal, deleteAnimal } from './animalsActions';

const animalsSlice = createSlice({
    name: 'animals',
    initialState: {
        animals: [],
        status: 'idle', 
        error: null
    },
    reducers: {
    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchAnimals.pending, (state) => {
                state.status = 'loading';
            })
            .addCase(fetchAnimals.fulfilled, (state, action) => {
                state.status = 'succeeded';
                state.animals = action.payload;
            })
            .addCase(fetchAnimals.rejected, (state, action) => {
                state.status = 'failed';
                state.error = action.error.message;
            })
            .addCase(addAnimal.fulfilled, (state, action) => {
                state.animals.push(action.payload);
            })
            .addCase(updateAnimal.fulfilled, (state, action) => {
                const index = state.animals.findIndex(
                    (animal) => animal.id === action.payload.id
                );
                if (index !== -1) {
                    state.animals[index] = action.payload;
                }
            })
            .addCase(deleteAnimal.fulfilled, (state, action) => {
                state.animals = state.animals.filter(
                    (animal) => animal.id !== action.payload
                );
            });
    }
});

export default animalsSlice.reducer;
