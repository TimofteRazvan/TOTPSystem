# TOTPSystem

<h2>Instructions for running locally</h2>
Clone the project

```bash
git clone https://github.com/TimofteRazvan/TOTPSystem.git
```

Open the backend solution (TOTPSystem.sln) in Visual Studio and run it

Start the frontend server (frontend/totp-system-frontend/totp-system-frontend) from VS in HTTPS config (otherwise the connection won't be secure and OTPs won't get encrypted in transmission)

```bash
npm run dev-https
```

If, for some reason, you want to start it in HTTP config (keep in mind, backend will need to be adjusted for the new URL), just do

```bash
npm run dev
```

For testing

```bash
npm run test
```
