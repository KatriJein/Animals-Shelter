import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';

const initialState = {
  articles: [],
  loading: false,
  error: null,
};

const API_URL = process.env.REACT_APP_API_URL;

export const fetchArticles = createAsyncThunk('articles/fetchArticles', async (_, { rejectWithValue }) => {
  try {
    const response = await fetch(`${API_URL}/articles/all`); 
    if (!response.ok) {
      throw new Error('Ошибка при получении статей');
    }
    return await response.json();
  } catch (error) {
    return rejectWithValue(error.message);
  }
});

const articlesSlice = createSlice({
  name: 'articles',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchArticles.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchArticles.fulfilled, (state, action) => {
        state.articles = action.payload;
        state.loading = false;
      })
      .addCase(fetchArticles.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload;
      });
  },
});

export const selectArticles = (state) => state.articles.articles;
export const selectArticlesLoading = (state) => state.articles.loading;
export const selectArticlesError = (state) => state.articles.error;

export default articlesSlice.reducer;
