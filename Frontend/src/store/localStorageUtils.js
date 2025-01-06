export const saveStateToLocalStorage = (state) => {
    try {
        const serializedState = JSON.stringify(state);
        localStorage.setItem('userState', serializedState);
    } catch (error) {
        console.error('Error saving to localStorage:', error);
    }
};

export const loadStateFromLocalStorage = () => {
    try {
        const serializedState = localStorage.getItem('userState');
        return serializedState ? JSON.parse(serializedState) : {};
    } catch (error) {
        console.error('Error loading from localStorage:', error);
        return {};
    }
};

export const clearStateFromLocalStorage = () => {
    localStorage.removeItem('userState');
};
