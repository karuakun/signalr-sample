export const environment = {
  production: true,
  auth: {
    clientID: 'fRIG3ACIoxspTQAep6p608jrySuvUrXI',
    domain: 'karuakun.auth0.com',
    audience: 'https://signalsample20180723053309.azurewebsites.net/',
    redirect: 'https://signalsample20180723053309.azurewebsites.net/callback',
    logoutRedirect: 'https://signalsample20180723053309.azurewebsites.net',
    scope: 'openid profile email'
  },
  signalR: {
    echoHubUrl: 'https://signalsample20180723053309.azurewebsites.net/hubs/echo',
    authHubUrl: 'https://signalsample20180723053309.azurewebsites.net/hubs/auth-echo'
  }
};
