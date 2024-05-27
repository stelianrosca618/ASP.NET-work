/** @type {import('tailwindcss').Config} */
const plugin = require('tailwindcss/plugin');
module.exports = {
  content: ["./**/*.{razor,html,cshtml}"],

  theme: {
      extend: {},
      screens: {
        sm: '640px',
        md: '768px',
        lg: '1024px',
        xl: '1142px',
    },
  },
  plugins: [
    require('tailwindcss'),
  ],
}