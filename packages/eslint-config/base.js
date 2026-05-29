import js from '@eslint/js';
import eslintConfigPrettier from 'eslint-config-prettier';
import tseslint from 'typescript-eslint';
// import onlyWarn from 'eslint-plugin-only-warn';

/**
 * A shared ESLint configuration for the repository.
 *
 * @type {import("eslint").Linter.Config[]}
 * */
const config = [
  js.configs.recommended,
  eslintConfigPrettier,
  ...tseslint.configs.recommended,
  {
    plugins: {
      // @ts-ignore
      // 'only-warn': onlyWarn,
    },
  },
  {
    ignores: ['dist/**'],
  },
];

export default config;
