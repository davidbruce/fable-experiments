import { defineConfig } from 'vite'

// https://vitejs.dev/config/
export default defineConfig({
    clearScreen: false,
    base: '/fable-experiments',
    server: {
        watch: {
            ignored: [
                "**/*.fs" // Don't watch F# files
            ]
        }
    },
    esbuild: {
        include: /\.js$/,
        exclude: [],
        loader: 'jsx',
        jsx: "transform",
        jsxFactory: "h",
		jsxFragment: "DocumentFragment",
    }
})