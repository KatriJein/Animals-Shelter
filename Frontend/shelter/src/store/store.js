import { configureStore } from '@reduxjs/toolkit';
import animalsReducer from './animalsSlice';
import userReducer from './userSlice';
import { saveStateToLocalStorage, loadStateFromLocalStorage } from './localStorageUtils';

const preloadedUserState = loadStateFromLocalStorage();

const store = configureStore({
    reducer: {
        animals: animalsReducer,
        user: userReducer,
    },
    preloadedState: {
        user: preloadedUserState,
    },
});

store.subscribe(() => {
    saveStateToLocalStorage(store.getState().user);
});

export default store;
