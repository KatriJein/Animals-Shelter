import { createSlice } from '@reduxjs/toolkit';

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
        },
    },
});

export const { loginSuccess, logout } = userSlice.actions;
export default userSlice.reducer;
