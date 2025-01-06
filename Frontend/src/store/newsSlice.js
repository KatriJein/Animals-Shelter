import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';

const initialState = {
    news: [],
    photos: [],
    loadingNews: false,
    loadingPhotos: false,
    error: null,
};

const API_URL = process.env.REACT_APP_API_URL;

export const fetchNews = createAsyncThunk('articles/fetchNews', async (_, { rejectWithValue }) => {
    try {
        const response = await fetch(`${API_URL}/articles/all?Category=news`);
        if (!response.ok) {
            throw new Error('Ошибка при получении новостей');
        }
        return await response.json();
    } catch (error) {
        return rejectWithValue(error.message);
    }
});

export const fetchPhotos = createAsyncThunk('articles/fetchPhotos', async (_, { rejectWithValue }) => {
    try {
        const response = await fetch(`${API_URL}/articles/files`);
        if (!response.ok) {
            throw new Error('Ошибка при получении фотографий');
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
            //fetchNews
            .addCase(fetchNews.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(fetchNews.fulfilled, (state, action) => {
                state.news = action.payload;
                state.loading = false;
            })
            .addCase(fetchNews.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload;
            })
            //fetchPhotos
            .addCase(fetchPhotos.pending, (state) => {
                state.loadingPhotos = true;
                state.error = null;
            })
            .addCase(fetchPhotos.fulfilled, (state, action) => {
                state.photos = action.payload.files;
                state.loadingPhotos = false;
            })
            .addCase(fetchPhotos.rejected, (state, action) => {
                state.loadingPhotos = false;
                state.error = action.payload;
            });
    },
});

export const selectNews = (state) => state.news.news;
export const selectNewsLoading = (state) => state.news.loadingNews;
export const selectNewsError = (state) => state.news.error;
export const selectPhotos = (state) => state.news.photos;
export const selectPhotosLoading = (state) => state.news.loadingPhotos;

export default newsSlice.reducer;
