import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { clearStateFromLocalStorage } from './localStorageUtils';

const initialState = {
    isAuthenticated: false,
    isAdmin: false,
    userInfo: null,
    favourites: [],
    loadingFavourites: false,
    error: null,
};

export const fetchFavourites = createAsyncThunk(
    'user/fetchFavourites',
    async (userId, { rejectWithValue }) => {
        try {
            const response = await fetch(`${process.env.REACT_APP_API_URL}/users/${userId}/favourites`);
            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || 'Ошибка при получении избранного');
            }
            return await response.json(); // Ожидается, что сервер возвращает массив избранных животных
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        loginSuccess: (state, action) => {
            state.isAuthenticated = true;
            state.isAdmin = action.payload.isAdmin;
            state.userInfo = action.payload.userInfo;
        },
        logout: (state) => {
            state.isAuthenticated = false;
            state.isAdmin = false;
            state.userInfo = null;
            state.favourites = [];
            clearStateFromLocalStorage();
        },
        changes: (state, action) => {
            state.userInfo = { ...state.userInfo, ...action.payload };
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchFavourites.pending, (state) => {
                state.loadingFavourites = true;
                state.error = null;
            })
            .addCase(fetchFavourites.fulfilled, (state, action) => {
                state.loadingFavourites = false;
                state.favourites = action.payload;
            })
            .addCase(fetchFavourites.rejected, (state, action) => {
                state.loadingFavourites = false;
                state.error = action.payload || 'Ошибка при загрузке избранного';
            });
    },
});

export const { loginSuccess, logout, changes } = userSlice.actions;
export default userSlice.reducer;
