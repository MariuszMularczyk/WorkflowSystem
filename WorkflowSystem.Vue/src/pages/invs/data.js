
const paymentTypes  = [{
    ID: 1,
    Name: 'Przelew',
}, {
    ID: 2,
    Name: 'Gotówka',
}, {
    ID: 3,
    Name: 'Karta służbowa',
}, {
    ID: 4,
    Name: 'Kompensata',
}];

const fvTypes = [{
    ID: 1,
    Name: 'Zakupowa',
}, {
    ID: 2,
    Name: 'Zaliczkowa',
}, {
    ID: 3,
    Name: 'Końcowa/rozliczeniowa',
}, {
    ID: 4,
    Name: 'Pro-forma',
}, {
    ID: 5,
    Name: 'Korygująca',
}, {
    ID: 6,
    Name: 'Duplikat',
}, {
    ID: 7,
    Name: 'Rachunek',
}, {
    ID: 8,
    Name: 'Inny dokument księgowy',
}];
const paymentStatuses = [{
    ID: 1,
    Name: 'Niezapłacone',
}, {
    ID: 2,
    Name: 'Zapłacone częściowo',
}, {
    ID: 3,
    Name: 'Całkowicie zapłacone',
}];
const currencies = [{
    ID: 1,
    Name: 'PLN',
}, {
    ID: 2,
    Name: 'EUR',
}, {
    ID: 3,
    Name: 'USD',
}];
const categories = [{
    ID: 1,
    Name: 'Kategoria 1',
}, {
    ID: 2,
    Name: 'Kategoria 2',
}, {
    ID: 3,
    Name: 'Kategoria 3',
}, {
    ID: 4,
    Name: 'Kategoria 4',
}, {
    ID: 5,
    Name: 'Kategoria 5',
}, {
    ID: 6,
    Name: 'Kategoria 6',
}, {
    ID: 7,
    Name: 'Kategoria 7',
}, {
    ID: 8,
    Name: 'Kategoria 8',
}];

export default {
    getPaymentTypes() {
        return paymentTypes;
    },
    getFvTypes() {
        return fvTypes;
    },
    getPaymentStatuses() {
        return paymentStatuses;
    },
    getCurrencies() {
        return currencies;
    },
    getCategories() {
        return categories;
    },
};
