const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7074';

const PROXY_CONFIG = [
  {
    context: [
      '/api/**',
    ],
    target,
    secure: false,
    changeOrigin: true
  }
]

console.log(PROXY_CONFIG);
module.exports = PROXY_CONFIG;
