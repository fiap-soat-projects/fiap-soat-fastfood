import http from 'k6/http';
import { check, sleep } from 'k6';

const VUS = 15;
const DURATION = '30s';
const BASE_URL = 'http://localhost:30080';

export const options = {
    discardResponseBodies: true,
    scenarios: {
        contacts: {
            executor: 'constant-vus',
            vus: VUS,
            duration: DURATION,
        },
    },
};

export default function () {
    const name = generateGuid();
    const cpf = generateUniqueCpf();
    const email = generateEmail(name);

    const payload = JSON.stringify({
        name: name,
        cpf: cpf,
        email: email,
    });

    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    const res = http.post(`${BASE_URL}/v1/self-ordering/customer`, payload, params);
    check(res, { 'status is 201': (r) => r.status === 201 });

    sleep(1);
}

function generateGuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0,
            v = c == 'x' ? r : (r & 0x3 | 0x8);

        return v.toString(16);
    }).substring(0, 40);
}

let generatedCpfs = new Set();

function generateUniqueCpf() {
    let cpf;
    do {
        const n = Array(9).fill(0).map(() => Math.floor(Math.random() * 10));
        const d1 = calculateCpfDigit(n, 10);
        const d2 = calculateCpfDigit([...n, d1], 11);

        cpf = `${n.slice(0, 3).join('')}.${n.slice(3, 6).join('')}.${n.slice(6, 9).join('')}-${d1}${d2}`;

    } while (generatedCpfs.has(cpf));

    generatedCpfs.add(cpf);

    return cpf;
}

function calculateCpfDigit(numbers, factor) {
    let sum = 0;

    for (let i = 0; i < numbers.length; i++) {
        sum += numbers[i] * (factor - i);
    }

    let remainder = sum % 11;

    return remainder < 2 ? 0 : 11 - remainder;
}

function generateEmail(name) {
    const domains = ['test.com', 'example.com', 'mail.com', 'domain.org'];
    const randomDomain = domains[Math.floor(Math.random() * domains.length)];
    return `${name.replace(/-/g, '').toLowerCase()}@${randomDomain}`;
}