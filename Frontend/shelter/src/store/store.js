import { configureStore } from '@reduxjs/toolkit';
import animalsReducer from './animalsSlice';
import userReducer from './userSlice';
import articlesReducer from './articlesSlice';
import { combineReducers } from '@reduxjs/toolkit';
import newsReducer from './newsSlice';

const rootReducer = combineReducers({
    animals: animalsReducer,
    user: userReducer,
    articles: articlesReducer,
    news: newsReducer
})

const store = configureStore({
    reducer: rootReducer
});

export default store;
