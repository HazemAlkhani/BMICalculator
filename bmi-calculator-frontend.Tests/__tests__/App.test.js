import React from 'react';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import App from '@src/App'; // Using alias defined in babel.config.js
import axios from 'axios';
import MockAdapter from 'axios-mock-adapter';

describe('App Component', () => {
    let mock;

    beforeEach(() => {
        mock = new MockAdapter(axios);
    });

    afterEach(() => {
        mock.restore();
    });

    it('renders the BMI Calculator form', () => {
        render(<App />);
        expect(screen.getByText(/BMI Calculator/i)).toBeInTheDocument();
        expect(screen.getByLabelText(/Name/i)).toBeInTheDocument();
        expect(screen.getByLabelText(/Height/i)).toBeInTheDocument();
    });

    it('calculates BMI and shows result', async () => {
        render(<App />);
        fireEvent.change(screen.getByLabelText(/Name/i), { target: { value: 'Test User' } });
        fireEvent.change(screen.getByLabelText(/Height/i), { target: { value: 175 } });
        fireEvent.change(screen.getByLabelText(/Weight/i), { target: { value: 70 } });

        // Mock the axios response
        mock.onPost('/calculate-bmi').reply(200, {
            bmi: 22.9
        });

        fireEvent.click(screen.getByText(/Calculate/i));

        await waitFor(() => {
            expect(screen.getByText(/Your BMI is/i)).toBeInTheDocument();
        });
    });
});
