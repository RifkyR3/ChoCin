import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-vue';
import fs from 'fs';
import path from 'path';
import child_process from 'child_process';
import { env } from 'process';

const baseFolder =
    env.APPDATA !== undefined && env.APPDATA !== ''
        ? `${env.APPDATA}/ASP.NET/https`
        : `${env.HOME}/.aspnet/https`;

const certificateName = "chocin.client";
const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
    if (0 !== child_process.spawnSync('dotnet', [
        'dev-certs',
        'https',
        '--export-path',
        certFilePath,
        '--format',
        'Pem',
        '--no-password',
    ], { stdio: 'inherit', }).status) {
        throw new Error("Could not create certificate.");
    }
}

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7034';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [
        plugin(),
    ],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url)),
            '@stores': fileURLToPath(new URL('./src/stores', import.meta.url)),
            '@components': fileURLToPath(new URL('./src/components', import.meta.url)),
            '@layouts': fileURLToPath(new URL('./src/layouts', import.meta.url)),
            '@views': fileURLToPath(new URL('./src/views', import.meta.url)),
            '~bootstrap': fileURLToPath(new URL('./node_modules/bootstrap', import.meta.url)),
            '~bootstrap-icons': fileURLToPath(new URL('./node_modules/bootstrap-icons', import.meta.url)),
            '~admin-lte': fileURLToPath(new URL('./node_modules/admin-lte', import.meta.url)),
        }
    },
    server: {
        proxy: {
            '/api': {
                target,
                secure: false
            }
        },
        port: 5173,
        https: {
            key: fs.readFileSync(keyFilePath),
            cert: fs.readFileSync(certFilePath),
        }
    }
})