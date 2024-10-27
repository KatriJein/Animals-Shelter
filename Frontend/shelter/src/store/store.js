import { configureStore } from '@reduxjs/toolkit';
import animalsReducer from './animalsSlice';

const store = configureStore({
    reducer: {
        animals: animalsReducer,
    },
});

export default store;
