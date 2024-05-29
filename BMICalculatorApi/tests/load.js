import http from 'k6/http';
import { sleep, check } from 'k6';
import { Rate } from 'k6/metrics';

const errorRate = new Rate('errors');

export let options = {
    stages: [
        { duration: '1m', target: 10 }, // simulate ramp-up of traffic from 1 to 10 users over 1 minute.
        { duration: '3m', target: 10 }, // stay at 10 users for 3 minutes
        { duration: '1m', target: 0 },  // ramp-down to 0 users
    ],
    thresholds: {
        errors: ['rate<0.1'], // <10% errors
    },
};

export default function () {
    const res = http.get('http://localhost:5027/api/BMIRecords');
    check(res, { 'status was 200': (r) => r.status === 200 });
    errorRate.add(res.status !== 200);
    sleep(1);
}
