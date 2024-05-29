import axios from 'axios';
import React, { useState } from 'react';
import styled from 'styled-components';

const Container = styled.div`
  max-width: 600px;
  margin: 50px auto;
  padding: 20px;
  border-radius: 8px;
  background-color: #f0f0f0;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
`;

const Title = styled.h1`
  text-align: center;
  margin-bottom: 20px;
`;

const Form = styled.form`
  display: flex;
  flex-direction: column;
`;

const Label = styled.label`
  margin-bottom: 5px;
  font-weight: bold;
`;

const Input = styled.input`
  padding: 10px;
  margin-bottom: 15px;
  border: 1px solid #ccc;
  border-radius: 4px;
`;

const Button = styled.button`
  padding: 10px;
  border: none;
  border-radius: 4px;
  background-color: #007bff;
  color: white;
  font-weight: bold;
  cursor: pointer;

  &:hover {
    background-color: #0056b3;
  }
`;

const Result = styled.div`
  margin-top: 20px;
  padding: 10px;
  border: 1px solid #007bff;
  border-radius: 4px;
  background-color: #e9f7ff;
  color: #0056b3;
`;

function App() {
  const [name, setName] = useState('');
  const [height, setHeight] = useState('');
  const [weight, setWeight] = useState('');
  const [bmi, setBmi] = useState(null);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const heightInMeters = height / 100;
    const calculatedBmi = (weight / (heightInMeters * heightInMeters)).toFixed(2);
    setBmi(calculatedBmi);

    const data = {
      name,
      height: heightInMeters,
      weight,
      bmi: calculatedBmi
    };

    console.log('Sending data:', data);

    try {
      const response = await axios.post('http://localhost:5027/api/BMIRecords', data);
      console.log('Record added successfully:', response.data);
    } catch (error) {
      console.error('Error adding record:', error);
      if (error.response) {
        console.error('Response data:', error.response.data);
        console.error('Response status:', error.response.status);
        console.error('Response headers:', error.response.headers);
      } else if (error.request) {
        console.error('Request data:', error.request);
      } else {
        console.error('Error message:', error.message);
      }
    }
  };

  return (
      <Container>
        <Title>BMI Calculator</Title>
        <Form onSubmit={handleSubmit}>
          <Label>Name</Label>
          <Input type="text" value={name} onChange={(e) => setName(e.target.value)} required />
          <Label>Height (cm)</Label>
          <Input type="number" value={height} onChange={(e) => setHeight(e.target.value)} required />
          <Label>Weight (kg)</Label>
          <Input type="number" value={weight} onChange={(e) => setWeight(e.target.value)} required />
          <Button type="submit">Add Record</Button>
        </Form>
        {bmi && <Result>Your BMI is: {bmi}</Result>}
      </Container>
  );
}

export default App;
