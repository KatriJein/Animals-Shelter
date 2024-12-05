import { createSlice } from '@reduxjs/toolkit';
import { fetchAnimals, addAnimal, updateAnimal, deleteAnimal } from './animalsActions';

const animalsSlice = createSlice({
    name: 'animals',
    initialState: {
        animals: [],
        loading: false, 
        error: null
    },
    reducers: {
    },
    extraReducers: (builder) => {
        builder
            //fetchAnimals
            .addCase(fetchAnimals.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(fetchAnimals.fulfilled, (state, action) => {
                state.loading = false;
                state.animals = action.payload;
            })
            .addCase(fetchAnimals.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message;
            })
            //addAnimal
            .addCase(addAnimal.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(addAnimal.fulfilled, (state, action) => {
                state.animals.push(action.payload);
            })
            .addCase(addAnimal.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message;
            })
            //updateAnimal
            .addCase(updateAnimal.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(updateAnimal.fulfilled, (state, action) => {
                const index = state.animals.findIndex(
                    (animal) => animal.id === action.payload.id
                );
                if (index !== -1) {
                    state.animals[index] = action.payload;
                }
            })
            .addCase(updateAnimal.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message;
            })
            //deleteAnimal
            .addCase(deleteAnimal.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(deleteAnimal.fulfilled, (state, action) => {
                state.animals = state.animals.filter(
                    (animal) => animal.id !== action.payload
                );
            })
            .addCase(deleteAnimal.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message;
            });
    }
});

export const selectAnimals = (state) => state.animals.animals;
export const selectAnimalsLoading = (state) => state.animals.loading;
export const selectAnimalsError = (state) => state.animals.error;

export default animalsSlice.reducer;
