describe('BMI Calculator', () => {
    it('should calculate BMI correctly', () => {
        cy.visit('/');
        cy.get('input[name="name"]').type('Test User');
        cy.get('input[name="height"]').type('175');
        cy.get('input[name="weight"]').type('70');
        cy.get('button[type="submit"]').click();
        cy.contains('Your BMI is: 22.86');
    });
});
