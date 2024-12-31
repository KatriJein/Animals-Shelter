export const FilterOptions = {
    age: {
        placeholder: "Возраст",
        type: "checkbox",
        options: {
            small: "До 1 года",
            young: "От 1 до 3 лет",
            adult: "От 3 до 7 лет",
            old: "Старше 7 лет"
        }
    },
    sex: {
        placeholder: "Пол",
        type: "radio",
        options: {
            male: "Мальчик",
            female: "Девочка"
        }
    },
    size: {
        placeholder: "Размер",
        type: "checkbox",
        options: {
            small: "До 5 кг",
            medium: "5–15 кг",
            large: "Более 15 кг"
        }
    },
    wool: {
        placeholder: "Тип шерсти",
        type: "checkbox",
        options: {
            short: "Короткая шерсть",
            medium: "Средняя шерсть",
            long: "Длинная шерсть",
            hypoallergenic: "Гипоаллергенная шерсть"
        }
    },
    color: {
        placeholder: "Окрас",
        type: "checkbox",
        options: {
            black: "Чёрный",
            white: "Белый",
            orange: "Рыжий",
            gray: "Серый",
            brown: "Коричневый",
            spotted: "Пятнистый",
            tricolor: "Трёхцветный"
        }
    },
    temperFeatures: {
        placeholder: "Особенности поведения",
        type: "checkbox",
        options: {
            active: "Активное",
            calm: "Спокойное",
            communicative: "Общительное",
            independent: "Независимое",
            curious: "Любознательное",
            kidsFriendly: "Дружелюбное к детям",
            animalsFriendly: "Дружелюбное к другим животным",
            trainingGiveIn: "Легко поддаётся дрессировке",
            specificTemper: "Нуждается в особом уходе"
        }
    },
    healthConditions: {
        placeholder: "Состояние здоровья",
        type: "checkbox",
        options: {
            vaccinated: "Привит",
            sterilized: "Стерилизован",
            requiresTreatment: "Особые медицинские потребности"
        }
    },
    livingCondition: {
        placeholder: "Условия проживания",
        type: "checkbox",
        options: {
            forApartment: "Подходит для квартиры",
            forPrivateApartment: "Подходит для частного дома",
            forEasyStreetAccess: "Необходим свободный доступ на улицу"
        }
    },
    receiptDate: {
        placeholder: "Дата поступления в приют",
        type: "radio",
        options: {
            recent: "Новые поступления",
            longAgo: "Давно в приюте"
        }
    }
}
