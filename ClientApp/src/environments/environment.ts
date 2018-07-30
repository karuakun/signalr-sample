// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
  production: false,
  auth: {
    clientID: 'fRIG3ACIoxspTQAep6p608jrySuvUrXI',
    domain: 'karuakun.auth0.com',
    audience: 'http://localhost:50416/',
    redirect: 'http://localhost:50416/callback',
    logoutRedirect:'http://localhost:50416',
    scope: 'openid profile email'
  },
  signalR: {
    echoHubUrl: 'http://localhost:50416/hubs/echo',
    authHubUrl: 'http://localhost:50416/hubs/auth-echo'
  }
};
