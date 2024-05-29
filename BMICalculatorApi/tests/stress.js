import http from 'k6/http';
import { sleep, check } from 'k6';
import { Rate } from 'k6/metrics';

const errorRate = new Rate('errors');

export let options = {
    stages: [
        { duration: '2m', target: 10 },
        { duration: '5m', target: 50 },
        { duration: '2m', target: 0 },
    ],
    thresholds: {
        errors: ['rate<0.1'],
    },
};

export default function () {
    const res = http.get('http://localhost:5027/api/BMIRecords');
    check(res, { 'status was 200': (r) => r.status === 200 });
    errorRate.add(res.status !== 200);
    sleep(1);
}
