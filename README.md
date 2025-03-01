# TOTPSystem

![](https://github.com/TimofteRazvan/TOTPSystem/blob/master/final.gif)
(ignore the pixels, gif conversion consequences)
<h2>Instructions for running locally</h2>
Clone the project

```bash
git clone https://github.com/TimofteRazvan/TOTPSystem.git
```

Open the backend solution (TOTPSystem.sln) in Visual Studio and run it

Open the frontend server (frontend/totp-system-frontend/totp-system-frontend) from VS

Install NextJS so that you can run the frontend

```bash
npm install next
```

Run the frontend in HTTPS config (otherwise the connection won't be secure and OTPs won't get encrypted in transmission)

```bash
npm run dev-https
```

Go to https://localhost:3000 and ignore the security risks your browser will likely throw at you (trust me, it's totally fine).

<h2>Usage Information</h2>
OTP duration until expiration: 10 seconds

OTP format: 6 characters, capital letters and numbers only (ex. "1234QW")

The app uses sessions (thus, cookies) to ensure that validation is consistent across the client and server side

<h2>Tech Stack:</h2>
.NET 9.0.2
xUnit 2.9.3
Moq 4.20.72
React 19.0.0
Next 15.2.0
SweetAlert 11.17.2
Vitest 3.0.7

<h2>Instructions for misc stuff</h2>

If, for some reason, you want to start it in HTTP config (keep in mind, backend will need to be adjusted for the new URL), just do

```bash
npm run dev
```

For testing

```bash
npm run test
```
