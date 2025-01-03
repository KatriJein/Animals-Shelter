import { createSlice } from '@reduxjs/toolkit';

const defaultFilters = {
    age: [],
    sex: "",
    size: [],
    wool: [],
    color: [],
    temperFeatures: [],
    healthConditions: [],
    livingCondition: [],
    receiptDate: ""
};

const filtersSlice = createSlice({
    name: 'filters',
    initialState: defaultFilters,
    reducers: {
        setFilter: (state, action) => {
            const { filterName, value } = action.payload;
            state[filterName] = value;
        },

        resetFilters: () => ({ ...defaultFilters })
    }
});

export const { setFilter, resetFilters } = filtersSlice.actions;
export const selectFilters = (state) => state.filters;

export default filtersSlice.reducer;
