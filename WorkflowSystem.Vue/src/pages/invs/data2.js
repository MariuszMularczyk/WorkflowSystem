
const vat = [{
    ID: 1,
    Name: 'ZW',
    Value: 0,
}, {
    ID: 2,
    Name: '0%',
    Value: 0,
}, {
    ID: 3,
    Name: '7%',
    Value: 5,
}, {
    ID: 4,
    Name: '22%',
    Value: 22,
}, {
    ID: 5,
    Name: 'NPV',
    Value: 0,
}, {
    ID: 6,
    Name: '3%',
    Value: 3,
}, {
    ID: 7,
    Name: '23%',
    Value: 23,
}, {
    ID: 8,
    Name: '8%',
    Value: 8,
}, {
    ID: 9,
    Name: '5%',
    Value: 5,
}, {
    ID: 10,
    Name: '12%',
    Value: 12,
}];
const productCode = [{
    ID: 1,
    Name: 'Kod 1',
}, {
    ID: 2,
    Name: 'kod 2',
}, {
    ID: 3,
    Name: 'kod 3',
}];
const jm = [{
    ID: 1,
    Name: 'kg',
}, {
    ID: 2,
    Name: 'ltr',
}, {
    ID: 3,
    Name: 'szt.',
}, {
    ID: 4,
    Name: 't',
}];

export default {
    getVat() {
        return vat;
    },
    getProductCode() {
        return productCode;
    },
    getJM() {
        return jm;
    },

};
