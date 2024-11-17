import { createSlice } from '@reduxjs/toolkit';
import { clearStateFromLocalStorage } from './localStorageUtils';

const initialState = {
    isAuthenticated: false,
    isAdmin: false,
    userInfo: null,
};

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
            clearStateFromLocalStorage();
        },
        changes: (state, action) => {
            state.userInfo = { ...state.userInfo, ...action.payload };
        },
    },
});

export const { loginSuccess, logout, changes } = userSlice.actions;
export default userSlice.reducer;
