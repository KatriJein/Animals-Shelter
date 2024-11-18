export function isPetIdInArray(array, id) {
    console.log(array, id);
    return array.some(item => item.id === id);
}
