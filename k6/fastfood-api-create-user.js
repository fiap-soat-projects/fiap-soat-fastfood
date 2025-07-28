import http from 'k6/http';
import { check, sleep } from 'k6';
import { SharedArray } from 'k6/data';


const VUS_UP_STAGE_DURATION = '2m';
const VUS_DOWN_STAGE_DURATION = '2m';
const RAMP_UP_TARGET_VUS = 500;
const RAMP_DOWN_TARGET_VUS = 10;
const BASE_URL = 'http://localhost:30080';

export const options = {
    stages: [
        { duration: VUS_UP_STAGE_DURATION, target: RAMP_UP_TARGET_VUS },
        { duration: VUS_DOWN_STAGE_DURATION, target: RAMP_DOWN_TARGET_VUS },
        { duration: '30s', target: RAMP_DOWN_TARGET_VUS },
    ],
    thresholds: {
        http_req_duration: ['p(95)<200'],
        http_req_failed: ['rate<0.01'],
    },
};

export default function () {
    const res = http.get(BASE_URL);

    check(res, {
        'status is 200': (r) => r.status === 200,
    });
}