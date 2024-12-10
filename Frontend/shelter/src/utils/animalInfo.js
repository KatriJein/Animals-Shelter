import { FilterOptions } from "../filterOptions";
import feedingIcon from '../img/feeding.svg';
import healthIcon from '../img/health.svg';
import careIcon from '../img/care.svg';
import behaviourIcon from '../img/behaviour.svg';
import trainingIcon from '../img/training.svg';

export function getAnimalInfo(pet) {

    const Information = [
        { heading: "Порода", text: "" },
        { heading: "Возраст", text: "" },
        { heading: "Пол", text: "" },
        { heading: "Размер", text: "" },
        { heading: "Тип шерсти", text: "" },
        { heading: "Окрас", text: "" },
        { heading: "Особенности поведения", text: "" },
        { heading: "Условия проживания", text: "" }
    ];

    const { age, sex, size, wool, color, temperFeatures, livingCondition, breed } = pet;

    const getOptionText = (field, value) => {
        if (FilterOptions[field] && FilterOptions[field].options[value]) {
            return FilterOptions[field].options[value];
        }
        return value;
    };

    Information[0].text = capitalizeFirstLetter(getOptionText('breed', breed)) || "Не указано";
    Information[1].text = `${getAgeString(age)}` || "Не указан";
    Information[2].text = capitalizeFirstLetter(getOptionText('sex', sex)) || "Не указан";
    Information[3].text = capitalizeFirstLetter(getOptionText('size', size)) || "Не указан";
    Information[4].text = capitalizeFirstLetter(getOptionText('wool', wool)) || "Не указан";
    Information[5].text = capitalizeFirstLetter(getOptionText('color', color)) || "Не указан";
    Information[6].text = capitalizeFirstLetter(temperFeatures.map(feature => getOptionText('temperFeatures', feature)).join(", ")) || "Не указано";
    Information[7].text = capitalizeFirstLetter(getOptionText('livingCondition', livingCondition)) || "Не указан";;

    return Information;
}

export function getAgeString(age) {
    const lastDigit = age % 10;
    const lastTwoDigits = age % 100;

    if (lastTwoDigits >= 11 && lastTwoDigits <= 14) {
        return `${age} лет`;
    }

    switch (lastDigit) {
        case 1:
            return `${age} год`;
        case 2:
        case 3:
        case 4:
            return `${age} года`;
        default:
            return `${age} лет`;
    }
}

export function capitalizeFirstLetter(str) {
    if (str.length === 0) return str;
    return str.charAt(0).toUpperCase() + str.slice(1);
}

export function getHealthConditionsWithGender(healthConditions, sex) {
    const translatedConditions = healthConditions.map(condition => {
        switch (condition) {
            case 'vaccinated':
                return sex === 'male' ? 'Привит' : 'Привита';
            case 'sterilized':
                return sex === 'male' ? 'Стерилизован' : 'Стерилизована';
            case 'requiresTreatment':
                return 'Нуждается в лечении';
            default:
                return condition;
        }
    });
    return translatedConditions;
}


export const newsTag = {
    newAnimals: {
        title: 'Новые поступления',
        color: '#374375'
    },
    successStories: {
        title: 'Успешные истории',
        color: '#BABCE2'
    },
    events: {
        title: 'Мероприятия',
        color: '#89515A'
    },
    reports: {
        title: 'Отчет и благодарность',
        color: '#9A526E'
    },
    volunteering: {
        title: 'Волонтерство',
        color: '#DFAEA0'
    },
    holidays: {
        title: 'Праздники',
        color: '#CDA3A9'
    },
    improvements: {
        title: 'Улучшения',
        color: '#9498E0'
    }
}

export const categories = [{ text: 'Кормление', icon: feedingIcon, color: '#9498E0', category: 'feedingArticles' },
{ text: 'Дрессировка', icon: trainingIcon, color: '#986C73', category: 'trainingArticles' },
{ text: 'Здоровье', icon: healthIcon, color: '#505B86', category: 'healthArticles' },
{ text: 'Уход', icon: careIcon, color: '#CC969D', category: 'treatmentArticles' },
{ text: 'Поведение', icon: behaviourIcon, color: '#8E4A64', category: 'behaviourArticles' }];

export const findCategoryText = (category) => {
    const categoryItem = categories.find(item => item.category === category);
    return categoryItem ? categoryItem.text : undefined;
};

