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
            const url = `${ process.env.REACT_APP_API_URL }/users/${ userId } /favourite/${ pet.id }`;
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
            const url = `${ process.env.REACT_APP_API_URL }/users/${ userId } /unfavourite/${ petId }`;
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

export const loginAsync = createAsyncThunk(
    'user/login',
    async ({ login, password }, { rejectWithValue }) => {
        try {
            const response = await fetch(`${process.env.REACT_APP_API_URL}/auth`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ login, password }),
            });

            const data = await response.json();
            if (!response.ok) {
                throw new Error(data.message || 'Ошибка при входе');
            }
            return data;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const updateUserDetails = createAsyncThunk(
    'user/updateUserDetails',
    async ({ userId, name, surname }, { rejectWithValue }) => {
        try {
            const userChanges = {
                name: name.trim(),
                surname: surname.trim(),
            };

            const response = await fetch(`${process.env.REACT_APP_API_URL}/auth/${userId}/finish`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(userChanges),
            });

            const data = await response.json();
            if (!response.ok) {
                throw new Error(data.message || 'Ошибка при обновлении данных');
            }

            return data;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const updateUserInfo = createAsyncThunk(
    'user/updateInfo',
    async ({ userId, info }, { rejectWithValue }) => {
        try {
            const response = await fetch(`${process.env.REACT_APP_API_URL}/users/${userId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json-patch+json',
                },
                body: JSON.stringify(info),
            });

            if (!response.ok) {
                throw new Error(data.message || 'Ошибка при обновлении данных');
            }
            return info;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const updateUserPassword = createAsyncThunk(
    'user/updatePassword',
    async ({ userId, oldPassword, newPassword }, { rejectWithValue }) => {
        try {
            const response = await fetch(`${process.env.REACT_APP_API_URL}/users/${userId}/password`, {
                method: 'PATCH',
                headers: {
                    'Content-Type': 'application/json-patch+json',
                },
                body: JSON.stringify({ oldPassword, newPassword }),
            });

            if (!response.ok) {
                throw new Error(data.message || 'Ошибка при изменении пароля');
            }
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const updateUserAvatar = createAsyncThunk(
    'user/updateAvatar',
    async ({ userId, avatarFile }, { rejectWithValue }) => {
        try {
            const formData = new FormData();
            formData.append('Avatar', avatarFile);

            const response = await fetch(`${process.env.REACT_APP_API_URL}/users/${userId}/avatar`, {
                method: 'PATCH',
                headers: {
                    'accept': '*/*',
                },
                body: formData,
            });

            if (!response.ok) {
                throw new Error(data.message || 'Ошибка при обновлении аватара');
            }

        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);


export const register = createAsyncThunk(
    'user/register',
    async ({ login, password }, { rejectWithValue }) => {
        try {
            const response = await fetch(`${process.env.REACT_APP_API_URL}/auth/register`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ login, password }),
            });

            const data = await response.json();
            if (!response.ok) {
                throw new Error(data.message || 'Ошибка при регистрации');
            }
            return data;
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

            if (response.status === 204) {
                return [];
            }

            if (!response.ok) {
                const errorData = await response.json().catch(() => ({})); 
                throw new Error(errorData.message || 'Ошибка при получении избранного');
            }

            const data = await response.json();
            return data;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const clearAllFavourites = createAsyncThunk(
    'user/clearAllFavourites',
    async (userId, { rejectWithValue }) => {
        try {
            const response = await fetch(`${process.env.REACT_APP_API_URL}/users/${userId}/favourites/clear`, {
                method: 'DELETE',
                headers: {
                    'Accept': '*/*',
                },
            });

            if (!response.ok) {
                const errorData = await response.json().catch(() => ({}));
                throw new Error(errorData.message || 'Ошибка при удалении всех избранных');
            }

            return { message: 'Все животные удалены из избранного' }; 
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        logout: (state) => {
            state.isAuthenticated = false;
            state.id = null;
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
            // Логин
            .addCase(loginAsync.pending, (state) => {
                state.error = null;
            })
            .addCase(loginAsync.fulfilled, (state, action) => {
                state.isAuthenticated = true;
                state.id = action.payload.userInfo.id;
                state.isAdmin = action.payload.userInfo.isAdmin;
                state.userInfo = action.payload.userInfo;
            })
            .addCase(loginAsync.rejected, (state, action) => {
                state.error = action.payload;
            })

            // Регистрация
            .addCase(register.pending, (state) => {
                state.error = null;
            })
            .addCase(register.fulfilled, (state, action) => {
                state.id = action.payload.userId;
            })
            .addCase(register.rejected, (state, action) => {
                state.error = action.payload;
            })

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
                state.error = null;
            })
            .addCase(addFavourite.fulfilled, (state, action) => {
                state.favourites = [...state.favourites, action.payload];
            })
            .addCase(addFavourite.rejected, (state, action) => {
                state.error = action.payload;
            })
            //removeFavourite
            .addCase(removeFavourite.pending, (state) => {
                state.error = null;
            })
            .addCase(removeFavourite.fulfilled, (state, action) => {
                state.favourites = state.favourites.filter(pet => pet.id !== action.payload);
            })
            .addCase(removeFavourite.rejected, (state, action) => {
                state.error = action.payload;
            })
            // Обновление данных пользователя
            .addCase(updateUserDetails.pending, (state) => {
                state.error = null;
            })
            .addCase(updateUserDetails.fulfilled, (state, action) => {
                state.isAuthenticated = true;
                state.userInfo = { ...state.userInfo, ...action.payload.userInfo };
                state.isAdmin = action.payload.userInfo.isAdmin;
            })
            .addCase(updateUserDetails.rejected, (state, action) => {
                state.error = action.payload;
            })
            // clearAllFavourites
            .addCase(clearAllFavourites.pending, (state) => {
                state.error = null;
            })
            .addCase(clearAllFavourites.fulfilled, (state) => {
                state.favourites = [];
            })
            .addCase(clearAllFavourites.rejected, (state, action) => {
                state.error = action.payload;
            })
            //updateInfo
            .addCase(updateUserInfo.pending, (state) => {
                state.error = null;
            })
            .addCase(updateUserInfo.fulfilled, (state, action) => {
                console.log(action.payload);
                state.userInfo = { ...state.userInfo, ...action.payload };
            })
            .addCase(updateUserInfo.rejected, (state, action) => {
                state.error = action.payload;
            })
            //updatePassword
            .addCase(updateUserPassword.pending, (state) => {
                state.error = null;
            })
            .addCase(updateUserPassword.rejected, (state, action) => {
                state.error = action.payload;
            });
    },
});

export const selectloadingFavourites = (state) => state.user.loadingFavourites;
export const selectUser = (state) => state.user;
export const selectUserInfo = (state) => state.user.userInfo;
export const selectUserError = (state) => state.user.error;
export const selectIsAuthenticated = (state) => state.user.isAuthenticated;

export const { logout, changes } = userSlice.actions;
export default userSlice.reducer;
