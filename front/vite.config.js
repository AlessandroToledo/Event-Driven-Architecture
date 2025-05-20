import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
      server: {
    allowedHosts: ['5457-2804-14d-7838-80e9-6440-f25d-cfca-74a1.ngrok-free.app'],
  }
})
