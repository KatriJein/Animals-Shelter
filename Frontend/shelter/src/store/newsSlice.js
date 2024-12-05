import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';

const initialState = {
    news: [],
    loading: false,
    error: null,
};

const API_URL = process.env.REACT_APP_API_URL;

export const fetchNews = createAsyncThunk('articles/fetchNews', async (_, { rejectWithValue }) => {
    try {
        const response = await fetch(`${API_URL}/articles/all?Category=news`);
        if (!response.ok) {
            throw new Error('Ошибка при получении статей');
        }
        return await response.json();
    } catch (error) {
        return rejectWithValue(error.message);
    }
});

const newsSlice = createSlice({
    name: 'news',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchNews.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(fetchNews.fulfilled, (state, action) => {
                state.articles = action.payload;
                state.loading = false;
            })
            .addCase(fetchNews.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload;
            });
    },
});

export const selectNews = (state) => state.news.articles;
export const selectNewsLoading = (state) => state.articles.loading;
export const selectNewsError = (state) => state.articles.error;

export default newsSlice.reducer;
