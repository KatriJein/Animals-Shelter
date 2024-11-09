const ageRanges = {
    small: { min: 0, max: 1 },    
    young: { min: 1, max: 3 },    
    adult: { min: 3, max: 7 },    
    old: { min: 7, max: Infinity }
};

export const isAgeInRange = (age, filters) => {
    if (!filters || filters.length === 0) return true; 
    return filters.some(filter => {
        const { min, max } = ageRanges[filter];
        return age >= min && age <= max;
    });
};
