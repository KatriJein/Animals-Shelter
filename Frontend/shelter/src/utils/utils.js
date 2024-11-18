export function isPetIdInArray(array, id) {
    return array.some(item => item.id === id);
}
