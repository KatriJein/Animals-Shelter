import { configureStore } from '@reduxjs/toolkit';
import animalsReducer from './animalsSlice';
import userReducer from './userSlice';
import { saveStateToLocalStorage, loadStateFromLocalStorage } from './localStorageUtils';
import { initialStateUser } from './userSlice'; 
import articlesReducer from './articlesSlice';

const preloadedUserState = {
    ...initialStateUser, 
    ...loadStateFromLocalStorage() || {}, 
};

const store = configureStore({
    reducer: {
        animals: animalsReducer,
        user: userReducer,
        articles: articlesReducer,
    },
    preloadedState: {
        user: preloadedUserState,
    },
});

store.subscribe(() => {
    saveStateToLocalStorage(store.getState().user);
});

export default store;
