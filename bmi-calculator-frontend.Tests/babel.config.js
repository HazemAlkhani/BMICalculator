module.exports = {
    presets: [
        "@babel/preset-env",
        "@babel/preset-react"
    ],
    plugins: [
        [
            "module-resolver",
            {
                root: ["../bmi-calculator-frontend/src"],
                alias: {
                    "@src": "../bmi-calculator-frontend/src"
                }
            }
        ]
    ]
};
