# Hyperdrive.Client

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 9.0.0.

## Installing packages
To install all the required dependencies in a clean and reproducible way, use:
```bash
npm ci --no-audit --no-fund
```
This command installs packages strictly following the lockfile, ensuring consistent environments across machines.

If you encounter issues related to optional dependencies or platform‑specific packages, try:
```bash
npm i --include=optional
```
This will reinstall dependencies while including optional modules that may be required on your system.

## Development server

To start a local development server, run:

```bash
ng run web
```

Once the server is running, open your browser and navigate to `https://localhost:4200/`. The application will automatically reload whenever you modify any of the source files.

## Code scaffolding

Angular CLI includes powerful code scaffolding tools. To generate a new component, run:

```bash
ng generate component component-name
```

For a complete list of available schematics (such as `components`, `directives`, or `pipes`), run:

```bash
ng generate --help
```

## Building Development 

Run `npm run dev` to build the project. The build artifacts will be stored in the `dist/` directory.

## Building Staging

Run `npm run stg` to build the project. The build artifacts will be stored in the `dist/` directory.

## Building Production

Run `npm run prod` to build the project. The build artifacts will be stored in the `dist/` directory.


## Additional Resources

For more information on using the Angular CLI, including detailed command references, visit the [Angular CLI Overview and Command Reference](https://angular.dev/tools/cli) page.
