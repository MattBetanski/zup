import type { Config } from 'tailwindcss';

const config: Config = {
  content: [
    './pages/**/*.{js,ts,jsx,tsx,mdx}',
    './components/**/*.{js,ts,jsx,tsx,mdx}',
    './app/**/*.{js,ts,jsx,tsx,mdx}',
  ],
  theme: {
    extend: {
      gridTemplateColumns: {
        '13': 'repeat(13, minmax(0, 1fr))',
      },
      colors: {
        surface: {
          100: "#131313",
          200: "#1e1e1e",
          300: "#353535",
          400: "#4e4e4e",
          500: "#696969",
          600: "#858585",
        },
        primary: {
          100: "#ab0000",
          200: "#b9341c",
          300: "#c65136",
          400: "#d26b50",
          500: "#dd846b",
          600: "#e69c87",
        },
        secondary: "#1e1e1e",
        tertiary: "#1e1e1e",
        accent: "#EC625F",
        blue: {
          400: '#2589FE',
          500: '#0070F3',
          600: '#2F6FEB',
        },
      },
    },
    keyframes: {
      shimmer: {
        '100%': {
          transform: 'translateX(100%)',
        },
      },
    },
  },
  plugins: [
    require('@tailwindcss/forms'),
    require('@tailwindcss/typography')
  ],
};
export default config;
