import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { clearStateFromLocalStorage } from './localStorageUtils';

export const initialState = {
    id: null,
    isAuthenticated: false,
    isAdmin: false,
    userInfo: null,
    favourites: [],
    loadingFavourites: false,
    error: null,
};

export const addFavourite = createAsyncThunk(
    'user/addFavourite',
    async ({ userId, pet }, { rejectWithValue }) => {
        try {
            const url = `${process.env.REACT_APP_API_URL}/users/${userId}/favourite/${pet.id}`;
            const response = await fetch(url, {
                method: 'PATCH',
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            const data = await response.json();
            if (!response.ok || !data.isSuccess) {
                throw new Error(data.message || 'Ошибка при добавлении в избранное');
            }

            return pet;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const removeFavourite = createAsyncThunk(
    'user/removeFavourite',
    async ({ userId, petId }, { rejectWithValue }) => {
        try {
            const url = `${process.env.REACT_APP_API_URL}/users/${userId}/unfavourite/${petId}`;
            const response = await fetch(url, {
                method: 'PATCH',
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            const data = await response.json();
            if (!response.ok || !data.isSuccess) {
                throw new Error(data.message || 'Ошибка при удалении из избранного');
            }

            return petId;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const fetchFavourites = createAsyncThunk(
    'user/fetchFavourites',
    async (userId, { rejectWithValue }) => {
        try {
            const response = await fetch(`${process.env.REACT_APP_API_URL}/users/${userId}/favourites`);
            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || 'Ошибка при получении избранного');
            }
            return await response.json(); 
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
            state.id = action.payload.id;
            state.isAdmin = action.payload.isAdmin;
            state.userInfo = action.payload.userInfo;
        },
        loginFinish: (state, action) => {
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
            //fetchFavourites
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
            })
            //addFavourite
            .addCase(addFavourite.pending, (state) => {
                state.loadingFavourites = true;
                state.error = null;
            })
            .addCase(addFavourite.fulfilled, (state, action) => {
                state.loadingFavourites = false;
                state.favourites = [...state.favourites, action.payload];
            })
            .addCase(addFavourite.rejected, (state, action) => {
                state.loadingFavourites = false;
                state.error = action.payload;
            })
            //removeFavourite
            .addCase(removeFavourite.pending, (state) => {
                state.loadingFavourites = true;
                state.error = null;
            })
            .addCase(removeFavourite.fulfilled, (state, action) => {
                state.loadingFavourites = false;
                state.favourites = state.favourites.filter(pet => pet.id !== action.payload);
            })
            .addCase(removeFavourite.rejected, (state, action) => {
                state.loadingFavourites = false;
                state.error = action.payload;
            });
    },
});


export const { loginSuccess, logout, changes, loginFinish } = userSlice.actions;
export default userSlice.reducer;
