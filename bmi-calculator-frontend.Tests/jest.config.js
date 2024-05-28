module.exports = {
    setupFilesAfterEnv: ["./setupTests.js"],
    moduleNameMapper: {
        "^@src/(.*)$": "<rootDir>/../bmi-calculator-frontend/src/$1"
    },
    transform: {
        "^.+\\.js$": "babel-jest"
    },
    testEnvironment: "jsdom",
    testMatch: [
        "**/__tests__/**/*.js"
    ]
};
